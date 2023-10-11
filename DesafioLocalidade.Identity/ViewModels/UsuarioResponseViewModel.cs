namespace Desafio_Balta_API_Localidade.ViewModels
{
    public class UsuarioResponseViewModel
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }

        public UsuarioResponseViewModel() =>
            Erros = new List<string>();

        public UsuarioResponseViewModel(bool sucesso = true) : this() => Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) => Erros.AddRange(erros);
    }
}
