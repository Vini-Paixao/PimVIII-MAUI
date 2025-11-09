using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimVIII.MauiCreator.Models
{
    public class Conteudo
    {
        public int ID { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int CriadorID { get; set; }
    }
}
