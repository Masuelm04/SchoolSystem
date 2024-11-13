using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.Materia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;
using SchoolSystem.Infrastructure.Data;
using SchoolSystem.Infrastructure.Repositories;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Materias")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IRepository<Materia> _repository;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public MateriaController(IRepository<Materia> repository, IMateriaRepository materiaRepository, IMapper mapper, ApplicationDbContext context)
        {
            _repository = repository;
            _materiaRepository = materiaRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("ListarMaterias")]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> ListarMaterias()
        {
            var materias = await _repository.GetAllAsync();
            var materiasDTOs = _mapper.Map<IEnumerable<MateriaDTO>>(materias);
            return Ok(materiasDTOs);
        }

        [HttpGet("ListarMateriasConCursos")]
        public async Task<ActionResult<IEnumerable<MateriaCursoDTO>>> GetMateriasConCursosAsync()
        {
            var materias = await _materiaRepository.GetMateriasConCursosAsync();

            if (materias == null || !materias.Any())
            {
                return NotFound(new { Message = "No se encontraron materias con cursos asociados." });
            }

            var materiasDTO = _mapper.Map<IEnumerable<MateriaCursoDTO>>(materias);
            return Ok(materiasDTO);
        }

        [HttpGet("ObtenerMateriaPorID/{id}")]
        public async Task<ActionResult<MateriaDTO>> ObtenerMateriaPorID(int id)
        {
            var materia = await _repository.GetByIdAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            var materiaDTO = _mapper.Map<MateriaDTO>(materia);
            return Ok(materiaDTO);
        }

        [HttpGet("ObtenerCursosPorMateria/{idMateria}")]
        public async Task<ActionResult<IEnumerable<CursoDTO>>> ObtenerCursosPorMateria(int idMateria)
        {
            var cursos = await _materiaRepository.ObtenerCursosPorMateriaAsync(idMateria);
            var cursosDTO = _mapper.Map<IEnumerable<CursoDTO>>(cursos);
            return Ok(cursosDTO);
        }

        [HttpGet("MateriaEstaEnCurso/{idMateria}/{idCurso}")]
        public async Task<ActionResult<bool>> MateriaEstaEnCursoAsync(int idMateria, int idCurso)
        {
            var materiaCurso = await _context.CursosMaterias
                .FirstOrDefaultAsync(cm => cm.IdMateria == idMateria && cm.IdCurso == idCurso && cm.Curso.Eliminado == false && cm.Materia.Eliminado == false);

            if (materiaCurso == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpPost("RegistrarMateria")]
        public async Task<ActionResult<MateriaDTO>> RegistrarMateria([FromBody] ModMateriaDTO materiasDTO)
        {
            var materia = _mapper.Map<Materia>(materiasDTO);
            await _repository.AddAsync(materia);
            var materiaDTO = _mapper.Map<MateriaDTO>(materia);
            return CreatedAtAction(nameof(ObtenerMateriaPorID), new { id = materiaDTO.Id }, materiaDTO);
        }

        [HttpPost("{idMateria}/AsignarCurso/{idCurso}")]
        public async Task<IActionResult> AsignarCurso(int idMateria, int idCurso)
        {
            await _materiaRepository.AsignarCursoAsync(idMateria, idCurso);
            return Ok(new { Message = "Curso asignado a la materia correctamente." });
        }

        [HttpPut("EditarMateria/{id}")]
        public async Task<IActionResult> EditarMateria(int id, [FromBody] ModMateriaDTO materiasDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var materiaExistente = await _repository.GetByIdAsync(id);

            if (materiaExistente == null)
            {
                return NotFound();
            }

            _mapper.Map(materiasDTO, materiaExistente);
            await _repository.UpdateAsync(materiaExistente);

            var materiaDTO = _mapper.Map<MateriaDTO>(materiaExistente);
            return Ok(materiaDTO);
        }

        [HttpDelete("EliminarMateria/{id}")]
        public async Task<ActionResult<bool>> EliminarMateria(int id)
        {
            var materia = await _repository.GetByIdAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            await _repository.DeleteByIdAsync(id);
            return Ok(materia);
        }

        [HttpDelete("{idMateria}/EliminarAsignacionCurso/{idCurso}")]
        public async Task<IActionResult> EliminarAsignacionCurso(int idMateria, int idCurso)
        {
            await _materiaRepository.EliminarAsignacionCursoAsync(idMateria, idCurso);
            return Ok(new { Message = "Asignación de curso eliminada de la materia correctamente." });
        }
    }
}
