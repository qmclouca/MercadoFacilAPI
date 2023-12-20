# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory
WORKDIR /app

# Install cultures (using Debian)
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y locales && \
    locale-gen en_US.UTF-8 && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LANG=en_US.UTF-8
ENV LANGUAGE=en_US:en
ENV LC_ALL=en_US.UTF-8

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Use SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["MercadoFacilAPI/MercadoFacilAPI.csproj", "MercadoFacilAPI/"]
RUN dotnet restore "MercadoFacilAPI/MercadoFacilAPI.csproj"
COPY . .
WORKDIR "/src/MercadoFacilAPI"
RUN dotnet build "MercadoFacilAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MercadoFacilAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Copy the build artifacts into the runtime image
FROM base AS final
COPY --from=publish /app/publish .

# Set the entry point of the application
ENTRYPOINT ["dotnet", "MercadoFacilAPI.dll"]
