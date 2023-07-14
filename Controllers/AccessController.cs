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
    public class AccessController : ControllerBase
    {
        private readonly APIEVENTOSContext _context;
        protected ResultadoApi _resultadoApi;

        public AccessController(APIEVENTOSContext context)
        {
            _context = context;
            _resultadoApi = new();
        }

        /*// GET: api/Access
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
          if (_context.Usuario == null)
          {
              return NotFound();
          }
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Access/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
          if (_context.Usuario == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Access/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.cedula)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            Console.WriteLine(usuario.mail);
            Usuario us = await _context.Usuario.FirstOrDefaultAsync(x => x.mail == usuario.mail && x.password == usuario.password);
            if (us == null)
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString().ToUpper();
                return BadRequest(_resultadoApi);
            }
            else
            {
                _resultadoApi.NUsuario = us;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
        }
        /*
        // DELETE: api/Access/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
        private bool UsuarioExists(string id)
        {
            return (_context.Usuario?.Any(e => e.cedula == id)).GetValueOrDefault();
        }
    }
}
