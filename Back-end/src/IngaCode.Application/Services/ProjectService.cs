using AutoMapper;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.ProjectsDTOs;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            await _projectRepository.AddAsync(project);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task UpdateProjectAsync(Guid id, ProjectUpdateDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project != null)
            {
                _mapper.Map(dto, project);
                await _projectRepository.UpdateAsync(project);
            }
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}
