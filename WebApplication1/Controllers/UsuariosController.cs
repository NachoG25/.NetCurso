using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.UI.WebControls;

namespace WebApplication1.Controllers
{
    public class UsuariosController : Controller
    {
        string connectionString = @"Data Source=NACHOGRIGNOLA;Initial Catalog = nueva; Integrated Security = True; MultipleActiveResultSets=True;Application Name = EntityFramework";

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Usuarios/Create/5
        [HttpPost]
        public ActionResult Create(usuarios usuario)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {

                    sqlconn.Open();
                    string query = "insert into usuarios(nombre,apellido,nameuser,pw) values(@nombre,@apellido,@nombre_usuario,@contrasenia)";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlconn);
                    sqlcmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = usuario.nombre;
                    sqlcmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = usuario.apellido;
                    sqlcmd.Parameters.Add("@nombre_usuario", SqlDbType.VarChar).Value = usuario.nameuser;
                    sqlcmd.Parameters.Add("@contrasenia", SqlDbType.VarChar).Value = usuario.pw;
                    sqlcmd.ExecuteNonQuery();
                    return RedirectToAction("../");
                }

                catch (SqlException ex)
                {
                    Random rnd = new Random();
                    int num = rnd.Next(0, 101);

                        TempData["Mensaje"] = "Usuario Ya Existe, pruebe con" + " "+ "|"+ " " + usuario.nameuser + num + " "+ "|"+ " " + usuario.nombre + num + " "+ "|"+ " " + usuario.apellido + num + " ";
                    //return RedirectToAction("Create", "Usuarios");
                    return View();             
                }
 
            }
        }

// GET: Usuarios/Edit/5
public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
