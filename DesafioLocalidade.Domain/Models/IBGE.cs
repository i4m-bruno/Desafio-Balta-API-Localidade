using System.ComponentModel.DataAnnotations;

namespace DesafioLocalidade.Domain.Models
{
    public class IBGE
    {
        public IBGE(string id, string state, string city)
        {
            Id = id;
            State = state;
            City = city;
        }

        [Key]
        [MaxLength(7)]
        public string Id { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(80)]
        public string City { get; set; }
    }
}
