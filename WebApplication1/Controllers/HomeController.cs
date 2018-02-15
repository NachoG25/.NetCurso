using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        string connstring = @"Data Source=NACHOGRIGNOLA;Initial Catalog = nueva; Integrated Security = True; MultipleActiveResultSets=True;Application Name = EntityFramework";

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpGet]
        public ActionResult Login(usuarios usuario)
        {
            using (SqlConnection sqlcon = new SqlConnection(connstring))
            {
                try
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM usuarios WHERE nameuser=@user AND pw=@pass", sqlcon);                  
                    cmd.Parameters.AddWithValue("@user", usuario.nameuser);
                    cmd.Parameters.AddWithValue("@pass", usuario.pw);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            usuarios u = new usuarios((int)reader["idusuarios"], (string)reader["nombre"], (string)reader["apellido"], (string)reader["nameuser"], (string)reader["pw"]);
                            Session["usuario"] = u;
                            return RedirectToAction("IndexNotas", "Notas");
                        }
                    }
                    else
                    {
                        TempData["MensajeLogin"] = "Usuario o Contraseña incorrectos";
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch (SqlException ex)
                {
                        if (( usuario.nameuser == null) & (usuario.pw == null))
                        {
                            TempData["MensajeLogin"] = "Ingrese un nombre de usuario y una contraseña";
                            return RedirectToAction("Index","Home");
                        }
                        else if(usuario.nameuser == null)
                        {
                        TempData["MensajeLogin"] = "Ingrese un nombre de usuario";
                        return RedirectToAction("Index", "Home");
                        }
                        else {
                        TempData["MensajeLogin"] = "Ingrese una contraseña";
                        return RedirectToAction("Index", "Home");
                        }
                   
                }
                        
            }
        }
    }
}
