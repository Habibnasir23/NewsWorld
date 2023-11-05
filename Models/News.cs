namespace NewsWorld.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? type { get; set; }
        public string? header { get; set; }
        public string? link { get; set; }
        public float? lat { get; set; }
        public float? lng { get; set; }
        public string? img { get; set; }

        public string? summary { get; set; }
        public News() { }

    }
}
