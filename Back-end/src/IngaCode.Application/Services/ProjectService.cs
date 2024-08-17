using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.ProjectsDTOs;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectDto> GetProjectByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project == null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto> CreateProjectAsync(ProjectEditDto dto)
    {
        var projectEntity = _mapper.Map<Project>(dto);
        await _projectRepository.AddAsync(projectEntity);
        var createdProject = await _projectRepository.GetByIdAsync(projectEntity.Id);
        return _mapper.Map<ProjectDto>(createdProject);
    }

    public async Task<bool> UpdateProjectAsync(Guid id, ProjectEditDto dto)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project != null)
        {
            _mapper.Map(dto, project);
            await _projectRepository.UpdateAsync(project);
            return true;
        }
        return false;
    }


    public async Task<bool> DeleteProjectAsync(Guid id)
    {
        try
        {
            await _projectRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}