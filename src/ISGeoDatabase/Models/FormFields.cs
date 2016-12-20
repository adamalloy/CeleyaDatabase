using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISGeoDatabase.Models
{
    public class FormFields
    {
        public string country { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string teamno { get; set; }

        public string fault { get; set; }
        public string type { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public double strikelow { get; set; }
        public double strikehigh { get; set; }
        public double thwlow { get; set; }
        public double thwhigh { get; set; }

        public double sliplow { get; set; }
        public double sliphigh { get; set; }
        public double anglelow { get; set; }
        public double anglehigh { get; set; }

        public string sortOrder { get; set; }
        public int? page { get; set; }
    }
}
