using Core.Mensagens;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AtualizacaoProdutor.Controllers
{
    [ApiController]
    [Route("/Atualizacao")]
    public class AtualizacaoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public AtualizacaoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarContato([FromBody] ContatoAtualizadoMensagem input)
        {
            try
            {
                var nomeFila = "filaAtualizacao";
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
