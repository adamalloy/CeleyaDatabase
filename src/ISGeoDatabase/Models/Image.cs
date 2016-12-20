using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISGeoDatabase.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public virtual SubsidencePoint SubsidencePoint { get; set; }
    }
}
