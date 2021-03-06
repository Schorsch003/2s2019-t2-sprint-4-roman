﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.Roman.WebApi.Domains;
using senai.Roman.WebApi.Repositories;

namespace senai.Roman.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemasController : ControllerBase
    {
        TemaRepository temaRepository = new TemaRepository();

        [Authorize(Roles = "Professor")]
        [HttpGet]
        public IEnumerable<Temas> Listar()
        {
            return temaRepository.Listar();
        }
        [Authorize(Roles = "Professor")]
        [HttpPost]
        public IActionResult Cadastrar(Temas tem)
        {
            try
            {
                temaRepository.CadastrarTema(tem);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult BuscarPorId (int id)
        {
            Temas temas = temaRepository.BuscarPorId(id);

            if (temas == null)
                return null;
            return Ok(temas);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Temas tem, int id)
        {
            Temas Atualizarpro = temaRepository.BuscarPorId(id);
            if (Atualizarpro == null)
                return NotFound();
            tem.IdTema = id;
            temaRepository.Atualizar(tem);
            return Ok();

        }

    }
}