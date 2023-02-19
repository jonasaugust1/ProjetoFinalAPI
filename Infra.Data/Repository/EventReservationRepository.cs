using CityEvents.Service.DTO;
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
        public EventReservationRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }
        public async Task<IEnumerable<EventReservationEntity>> GetReservationByNameAndEventTitle(string name, string eventTitle)
        {
            string query = "SELECT * FROM EventReservation  INNER JOIN CityEvent ON CityEvent.IdEvent = EventReservation.IdEvent WHERE PersonName = @nome  AND Title LIKE @titulo";
            DynamicParameters param = new();
            eventTitle = $"%{eventTitle}%";
            param.Add("nome", name);
            param.Add("titulo", eventTitle);
            using MySqlConnection conn = new(_stringConnection);
            return conn.Query<EventReservationEntity>(query, param).ToList();
        }

        public async Task<bool> DeleteReservation(long id)
        {
            string query = "DELETE FROM EventReservation where id = @id";
            DynamicParameters param = new(id);
            using MySqlConnection conn = new(_stringConnection);
            int rowsAffected = await conn.ExecuteAsync(query, param);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateReservationAmount(long id, int quantity)
        {
            string query = "UPDATE EventReservation SET Quantity = @quantidade where idReservation = @id";
            DynamicParameters param = new(id);
            param.Add("quantidade", quantity);
            using MySqlConnection conn = new(_stringConnection);
            int rowsAffected = await conn.ExecuteAsync(query, param);
            return rowsAffected > 0;
        }

        public async Task<bool> AddReservation(EventReservationEntity reservation)
        {
            string query = "INSERT INTO EventReservation (IdEvent,PersonName,Quantity) VALUES (@IdEvent,@PersonName,@Quantity)";
            DynamicParameters param = new(reservation);
            using MySqlConnection conn = new(_stringConnection);
            int rowsAffected = await conn.ExecuteAsync(query, param);
            return rowsAffected > 0;
        }

        public async Task<bool> ValidateEventStatus(long eventId)
        {
            string query = "SELECT * FROM CityEvent where idEvent = @idEvento";
            DynamicParameters param = new();
            param.Add("idEvento", eventId);
            using MySqlConnection conn = new(_stringConnection);
            var value = await conn.QueryFirstOrDefaultAsync<CityEventDto>(query, param);
            return value.Status;
        }
    }
}