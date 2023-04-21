using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia_DTO
{
    public class Persona_DTO
    {
        public int PersonaId { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public bool Estado { get; set; }
        public string Genero { get; set; }
        public string Celular { get; set; }
        public bool FlagEnviado { get; set; }
        
    }
}
