using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.Materia;
using SchoolSystem.Core.Interfaces;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.UI.WebAPI.Controllers
{
    [Route("api/Materias")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IRepository<Materia> _repository;
        private readonly IMapper _mapper;
        public MateriaController(IRepository<Materia> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("ListarMaterias")]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> ListarMaterias()
        {
            var materias = await _repository.GetAllAsync();
            var materiasDTOs = _mapper.Map<IEnumerable<MateriaDTO>>(materias);
            return Ok(materiasDTOs);
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

        [HttpPost("RegistrarMateria")]
        public async Task<ActionResult<MateriaDTO>> RegistrarMateria([FromBody] ModMateriaDTO materiasDTO)
        {
            var materia = _mapper.Map<Materia>(materiasDTO);
            await _repository.AddAsync(materia);
            var materiaDTO = _mapper.Map<MateriaDTO>(materia);
            return CreatedAtAction(nameof(ObtenerMateriaPorID), new { id = materiaDTO.Id }, materiaDTO);
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
    }
}
