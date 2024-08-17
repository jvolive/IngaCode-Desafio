using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;
using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Application.DTOs.ProjectsDTOs;
using AutoMapper;

namespace IngaCode.Application.Configuration;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {

        CreateMap<TaskEntity, TaskEntityDto>()
            .ReverseMap();
        CreateMap<TaskEntityEditDto, TaskEntity>();

        CreateMap<Project, ProjectDto>()
            .ReverseMap();
        CreateMap<ProjectEditDto, Project>();

        CreateMap<TimeTracker, TimeTrackerDto>()
            .ReverseMap();

        CreateMap<Collaborator, CollaboratorDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
        .ReverseMap();
    }
}