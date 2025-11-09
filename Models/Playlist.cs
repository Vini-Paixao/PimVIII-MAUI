using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PimVIII.MauiCreator.Models
{
    public class Playlist
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int UsuarioID { get; set; }
    }
}
