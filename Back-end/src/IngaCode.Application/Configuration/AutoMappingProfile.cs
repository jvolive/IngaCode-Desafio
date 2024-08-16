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

        CreateMap<TaskEntity, TaskEntityDto>()
            .ForMember(dest => dest.TimeTrackers, opt => opt.MapFrom(src => src.TimeTrackers));
        CreateMap<TaskEntityEditDto, TaskEntity>();


        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectEditDto, Project>();

        CreateMap<TimeTracker, TimeTrackerDto>();
        CreateMap<TimeTrackerEditDto, TimeTracker>();

        CreateMap<Collaborator, CollaboratorDto>()
            .ReverseMap();

        CreateMap<User, UserDto>()
        .ReverseMap();
    }
}