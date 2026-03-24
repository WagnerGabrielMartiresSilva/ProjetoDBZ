using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoDBZ.Data;
using ProjetoDBZ.Interfaces;
using ProjetoDBZ.Models;

namespace ProjetoDBZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly IPersonagemService _service;
        public PersonagensController(IPersonagemService service)
{
    _service = service;
}
[HttpGet]
       public async Task<IActionResult> Get()
{
    var lista = await _service.ListarTodos();

    // Se a lista vier nula ou sem nenhum item
    if (lista == null || !lista.Any())
    {
        return NotFound(new { mensagem = "Nenhum guerreiro Z encontrado no banco de dados." });
    }

    return Ok(lista);
}
[HttpPost]
public async Task<IActionResult> Post(Personagem personagem)
        {
            try 
            {
                // Tenta adicionar usando o serviço
                var novo = await _service.Adicionar(personagem);
                return Ok(novo); // Retorna 200 se der certo
            }
            catch (Exception ex)
            {
                // Se o serviço "gritar" (throw Exception), o código cai aqui
                // Retornamos 400 (Bad Request) com a mensagem que escrevemos no Service
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}