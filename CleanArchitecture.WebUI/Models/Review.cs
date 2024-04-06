namespace CleanArchitecture.WebUI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Rating { get; set; }
        public string? UserId { get; set; }

        public int VillaId { get; set; }
        public Villa? Villa { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
