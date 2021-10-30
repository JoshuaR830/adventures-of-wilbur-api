namespace AdventuresOfWilburApi.Models
{
    public class WilburCard
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string WilburImage { get; set; }

        public WilburCard(long id, string title, string body, string wilburImage)
        {
            Id = id;
            Title = title;
            Body = body;
            WilburImage = wilburImage;
        }
    }
}