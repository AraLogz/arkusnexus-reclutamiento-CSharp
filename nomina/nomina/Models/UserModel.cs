using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina.Models
{
    public class UserModel : UserLogInModel
    {

        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.NombreRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjNombre))]
        public string Nombre { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.ApellidoPRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjApellidoP))]
        public string ApellidoP { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.ApellidoMRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjApellidoM))]
        public string ApellidoM { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.IngresoRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjIngreso))]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:C0}")]
        public decimal IngresoBase { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.DedDesayunoRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjDesayuno))]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:C0}")]
        public decimal DedDesayuno { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.DedAhorroRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjAhorro))]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:C0}")]
        public decimal DedAhorro { get; set; }

        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjStatus))]
        public string Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Strings), ErrorMessageResourceName = nameof(Resources.Strings.RoleRequired))]
        [Display(ResourceType = typeof(Resources.Strings), Name = nameof(Resources.Strings.MsjRole))]
        public string Role { get; set; }

    }
}