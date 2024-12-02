using SchoolSystem.Core.DTOs.Estudiante;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class EstudianteService
    {
        private readonly HttpClient _httpClient;

        public EstudianteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstudianteDTO>> ListarEstudiantesAsync()
        {
            try
            {
                var estudiantes = await _httpClient.GetFromJsonAsync<List<EstudianteDTO>>("api/Estudiantes/ListarEstudiantes");

                if (estudiantes == null || estudiantes.Count == 0)
                {
                    throw new KeyNotFoundException("No se encontraron estudiantes.");
                }

                return estudiantes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar los estudiantes.", ex);
            }
        }

        public async Task<EstudianteDTO> ObtenerEstudiantePorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var estudiante = await _httpClient.GetFromJsonAsync<EstudianteDTO>($"api/Estudiantes/ObtenerEstudiantePorID/{id}");

                if (estudiante == null)
                {
                    throw new KeyNotFoundException("No se encontró el estudiante con el ID proporcionado.");
                }

                return estudiante;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el estudiante con ID {id}.", ex);
            }
        }

        public async Task<EstudianteDTO> RegistrarEstudianteAsync(ModEstudianteDTO nuevoEstudiante)
        {
            if (nuevoEstudiante == null)
            {
                throw new ArgumentNullException(nameof(nuevoEstudiante), "El estudiante no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Estudiantes/RegistrarEstudiante", nuevoEstudiante);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<EstudianteDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar el estudiante.", ex);
            }
        }

        public async Task<EstudianteResponseDTO> EditarEstudianteAsync(int id, ModEstudianteDTO estudianteActualizado)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            if (estudianteActualizado == null)
            {
                throw new ArgumentNullException(nameof(estudianteActualizado), "El estudiante no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Estudiantes/EditarEstudiante/{id}", estudianteActualizado);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<EstudianteResponseDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar el estudiante con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarEstudianteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Estudiantes/EliminarEstudiante/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar el estudiante con ID {id}.", ex);
            }
        }

        public async Task<List<EstudianteDTO>> FiltrarEstudiantesAsync(string nombre, string apellido, string curso)
        {
            try
            {
                var url = $"api/Estudiantes/FiltrarEstudiantes?nombre={nombre}&apellido={apellido}&curso={curso}";
                var estudiantes = await _httpClient.GetFromJsonAsync<List<EstudianteDTO>>(url);

                return estudiantes;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al filtrar los estudiantes.", ex);
            }
        }
    }
}
