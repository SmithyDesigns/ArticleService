namespace Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Html { get; set; }
    }
}
