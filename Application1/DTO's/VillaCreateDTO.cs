using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application1.DTO_s
{
    public class VillaCreateDTO
    {   

        [Required]
        [MaxLength]
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public string Amenity { get; set; }
        [Required]
        public string ImageUrl { get; set; }

    }
}
