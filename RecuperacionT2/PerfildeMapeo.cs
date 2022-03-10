using AutoMapper;
using RecuperacionT2.Models.Entidades;
using RecuperacionT2.Models.ViewModels;

namespace RecuperacionT2
{
    public class PerfilMapeo : Profile
    {   
        public PerfilMapeo()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioAltaViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginViewModel>().ReverseMap();
            CreateMap<Usuario,UsuarioUpdateViewModel>().ReverseMap();
        }
        
        
    }
}
