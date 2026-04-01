using ProjetoDBZ.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDBZ.Interfaces
{
    public interface IPersonagemService
    {
        Task<IEnumerable<Personagem>> ListarTodos();
        Task<Personagem> Adicionar(Personagem personagem);
        Task<Personagem> Atualizar(int id, Personagem personagem); // <-- Verifique esta linha
    }
}