using System.Collections.Generic;

namespace PsychologyTest.Models
{
    public class Psicologo
    {
        public int Id { get; set; }
        public PsyTestUser Usuario { get; set; }
        public List<Grupo> Grupos { get; set; }
    }
}