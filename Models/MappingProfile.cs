using AutoMapper;

namespace vSaude.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TarefaCreateDto, TarefaMedica>();
            CreateMap<TarefaUpdateDto, TarefaMedica>();
            CreateMap<TarefaMedica, TarefaViewDto>()
                .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => src.Status.ObterDescricao()))
                .ForMember(dest => dest.PrioridadeDescricao, opt => opt.MapFrom(src => src.Prioridade.ObterDescricao()))
                .ForMember(dest => dest.CategoriaDescricao, opt => opt.MapFrom(src => src.Categoria.ObterDescricao()));
        }
    }
}



