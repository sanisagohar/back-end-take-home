using CsvHelper;
using CsvHelper.Configuration;
using SearchEngine.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Api.Infrastructure
{
    public class CsvContext
    {
        public List<Airline> Airlines;
        public List<Airport> Airports;
        public List<Route> Routes;
        public string _dataRepositoryPath;

        public CsvContext()
        {
            Airlines = new List<Airline>();
            Airports = new List<Airport>();
            Routes = new List<Route>();

            _dataRepositoryPath = System.Configuration.ConfigurationManager.AppSettings["DataRepositoryPath"];
            LoadData();
        }

        private void LoadData()
        {
            Airlines = GetCollection<Airline, AirlineMap>(Path.Combine(_dataRepositoryPath, "airlines.csv"));
            Airports = GetCollection<Airport, AirportMap>(Path.Combine(_dataRepositoryPath, "airports.csv"));
            Routes = GetCollection<Route, RouteMap>(Path.Combine(_dataRepositoryPath, "routes.csv"));
        }

        private List<T> GetCollection<T,TMap>(string entityPath) where TMap: ClassMap<T>
        {
            List<T> list = new List<T>();
            using (var reader = new StreamReader(entityPath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<TMap>();
                list = csv.GetRecords<T>().ToList();
            }

            return list;
        }
    }
}
