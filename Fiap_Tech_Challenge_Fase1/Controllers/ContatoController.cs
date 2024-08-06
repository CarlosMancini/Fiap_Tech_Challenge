﻿using Core.Entities;
using Core.Inptus;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap_Tech_Challenge_Fase1.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContatoController(IContatoService contatoService) : ControllerBase
    {
        private readonly IContatoService _contatoService = contatoService;

        [HttpPost, Route("[controller]")]
        public IActionResult Post([FromBody] ContatoInput input)
        {
            try
            {
                var contato = new Contato()
                {
                    ContatoNome = input.ContatoNome,
                    ContatoTelefone = input.ContatoTelefone,
                    ContatoEmail = input.ContatoEmail
                };

                // To do: criar service com lógica de identificação do DDD da região a partir do telefone recebido

                _contatoService.Cadastrar(contato);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPatch, Route("[controller]")]
        public IActionResult Update([FromBody] ContatoInput input)
        {
            try
            {
                var contato = new Contato()
                {
                    Id = input.Id,
                    ContatoNome = input.ContatoNome,
                    ContatoTelefone = input.ContatoTelefone,
                    ContatoEmail = input.ContatoEmail
                };


                _contatoService.Alterar(contato);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet, Route("[controller]")]
        public IActionResult Get()
        {
            try
            {
                
                return Ok(_contatoService.ObterTodos());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet, Route("[controller]/{Id}")]
        public IActionResult GetOne( int Id)
        {
            try
            {

                return Ok(_contatoService.ObterPorId(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete, Route("[controller]/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                _contatoService.Deletar(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpDelete, Route("[controller]/regions/{Id}")]
        public IActionResult ObterPorRegiao(int Id)
        {
            try
            {
                return Ok(_contatoService.ObterPorRegião(Id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
