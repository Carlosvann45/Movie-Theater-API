namespace Movie.Theater.Enterprises.Models.Entities
{
    /// <summary>
    /// Base model for all entitys in the database
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; } = default;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }
}
