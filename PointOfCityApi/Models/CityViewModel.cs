using System.ComponentModel.DataAnnotations;

namespace PointOfCityApi.Models
{
    public class CityViewModel
    {
        public int CityID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string? Description { get; set; }
        public int CountPoints { get; set; } = 0;

    }
}
