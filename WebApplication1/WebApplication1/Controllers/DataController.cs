using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class DataController : Controller
    {

        //URLS DE USUARIOS
        //string url = "http://testingcac.azurewebsites.net/api/services/v1/user/";
        string urlUser = "http://192.168.160.98:10090/api/services/v1/user/";
        

        // GET: Data
        public ActionResult Index()
        {
            return Content("Chugar");
        }

        [HttpGet]
        public ActionResult getUsuariosList()
        {

            var response = "";
            try
            {

                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create(urlUser + "list");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        [HttpPost]
        public ActionResult crearUsuario()
        {
            string json = Request["datos"];
            var response = "";
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(urlUser + "add");

                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";

                /*JsonSerializer serializer = new JsonSerializer();

                StringReader sr = new StringReader(json);
                Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(sr);

                JsonRequest jsonRequest = (JsonRequest)serializer.Deserialize(reader, typeof(JsonRequest));

                //do work with object*/

                using (var streamWriter = new StreamWriter(myRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }


        [HttpGet]
        public ActionResult getRolesList()
        {

            var response = "";
            try
            {
                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create(urlUser + "listrole");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        [HttpGet]
        public ActionResult getEmpresasList()
        {

            var response = "";
            try
            {
                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create(urlUser + "listorganization");

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Content(response);
        }

        //ESTE METODO PIDE LA LISTA DE USUARIOS TAMBIEN, PERO NO LO ELIMINO HASTA SABER SI AGUIRRE LO UTILIZA
        [HttpGet]
        public ActionResult getList()
        {
            
            var response = "";
            try
            {
                string url = "http://testingcac.azurewebsites.net/api/services/v1/user/list";
                HttpWebRequest myRequest =
                    (HttpWebRequest)WebRequest.Create(url);

                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse webresponse = (HttpWebResponse)myRequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e) {
                throw e;
            }

            return Content(response);
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file = null)
        {
            var f = file;
            var response = "";
            try
            {
                // convert xmlstring to byte using ascii encoding
                byte[] binario;
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    binario = ms.ToArray();
                }
                byte[] data = binario;
                // declare httpwebrequet wrt url defined above
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://testingcac.azurewebsites.net/api/services/v1/fake/validator2");
                //HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("http://192.168.160.98:10090/api/services/v1/file/validator2");
                // set method as post
                webrequest.Method = "POST";
                // set content type
                webrequest.ContentType = "application/x-www-form-urlenconded";
                // set content length
                webrequest.ContentLength = data.Length;
                webrequest.Timeout = 300000;
                // get stream data out of webrequest object
                Stream newStream = webrequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                // declare & read response from service
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                using (var streamReader = new StreamReader(webresponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();

                }
            }
            catch (Exception ex)
            {
                return Json( ex.ToString() );
            }
            return Json(response);
        }
        
    }
}
