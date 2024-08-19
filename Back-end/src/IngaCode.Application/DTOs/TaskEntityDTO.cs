namespace IngaCode.Application.DTOs;

public class TaskEntityDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
}