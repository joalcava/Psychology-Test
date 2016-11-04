using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PsychologyTest.Models
{
    public class Institucion
    {
        [Key]
        public int Nit { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string SitioWeb { get; set; }
        public ICollection<Grupo> Grupos { get; set; }
    }
}
