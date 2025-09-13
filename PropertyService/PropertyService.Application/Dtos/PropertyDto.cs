namespace PropertyService.Application.Dtos
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public int CodeOwner { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareFootage { get; set; }
        public int YearBuilt { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public string PropertyType { get; set; }
    }
}
