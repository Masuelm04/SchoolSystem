using AutoMapper;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.EstadoAsistencia;
using SchoolSystem.Core.DTOs.Estudiante;
using SchoolSystem.Core.DTOs.Materia;
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

            CreateMap<Estudiante, EstudianteDTO>().
                ForMember(d => d.NombreCurso, o => o.MapFrom(c => c.Curso.Nombre)).ReverseMap();
            CreateMap<ModEstudianteDTO, Estudiante>();
            CreateMap<Estudiante, EstudianteResponseDTO>();

            CreateMap<Materia, MateriaDTO>().ReverseMap();
            CreateMap<ModMateriaDTO, Materia>();
        }
    }
}
