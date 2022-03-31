using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Console;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static int id = 1;
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost] 
        public IActionResult AdicionaFilme([FromBody] Filme filme) 
        { // Boa prática é retornar o caminho para ao recurso que está criando
            filme.Id = id ++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new {Id = filme.Id}, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes()
        { //como o usuario n esta requerindo é uma boa prática retornar uma lista vazia
            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        { //problema se por acaso o filme não for encontrado
            var filme =  filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }
    }
}