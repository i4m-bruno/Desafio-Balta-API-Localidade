using Flunt.Notifications;
using Flunt.Validations;

namespace DesafioLocalidade.Identity.ViewModels
{
    public class UsuarioLoginViewModel : Notifiable<Notification>
    {
        public UsuarioLoginViewModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
        public string Email { get; set; }
        public string Senha { get; set; }

        public void Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Email, "Informe o email")
                .IsEmail(Email, "Informe um email válido")
                .IsNotNullOrEmpty(Senha, "Informe a senha");

            AddNotifications(contract);
        }
    }
}
