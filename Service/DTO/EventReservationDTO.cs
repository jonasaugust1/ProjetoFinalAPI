using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.DTO
{
    public class EventReservationDto
    {
        [Required(ErrorMessage = "Id do evento é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Precisa informar um não nulo válido")]
        public int IdEvent { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "O nome para registro da reserva é obrigatório")]
        [MaxLength(100)]
        public string PersonName { get; set; }
        [Required(ErrorMessage = "A quantidade de reservas é obrigatória")]
        [Range(1, 10, ErrorMessage = "A quantidade deve estar entre 1 e 10")]
        public int Quantity { get; set; }
    }
}