using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
using AutoMapper;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

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
        return _mapper.Map<IEnumerable<ProjectDto>>(projects);
    }

    public async Task<ProjectDto> GetByNameAsync(string name)
    {
        var project = await _projectRepository.GetByNameAsync(name);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task AddAsync(Project entity)
    {
        await _projectRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(Project entity)
    {
        await _projectRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(string name)
    {
        await _projectRepository.DeleteAsync(name);
    }
}
