using Core.Mensagens;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutor.Controllers
{
    [ApiController]
    [Route("/Cadastro")]
    public class ContatoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public ContatoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarContato([FromBody] ContatoCriadoMensagem input)
        {
            try
            {
                var nomeFila = "filaCadastro";
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

                await endpoint.Send(input);
                return Ok("Contato enviado para a fila.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
