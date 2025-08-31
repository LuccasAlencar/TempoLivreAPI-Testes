namespace TempoLivreAPI.DTOs
{
    public class UsuarioReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataCadastro { get; set; }
    }

    public class UsuarioCreateDto
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }

    public class UsuarioUpdateDto
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Senha { get; set; }
    }
}