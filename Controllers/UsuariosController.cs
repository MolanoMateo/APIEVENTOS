using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIEVENTOS.Data;
using APIEVENTOS.Models;
using System.Net;

namespace APIEVENTOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly APIEVENTOSContext _context;
        protected ResultadoApi _resultadoApi;

        public UsuariosController(APIEVENTOSContext context)
        {
            _context = context;
            _resultadoApi = new();
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            var usu = await _context.Usuario.ToListAsync();

            if (usu != null)
            {
                _resultadoApi.LUsuario = usu;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
            return Ok(_resultadoApi);
        }

        // GET: api/Usuarios/5
        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetUsuarioId(string cedula)
        {
            Usuario usu = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == cedula);
            if (usu != null)
            {
                _resultadoApi.NUsuario = usu;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cedula}")]
        public async Task<IActionResult> PutUsuario(string cedula, [FromBody] Usuario usuario)
        {
            Usuario usunew = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == cedula);

            if (usunew != null)
            {
                usunew.nombre = usuario.nombre != null ? usuario.nombre : usunew.nombre;
                usunew.mail = usuario.mail != null ? usuario.mail : usunew.mail;
                usunew.telefono = usuario.telefono != null ? usuario.telefono : usunew.telefono;
                usunew.edad = usuario.edad != 0 ? usuario.edad : usunew.edad;
                usunew.foto = usuario.foto != null ? usuario.foto : usunew.foto;
                usunew.password = usuario.password != null ? usuario.password : usunew.password;
                usunew.rol = usuario.rol != null ? usuario.rol : usunew.rol;

                _context.Update(usunew);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            Usuario us = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == usuario.cedula);
            if (us == null)
            {
                usuario.rol = "User";
                await _context.Usuario.AddAsync(usuario);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString().ToUpper();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString().ToUpper();
                return BadRequest(_resultadoApi);
            }
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> DeleteUsuario(string cedula)
        {
            Usuario us = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == cedula);
            if (us != null)
            {
                _context.Usuario.Remove(us);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        private bool UsuarioExists(string cedula)
        {
            return (_context.Usuario?.Any(e => e.cedula == cedula)).GetValueOrDefault();
        }
    }
}
