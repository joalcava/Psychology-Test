namespace PsychologyTest.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public PsyTestUser Usuario { get; set; }
        public Institucion Institucion { get; set; }
        public Grupo Grupo { get; set; }
    }
}
