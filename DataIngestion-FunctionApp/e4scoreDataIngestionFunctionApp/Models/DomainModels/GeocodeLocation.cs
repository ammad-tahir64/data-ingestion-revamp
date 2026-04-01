using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class GeocodeLocation
    {  
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Postal { get; set; }
        public string? Address { get; set; }
    }
}
