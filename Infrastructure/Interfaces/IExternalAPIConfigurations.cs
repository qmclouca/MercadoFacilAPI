namespace Infrastructure.Interfaces
{
    public interface IExternalAPIConfigurations
    {
        public string BRAPI_API_KEY { get; set; }
        public string API_KEY { get; set; }
        public string BRAPI_URL { get; set; }
    }
}
