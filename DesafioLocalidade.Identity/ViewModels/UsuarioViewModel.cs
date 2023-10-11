using Flunt.Notifications;
using Flunt.Validations;

namespace Desafio_Balta_API_Localidade.ViewModels
{
    public class UsuarioViewModel : Notifiable<Notification>
    {
        public UsuarioViewModel(string email, string senha, string senhaConfirma)
        {
            Email = email;
            Senha = senha;
            SenhaConfirma = senhaConfirma;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirma { get; set; }

        public void Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Email, "Informe o email")
                .IsEmail(Email, "Informe um email válido")
                .IsNotNullOrEmpty(Senha, "Informe a senha")
                .AreEquals(Senha, SenhaConfirma, "Senhas não conferem");

            AddNotifications(contract);
        }
    }
}
