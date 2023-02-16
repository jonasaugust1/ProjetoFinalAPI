using CityEvents.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Interface
{
    public interface ICityEventRepository
    {
        bool AdicionarEvento(CityEventsEntity evento);

        bool EditarEvento(CityEventsEntity evento, int id);

        bool ExcluirEvento(int id);

        bool InativarEvento(int id);

        List<CityEventsEntity> ConsultaPorTitulo(string titulo);

        List<CityEventsEntity> ConsultaPorLocalEData(string local, DateTime data);

        List<CityEventsEntity> ConsultaPorPrecoEData(double precoMin, double precoMax, DateTime data);
    }
}