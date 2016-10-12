using System.Collections.Generic;

namespace PsychologyTest.Models
{
    public class Institucion
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string SitioWeb { get; set; }
        public ICollection<Grupo> Grupos { get; set; }
    }
}
