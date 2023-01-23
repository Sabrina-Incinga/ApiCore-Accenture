﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLibros.Models
{
    [Table("Autor")]
    public class Autor
    {
        [Key]
        public int IdAutor { get; set; }
        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Apellido { get; set; }
        [Range(0, 120, ErrorMessage ="La edad debe estar entre 0 y 120 años")]
        public int? Edad { get; set; }
        public List<Libro> Libros { get; set; }

    }
}
