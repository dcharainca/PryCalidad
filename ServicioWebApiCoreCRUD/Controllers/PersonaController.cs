using AngleSharp;
using Canvia_DTO;
using Canvia_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ServicioWebApiCoreCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        string urlPlantilla = "https://web.whatsapp.com/send/?phone=@NUMEROWHATSPP@&text=@TEXTO@";
        //string perfilUsuarioChrome = @"C:\Users\programador1\AppData\Local\Google\Chrome\User Data\";
        string perfilUsuarioChrome = String.Empty;
        IWebDriver objRemote;
        private readonly IPersonaServices _IPersonaServices;
        public PersonaController(IPersonaServices IPersonaServices)
        {
            this._IPersonaServices = IPersonaServices;
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            perfilUsuarioChrome = config.GetValue<string>("perfilUsuarioChrome");
            urlPlantilla = config.GetValue<string>("plantillaMensaje");
        }
        [HttpPost]
        [Route("InsertarPersona")]
        public IActionResult Insertar([FromBody] Persona_DTO BE)
        {
            object result;
            try
            {
                result = new { code = 101, message = "hubo un problema para registrar" };
                var res = _IPersonaServices.Insertar(BE);
                if (res != null)
                {
                    result = new { code = 200, message = "Se registro correctamente", result = true, obj = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("ListarTodo")]
        public IActionResult ListarTodo()
        {
            object result;
            try
            {
                result = new { code = 101, message = "Success, id not found" };

                var res = _IPersonaServices.Listar();
                if (res != null)
                {
                    result = new { code = 200, message = "Success", result = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("ListarById")]
        public IActionResult ListarById(int intPersonaId)
        {
            object result;
            try
            {
                result = new { code = 101, message = "Success, id not found" };
                Persona_DTO BE = new Persona_DTO();
                var res = _IPersonaServices.ListarById(intPersonaId);
                if (res != null)
                {
                    result = new { code = 200, message = "Success", result = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("Elimminar")]
        public IActionResult Eliminar(int intPersonaId)
        {
            object result;
            try
            {
                result = new { code = 101, message = "Success, id not found" };
                Persona_DTO BE = new Persona_DTO();
                var res = _IPersonaServices.Eliminar(intPersonaId);
                if (res)
                {
                    result = new { code = 200, message = "Success", result = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AnulacionLogica")]
        public IActionResult AnulacionLogica(int intPersonaId)
        {
            object result;
            try
            {
                result = new { code = 101, message = "hubo un problema para anular" };
                Persona_DTO BE = new Persona_DTO();
                var res = _IPersonaServices.AnulacionLogica(intPersonaId);
                if (res != null)
                {
                    result = new { code = 200, message = "Se Anulo correctamente", result = true, obj = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("Actualizar")]
        public IActionResult Actualizar(int intPersonaId, Persona_DTO BE)
        {

            object result;
            try
            {
                result = new { code = 101, message = "hubo un problema para Actualizar" };
                var res = _IPersonaServices.Actualizar(intPersonaId, BE);
                if (res != null)
                {
                    result = new { code = 200, message = "Se Actualizo correctamente", result = true, obj = res };
                }
            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("EnviarMensaje")]
        public IActionResult EnviarMensaje(string celular, string mensaje)
        {

            object result;
            try
            {

                var options = new ChromeOptions();                                                
                options.AddArguments(@"user-data-dir=" + perfilUsuarioChrome);
                options.AddArguments("start-maximized");
                options.AddArguments("disable-infobars");
                options.AddArguments("--no-sandbox");
                options.AddArguments("--disable-setuid-sandbox");
                options.AddArgument("--remote-debugging-port=9222");
                options.AddArguments("--disable-dev-shm-using");
                options.AddArguments("--disable-dev-shm-usage");
                options.AddArguments("--disable-extensions");
                options.AddArguments("--disable-gpu");
                options.PlatformName = "windows";

                string urlWhatsapp = urlPlantilla.Replace("@NUMEROWHATSPP@","+51"+ celular);
                urlWhatsapp = urlWhatsapp.Replace("@TEXTO@", mensaje);

                if (objRemote == null)
                {
                    objRemote = new ChromeDriver(".", options);
                }
                objRemote.Navigate().GoToUrl(urlWhatsapp);
                Thread.Sleep(15000);
                var botonEnviar = objRemote.FindElement(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[2]/button"));
                botonEnviar.Click();
                Thread.Sleep(5000);
                objRemote.Close();
                result = new { code = 200, message = "Ok" };

            }
            catch (Exception ex)
            {
                result = new { code = -100, message = ex.Message };
            }

            return Ok(result);
        }

    }
}
