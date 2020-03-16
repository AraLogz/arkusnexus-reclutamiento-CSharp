using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nomina.Models
{
    public class NomUserViewModel : UserModel
    {

        public string IdNom { get; set; }

        public string IdUser { get; set; }

    }
}