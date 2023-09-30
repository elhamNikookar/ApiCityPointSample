using System.ComponentModel.DataAnnotations;

namespace PointOfCityApi.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string? Description { get; set; }


        #region Navigation Properties
        public List<Point> Points { get; set; } = new List<Point>();
        #endregion
    }
}
