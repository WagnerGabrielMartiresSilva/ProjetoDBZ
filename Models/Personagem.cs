
namespace ProjetoDBZ.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public required string Tipo { get; set; }
    }
}