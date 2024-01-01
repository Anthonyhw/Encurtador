using Azure.Core;
using Encurtador.Context;
using Encurtador.DTOs;
using Encurtador.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Encurtador.Controllers
{
    [ApiController]
    [Route("")]
    public class EncurtadorController : ControllerBase
    {
        private readonly EncurtadorContext _context;
        public EncurtadorController(EncurtadorContext context)
        {
            _context = context;
        }

        [HttpGet("{shortenName}")]
        public IActionResult RedirectTo(string shortenName)
        {
            var foundLink = _context.Links.FirstOrDefault(l => l.Encurtador == shortenName);

            if (foundLink == null)
            {
                return NotFound("Não há nenhum link vinculado ao encurtador!");
            }

            foundLink.Visitas += 1;

            _context.SaveChanges();

            return Redirect(foundLink.URL);

        }

        [HttpPost("CreateShortner")]
        public IActionResult CreateShortner(CreateShortnerRequest request)
        {
            if (request.ShortName == null)
            {
                request.ShortName = GenerateRandomShortner(7);
            }
            else
            {
                if (_context.Links.FirstOrDefault(l => request.ShortName == l.Encurtador) != null)
                    return BadRequest("Encurtador já utilizado!");
            }
            

            var link = new Link()
            {
                Encurtador = request.ShortName,
                URL = request.URL,
                Visitas = 0
            };

            _context.Links.Add(link);

            if (_context.SaveChanges() > 0)
                return Ok(link.Encurtador);
            return BadRequest("Ocorreu um problema. Por favor, tente mais tarde!");
        }

        private string GenerateRandomShortner(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();

            char[] randomArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            if (_context.Links.FirstOrDefault(l => new string(randomArray) == l.Encurtador) != null)
                return new string(randomArray);
            else
                return GenerateRandomShortner(length);
        }
    }
}