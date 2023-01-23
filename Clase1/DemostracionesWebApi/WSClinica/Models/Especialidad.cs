using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WSClinica.Models
{
    [Table("Especialidad")]
    public class Especialidad
    {
        public int Id { get; set; }
        [Column(TypeName ="varchar(50)"), Required]
        public string Nombre { get; set; }
    }
}