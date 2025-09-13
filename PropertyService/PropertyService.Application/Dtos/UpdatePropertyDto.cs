using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Added for IFormFile

namespace PropertyService.Application.Dtos
{
    public class UpdatePropertyDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Range(0, int.MaxValue)]
        public int Bedrooms { get; set; }

        [Range(0, int.MaxValue)]
        public int Bathrooms { get; set; }

        [Range(1, int.MaxValue)]
        public int SquareFootage { get; set; }

        [Range(1800, 2100)]
        public int YearBuilt { get; set; }

        public IFormFile ImageFile { get; set; } // Changed from string ImageUrl

        [Required]
        public string Status { get; set; }

        [Required]
        public string PropertyType { get; set; }
    }
}
