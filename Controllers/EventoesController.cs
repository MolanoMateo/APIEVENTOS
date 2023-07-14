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
    public class EventoesController : ControllerBase
    {
        private readonly APIEVENTOSContext _context;
        protected ResultadoApi _resultadoApi;

        public EventoesController(APIEVENTOSContext context)
        {
            _context = context;
            _resultadoApi = new();
        }

        // GET: api/Eventoes
        [HttpGet]
        public async Task<IActionResult> GetEvento()
        {
            var ev = await _context.Evento.ToListAsync();

            if (ev != null)
            {
                _resultadoApi.LEvento = ev;
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

        // GET: api/Eventoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoId(int id)
        {
            Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == id);
            if (ev != null)
            {
                _resultadoApi.NEvento = ev;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // PUT: api/Eventoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] Evento evento)
        {
            Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == id);

            if (ev != null)
            {

                ev.titulo = evento.titulo != null ? evento.titulo : ev.titulo;
                ev.descripcion = evento.descripcion != null ? evento.descripcion : ev.descripcion;
                ev.costo = evento.costo != -1 ? evento.costo : ev.costo;
                ev.ubicacion = evento.ubicacion != null ? evento.ubicacion : ev.ubicacion;
                ev.fecha = evento.fecha != null ? evento.fecha : ev.fecha;

                _context.Update(ev);
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

        // POST: api/Eventoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEvento([FromBody] Evento evento)
        {
            Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == evento.Id);
            if (ev == null)
            {
                await _context.Evento.AddAsync(evento);
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

        // DELETE: api/Eventoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == id);

            if (ev != null)
            {
                _context.Evento.Remove(ev);
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

        private bool EventoExists(int id)
        {
            return (_context.Evento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
