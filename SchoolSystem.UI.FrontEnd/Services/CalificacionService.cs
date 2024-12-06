using SchoolSystem.Core.DTOs.Calificacion;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class CalificacionService
    {
        private readonly HttpClient _httpClient;

        public CalificacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CalificacionDTO>> ListarCalificacionesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CalificacionDTO>>("api/Calificaciones/ListarCalificaciones")
                    ?? new List<CalificacionDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar las calificaciones.", ex);
            }
        }

        public async Task<CalificacionDTO> ObtenerCalificacionPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var calificacion = await _httpClient.GetFromJsonAsync<CalificacionDTO>($"api/Calificaciones/ObtenerCalificacionPorID/{id}");

                if (calificacion == null)
                {
                    throw new KeyNotFoundException("No se encontró una calificación con el ID proporcionado.");
                }

                return calificacion;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener la calificación con ID {id}.", ex);
            }
        }

        public async Task RegistrarCalificacionAsync(CalificacionDTO nuevaCalificacion)
        {
            ValidarCalificacion(nuevaCalificacion);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Calificaciones/RegistrarCalificacion", nuevaCalificacion);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la calificación.", ex);
            }
        }

        public async Task EditarCalificacionAsync(int id, CalificacionDTO calificacionActualizada)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            ValidarCalificacion(calificacionActualizada);

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Calificaciones/EditarCalificacion/{id}", calificacionActualizada);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar la calificación con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarCalificacionAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Calificaciones/EliminarCalificacion/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar la calificación con ID {id}.", ex);
            }
        }

        private void ValidarCalificacion(CalificacionDTO calificacion)
        {
            if (calificacion == null)
            {
                throw new ArgumentNullException(nameof(calificacion), "La calificación no puede ser nula.");
            }

            if (calificacion.IdEstudiante <= 0)
            {
                throw new ArgumentException("El ID del estudiante debe ser mayor que cero.", nameof(calificacion.IdEstudiante));
            }

            if (calificacion.IdMateria <= 0)
            {
                throw new ArgumentException("El ID de la materia debe ser mayor que cero.", nameof(calificacion.IdMateria));
            }

            if (calificacion.Nota < 0 || calificacion.Nota > 100)
            {
                throw new ArgumentException("El valor de la calificación debe estar entre 0 y 100.", nameof(calificacion.Nota));
            }
        }
    }
}
