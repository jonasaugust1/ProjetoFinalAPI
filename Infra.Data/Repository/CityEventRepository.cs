using CityEvents.Service.DTO;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Infra.date.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly string _stringConnection;
        public CityEventRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("dateBASE_CONFIG");
        }
        public async Task<bool> AddEvent(CityEventsEntity evento)
        {
            string query = @"INSERT INTO CityEvent(title,description, dateHourEvent, local, address, price,status) 
             VALUES (@title, @description, @dateHourEvent, @local, @address, @price,true)";
            DynamicParameters parameters = new(evento);

            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parameters);
            return linhasAfetadas > 0;
        }

        public async Task<List<CityEventsEntity>> GetEventByLocalAndDate(string local, DateTime date)
        {
            string query = @"SELECT * FROM CityEvent where local = @local and DATE(dateHourEvent) = @date";
            DynamicParameters parameters = new();
            parameters.Add("local", local);
            parameters.Add("date", date);
            using MySqlConnection conn = new(_stringConnection);
            return (conn.Query<CityEventsEntity>(query, parameters)).ToList();
        }

        public async Task<List<CityEventsEntity>> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            string query = "SELECT * FROsM CityEvent where DATE(dateHourEvent) = @date and price between @minPrice and @maxPrice";
            DynamicParameters parameters = new();
            parameters.Add("date", date);
            parameters.Add("minPrice", minPrice);
            parameters.Add("maxPrice", maxPrice);
            using MySqlConnection conn = new(_stringConnection);
            return (conn.Query<CityEventsEntity>(query, parameters)).ToList();
        }

        public async Task<List<CityEventsEntity>> GetEventByTitle(string titulo)
        {
            string query = "SELECT * FROM CityEvent where title like @titulo";
            titulo = $"%{titulo}%";
            DynamicParameters parameters = new();
            parameters.Add("titulo", titulo);
            using MySqlConnection conn = new(_stringConnection);
            return (conn.Query<CityEventsEntity>(query, parameters)).ToList();
        }

        public async Task<bool> UpdateEvent(CityEventsEntity evento, long id)
        {
            string query = "UPDATE CityEvent set title=@title,description=@description, dateHourEvent=@dateHourEvent, local=@local, address=@address, price=@price where idEvent=@id";
            DynamicParameters parameters = new(evento);
            parameters.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parameters);
            return linhasAfetadas > 0;
        }

        public async Task<bool> DeleteEvent(long id)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parameters);
            return linhasAfetadas > 0;
        }
        public async Task<bool> InativateEvent(long id)
        {
            string query = "UPDATE CityEvent set status = false WHERE IdEvent = @id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parameters);
            return linhasAfetadas > 0;
        }
        public async Task<bool> GetEventsReservation(long idEvento)
        {
            string query = "SELECT * FROM EventReservation  WHERE idEvent = @idEvento";
            DynamicParameters parameters = new();
            parameters.Add("idEvento", idEvento);
            using MySqlConnection conn = new(_stringConnection);
            return conn.QueryFirstOrDefault(query, parameters) == null;
        }
    }
}