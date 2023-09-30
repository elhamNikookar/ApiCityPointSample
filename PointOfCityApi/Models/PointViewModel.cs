using System.ComponentModel.DataAnnotations;

namespace PointOfCityApi.Models
{
    public class PointViewModel
    {
        public int PointID { get; set; }

        [Required(ErrorMessage = "This field is Required!")]
        [MaxLength(50, ErrorMessage = "Max lenght of this field is 50 char!")]
        public string PointName { get; set; }

        [MaxLength(150, ErrorMessage = "Max lenght of this field is 150 char!")]
        public string? PointDescription { get; set; }
    }

    public class CreatePointViewModel
    {
        [Required(ErrorMessage = "This field is Required!")]
        [MaxLength(50, ErrorMessage = "Max lenght of this field is 50 char!")]
        public string PointName { get; set; }

        [MaxLength(150, ErrorMessage = "Max lenght of this field is 150 char!")]
        public string? PointDescription { get; set; }
    }

    public class UpdatePointViewModel
    {
        [Required(ErrorMessage = "This field is Required!")]
        [MaxLength(50, ErrorMessage = "Max lenght of this field is 50 char!")]
        public string PointName { get; set; }

        [MaxLength(150, ErrorMessage = "Max lenght of this field is 150 char!")]
        public string? PointDescription { get; set; }
    }
}
