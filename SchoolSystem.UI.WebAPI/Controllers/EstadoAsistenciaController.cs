using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Core.DTOs.EstadoAsistencia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/EstadosAsistencia")]
    [ApiController]
    public class EstadoAsistenciaController : ControllerBase
    {
        private readonly IRepository<EstadoAsistencia> _repository;
        private readonly IMapper _mapper;
        public EstadoAsistenciaController(IRepository<EstadoAsistencia> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ListarEstadosAsistencia")]
        public async Task<ActionResult<IEnumerable<EstadoAsistenciaDTO>>> ListarEstadosAsistencia()
        {
            var estados = await _repository.GetAllAsync();
            var estadosDTOs = _mapper.Map<IEnumerable<EstadoAsistenciaDTO>>(estados);
            return Ok(estadosDTOs);
        }

        [HttpGet("ObtenerEstadoAsistenciaPorID/{id}")]
        public async Task<ActionResult<EstadoAsistenciaDTO>> ObtenerEstadoAsistenciaPorID(int id)
        {
            var estado = await _repository.GetByIdAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            var estadoDTO = _mapper.Map<EstadoAsistenciaDTO>(estado);
            return Ok(estadoDTO);
        }

        [HttpPost("RegistrarEstadoAsistencia")]
        public async Task<ActionResult<EstadoAsistenciaDTO>> RegistrarEstadoAsistencia([FromBody] ModEstadoAsistenciaDTO estadosDTO)
        {
            var estado = _mapper.Map<EstadoAsistencia>(estadosDTO);
            await _repository.AddAsync(estado);
            var estadoDTO = _mapper.Map<EstadoAsistenciaDTO>(estado);
            return CreatedAtAction(nameof(ObtenerEstadoAsistenciaPorID), new { id = estadoDTO.Id }, estadoDTO);
        }

        [HttpPut("EditarEstadoAsistencia/{id}")]
        public async Task<IActionResult> EditarEstadoAsistencia(int id, [FromBody] ModEstadoAsistenciaDTO estadosDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var estadoExistente = await _repository.GetByIdAsync(id);

            if (estadoExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(estadosDTO, estadoExistente);
            await _repository.UpdateAsync(estadoExistente);

            var estadoDTO = _mapper.Map<EstadoAsistenciaDTO>(estadoExistente);
            return Ok(estadoDTO);
        }

        [HttpDelete("EliminarEstadoAsistencia/{id}")]
        public async Task<ActionResult<bool>> EliminarEstadoAsistencia(int id)
        {
            var estado = await _repository.GetByIdAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(estado);
        }
    }
}
