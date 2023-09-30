using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfCityApi.Models
{
    public class Point
    {
        [Key]
        public int PointID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PointName { get; set; }

        [MaxLength(150)]
        public string? PointDescription { get; set; }

        public int CityID { get; set; }


        #region Navigation Properties

        [ForeignKey("CityID")]
        public City City { get; set; }

        #endregion


    }
}
