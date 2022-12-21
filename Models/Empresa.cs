using Desafio_itera.Models;
using System;
using System.Collections.Generic;

namespace Desafio_itera.Models
{
    public class Empresa
    {
        public string id { get; set; }
        public string status { get; set; }
        public string date_ingestion { get; set;}
        public string last_update { get; set; }
        public List<Custo> custos { get; set; }

    }
}
