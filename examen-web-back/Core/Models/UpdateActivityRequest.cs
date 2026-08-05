namespace Core.Models
{
    public class UpdateActivityRequest
    {
        public string Name {get ; set; }
        public string Description { get ; set; }
        public bool IsActive { get ; set; }
    }
}