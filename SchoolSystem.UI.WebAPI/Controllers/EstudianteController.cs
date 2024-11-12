using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Core.DTOs.Estudiante;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Estudiantes")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IRepository<Estudiante> _repository;
        private readonly IMapper _mapper;
        public EstudianteController(IRepository<Estudiante> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ListarEstudiantes")]
        public async Task<ActionResult<IEnumerable<EstudianteDTO>>> ListarEstudiantes()
        {
            var estudiantes = await _repository.GetAllAsync(a => !a.Eliminado && a.Curso != null && !a.Curso.Eliminado,
                a => a.Curso,
                a => a.Calificaciones,
                a => a.Asistencias
            );

            var estudiantesDTOs = _mapper.Map<IEnumerable<EstudianteDTO>>(estudiantes);
            return Ok(estudiantesDTOs);
        }

        [HttpGet("ObtenerEstudiantePorID/{id}")]
        public async Task<ActionResult<EstudianteDTO>> ObtenerEstudiantePorID(int id)
        {
            var estudiante = await _repository.GetByIdAsync(id, 
                a => a.Curso,
                a => a.Calificaciones,
                a => a.Asistencias);

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
