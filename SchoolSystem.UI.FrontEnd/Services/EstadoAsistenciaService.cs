using SchoolSystem.Core.DTOs.EstadoAsistencia;
using System.Net.Http.Json;

namespace SchoolSystem.UI.FrontEnd.Services
{
    public class EstadoAsistenciaService
    {
        private readonly HttpClient _httpClient;

        public EstadoAsistenciaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EstadoAsistenciaDTO>> ListarEstadosAsistenciaAsync()
        {
            try
            {
                var estados = await _httpClient.GetFromJsonAsync<List<EstadoAsistenciaDTO>>("api/EstadosAsistencia/ListarEstadosAsistencia");

                if (estados == null || !estados.Any())
                {
                    throw new KeyNotFoundException("No se encontraron estados de asistencia.");
                }

                return estados;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar los estados de asistencia.", ex);
            }
        }

        public async Task<EstadoAsistenciaDTO> ObtenerEstadoAsistenciaPorIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var estado = await _httpClient.GetFromJsonAsync<EstadoAsistenciaDTO>($"api/EstadosAsistencia/ObtenerEstadoAsistenciaPorID/{id}");

                if (estado == null)
                {
                    throw new KeyNotFoundException("No se encontró el estado de asistencia con el ID proporcionado.");
                }

                return estado;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener el estado de asistencia con ID {id}.", ex);
            }
        }

        public async Task RegistrarEstadoAsistenciaAsync(EstadoAsistenciaDTO nuevoEstado)
        {
            if (nuevoEstado == null)
            {
                throw new ArgumentNullException(nameof(nuevoEstado), "El estado de asistencia no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/EstadosAsistencia/RegistrarEstadoAsistencia", nuevoEstado);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar el estado de asistencia.", ex);
            }
        }

        public async Task EditarEstadoAsistenciaAsync(int id, EstadoAsistenciaDTO estadoActualizado)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            if (estadoActualizado == null)
            {
                throw new ArgumentNullException(nameof(estadoActualizado), "El estado de asistencia no puede ser nulo.");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/EstadosAsistencia/EditarEstadoAsistencia/{id}", estadoActualizado);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al editar el estado de asistencia con ID {id}.", ex);
            }
        }

        public async Task<bool> EliminarEstadoAsistenciaAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/EstadosAsistencia/EliminarEstadoAsistencia/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al eliminar el estado de asistencia con ID {id}.", ex);
            }
        }
    }
}
