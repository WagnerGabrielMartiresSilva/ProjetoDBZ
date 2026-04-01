
namespace ProjetoDBZ.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        public double PoderBase { get; set; }

        public required string FotoUrl { get; set; }

        public required string Tipo { get; set; }
    }
}