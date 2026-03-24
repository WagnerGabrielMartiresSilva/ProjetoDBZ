using Microsoft.EntityFrameworkCore;
using ProjetoDBZ.Data;
using ProjetoDBZ.Interfaces;
using ProjetoDBZ.Models;

namespace ProjetoDBZ.Services
{
    public class PersonagemService : IPersonagemService
    {
        private readonly AppDbContext _context;

        public PersonagemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Personagem>> ListarTodos()
        {
            return await _context.DBZ.ToListAsync();
        }

        public async Task<Personagem> Adicionar(Personagem personagem)
        {
    // 1. REGRA DE NEGÓCIO: Verificar se o personagem já existe (pelo nome)
    var existeNoBanco = await _context.DBZ
        .AnyAsync(p => p.Nome.ToLower() == personagem.Nome.ToLower());

    if (existeNoBanco)
    {
        // Se já existe, lançamos uma "exceção" (um erro)
        // Isso impede que o C# chegue na linha do banco de dados abaixo.
        throw new Exception("Um personagem com este nome já está cadastrado.");
    }

    // 2. Se não existe, segue o fluxo normal de banco
    _context.DBZ.Add(personagem);
    await _context.SaveChangesAsync();
    return personagem;
}
    }
}