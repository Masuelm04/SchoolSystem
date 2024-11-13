using AutoMapper;
using SchoolSystem.Core.DTOs.Asistencia;
using SchoolSystem.Core.DTOs.Calificacion;
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
            //Asistencia
            CreateMap<Asistencia, AsistenciaDTO>().
                ForMember(d => d.NombreEstudiante, o => o.MapFrom(c => c.Estudiante.Nombre)).
                ForMember(d => d.NombreEstadoAsistencia, o => o.MapFrom(c => c.Estado.Nombre)).ReverseMap();
            CreateMap<ModAsistenciaDTO, Asistencia>();
            CreateMap<Asistencia, AsistenciaResponseDTO>();


            //Calificacion
            CreateMap<Calificacion, CalificacionDTO>().
                ForMember(d => d.NombreEstudiante, o => o.MapFrom(c => c.Estudiante.Nombre)).
                ForMember(d => d.NombreMateria, o => o.MapFrom(c => c.Materia.Nombre)).ReverseMap();
            CreateMap<ModCalificacionDTO, Calificacion>();
            CreateMap<Calificacion, CalificacionResponseDTO>();

            
            //Curso
            CreateMap<Curso, CursoMateriaDTO>().
                ForMember(d => d.Materias, o => o.MapFrom(c => c.CursoMaterias.Select(cm => cm.Materia))).ReverseMap();
            CreateMap<Curso, CursoDTO>().ReverseMap();
            CreateMap<ModCursoDTO, Curso>();


            //EstadoAsistencia
            CreateMap<EstadoAsistencia, EstadoAsistenciaDTO>().ReverseMap();
            CreateMap<ModEstadoAsistenciaDTO, EstadoAsistencia>();


            //Estudiante
            CreateMap<Estudiante, EstudianteDTO>().
                ForMember(d => d.NombreCurso, o => o.MapFrom(c => c.Curso.Nombre)).ReverseMap();
            CreateMap<ModEstudianteDTO, Estudiante>();
            CreateMap<Estudiante, EstudianteResponseDTO>();


            //Materia
            CreateMap<Materia, MateriaCursoDTO>().
                ForMember(d => d.Cursos, o => o.MapFrom(c => c.CursoMaterias.Select(cm => cm.Curso))).ReverseMap();
            CreateMap<Materia, MateriaDTO>().ReverseMap();
            CreateMap<ModMateriaDTO, Materia>();
        }
    }
}
