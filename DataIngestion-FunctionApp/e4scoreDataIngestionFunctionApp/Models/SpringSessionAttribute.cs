using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class SpringSessionAttribute
    {
        public Guid SessionPrimaryId { get; set; }
        public string AttributeName { get; set; }
        public byte[] AttributeBytes { get; set; }

        public virtual SpringSession SessionPrimary { get; set; }
    }
}
