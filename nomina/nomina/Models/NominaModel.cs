using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina.Models
{
    public class NominaModel
    {
        public string Id { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjMes))]
        public DateTime Mes { get; set; }

    }
}