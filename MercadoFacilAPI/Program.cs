    using CrossCutting.DependencyInjection;
    using Data;
    using Data.Repositories;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<MercadoFacilDbContext>(options =>
        options.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnection")
            , b => b.MigrationsAssembly("../Data")));

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
#pragma warning disable CS8600, CS8603
    builder.Services.AddScoped<IMercadoFacilDbContext>(provider => (IMercadoFacilDbContext)provider.GetService<MercadoFacilDbContext>());
#pragma warning restore CS8600, CS8603
    builder.Services.AddApplicationServices();    
    //builder.Services.AddTransient<IInfrastructureService, InfrastructureService>();
    builder.Services.AddControllers();
    builder.Services.AddHealthChecks();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();

#if !DEBUG
    app.UseAuthentication();
    app.UseAuthorization();
#endif

    app.MapControllers();
    app.MapHealthChecks("/health");

    app.Run();