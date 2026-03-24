using ProjetoDBZ.Models;

namespace ProjetoDBZ.Interfaces
{
    public interface IPersonagemService
    {
        Task<IEnumerable<Personagem>> ListarTodos();
        Task<Personagem> Adicionar(Personagem personagem);
    }
}