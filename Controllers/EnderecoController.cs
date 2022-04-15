using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto readEndereco = _enderecoService.AdicionaEndereco(enderecoDto);
            return CreatedAtAction(nameof (RecuperaEnderecoPorId), new {Id = readEndereco.Id}, readEndereco); //Boa pratica colocar o id depois de criar o metodo de retorna por id
        }

        [HttpGet]
        public IActionResult RecuperaEndereco()
        {
            List<ReadEnderecoDto> readEndereco = _enderecoService.RecuperaEndereco();
            return Ok(readEndereco);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            ReadEnderecoDto readEndereco = _enderecoService.RecuperaEnderecoPorId(id);
            if(readEndereco == null) return NotFound();
            return Ok(readEndereco);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Result resultado = _enderecoService.AtualizaEndereco(id, enderecoDto);
            if(resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Result resultado = _enderecoService.DeletaEndereco(id);
            if(resultado.IsFailed) return NotFound();
            return NoContent();
        }
        
    }
}