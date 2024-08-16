using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;
using IngaCode.Application.DTOs.UserDTOs;
using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Application.DTOs.ProjectsDTOs;
using IngaCode.Application.DTOs.CollaboratorDTOs;
using IngaCode.Application.DTOs.TimeTrackerDTOs;
using AutoMapper;

namespace IngaCode.Application.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>();
            CreateMap<UserRegisterDto, User>();

            CreateMap<TaskEntity, TaskEntityDto>();
            CreateMap<TaskEntityCreateDto, TaskEntity>();
            CreateMap<TaskEntityUpdateDto, TaskEntity>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();

            CreateMap<TimeTracker, TimeTrackerDto>();
            CreateMap<TimeTrackerCreateDto, TimeTracker>();
            CreateMap<TimeTrackerUpdateDto, TimeTracker>();

            CreateMap<Collaborator, CollaboratorDto>();
            CreateMap<CollaboratorCreateDto, Collaborator>();
            CreateMap<CollaboratorUpdateDto, Collaborator>();
        }
    }
}
