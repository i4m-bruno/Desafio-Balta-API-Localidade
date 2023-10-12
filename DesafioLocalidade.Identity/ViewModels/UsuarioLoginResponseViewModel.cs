using System.Text.Json.Serialization;

namespace DesafioLocalidade.Identity.ViewModels
{
    public class UsuarioLoginResponseViewModel
    {
        public bool Sucesso { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; private set; }
        public List<string> Erros { get; private set; }

        public UsuarioLoginResponseViewModel()
            => Erros = new List<string>();

        public UsuarioLoginResponseViewModel(bool sucesso = true) :
            this() => Sucesso = sucesso;

        public UsuarioLoginResponseViewModel(bool sucesso, string token, DateTime? dataExpiracao) : this(sucesso)
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }

        public void AdicionarErro(string erro) => Erros.Add(erro);
        public void AdicionarErros(IEnumerable<string> erros) => Erros.AddRange(erros);
    }
}
