using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsychologyTest.Models
{
    public class PruebaPsicologica
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaModificado { get; set; }
        public bool Activo { get; set; }
    }

    public class Pregunta
    {
        public int Id { get; set; }
        public PruebaPsicologica Prueba { get; set; }
        public TipoPregunta Tipo { get; set; }
        public string Texto { get; set; }
        public int Orden { get; set; }
        public string RespuestaEsperada { get; set; }
        public DateTime FechaModificado { get; set; }
        public List<OpcionMultiple> Opciones { get; set; }
    }

    public class TipoPregunta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool OpcionMultiple { get; set; }
    }

    public class OpcionMultiple
    {
        public int Id { get; set; }
        public Pregunta PreguntaOrigen { get; set; }
        public string Texto { get; set; }
        public int Valor { get; set; }
    }

    public class Respuesta
    {
        public int Id { get; set; }
        public PruebaAsignada Prueba { get; set; }
        public Pregunta PreguntaOrigen { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public string Valor { get; set; }
    }

    public class PruebaAsignada
    {
        public int Id { get; set; }
        public Estudiante Encuestado { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public bool Iniciado { get; set; }
        public bool Completado { get; set; }
        public string Diagnostico { get; set; }
    }
}
