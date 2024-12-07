﻿using Core.Entities;
using Core.Inputs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [ApiController]
    [Route("/Cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public CadastroController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarContato([FromBody] ContatoInputCadastrar input)
        {
            try
            {
                var nomeFila = _configuration.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

                var contato = new Contato
                {
                    ContatoNome = input.ContatoNome,
                    ContatoEmail = input.ContatoEmail,
                    ContatoTelefone = input.ContatoTelefone
                };

                await endpoint.Send(contato);
                return Ok("Contato enviado para a fila.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
