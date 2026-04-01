using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.RequestModels
{
    public class MatrackRequestData
    {
        public List<MatrackRequest> data { get; set; }
    }

    public class Data
    {
        public List<MatrackRequest> MatrackRequest { get; set; }
    }


}
