using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.Application.DTOs.ProjectsDTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<TaskEntityDto> Tasks { get; set; }
    }
}