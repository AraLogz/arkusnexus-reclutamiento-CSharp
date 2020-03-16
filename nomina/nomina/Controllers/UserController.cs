using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nomina.Models;

namespace nomina.Controllers
{
    public class UserController : Controller
    {

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLogInModel user)
        {
            if (ModelState.IsValid)
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    Encrypt pass = new Encrypt();
                    var encPass = pass.ComputeSha256Hash(user.Password);

                    var db_user = db.User.FirstOrDefault(u => u.Password == encPass && u.Email == user.Email);
                    if (db_user != null)
                    {
                        Session["UserID"] = db_user.Id.ToString();
                        Session["Role"] = db_user.Role.ToString();
                        //check if user is admin or final
                        //0 for admin, 1 for final user
                        if (db_user.Role == 0)
                        {
                            return RedirectToAction("UserAdminModule");
                        }
                        else
                        {
                            return RedirectToAction("UserFinalModule", "Nomina", new { id = db_user.Id });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Password", Resources.Strings.LogInError);
                    }
                }
            }
            return View();
        }

        public ActionResult UserAdminModule()
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "0")
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {

                    var users = db.User.ToList();
                    List<UserModel> uModel = new List<UserModel>();
                    foreach (var user in users)
                    {
                        uModel.Add(new UserModel
                        {
                            Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM),
                            Id = user.Id
                        }) ;
                    }
                    return View(uModel);
                }
            }
            return View("LogIn");
        }

        public ActionResult Details(string id)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "0")
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    var user = db.User.FirstOrDefault(u => u.Id == id);

                    if (user != null)
                    {
                        UserModel uModel = new UserModel
                        {
                            Email = user.Email,
                            Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM),
                            Role = ((userRoles)user.Role).ToString(),
                            IngresoBase = (int)user.IngresoBase,
                            DedDesayuno = (int)user.DedDesayuno,
                            DedAhorro = (int)user.DedAhorro,
                            Status =((userStatus)user.Status).ToString()
                        };
                        return View(uModel);
                    }

                    return RedirectToAction("UserAdminModule", "User");
                }
            }
            return View("Login");
        }

        public ActionResult Create()
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "0")
            {
                ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                return View();
            }
            return View("LogIn");
        }

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            try
            {
                if (Session["UserId"] != null && Session["Role"].ToString() == "0")
                {
                    if (ModelState.IsValid)
                    {
                        //Encrypt password
                        Encrypt pass = new Encrypt();
                        var encPass = pass.ComputeSha256Hash(user.Password);

                        //Get role id
                        var rId = (int)Enum.Parse(typeof(userRoles), user.Role);

                        //Get status id
                        var sId = (int)Enum.Parse(typeof(userStatus), user.Status);

                        //Generate user id
                        string uId = Guid.NewGuid().ToString();

                        // TODO: Add insert logic here
                        using (nominaDBEntities db = new nominaDBEntities())
                        {
                            db.User.Add(new User
                            {
                                Id = uId,
                                Nombre = user.Nombre,
                                ApellidoM = user.ApellidoM,
                                ApellidoP = user.ApellidoP,
                                Email = user.Email,
                                Role = rId,
                                Status = sId,
                                Password = encPass,
                                IngresoBase = (int)user.IngresoBase,
                                DedDesayuno = (int)user.DedDesayuno,
                                DedAhorro = (int)user.DedAhorro
                            });
                            db.SaveChanges();
                        }
                        ModelState.Clear();
                        return RedirectToAction("Details", "User", new { id = uId });
                    }
                    ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                    ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                    return View();
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Password", String.Format(Resources.Strings.CreateUserError, ex.InnerException.InnerException.Message));
                ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "0")
            {
                //Add types
                ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    var user = db.User.Where(u => u.Id == id).FirstOrDefault();
                    if (user != null)
                    {
                        //Get role id
                        var rId = ((userRoles)user.Role).ToString();

                        //Get status id
                        var sId = ((userStatus)user.Status).ToString();
                        return View(new UserModel
                        {
                            Nombre = user.Nombre,
                            ApellidoP = user.ApellidoP,
                            ApellidoM = user.ApellidoM,
                            Email = user.Email,
                            Role = rId,
                            Status = sId,
                            IngresoBase = (int)user.IngresoBase,
                            DedDesayuno = (int)user.DedDesayuno,
                            DedAhorro = (int)user.DedAhorro,
                            Password = "",
                            Id = user.Id
                        });
                    }
                    return HttpNotFound();
                }
            }
            return View("Login");
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            try
            {
                if (Session["UserId"] != null && Session["Role"].ToString() == "0")
                {
                    ModelState.Remove("Password");
                    if (ModelState.IsValid)
                    {
                        var modifiedUsername = Session["UserId"];
                        using (nominaDBEntities db = new nominaDBEntities())
                        {
                            //Add created by user
                            var cUser = db.User.FirstOrDefault(u => u.Id == user.Id);

                            if (user.Password != null)
                            {
                                //Encrypt new password
                                Encrypt ePass = new Encrypt();
                                cUser.Password = ePass.ComputeSha256Hash(user.Password);
                            }

                            //Get role and status id
                            var rId = (int)Enum.Parse(typeof(userRoles), user.Role);
                            var sId = (int)Enum.Parse(typeof(userStatus), user.Status);

                            cUser.Role = rId;
                            cUser.Email = user.Email;
                            cUser.Nombre = user.Nombre;
                            cUser.ApellidoM = user.ApellidoM;
                            cUser.ApellidoP = user.ApellidoP;

                            cUser.IngresoBase = Decimal.Parse(user.IngresoBase.ToString(), System.Globalization.NumberStyles.Currency);
                            cUser.DedAhorro = Decimal.Parse(user.DedAhorro.ToString(), System.Globalization.NumberStyles.Currency);
                            cUser.DedDesayuno = Decimal.Parse(user.DedDesayuno.ToString(), System.Globalization.NumberStyles.Currency);

                            db.Entry(cUser).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        ModelState.Clear();
                        return RedirectToAction("Details", "User", new { id = user.Id });
                    }
                    ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                    ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                    return View();
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Password", String.Format(Resources.Strings.CreateUserError, ex.InnerException.InnerException.Message));
                ViewBag.DropDownRole = new SelectList(Enum.GetValues(typeof(userRoles)));
                ViewBag.DropDownStatus = new SelectList(Enum.GetValues(typeof(userStatus)));
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "0")
            {
                using (nominaDBEntities db = new nominaDBEntities())
                {
                    var user = db.User.Where(u => u.Id == id).FirstOrDefault();
                    if (user != null)
                    {
                        ViewBag.DeleteMessage = Resources.Strings.DeleteMessage;
                        return View(new UserModel
                        {
                            Email = user.Email,
                            Nombre = String.Format("{0} {1} {2}", user.Nombre, user.ApellidoP, user.ApellidoM),
                            Role = ((userRoles)user.Role).ToString(),
                            Status = ((userStatus)user.Status).ToString()
                        });
                    }
                    return HttpNotFound();
                }
            }
            return View("Login");
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                if (Session["UserId"] != null && Session["Role"].ToString() == "0")
                {
                    using (nominaDBEntities db = new nominaDBEntities())
                    {
                        var dUser = db.User.FirstOrDefault(u => u.Id == id);
                        if (dUser != null)
                        {
                            db.Entry(dUser).State = EntityState.Deleted;
                            db.SaveChanges();
                            ModelState.Clear();
                            return RedirectToAction("UserAdminModule", "User");
                        }
                        return HttpNotFound();
                    }
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}