namespace Products.Models
{
    public class BaseEntity
    {
        public DateTime CraetedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDelete { get; set; }

        public BaseEntity()
        {
            CraetedAt = DateTime.Now;
        }
    }
}