using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;
using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Application.DTOs.ProjectsDTOs;
using IngaCode.Application.DTOs.TimeTrackerDTOs;
using AutoMapper;

namespace IngaCode.Application.Configuration;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {

        CreateMap<TaskEntityEditDto, TaskEntity>()
            .ForMember(dest => dest.TimeTrackers, opt => opt.MapFrom(src => src.TimeTrackers));

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