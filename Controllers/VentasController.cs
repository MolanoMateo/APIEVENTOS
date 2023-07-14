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
    public class VentasController : ControllerBase
    {
        private readonly APIEVENTOSContext _context;
        protected ResultadoApi _resultadoApi;

        public VentasController(APIEVENTOSContext context)
        {
            _context = context;
            _resultadoApi = new();
        }

        // GET: api/Ventas
        
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> GetVenta()
        {
            var ven = await _context.Venta.ToListAsync();

            if (ven != null)
            {
                List<Usuario> us=new List<Usuario>();
                List<Evento> ev=new List<Evento>();
                foreach (Venta v in ven)
                {
                    Usuario usu = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == v.usuario);
                    if (usu==null)
                    { usu = new Usuario();
                        usu.cedula = v.usuario;
                        usu.mail = " ";
                        usu.nombre = " ";
                        usu.telefono = " ";
                        usu.edad = 99;
                        usu.foto = " ";
    }
                    Evento eve = await _context.Evento.FirstOrDefaultAsync(x => x.Id == v.idEvento);
                    us.Add(usu);
                    ev.Add(eve);
                }
                _resultadoApi.LVenta = ven;
                _resultadoApi.LUsuario = us;
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

        // GET: api/Ventas/5
        
        [HttpGet]
        [Route("Ven/{id}")]
        public async Task<IActionResult> GetVenta(int id)
        {
            Venta ven = await _context.Venta.FirstOrDefaultAsync(x => x.Id == id);
            if (ven != null)
            {
                    Usuario usu = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == ven.usuario);
                    if (usu == null)
                    {
                        usu = new Usuario();
                        usu.cedula = ven.usuario;
                        usu.mail = " ";
                        usu.nombre = " ";
                        usu.telefono = " ";
                        usu.edad = 99;
                        usu.foto = " ";
                    }
                    Evento eve = await _context.Evento.FirstOrDefaultAsync(x => x.Id == ven.idEvento);
                    
                
                Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == ven.idEvento);
                _resultadoApi.NVenta = ven;
                _resultadoApi.NUsuario = usu;
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
        
        [HttpGet]
        [Route("compra/{cedula}")]
        public async Task<IActionResult> GetCompra(string cedula)
        {
            var ven = await _context.Venta.Where(x => x.usuario == cedula).ToListAsync();
            if (ven != null)
            {
                List<Evento> ev = new List<Evento>();
                foreach (Venta v in ven)
                {
                    Evento eve = await _context.Evento.FirstOrDefaultAsync(x => x.Id == v.idEvento);
                    ev.Add(eve);
                }
                _resultadoApi.LVenta = ven;
                _resultadoApi.LEvento = ev;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> PostVenta([FromBody] Venta venta)
        {
            Evento ev = await _context.Evento.FirstOrDefaultAsync(x => x.Id == venta.idEvento);
            Usuario usu = await _context.Usuario.FirstOrDefaultAsync(x => x.cedula == venta.usuario);
            //Venta ven = await _context.Venta.FirstOrDefaultAsync(x => x.Id == venta.Id);
            if (ev != null && usu != null)
            {
                await _context.Venta.AddAsync(venta);
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

        private bool VentaExists(int id)
        {
            return (_context.Venta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
