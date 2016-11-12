using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsychologyTest.Models
{
    // TODO: Agregar boton de desactivar/activar en la vista.
    public class PruebaPsicologica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public DateTime FechaCreado { get; set; }
        public DateTime FechaModificado { get; set; }
        public bool Activo { get; set; }
    }

    public abstract class Pregunta
    {
        public int Id { get; set; }
        public PruebaPsicologica Prueba { get; set; }
        public string Descripcion { get; set; }
        public string Anotaciones { get; set; }
        public int Posicion { get; set; }
    }

    public class PreguntaAbierta : Pregunta
    {
        public bool Larga { get; set; }
    }

    public class PreguntaDeOpcionMultiple : Pregunta
    {
        public List<Opcion> Opciones { get; set; }
        
        // Indica si el encuestado puede seleccionar mas de una opcion para su respuesta
        public bool MultiplesRespuestas { get; set; }
        
        // Se usa para indicar que la respuesta esta consitituida por por la opcion
        // seleccionada y un valor de verdad asignado a esa opcion.
        public bool RespuestaConValorDeVerdad{ get; set; }
    }

    public class Opcion
    {
        public string Texto {get; set;}
        
        // Es un identificador de posicion 
        public int Valor {get; set;}
    }

    public class PruebaPsicologicaAsignada
    {
        public int Id { get; set; }
        public PruebaPsicologica PruebaAsignada { get; set; }
        public Estudiante Encuestado { get; set; }
        public List<Respuesta> Respuestas { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public bool Iniciado { get; set; }
        public bool Completado { get; set; }
        public string Diagnostico { get; set; }
    }

    public abstract class Respuesta
    {
        public int Id { get; set; }
        public Pregunta Pregunta { get; set; }
        public DateTime FechaRespondida { get; set; }
    }

    public class RespuestaAbierta : Respuesta
    {
        public string TextoRespuesta { get; set; } 
    }

    public class RespuestaCerrada : Respuesta 
    {
        public int OpcionRespuesta { get; set; }
    }

    public class MultiplesRespuestas : Respuesta
    {
        public List<int> Respuestas { get; set; }
    }

    public class MultiplesRespuestasConValor : Respuesta
    {
        public List<OpcionConValorDeVerdad> Respuestas { get; set; }
    }

    public class OpcionConValorDeVerdad{
        public int Id { get; set; }
        public int Opcion { get; set; }
        public int Valor { get; set; }
    }

}
