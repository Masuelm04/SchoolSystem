using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.Materia;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class MateriaService
    {
        private readonly HttpClient _httpClient;

        public MateriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MateriaDTO>> ListarMateriasAsync()
        {
            try
            {
                var materias = await _httpClient.GetFromJsonAsync<List<MateriaDTO>>("api/Materias/ListarMaterias");

                if (materias == null || materias.Count == 0)
                {
                    throw new KeyNotFoundException("No se encontraron materias.");
                }

                return materias;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar las materias.", ex);
            }
        }

        public async Task<MateriaDTO> ObtenerMateriaPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var materia = await _httpClient.GetFromJsonAsync<MateriaDTO>($"api/Materias/ObtenerMateriaPorID/{id}");

                if (materia == null)
                {
                    throw new KeyNotFoundException("No se encontró la materia con el ID proporcionado.");
                }

                return materia;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener la materia con ID {id}.", ex);
            }
        }

        public async Task<List<CursoDTO>> ObtenerCursosPorMateriaAsync(int idMateria)
        {
            if (idMateria <= 0)
            {
                throw new ArgumentException("El ID de la materia debe ser mayor que cero.", nameof(idMateria));
            }

            try
            {
                var cursos = await _httpClient.GetFromJsonAsync<List<CursoDTO>>($"api/Materias/ObtenerCursosPorMateria/{idMateria}");

                if (cursos == null || cursos.Count == 0)
                {
                    throw new KeyNotFoundException("No se encontraron cursos para la materia proporcionada.");
                }

                return cursos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener los cursos para la materia con ID {idMateria}.", ex);
            }
        }

        public async Task<bool> MateriaEstaEnCursoAsync(int idMateria, int idCurso)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<bool>($"api/Materias/MateriaEstaEnCurso/{idMateria}/{idCurso}");
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al verificar la asignación de la materia en el curso.", ex);
            }
        }

        public async Task RegistrarMateriaAsync(MateriaDTO nuevaMateria)
        {
            if (nuevaMateria == null)
            {
                throw new ArgumentNullException(nameof(nuevaMateria), "La materia no puede ser nula.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Materias/RegistrarMateria", nuevaMateria);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la materia.", ex);
            }
        }

        public async Task AsignarCursoAsync(int idMateria, int idCurso)
        {
            if (idMateria <= 0 || idCurso <= 0)
            {
                throw new ArgumentException("El ID de la materia y el curso deben ser mayores que cero.");
            }

            try
            {
                var response = await _httpClient.PostAsync($"api/Materias/{idMateria}/AsignarCurso/{idCurso}", null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al asignar el curso a la materia.", ex);
            }
        }

        public async Task EliminarAsignacionCursoAsync(int idMateria, int idCurso)
        {
            if (idMateria <= 0 || idCurso <= 0)
            {
                throw new ArgumentException("El ID de la materia y el curso deben ser mayores que cero.");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Materias/{idMateria}/EliminarAsignacionCurso/{idCurso}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar la asignación del curso a la materia.", ex);
            }
        }

        public async Task EditarMateriaAsync(int id, MateriaDTO materiaActualizada)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            if (materiaActualizada == null)
            {
                throw new ArgumentNullException(nameof(materiaActualizada), "La materia no puede ser nula.");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Materias/EditarMateria/{id}", materiaActualizada);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar la materia con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarMateriaAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Materias/EliminarMateria/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar la materia con ID {id}.", ex);
            }
        }
    }
}
