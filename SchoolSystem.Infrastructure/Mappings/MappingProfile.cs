using AutoMapper;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.EstadoAsistencia;
using SchoolSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>().ReverseMap();
            CreateMap<ModCursoDTO, Curso>();

            CreateMap<EstadoAsistencia, EstadoAsistenciaDTO>().ReverseMap();
            CreateMap<ModEstadoAsistenciaDTO, EstadoAsistencia>();
        }
    }
}
