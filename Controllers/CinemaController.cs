using System;
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
    public class CinemaController : ControllerBase
    {
        private CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }


        [HttpPost]
        public IActionResult AdicionaCinema ([FromBody] CreateCinemaDto cinemaDto)
        {
            ReadCinemaDto readCinema = _cinemaService.AdicionaCinema(cinemaDto);
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new {Id = readCinema.Id}, readCinema);
        }

        [HttpGet]
        public  IActionResult  RecuperaCinema([FromQuery] string nomeDoFilme)
        {
            List<ReadCinemaDto> readCinema = _cinemaService.RecuperaCinema(nomeDoFilme);
            if (readCinema == null) return NotFound();
            return Ok(readCinema);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            ReadCinemaDto readCinema = _cinemaService.RecuperaCinemaPorId(id);
            if(readCinema == null) return NotFound();
            return Ok(readCinema);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Result resultado = _cinemaService.AtualizaCinema(id, cinemaDto);
            if(resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Result resultado = _cinemaService.DeletaCinema(id);
            if(resultado.IsFailed) return NotFound();
            return NoContent();
        }
    }
}