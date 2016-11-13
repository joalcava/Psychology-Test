using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;

namespace PsychologyTest.Models
{
    public class Institucion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Nit { get; set; }

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public string SitioWeb { get; set; }
        public ICollection<Grupo> Grupos { get; set; }
    }
}
