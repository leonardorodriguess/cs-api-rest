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

        [HttpPost] //Verbo Post criar novo recurso no sistema
        public void AdicionaFilme([FromBody] Filme filme) //[FromBody] vem do corpo da minha requisição
        {
            filme.Id = id ++;
            filmes.Add(filme);
            WriteLine(filme.Titulo);

        }
        [HttpGet]
        //public List<Filme> RecuperarFilmes()
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return filmes;
        }


    }
}