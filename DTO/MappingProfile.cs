using AutoMapper;
using Todo.Models;

namespace Todo.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoTask, TodoTaskResponseDTO>();

            CreateMap<TodoTaskRequestDTO, TodoTask>()
                .ForMember(dest => dest.Status,   opt => opt.MapFrom(src => Enum.Parse<TodoStatus>(  src.status,   true)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TodoPriority>(src.priority, true)));
        }
    }
}
