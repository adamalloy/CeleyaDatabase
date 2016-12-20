using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISGeoDatabase.Models
{
    public class SubTable
    {
        public IEnumerable<SubsidencePoint> Table { get; set; }

        public int Size { get; set; }

        public int Page { get; set; }
        public int NumPages { get; set; }
        public FormFields Filter { get; set; }
    }
}
