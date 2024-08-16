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
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
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

            CreateMap<Collaborator, CollaboratorDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime))
                .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt.HasValue ? src.DeletedAt.Value.UtcDateTime : (DateTime?)null));
            CreateMap<CollaboratorCreateDto, Collaborator>();
            CreateMap<CollaboratorUpdateDto, Collaborator>();
        }
    }
}
