using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
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

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();
        var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectsDto;
    }

    public async Task<ProjectDto> GetByNameAsync(string name)
    {
        var project = await _projectRepository.GetByNameAsync(name);
        var projectDto = _mapper.Map<ProjectDto>(project);
        return projectDto;
    }

    public async Task AddAsync(ProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _projectRepository.AddAsync(project);
    }

    public async Task UpdateAsync(ProjectDto projectDto, string oldName)
    {
        var project = _mapper.Map<Project>(projectDto);
        await _projectRepository.UpdateByNameAsync(project, oldName);
    }

    public async Task DeleteAsync(string name)
    {
        await _projectRepository.DeleteAsync(name);
    }
}