using nomina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nomina.Controllers
{
    public class NominaController : Controller
    {
        // GET: Nomina
        public ActionResult List()
        {
            if (Session["UserId"] != null)
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {

                    var nom = db.Nomina.ToList();
                    List<NominaModel> nModel = new List<NominaModel>();
                    foreach (var n in nom)
                    {
                        nModel.Add(new NominaModel
                        {
                            Mes = n.Mes,
                            Id = n.Id
                        });
                    }
                    return View(nModel);
                }
            }
            return View("LogIn");
        }

        public ActionResult NomUserList(string id)
        {
            if (Session["UserId"] != null)
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    var userid = Session["UserId"].ToString();
                    //Check user role
                    var user = db.User.Where(u => u.Id == userid).FirstOrDefault();
                    if (user.Role == 1)
                    {
                        //For non admin users
                        return RedirectToAction("ConsultarUser", "Nomina", new { IdNom = id, IdUser = Session["UserId"].ToString() });
                    }

                    //Search all nom in selected month by id
                    var nom = db.NomUser.Where(n => n.IdNom == id).ToList();
                    var nomId = db.Nomina.Where(n => n.Id == id).FirstOrDefault();

                    ViewBag.Fecha = nomId.Mes;

                    List<NomUserViewModel> nModel = new List<NomUserViewModel>();
                    foreach (var n in nom)
                    {
                        user = db.User.Where(u => u.Id == n.IdUser).FirstOrDefault();
                        nModel.Add(new NomUserViewModel
                        {
                            IdNom = n.IdNom,
                            IdUser = n.IdUser,
                            Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM),
                        });
                    }
                    return View(nModel);
                }
            }
            return View("LogIn");
        }

        public ActionResult ConsultarUser(string IdNom, string IdUser)
        {
            if (Session["UserId"] != null)
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    //Search all nom in selected month by id
                    var nom = db.NomUser.Where(n => n.IdNom == IdNom && n.IdUser == IdUser).FirstOrDefault();
                    if (nom != null)
                    {
                        var user = db.User.Where(u => u.Id == nom.IdUser).FirstOrDefault();

                        var tPer = user.IngresoBase + nom.DedPrestamo;
                        var tDed = nom.DedGas + user.DedAhorro + user.DedDesayuno;

                        NomUserDetailsModel nModel = new NomUserDetailsModel
                        {
                            IdNom = nom.IdNom,
                            IdUser = nom.IdUser,
                            Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM),
                            DedGas = nom.DedGas,
                            DedPrestamo = nom.DedPrestamo,
                            IngresoBase = user.IngresoBase,
                            DedAhorro = user.DedAhorro,
                            DedDesayuno = user.DedDesayuno,
                            TotalDeduc = tDed,
                            TotalPercep = tPer,
                            Depositado = tPer - tDed
                        };
                        return View(nModel);
                    }
                }
            }
            return View("LogIn");
        }

        public ActionResult UserFinalModule(string id)
        {
            if (Session["UserId"] != null)
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    var user = db.User.Where(u => u.Id == id).FirstOrDefault();
                    ViewBag.Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM);

                    var nomina = (from n in db.Nomina
                                  join nu in db.NomUser
                                  on n.Id equals nu.IdNom
                                  where nu.IdUser == id
                                  select new
                                  {
                                      n.Mes,
                                      n.Id
                                  }).ToList();

                    List<NominaModel> nModel = new List<NominaModel>();
                    foreach (var nom in nomina)
                    {
                        nModel.Add(new NominaModel
                        {
                            Mes = nom.Mes,
                            Id = nom.Id
                        });
                    }
                    return View(nModel);
                }
            }
            return View("LogIn");
        }

    }
}