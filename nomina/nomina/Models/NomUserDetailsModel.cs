using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina.Models
{
    public class NomUserDetailsModel : NomUserViewModel
    {

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjPrestamo))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal DedPrestamo { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjUtilizadoTar))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal DedGas { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjTotalPercepciones))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal TotalPercep { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjTotalDed))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal TotalDeduc { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjDepositado))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Depositado { get; set; }


    }
}