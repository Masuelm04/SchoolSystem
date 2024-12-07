using SchoolSystem.Core.DTOs.Curso;
using SchoolSystem.Core.DTOs.Materia;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class CursoService
    {
        private readonly HttpClient _httpClient;

        public CursoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CursoDTO>> ListarCursosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CursoDTO>>("api/Cursos/ListarCursos")
                    ?? new List<CursoDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar los cursos.", ex);
            }
        }

        public async Task<List<CursoMateriaDTO>> GetCursosConMateriasAsync()
        {
            try
            {
                var cursos = await _httpClient.GetFromJsonAsync<List<CursoMateriaDTO>>("api/Cursos/ListarCursosConMaterias");

                if (cursos == null || !cursos.Any())
                {
                    throw new KeyNotFoundException("No se encontraron cursos con materias asociadas.");
                }

                return cursos;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los cursos con materias asociadas.", ex);
            }
        }

        public async Task<CursoDTO> ObtenerCursoPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var curso = await _httpClient.GetFromJsonAsync<CursoDTO>($"api/Cursos/ObtenerCursoPorID/{id}");

                if (curso == null)
                {
                    throw new KeyNotFoundException("No se encontró el curso con el ID proporcionado.");
                }

                return curso;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el curso con ID {id}.", ex);
            }
        }

        public async Task RegistrarCursoAsync(CursoDTO nuevoCurso)
        {
            if (nuevoCurso == null)
            {
                throw new ArgumentNullException(nameof(nuevoCurso), "El curso no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Cursos/RegistrarCurso", nuevoCurso);
                response.EnsureSuccessStatusCode();
                //return await response.Content.ReadFromJsonAsync<CursoDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar el curso.", ex);
            }
        }

        public async Task AsignarMateriaAsync(int idCurso, int idMateria)
        {
            if (idCurso <= 0 || idMateria <= 0)
            {
                throw new ArgumentException("El ID del curso y la materia deben ser mayores que cero.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Cursos/{idCurso}/AsignarMateria/{idMateria}", new { });
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al asignar la materia al curso.", ex);
            }
        }

        public async Task<List<MateriaDTO>> ObtenerMateriasPorCursoAsync(int idCurso)
        {
            if (idCurso <= 0)
            {
                throw new ArgumentException("El ID del curso debe ser mayor que cero.", nameof(idCurso));
            }

            try
            {
                var materias = await _httpClient.GetFromJsonAsync<List<MateriaDTO>>($"api/Cursos/ObtenerMateriasPorCurso/{idCurso}");

                if (materias == null || !materias.Any())
                {
                    throw new KeyNotFoundException("No se encontraron materias para el curso proporcionado.");
                }

                return materias;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener las materias para el curso.", ex);
            }
        }

        public async Task EliminarAsignacionMateriaAsync(int idCurso, int idMateria)
        {
            if (idCurso <= 0 || idMateria <= 0)
            {
                throw new ArgumentException("El ID del curso y la materia deben ser mayores que cero.");
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Cursos/{idCurso}/EliminarAsignacionMateria/{idMateria}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar la asignación de la materia del curso.", ex);
            }
        }

        public async Task EditarCursoAsync(int id, CursoDTO cursoActualizado)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            if (cursoActualizado == null)
            {
                throw new ArgumentNullException(nameof(cursoActualizado), "El curso no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Cursos/EditarCurso/{id}", cursoActualizado);
                response.EnsureSuccessStatusCode();
                //return await response.Content.ReadFromJsonAsync<CursoDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar el curso con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarCursoAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Cursos/EliminarCurso/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar el curso con ID {id}.", ex);
            }
        }
    }
}
