namespace IngaCode.Application.DTOs.TaskEntity
{
    public class TaskEntityUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? CollaboratorId { get; set; }
    }
}