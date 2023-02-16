using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private readonly string _stringConnection;
        public CityEventRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }
        public bool AdicionarEvento(CityEventsEntity evento)
        {
            string query = @"INSERT INTO CityEvent(title,description, dateHourEvent, local, address, price,status) 
             VALUES (@title, @description, @dateHourEvent, @local, @address, @price,true)";
            DynamicParameters parametros = new(evento);

            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametros);
            return linhasAfetadas > 0;
        }

        public List<CityEventsEntity> ConsultaPorLocalEData(string local, DateTime data)
        {
            string query = @"SELECT * FROM CityEvent where local = @local and DATE(dateHourEvent) = @data";
            DynamicParameters parametros = new();
            parametros.Add("local", local);
            parametros.Add("data", data);
            using MySqlConnection conn = new(_stringConnection);

            return conn.Query<CityEventsEntity>(query, parametros).ToList();
        }

        public List<CityEventsEntity> ConsultaPorPrecoEData(double precoMin, double precoMax, DateTime data)
        {
            string query = "SELECT * FROM CityEvent where DATE(dateHourEvent) = @data and price between @precoMin and @precoMax";
            DynamicParameters parametros = new();
            parametros.Add("data", data);
            parametros.Add("precoMin", precoMin);
            parametros.Add("precoMax", precoMax);
            using MySqlConnection conn = new(_stringConnection);
            return conn.Query<CityEventsEntity>(query, parametros).ToList();
        }

        public List<CityEventsEntity> ConsultaPorTitulo(string titulo)
        {
            string query = "SELECT * FROM CityEvent where title like @titulo";
            titulo = $"%{titulo}%";
            DynamicParameters parametros = new();
            parametros.Add("titulo", titulo);
            using MySqlConnection conn = new(_stringConnection);
            return conn.Query<CityEventsEntity>(query, parametros).ToList();
        }

        public bool EditarEvento(CityEventsEntity evento, int id)
        {
            string query = "UPDATE CityEvent set title=@title,description=@description, " +
                "dateHourEvent=@dateHourEvent, local=@local, address=@address, price=@price where idEvent=@id";
            DynamicParameters parametros = new(evento);
            parametros.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametros);
            return linhasAfetadas > 0;
        }

        public bool ExcluirEvento(int id)
        {
            string query = "DELETE FROM CityEvent where idEvent = @id";
            DynamicParameters parametros = new();
            parametros.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametros);
            return linhasAfetadas > 0;
        }

        public bool InativarEvento(int id)
        {
            string query = "UPDATE CityEvent set Status = false";
            DynamicParameters parametros = new();
            parametros.Add("id", id);
            using MySqlConnection conn = new(_stringConnection);
            int linhasAfetadas = conn.Execute(query, parametros);
            return linhasAfetadas > 0;
        }

        //public bool ExcluirInativarEvento(int id)
        //{
        //    string queryExclusao = "DELETE FROM CityEvent where idEvent = @id";
        //    string queryEdicao = "UPDATE CityEvent set Status = false";
        //    DynamicParameters parametros = new();
        //    parametros.Add("id", id);
        //    using MySqlConnection conn = new(_stringConnection);
        //    int linhasAfetadas = conn.Execute(query, parametros);
        //    return linhasAfetadas > 0;
        //}
    }
}