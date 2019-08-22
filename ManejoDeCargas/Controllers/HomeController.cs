using Microsoft.AspNetCore.Mvc;
using ManejoDeCargas.Models;
using ManejoDeCargas.Models.DB;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using ManejoDeCargas.Models.Helpers;

namespace ManejoDeCargas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (Security.AllowManejoDeCargas(User.Identity.Name))
            {
                return View();
            }
            else
            {
                string error = "El usuario no tiene permisos para acceder a esta aplicación";
                ViewBag.error = error;
                return View("~/Views/Home/Error.cshtml");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchPesada(int pesadaId)
        {

            if (Security.AllowManejoDeCargas(User.Identity.Name))
            {
                Pesada pesada = new Pesada();
                string error = null;
                string connStr = GestorDevesaDbContext.GetStringConection();
                string storedProcedure = "GD_consultarPesadaPorID";
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(storedProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nroPesada", SqlDbType.Int);
                    cmd.Parameters["@nroPesada"].Value = pesadaId;

                    try
                    {
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            pesada.PesadaId = (int)rdr["PesadaId"];
                            pesada.Estado = (string)rdr["Estado"];
                            pesada.FechaEntrada = (DateTime)rdr["Fecha_Entrada"];
                            pesada.HoraEntrada = (string)rdr["Hora_Entrada"];
                            pesada.FechaSalida = (DateTime)rdr["Fecha_Salida"];
                            pesada.HoraSalida = (string)rdr["Hora_Salida"];
                            pesada.Transporte = (string)rdr["Transporte"];
                            pesada.Patente = (string)rdr["Patente"];
                            pesada.Conductor = (string)rdr["Conductor"];

                        }
                        else
                        {
                            error = "No se encontró ninguna pesada con ese número";
                            ViewBag.error = error;
                            return View("~/Views/Home/Error.cshtml");
                        }


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        error = "Ocurrió un error inesperado";
                        ViewBag.error = error;
                        return View("~/Views/Home/Error.cshtml");
                    }

                }

                return View(pesada);
            }
            else
            {
                string error = "El usuario no tiene permisos para acceder a esta aplicación";
                ViewBag.error = error;
                return View("~/Views/Home/Error.cshtml");
            }



            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store([FromBody] JObject data)
        {
            string connStr = GestorDevesaDbContext.GetStringConection();
            string storedProcedure = null;
            int rdr = 0;
            string estado = data["estado"].ToString();
            int pesadaID = (int)data["pesadaID"];

            if (estado.Equals("BE"))
            {
                storedProcedure = "GD_cerrarPesada";
            }
            else
            {
               storedProcedure = "GD_abrirPesada";
            }

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(storedProcedure, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@pesada", SqlDbType.Int);
                cmd.Parameters["@pesada"].Value = pesadaID;

                try
                {
                    conn.Open();
                    rdr = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }

            return new JsonResult(rdr);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
