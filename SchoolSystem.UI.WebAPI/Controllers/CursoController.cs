using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.EstadoAsistencia;
using SchoolSystem.Core.DTOs.Materia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using SchoolSystem.Infrastructure.Repositories;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Cursos")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly IRepository<Curso> _repository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public CursoController(IRepository<Curso> repository, ICursoRepository cursoRepository, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _cursoRepository = cursoRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("ListarCursos")]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> ListarCursos()
        {
            var cursos = await _repository.GetAllAsync();
            var cursosDTOs = _mapper.Map<IEnumerable<CursoDTO>>(cursos);
            return Ok(cursosDTOs);
        }

        [HttpGet("ListarCursosConMaterias")]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> GetCursosConMateriasAsync()
        {
            var cursos = await _cursoRepository.GetCursosConMateriasAsync();

            if (cursos == null || !cursos.Any())
            {
                return NotFound(new { Message = "No se encontraron cursos con materias asociadas." });
            }

            var cursosDTO = _mapper.Map<IEnumerable<CursoDTO>>(cursos);
            return Ok(cursosDTO);
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

        [HttpGet("ObtenerMateriasPorCurso/{idCurso}")]
        public async Task<ActionResult<IEnumerable<MateriaCursoDTO>>> ObtenerMateriasPorCurso(int idCurso)
        {
            var materias = await _cursoRepository.ObtenerMateriasPorCursoAsync(idCurso);
            var materiasDTO = _mapper.Map<IEnumerable<MateriaCursoDTO>>(materias);
            return Ok(materiasDTO);
        }

        [HttpGet("CursoTieneMateria/{idCurso}/{idMateria}")]
        public async Task<ActionResult<bool>> CursoTieneMateriaAsync(int idCurso, int idMateria)
        {
            var cursoMateria = await _context.CursosMaterias
                .FirstOrDefaultAsync(cm => cm.IdCurso == idCurso && cm.IdMateria == idMateria && cm.Curso.Eliminado == false && cm.Materia.Eliminado == false);

            if (cursoMateria == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost("RegistrarCurso")]
        public async Task<ActionResult<CursoDTO>> RegistrarCurso([FromBody] ModCursoDTO cursosDTO)
        {
            var curso = _mapper.Map<Curso>(cursosDTO);
            await _repository.AddAsync(curso);
            var cursoDTO = _mapper.Map<CursoDTO>(curso);
            return CreatedAtAction(nameof(ObtenerCursoPorID), new { id = cursoDTO.Id }, cursoDTO);
        }

        [HttpPost("{idCurso}/AsignarMateria/{idMateria}")]
        public async Task<IActionResult> AsignarMateria(int idCurso, int idMateria)
        {
            await _cursoRepository.AsignarMateriaAsync(idCurso, idMateria);
            return Ok(new { Message = "Materia asignada al curso correctamente." });
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

        [HttpDelete("{idCurso}/EliminarAsignacionMateria/{idMateria}")]
        public async Task<IActionResult> EliminarAsignacionMateria(int idCurso, int idMateria)
        {
            await _cursoRepository.EliminarAsignacionMateriaAsync(idCurso, idMateria);
            return Ok(new { Message = "Asignación de materia eliminada del curso correctamente." });
        }
    }
}
