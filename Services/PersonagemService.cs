using Microsoft.EntityFrameworkCore;
using ProjetoDBZ.Data;
using ProjetoDBZ.Interfaces;
using ProjetoDBZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDBZ.Services
{
    public class PersonagemService : IPersonagemService
    {
        private readonly AppDbContext _context;

        public PersonagemService(AppDbContext context)
        {
            _context = context;
        }

        // AGORA O LISTAR TODOS ESTÁ AQUI DE VERDADE:
        public async Task<IEnumerable<Personagem>> ListarTodos()
        {
            return await _context.DBZ.ToListAsync();
        }

        public async Task<Personagem> Atualizar(int id, Personagem personagem)
        {
            var guerreiroNoBanco = await _context.DBZ.FindAsync(id);

            if (guerreiroNoBanco == null)
                throw new Exception("Guerreiro não encontrado!");

            guerreiroNoBanco.Nome = personagem.Nome;
            guerreiroNoBanco.PoderBase = personagem.PoderBase;
            guerreiroNoBanco.FotoUrl = personagem.FotoUrl;
            guerreiroNoBanco.Tipo = personagem.Tipo;

            await _context.SaveChangesAsync();
            return guerreiroNoBanco;
        }

        public async Task<Personagem> Adicionar(Personagem personagem) 
        { 
            var existeNoBanco = await _context.DBZ
                .AnyAsync(p => p.Nome.ToLower() == personagem.Nome.ToLower());

            if (existeNoBanco)
            {
                throw new Exception("Um personagem com este nome já está cadastrado.");
            }

            _context.DBZ.Add(personagem);
            await _context.SaveChangesAsync();
            return personagem;
        }
    }
}