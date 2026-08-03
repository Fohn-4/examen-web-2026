namespace Infrastructure.Models
{
    public class ActivityEntity
    {
        public string UUID { get ; set; }
        public string Name { get ; set; }
        public string? Thumbnail { get ; set; }
        public string Description { get ; set; }
        public bool IsActive { get ; set; }

    }
}
