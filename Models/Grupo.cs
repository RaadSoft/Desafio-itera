using Desafio_itera.Models;
using System;
using System.Collections.Generic;

namespace Desatio_itera.Models
{
    public class Grupo
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string category { get; set;}
        public string date_ingestion { get; set;}
        public List<Empresa> companys { get; set; }
    }
}
