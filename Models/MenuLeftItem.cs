namespace voltaire.Models
{
    public class MenuLeftItem : BaseModel
    {
        public bool IsSelected { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public bool IsEnabled { get; set; }
        public double opacity { get; set; }
    }
}
