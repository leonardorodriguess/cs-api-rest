using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesApi.Services;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Console;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost] 
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) 
        {
            ReadFilmeDto readDto = _filmeService.AdicionaFilme(filmeDto);
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new {Id = readDto.Id}, readDto);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<ReadFilmeDto> readDto = _filmeService.RecuperaFilmes(classificacaoEtaria);   
            if(readDto == null)
            {
                return NotFound();
            }
            return Ok(readDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            ReadFilmeDto readDto = _filmeService.RecuperaFilmesPorId(id);
            if(readDto == null) return NotFound();
            return Ok(readDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            ReadFilmeDto readFilme = _filmeService.AtualizaFilme(id, filmeDto);
            if(readFilme == null) return NotFound();
            return NoContent();
             
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilme (int id)
        {
            bool delFilme = _filmeService.DeleteFilme(id);
            if(delFilme == false) return NotFound();
            return NoContent();
        }
    }
}