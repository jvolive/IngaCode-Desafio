namespace IngaCode.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
