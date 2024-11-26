using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.DTOs.Asistencia;
using SchoolSystem.Core.DTOs.Calificacion;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using SchoolSystem.Infrastructure.Repositories;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Asistencias")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IRepository<Asistencia> _repository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public AsistenciaController(IRepository<Asistencia> repository, IMapper mapper, ApplicationDbContext context, IAsistenciaRepository asistenciaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
            _asistenciaRepository = asistenciaRepository;
        }

        [HttpGet("ObtenerHistorialAsistenciaPorFecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<HistorialAsistenciaDTO>>> ObtenerHistorialAsistencia(DateTime fecha)
        {
            if (fecha == null || fecha == DateTime.MinValue)
            {
                return BadRequest("La fecha proporcionada no es válida.");
            }

            var historial = await _asistenciaRepository.ObtenerHistorialPorFechaAsync(fecha);

            if (historial == null || !historial.Any())
            {
                return NotFound($"No se encontraron registros de asistencia para la fecha {fecha.ToShortDateString()}.");
            }

            var historialDTO = _mapper.Map<IEnumerable<HistorialAsistenciaDTO>>(historial);

            return Ok(historialDTO);
        }

        [HttpGet("ListarAsistencias")]
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> ListarAsistencias()
        {
            var asistencias = await _repository.GetAllAsync(a => !a.Eliminado && a.Estudiante != null && !a.Estudiante.Eliminado && a.Estado != null && !a.Estado.Eliminado,
                a => a.Estudiante,
                a => a.Estado
            );

            var asistenciasDTOs = _mapper.Map<IEnumerable<AsistenciaDTO>>(asistencias);
            return Ok(asistenciasDTOs);
        }

        [HttpGet("ObtenerAsistenciaPorID/{id}")]
        public async Task<ActionResult<AsistenciaDTO>> ObtenerAsistenciaPorID(int id)
        {
            var asistencia = await _repository.GetByIdAsync(id, a => a.Estudiante, a => a.Estado);

            if (asistencia == null)
            {
                return NotFound();
            }

            var asistenciaDTO = _mapper.Map<AsistenciaDTO>(asistencia);
            return Ok(asistenciaDTO);
        }

        [HttpPost("RegistrarAsistencia")]
        public async Task<ActionResult<AsistenciaDTO>> RegistrarAsistencia([FromBody] ModAsistenciaDTO asistenciasDTO)
        {
            var asistencia = _mapper.Map<Asistencia>(asistenciasDTO);
            await _repository.AddAsync(asistencia);
            var asistenciaDTO = _mapper.Map<AsistenciaDTO>(asistencia);
            return CreatedAtAction(nameof(ObtenerAsistenciaPorID), new { id = asistenciaDTO.Id }, asistenciaDTO);
        }

        [HttpPut("EditarAsistencia/{id}")]
        public async Task<IActionResult> EditarAsistencia(int id, [FromBody] ModAsistenciaDTO asistenciasDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asistenciaExistente = await _repository.GetByIdAsync(id);

            if (asistenciaExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(asistenciasDTO, asistenciaExistente);
            await _repository.UpdateAsync(asistenciaExistente);

            var asistenciaDTO = _mapper.Map<AsistenciaResponseDTO>(asistenciaExistente);
            return Ok(asistenciaDTO);
        }

        [HttpDelete("EliminarAsistencia/{id}")]
        public async Task<ActionResult<bool>> EliminarAsistencia(int id)
        {
            var asistencia = await _repository.GetByIdAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(asistencia);
        }
    }
}
