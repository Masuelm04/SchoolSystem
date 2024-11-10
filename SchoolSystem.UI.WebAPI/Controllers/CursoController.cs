using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.EstadoAsistencia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Cursos")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly IRepository<Curso> _repository;
        private readonly IMapper _mapper;
        public CursoController(IRepository<Curso> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ListarCursos")]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> ListarCursos()
        {
            var cursos = await _repository.GetAllAsync();
            var cursosDTOs = _mapper.Map<IEnumerable<CursoDTO>>(cursos);
            return Ok(cursosDTOs);
        }

        [HttpGet("ObtenerCursoPorID/{id}")]
        public async Task<ActionResult<CursoDTO>> ObtenerCursoPorID(int id)
        {
            var curso = await _repository.GetByIdAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            var cursoDTO = _mapper.Map<CursoDTO>(curso);
            return Ok(cursoDTO);
        }

        [HttpPost("RegistrarCurso")]
        public async Task<ActionResult<CursoDTO>> RegistrarCurso([FromBody] ModCursoDTO cursosDTO)
        {
            var curso = _mapper.Map<Curso>(cursosDTO);
            await _repository.AddAsync(curso);
            var cursoDTO = _mapper.Map<CursoDTO>(curso);
            return CreatedAtAction(nameof(ObtenerCursoPorID), new { id = cursoDTO.Id }, cursoDTO);
        }

        [HttpPut("EditarCurso/{id}")]
        public async Task<IActionResult> EditarCurso(int id, [FromBody] ModCursoDTO cursosDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cursoExistente = await _repository.GetByIdAsync(id);

            if (cursoExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(cursosDTO, cursoExistente);
            await _repository.UpdateAsync(cursoExistente);

            var cursoDTO = _mapper.Map<CursoDTO>(cursoExistente);
            return Ok(cursoDTO);
        }

        [HttpDelete("EliminarCurso/{id}")]
        public async Task<ActionResult<bool>> EliminarCurso(int id)
        {
            var curso = await _repository.GetByIdAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(curso);
        }
    }
}
