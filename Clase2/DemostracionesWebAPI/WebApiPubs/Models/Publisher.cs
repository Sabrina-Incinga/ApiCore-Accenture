using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebApiPubs.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Employee = new HashSet<Employee>();
            Titles = new HashSet<Titles>();
        }
        [RegularExpression(@"^1756$|^1622$|^0877$|^0736$|^1389$|^[9][9][0-9][0-9]$", ErrorMessage = "PubId debe contener 4 caracteres, comenzando con 99 y los dos últimos caracteres entre 0 y 9 cada uno")]
        public string PubId { get; set; }
        public string PubName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual PubInfo PubInfo { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Titles> Titles { get; set; }
    }
}
