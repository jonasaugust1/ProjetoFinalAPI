using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface IEventReservationRepository
    {
        bool AdicionarReserva(EventReservationEntity reserva, long idEvento);

        bool EditarQuantidadeReserva(long id, int quantidade);

        bool DeletaReserva(long id);

        bool ConsultaReserva(string nome, string tituloEvento);
    }
}