using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.DTOs.Estudiante;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Estudiantes")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IRepository<Estudiante> _repository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public EstudianteController(IRepository<Estudiante> repository, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        //[HttpGet("FiltrarEstudiantes")]
        //public async Task<IActionResult> FiltrarEstudiantes(string? nombre, string? apellido, string? curso)
        //{
        //    var query = _context.Estudiantes.AsQueryable();

        //    if (!string.IsNullOrEmpty(nombre))
        //        query = query.Where(e => e.Nombre.Contains(nombre));

        //    if (!string.IsNullOrEmpty(apellido))
        //        query = query.Where(e => e.Apellido.Contains(apellido));

        //    if (!string.IsNullOrEmpty(curso))
        //        query = query.Where(e => e.Curso.Nombre == curso);

        //    var estudiantes = await query.ToListAsync();
        //    return Ok(estudiantes);
        //}

        [HttpGet("FiltrarEstudiantes")]
        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> FiltrarEstudiantes(string? nombre, string? apellido, string? curso)
        {
            var query = _context.Estudiantes
                .Include(e => e.Curso)
                .Where(e => !e.Eliminado && e.Curso != null && !e.Curso.Eliminado)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(e => e.Nombre.Contains(nombre));

            if (!string.IsNullOrEmpty(apellido))
                query = query.Where(e => e.Apellido.Contains(apellido));

            if (!string.IsNullOrEmpty(curso))
                query = query.Where(e => e.Curso.Nombre == curso);

            var estudiantes = await query.ToListAsync();

            var estudiantesDTOs = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes);

            return Ok(estudiantesDTOs);
        }

        [HttpGet("ListarEstudiantes")]
        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> ListarEstudiantes()
        {
            var estudiantes = await _repository.GetAllAsync(a => !a.Eliminado && a.Curso != null && !a.Curso.Eliminado,
                a => a.Curso
            );

            var estudiantesDTOs = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes);
            return Ok(estudiantesDTOs);
        }

        [HttpGet("ObtenerEstudiantePorID/{id}")]
        public async Task<ActionResult<EstudianteDTO>> ObtenerEstudiantePorID(int id)
        {
            var estudiante = await _repository.GetByIdAsync(id, 
                a => a.Curso);

            if (estudiante == null)
            {
                return NotFound();
            }

            var estudianteDTO = _mapper.Map<EstudianteDTO>(estudiante);
            return Ok(estudianteDTO);
        }

        [HttpPost("RegistrarEstudiante")]
        public async Task<ActionResult<EstudianteDTO>> RegistrarEstudiante([FromBody] ModEstudianteDTO estudiantesDTO)
        {
            var estudiante = _mapper.Map<Estudiante>(estudiantesDTO);
            await _repository.AddAsync(estudiante);
            var estudianteDTO = _mapper.Map<EstudianteDTO>(estudiante);
            return CreatedAtAction(nameof(ObtenerEstudiantePorID), new { id = estudianteDTO.Id }, estudianteDTO);
        }

        [HttpPut("EditarEstudiante/{id}")]
        public async Task<IActionResult> EditarEstudiante(int id, [FromBody] ModEstudianteDTO estudiantesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estudianteExistente = await _repository.GetByIdAsync(id);

            if (estudianteExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(estudiantesDTO, estudianteExistente);
            await _repository.UpdateAsync(estudianteExistente);

            var estudianteDTO = _mapper.Map<EstudianteResponseDTO>(estudianteExistente);
            return Ok(estudianteDTO);
        }

        [HttpDelete("EliminarEstudiante/{id}")]
        public async Task<ActionResult<bool>> EliminarEstudiante(int id)
        {
            var estudiante = await _repository.GetByIdAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(estudiante);
        }
    }
}
