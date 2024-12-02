using SchoolSystem.Core.DTOs.Asistencia;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class AsistenciaService
    {
        private readonly HttpClient _httpClient;

        public AsistenciaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AsistenciaDTO>> ListarAsistenciasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AsistenciaDTO>>("api/Asistencias/ListarAsistencias")
                    ?? new List<AsistenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar las asistencias.", ex);
            }
        }

        public async Task<AsistenciaDTO> ObtenerAsistenciaPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var asistencia = await _httpClient.GetFromJsonAsync<AsistenciaDTO>($"api/Asistencias/ObtenerAsistenciaPorID/{id}");

                if (asistencia == null)
                {
                    throw new KeyNotFoundException("No se encontró una asistencia con el ID proporcionado.");
                }

                return asistencia;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener la asistencia con ID {id}.", ex);
            }
        }

        public async Task<List<HistorialAsistenciaDTO>> ObtenerHistorialAsistenciaPorFechaAsync(DateTime fecha)
        {
            if (fecha == null || fecha == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha proporcionada no es válida.", nameof(fecha));
            }

            try
            {
                var historial = await _httpClient.GetFromJsonAsync<List<HistorialAsistenciaDTO>>($"api/Asistencias/ObtenerHistorialAsistenciaPorFecha/{fecha.ToString("yyyy-MM-dd")}");

                if (historial == null || !historial.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron registros de asistencia para la fecha {fecha.ToShortDateString()}.");
                }

                return historial;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el historial de asistencia para la fecha {fecha.ToShortDateString()}.", ex);
            }
        }

        public async Task<AsistenciaDTO> RegistrarAsistenciaAsync(ModAsistenciaDTO nuevaAsistencia)
        {
            ValidarAsistencia(nuevaAsistencia);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Asistencias/RegistrarAsistencia", nuevaAsistencia);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AsistenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la asistencia.", ex);
            }
        }

        public async Task<AsistenciaResponseDTO> EditarAsistenciaAsync(int id, ModAsistenciaDTO asistenciaActualizada)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            ValidarAsistencia(asistenciaActualizada);

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Asistencias/EditarAsistencia/{id}", asistenciaActualizada);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AsistenciaResponseDTO>();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar la asistencia con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarAsistenciaAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Asistencias/EliminarAsistencia/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar la asistencia con ID {id}.", ex);
            }
        }

        private void ValidarAsistencia(ModAsistenciaDTO asistencia)
        {
            if (asistencia == null)
            {
                throw new ArgumentNullException(nameof(asistencia), "La asistencia no puede ser nula.");
            }

            if (asistencia.IdEstudiante <= 0)
            {
                throw new ArgumentException("El ID del estudiante debe ser mayor que cero.", nameof(asistencia.IdEstudiante));
            }

            if (asistencia.IdEstadoAsistencia <= 0)
            {
                throw new ArgumentException("El ID del estado debe ser mayor que cero.", nameof(asistencia.IdEstadoAsistencia));
            }

            if (asistencia.Fecha == null || asistencia.Fecha == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha de asistencia es requerida y no puede estar vacía.", nameof(asistencia.Fecha));
            }
        }
    }
}
