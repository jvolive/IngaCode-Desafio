using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;
using AutoMapper;

namespace IngaCode.Application.Configuration;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<TaskEntity, TaskEntityDto>()
            .ReverseMap();

        CreateMap<Project, ProjectDto>()
            .ReverseMap();

        CreateMap<TimeTracker, TimeTrackerDto>()
            .ReverseMap();
    }
}