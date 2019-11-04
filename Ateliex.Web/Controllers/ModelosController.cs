﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ateliex.Models;
using Ateliex.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ateliex.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        private readonly IModelosService modelosService;

        public ModelosController(IModelosService modelosService)
        {
            this.modelosService = modelosService;
        }

        // GET: api/Modelos
        [HttpGet]
        public async Task<Modelo[]> Get()
        {
            var modelos = await modelosService.ObtemModelosAsync();

            return modelos;
        }

        // GET: api/Modelos/5
        [HttpGet("{codigo}", Name = "Get")]
        public string Get(int codigo)
        {
            throw new NotImplementedException();
        }

        // POST: api/Modelos
        [HttpPost]
        public async Task<Modelo> Post(Modelo modelo)
        {
            var resposta = await modelosService.CadastraModeloAsync(modelo);

            return resposta;
        }

        // PUT: api/Modelos/5
        [HttpPut("{codigo}")]
        public void Put(int codigo, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{codigo}")]
        public void Delete(string codigo)
        {
            modelosService.RemoveModeloAsync(codigo);
        }
    }
}
