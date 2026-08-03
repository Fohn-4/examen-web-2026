namespace Core.Models
{
    public class CreateActivityRequest
    {
        public string Name {get ; set; }
        public string Description { get ; set; }
        public bool IsActive { get ; set; }
    }
}