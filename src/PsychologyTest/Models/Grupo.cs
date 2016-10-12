using System.Collections.Generic;

namespace PsychologyTest.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Grado { get; set; }
        public string Jornada { get; set; }
        public Institucion Institucion { get; set; }
    }
}