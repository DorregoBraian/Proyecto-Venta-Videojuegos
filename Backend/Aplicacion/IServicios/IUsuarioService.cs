using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.IServicios
{
    public interface IUsuarioService
    {
        Task<RegistrarUsuarioDTO> ObtenerDatosDelUsuario(int id); // Método para obtener los datos de un usuario
        Task<(string mensaje, int userId)> LoginUsuarioAsync(LoginUsuarioDTO loginDto); // Método para loguear un usuario
        Task<RegistrarUsuarioDTO> RegistrarUsuarioAsync(RegistrarUsuarioDTO registrarDto); // Método para registrar un usuario
        Task<string> RecuperarContrasenaAsync(string email, string nuevaContrasena); // Método para recuperar la contraseña
        Task EnviarCorreo(string destinatario, string asunto, string mensaje); // Método para enviar un correo
    }
}
