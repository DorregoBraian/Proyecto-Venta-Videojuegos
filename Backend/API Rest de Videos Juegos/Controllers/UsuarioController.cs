using Aplicacion.DTOs;
using Aplicacion.IServicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API_Rest_de_Videos_Juegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService servicio)
        {
            _usuarioService = servicio;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDatosDelUsuario(int id)
        {
            try
            {
                // Llamamos al servicio para obtener los datos del usuario
                var usuario = await _usuarioService.ObtenerDatosDelUsuario(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos un mensaje de error
                return BadRequest(ex.Message);
            }
        }

        // Método para registrar un nuevo usuario
        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioDTO registrarDto)
        {
            try
            {
                // Llamamos al servicio para registrar al usuario
                await _usuarioService.RegistrarUsuarioAsync(registrarDto);
                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos un mensaje de error
                return BadRequest(ex.Message);
            }
        }

        // Método para hacer login (autenticación) del usuario
        [HttpPost("login")]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginUsuarioDTO loginDto)
        {
            try
            {
                // Llamamos al servicio para hacer login
                var servisResponse = await _usuarioService.LoginUsuarioAsync(loginDto);

                var response = new LoginResponse
                {
                    Message = servisResponse.mensaje,
                    UserId = servisResponse.userId,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos un mensaje de error
                return Unauthorized(new { message = ex.Message }); 
            }
        }

        // Método para solicitar la recuperación de contraseña
        [HttpPost("recuperar-contrasena")]
        public async Task<IActionResult> RecuperarContrasena([FromBody] RecuperarContrasenaDTO recuperarContrasena)
        {
            try
            {
                // Llamamos al servicio para generar y enviar el token de recuperación
                var enlace = await _usuarioService.RecuperarContrasenaAsync(recuperarContrasena.Email, recuperarContrasena.nuevaContrasena);
                return Ok(new { message = enlace });
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolvemos un mensaje de error
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("enviar-correo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] EmailDTO emailRequest)
        {
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.Destinatario) ||
                string.IsNullOrEmpty(emailRequest.Asunto) || string.IsNullOrEmpty(emailRequest.Mensaje))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            try
            {
                await _usuarioService.EnviarCorreo(emailRequest.Destinatario, emailRequest.Asunto, emailRequest.Mensaje);
                return Ok(new { message = "Correo enviado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al enviar el correo: {ex.Message}" });
            }
        }

    }
}
