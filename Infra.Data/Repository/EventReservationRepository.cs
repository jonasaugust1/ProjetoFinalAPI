using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly string _stringConnection;
        EventReservationRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }
        public void ConsultaReserva(string nome, string tituloEvento)
        {
            string query = "SELECT * FROM eventReservation INNER JOIN CityEvent ON CityEvent.IdEvent = EventReservation.IdEvent WHERE PersonName = @nome AND Title LIKE @%tituloEvento%;";
        }

        public bool DeletaReserva(int id)
        {
            string query = "DELETE FROM EventReservation where id = @id";
            DynamicParameters parametro = new(id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametro);
            return linhasAfetadas > 0;
        }

        public bool EditarQuantidadeReserva(int id, int quantidade)
        {
            string query = "UPDATE EventReservation SET Quantity = @quantidade where idReservation = @id";
            DynamicParameters parametro = new(id);
            parametro.Add("quantidade", quantidade);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametro);
            return linhasAfetadas > 0;
        }

        public bool AdicionarReserva(EventReservationEntity reserva, int idEvento)
        {
            string query = "INSERT INTO EventReservation (IdEvent,PersonName,Quantity) VALUES (@IdEvent,@PersonName,@Quantity)";
            DynamicParameters parametro = new(reserva);
            parametro.Add("IdEvent", idEvento);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametro);
            return linhasAfetadas > 0;
        }
    }
}