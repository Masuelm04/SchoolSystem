using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Core.DTOs.Calificacion;
using SchoolSystem.Core.DTOs.Estudiante;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Calificaciones")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private readonly IRepository<Calificacion> _repository;
        private readonly IMapper _mapper;
        public CalificacionController(IRepository<Calificacion> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ListarCalificaciones")]
        public async Task<ActionResult<IEnumerable<CalificacionDTO>>> ListarCalificaciones()
        {
            var calificaciones = await _repository.GetAllAsync(a => !a.Eliminado && a.Estudiante != null && !a.Estudiante.Eliminado && a.Materia != null && !a.Materia.Eliminado,
                a => a.Estudiante,
                a => a.Materia
            );

            var calificacionesDTOs = _mapper.Map<IEnumerable<CalificacionDTO>>(calificaciones);
            return Ok(calificacionesDTOs);
        }

        [HttpGet("ObtenerCalificacionPorID/{id}")]
        public async Task<ActionResult<CalificacionDTO>> ObtenerCalificacionPorID(int id)
        {
            var calificacion = await _repository.GetByIdAsync(id, a => a.Estudiante, a => a.Materia);

            if (calificacion == null)
            {
                return NotFound();
            }

            var calificacionDTO = _mapper.Map<CalificacionDTO>(calificacion);
            return Ok(calificacionDTO);
        }

        [HttpPost("RegistrarCalificacion")]
        public async Task<ActionResult<CalificacionDTO>> RegistrarCalificacion([FromBody] ModCalificacionDTO calificacionesDTO)
        {
            var calificacion = _mapper.Map<Calificacion>(calificacionesDTO);
            await _repository.AddAsync(calificacion);
            var calificacionDTO = _mapper.Map<CalificacionDTO>(calificacion);
            return CreatedAtAction(nameof(ObtenerCalificacionPorID), new { id = calificacionDTO.Id }, calificacionDTO);
        }

        [HttpPut("EditarCalificacion/{id}")]
        public async Task<IActionResult> EditarCalificacion(int id, [FromBody] ModCalificacionDTO calificacionesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var calificacionExistente = await _repository.GetByIdAsync(id);

            if (calificacionExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(calificacionesDTO, calificacionExistente);
            await _repository.UpdateAsync(calificacionExistente);

            var calificacionDTO = _mapper.Map<CalificacionResponseDTO>(calificacionExistente);
            return Ok(calificacionDTO);
        }

        [HttpDelete("EliminarCalificacion/{id}")]
        public async Task<ActionResult<bool>> EliminarCalificacion(int id)
        {
            var calificacion = await _repository.GetByIdAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(calificacion);
        }
    }
}
