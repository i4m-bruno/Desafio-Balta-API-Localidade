using Flunt.Notifications;
using Flunt.Validations;

namespace DesafioLocalidade.Domain.ViewModels
{
    public class IBGEViewModel : Notifiable<Notification>
    {
        public IBGEViewModel() { }
        public IBGEViewModel(string id, string city, string state)
        { 
            Id = id;
            City = city;
            State = state;
        }
        public string Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public void Validate()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Id, "Id can´t be null or empty")
                .IsNotNullOrEmpty(City, "City can´t be null or empty")
                .IsNotNullOrEmpty(State, "State can´t be null or empty");

            AddNotifications(contract);
        }

    }
}
