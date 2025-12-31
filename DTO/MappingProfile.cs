using AutoMapper;
using Todo.APi.Models;

namespace Todo.APi.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoTask, TodoTaskResponseDTO>()
                .ForMember(dest => dest.duedatetime,        opt => opt.MapFrom(src => src.DueDateTime.HasValue ? src.DueDateTime.Value.ToString("o") : string.Empty))
                .ForMember(dest => dest.createdatetime,     opt => opt.MapFrom(src => src.CreateDateTime.ToString("o")))
                .ForMember(dest => dest.lastupdatedatetime, opt => opt.MapFrom(src => src.LastUpdateDateTime.ToString("o")));

            CreateMap<TodoTaskRequestDTO, TodoTask>()
                .ForMember(dest => dest.Status,   opt => opt.MapFrom(src => Enum.Parse<TodoStatus>(  src.status,   true)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TodoPriority>(src.priority, true)));
        }
    }
}
