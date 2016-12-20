using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISGeoDatabase.Models
{
    public class SubsidencePoint
    {
        private ICollection<Image> images;
        public int ID { get; set; }
        [Required] 
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Display(Name = "Team Number")]
        public string TeamNumber { get; set; }
        [Display(Name = "Station Number")]
        public string StationNumber { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime DateAndTime { get; set; }
        [Required]
        [RegularExpression(@"^([-+]?\d{1,2}([.]\d+)?),\s*([-+]?\d{1,3}([.]\d+)?)$")]
        public string Coordinates { get; set; } //might need another type
        [Display(Name = "PT")]
        public string PrependedText { get; set; }
        [Display(Name = "Station Reference")]
        public string StationRef { get; set; }
        [Required]
        public string Fault { get; set; }
        [Display(Name = "Type of Data")]
        public string DataType { get; set; }

        //[RegularExpression(@"[-+]?[0-9]*\.?[0-9]+")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double? Strike { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]+")]
        public double? Angle { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]+")]
        public double? Throw { get; set; }
        [RegularExpression(@"[-+]?[0-9]*\.?[0-9]+")]
        public double? Slip { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Notes { get; set; }
        public virtual ICollection<Image> Images { get { return images ?? (images = new List<Image>()); } set { images = value; } }        
        [NotMapped]
        public IEnumerable<string> Countries { get; set; }
        [NotMapped]
        public IEnumerable<string> Regions { get; set; }
        [NotMapped]
        public IEnumerable<string> DataTypes { get; set; }
        [NotMapped]
        public IEnumerable<string> Cities { get; set; }
       // public FormFields form { get; set; }
       
    }
}
