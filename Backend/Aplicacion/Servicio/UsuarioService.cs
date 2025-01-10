using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Aplicacion.Servicio
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository repositorio, IMapper mapper, IConfiguration configuration)
        {
            _usuarioRepository = repositorio;
            _mapper = mapper;
            _configuration = configuration;
        }

        // Login de un usuario
        public async Task<(string mensaje, int userId)> LoginUsuarioAsync(LoginUsuarioDTO loginDto)
        {
            if (loginDto != null)
            {
                // Buscar usuario por email
                var usuario = await _usuarioRepository.ObtenerPorEmailAsync(loginDto.Email);

                if (usuario != null && usuario.PasswordHash == HashearContrasena(loginDto.Password))
                {
                    // Devuelve el mensaje de éxito y el ID del usuario
                    return ("Inicio de sesión exitoso.", usuario.Id);
                }
                throw new Exception("Email o contraseña incorrectos.");
            }
            throw new ArgumentNullException("El DTO de login es nulo.");
        }

        // Registrar un nuevo usuario
        public async Task<RegistrarUsuarioDTO> RegistrarUsuarioAsync(RegistrarUsuarioDTO registrarDto)
        {
            if (registrarDto != null)
            {
                // Verificar si el email ya está registrado
                if (!await _usuarioRepository.EmailExisteAsync(registrarDto.Email))
                {
                    var usuario = _mapper.Map<Usuario>(registrarDto);// Mapear el DTO al modelo de Usuario
                    usuario.PasswordHash = HashearContrasena(registrarDto.Password); // Hash de la contraseña

                    // Guardar el usuario en la base de datos
                    await _usuarioRepository.AgregarAsync(usuario);
                    return registrarDto;
                }
                throw new ArgumentNullException(nameof(registrarDto), "El email ya está registrado.");
            }

            throw new ArgumentNullException("El DTO de registro es nulo.");

        }

        // Obtener los datos de un usuario por su ID
        public async Task<RegistrarUsuarioDTO> ObtenerDatosDelUsuario(int id)
        {
            if (id > 0)
            {
                var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
                if (usuario != null)
                {
                    return _mapper.Map<RegistrarUsuarioDTO>(usuario);
                }
                throw new ArgumentNullException($"No se encontró el usuario con el ID {id}.");
            }
            throw new ArgumentNullException("El ID no puede ser 0.");
        }

        // Método para recuperar la contraseña de un usuario
        public async Task<string> RecuperarContrasenaAsync(string email, string nuevaContrasena)
        {
            if (email != null || nuevaContrasena != null)
            {
                // Buscar al usuario por su email
                var usuario = await _usuarioRepository.ObtenerPorEmailAsync(email);
                if (usuario != null)
                {
                    // Actualizar el usuario con la nueva contraseña y un token de seguridad
                    usuario.PasswordHash = HashearContrasena(nuevaContrasena); // Almacenar la nueva contraseña hasheada
                    usuario.ResetToken = GenerarToken(); // Generar un nuevo token de restablecimiento
                    usuario.TokenExpiracion = DateTime.UtcNow.AddHours(1); // Expiración del token: 1 hora

                    await _usuarioRepository.ActualizarAsync(usuario);

                    // Enviar un correo al usuario con la nueva contraseña
                    var asunto = "Recuperación de contraseña";
                    var mensaje = $"Hola {usuario.Nombre},\n\nTu nueva contraseña es: {nuevaContrasena}\nPor seguridad, te recomendamos cambiarla al iniciar sesión.";
                    await EnviarCorreo(usuario.Email, asunto, mensaje); // Llama al método que ya tienes para enviar correos

                    return "Se a enviado un Correo con la nueva contraseña a su email";
                }
                throw new Exception("El email no está registrado.");
            }
            throw new ArgumentNullException("El email y/o la nueva contraseña son nulos.");
        }
        
        //Metodo para enviar el correo de la confirmacion del canbio de contraseña
        public async Task EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            // Obtener credenciales desde User Secrets
            string emailEmisor = _configuration["EmailDeGemail"];
            string password = _configuration["PasswordDeGemail"];

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,                                   // Puerto de Gmail con TLS
                EnableSsl = true,                             // Activar seguridad SSL
                UseDefaultCredentials = false,                // Desactivo las credenciales por defecto
                Credentials = new NetworkCredential(emailEmisor, password) // Usuario y contraseña
            };

            // Crear el mensaje de correo.
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailEmisor),
                Subject = asunto,  // Asunto del correo.
                Body = mensaje,    // Mensaje (puede ser HTML o texto plano).
                IsBodyHtml = true  // Indicar que el mensaje incluye formato HTML.
            };

            // Agregar el destinatario al correo.
            mailMessage.To.Add(destinatario);

            try
            {
                // Intentar enviar el correo de manera asíncrona.
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Si hay un error, registrarlo en consola y volver a lanzar la excepción.
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                throw;
            }
        }
        
        // ---------------- Metodos Auxiliares ----------------

        // Método para hacer el hash de la contraseña (SHA256)
        private string HashearContrasena(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashed); // Retornar la contraseña en hash
            }
        }

        // Método para Generar un Token
        private string GenerarToken()
        {
            var tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
        }

    }
}
