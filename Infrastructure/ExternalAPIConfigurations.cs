using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ExternalAPIConfigurations: IExternalAPIConfigurations
    {
        public string BRAPI_API_KEY { get; set; }
        public string API_KEY { get; set; }
        public string BRAPI_URL { get; set; }
    }
}
