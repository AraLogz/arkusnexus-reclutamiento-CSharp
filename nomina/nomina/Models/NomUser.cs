//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nomina.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NomUser
    {
        public string IdNom { get; set; }
        public string IdUser { get; set; }
        public decimal DedPrestamo { get; set; }
        public decimal DedGas { get; set; }
    
        public virtual Nomina Nomina { get; set; }
        public virtual User User { get; set; }
    }
}
