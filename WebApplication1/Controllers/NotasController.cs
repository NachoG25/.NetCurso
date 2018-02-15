using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class NotasController : Controller
    {
        string connectionString = @"Data Source=NACHOGRIGNOLA;Initial Catalog = nueva; Integrated Security = True; MultipleActiveResultSets=True;Application Name = EntityFramework";

        // GET: Notas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Notas/Details/5
        public ActionResult Mostrar(int id)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                sqlconn.Open();
                string query = "select * from notas where idnotas=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                    {
                        reader.Read();
                        notas n = new notas();

                        for (int inc = 0; inc < reader.FieldCount; inc++)
                        {
                            Type type = n.GetType();
                            PropertyInfo prop = type.GetProperty(reader.GetName(inc));
                            prop.SetValue(n, reader.GetValue(inc), null);

                        }
                        return View(n);
                    }
                return View();
            }
        }

        // GET: Notas/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Notas/Create
        [HttpPost]
        public ActionResult Create(notas nota)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        [HttpGet]
        public ActionResult IndexNotas()
        {
            var listaNotas = new List<notas>();
            if (Session["usuario"] != null)
            {
                usuarios u = (usuarios)Session["usuario"];

                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();
                    string query = "select * from notas where notas_usuario_fk=" + u.idusuarios;
                    SqlCommand cmd = new SqlCommand(query, sqlconn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                notas n = new notas();

                                for (int inc = 0; inc < reader.FieldCount; inc++)
                                {
                                    Type type = n.GetType();
                                    PropertyInfo prop = type.GetProperty(reader.GetName(inc));
                                    prop.SetValue(n, reader.GetValue(inc), null);

                                }
                                listaNotas.Add(n);
                            }
                            return View(listaNotas);
                        }
                }
            }
            return View(listaNotas);
        }

        [HttpPost]
        public ActionResult crearNotas(notas nota)
        {

            if (Session["usuario"] != null)
            {
                usuarios usuario = (usuarios)Session["usuario"];
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    using (SqlConnection sqlconn = new SqlConnection(connectionString))
                    {
                        {
                            sqlconn.Open();
                            string query = "insert into notas(nombre,texto,notas_usuario_fk) values(@nombre,@texto,@idusuario)";
                            SqlCommand sqlcmd = new SqlCommand(query, sqlconn);
                            sqlcmd.Parameters.AddWithValue("@nombre", nota.nombre);
                            sqlcmd.Parameters.AddWithValue("@texto", nota.texto);
                            sqlcmd.Parameters.AddWithValue("@idusuario", usuario.idusuarios);
                            sqlcmd.ExecuteNonQuery();
                            return RedirectToAction("IndexNotas", "Notas");
                        }
                    }
                }
            }
            return View();
        }


        // GET: Notas/Edit/5
        public ActionResult Editar(int id)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                sqlconn.Open();
                string query = "select * from notas where idnotas=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                    {
                        reader.Read();
                        notas n = new notas();

                        for (int inc = 0; inc < reader.FieldCount; inc++)
                        {
                            Type type = n.GetType();
                            PropertyInfo prop = type.GetProperty(reader.GetName(inc));
                            prop.SetValue(n, reader.GetValue(inc), null);

                        }
                        return View(n);
                    }
                return View();
            }
        }

        // POST: Notas/Edit/5
        [HttpPost]
        public ActionResult Editar(int id, notas nota)
        {
            try
            {
                if (Session["usuario"] != null)
                {
                    usuarios usuario = (usuarios)Session["usuario"];
                    if (!ModelState.IsValid)
                    {
                        return View();
                    }
                    else
                    {
                        using (SqlConnection sqlconn = new SqlConnection(connectionString))
                        {
                            {
                                sqlconn.Open();
                                string query = "update notas SET nombre=@nombre, texto=@texto, notas_usuario_fk=@idusuario where idnotas=" + id;
                                SqlCommand sqlcmd = new SqlCommand(query, sqlconn);
                                sqlcmd.Parameters.AddWithValue("@idnota", id);
                                sqlcmd.Parameters.AddWithValue("@nombre", nota.nombre);
                                sqlcmd.Parameters.AddWithValue("@texto", nota.texto);
                                sqlcmd.Parameters.AddWithValue("@idusuario", usuario.idusuarios);
                                sqlcmd.ExecuteNonQuery();
                                return RedirectToAction("IndexNotas", "Notas");
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notas/Delete/5
        public ActionResult Delete(int id)
        {
            
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                sqlconn.Open();
                string query = "delete notas where idnotas=" + id;
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                cmd.ExecuteNonQuery();
                return RedirectToAction("IndexNotas", "Notas");
            }
        }

        // POST: Notas/Delete/5
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

        [HttpGet]
        public ActionResult Volver()
        {
            return RedirectToAction("IndexNotas");
        }


    }
    }

    
