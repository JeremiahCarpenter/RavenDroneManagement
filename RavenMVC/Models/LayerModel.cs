using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RavenMVC.Models
{
    public class LayerModel
    {
        public int ObsID { get; set; }
        public int RecordSpeed { get; set; }
        public string LatNumber { get; set; }
        public string LongNumber { get; set; }
        public int PlateID { get; set; }
        public string RegisteredOwner { get; set; }
        public Decimal FineAmount { get; set; }
        public string ViolationDesc { get; set; }
        public string PaidFine { get; set; }
        public string Address1 { get; set; }
        public string State { get; set; }
        
    }
}