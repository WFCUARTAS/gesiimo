using AspNetCore.Reporting;
using GESIIMO.Data;
using GESIIMO.Model;
using GESIIMO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GESIIMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        EmployeeService employeeService = new EmployeeService();

        private readonly IReportService reportService;

        List<string> meses = new List<string>() { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };

        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("InformeVotacion/{id}")]
        [Authorize(Roles = "administrador,moderador")]
        public async Task<IActionResult> InformeVotacionAsync(int id, [FromServices] IReportService r, [FromServices] IAsambleaService a, [FromServices] IConjuntoService c)
        {
            DataTable dt = await r.GetInformeVotaciones(id);
            Asamblea asamblea = await a.GetAsamblea(Convert.ToInt32(id));
            Conjunto conjunto = await c.GetConjunto(asamblea.IdConjunto);

            string logo = "default.png";
            if (conjunto.Logo != null)
            {
                logo = conjunto.Logo;
            }

            string mimetype = "";
            int extencion = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\InformeVotaciones.rdlc";

            string imagenParam = "";
            var imagenPath = $"{this._webHostEnvironment.WebRootPath}\\logos\\{logo}";
            using (var b = new Bitmap(imagenPath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imagenParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Descripcion", asamblea.Descripcion);
            parameters.Add("Conjunto",  conjunto.Nombre);
            parameters.Add("Fecha", asamblea.FechaInicio.Day + "-" + meses[asamblea.FechaInicio.Month - 1] + "-" + asamblea.FechaInicio.Year);
            parameters.Add("logo", imagenParam);

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("InfomeVotacion", dt);
            var result = localReport.Execute(RenderType.Pdf, extencion, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("InfomeActualizacionDatos/{id}")]
        [Authorize(Roles = "administrador,moderador")]
        public async Task<IActionResult> InfomeActualizacionDatos(int id, [FromServices] IReportService r, [FromServices] IAsambleaService a, [FromServices] IConjuntoService c)
        {
            DataTable dt = await r.GetInfomeActualizacionDatos(id);
            Asamblea asamblea = await a.GetAsamblea(Convert.ToInt32(id));
            Conjunto conjunto = await c.GetConjunto(asamblea.IdConjunto);

            string logo = "default.png";
            if (conjunto.Logo != null)
            {
                logo = conjunto.Logo;
            }

            string mimetype = "";
            int extencion = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\InfomeActualizacionDatos.rdlc";

            string imagenParam = "";
            var imagenPath = $"{this._webHostEnvironment.WebRootPath}\\logos\\{logo}";
            using (var b = new Bitmap(imagenPath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imagenParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Descripcion", asamblea.Descripcion);
            parameters.Add("Conjunto", conjunto.Nombre);
            parameters.Add("Fecha", asamblea.FechaInicio.Day + "-" + meses[asamblea.FechaInicio.Month - 1] + "-" + asamblea.FechaInicio.Year);
            parameters.Add("logo", imagenParam);

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("ActualizacionDatos", dt);

            var result = localReport.Execute(RenderType.Pdf, extencion, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("InfomePoderes/{id}")]
        [Authorize(Roles = "administrador,moderador")]
        public async Task<IActionResult> InfomePoderes(int id, [FromServices] IReportService r, [FromServices] IAsambleaService a, [FromServices] IConjuntoService c)
        {
            DataTable dt = await r.GetInfomePoderes(id);
            Asamblea asamblea = await a.GetAsamblea(Convert.ToInt32(id));
            Conjunto conjunto = await c.GetConjunto(asamblea.IdConjunto);

            string logo = "default.png";
            if (conjunto.Logo != null)
            {
                logo = conjunto.Logo;
            }

            string mimetype = "";
            int extencion = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\InfomePoderes.rdlc";

            string imagenParam = "";
            var imagenPath = $"{this._webHostEnvironment.WebRootPath}\\logos\\{logo}";
            using (var b = new Bitmap(imagenPath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imagenParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Descripcion", asamblea.Descripcion);
            parameters.Add("Conjunto", conjunto.Nombre);
            parameters.Add("Fecha", asamblea.FechaInicio.Day + "-" + meses[asamblea.FechaInicio.Month - 1] + "-" + asamblea.FechaInicio.Year);
            parameters.Add("logo", imagenParam);

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("Poderes", dt);

            var result = localReport.Execute(RenderType.Pdf, extencion, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }

        [HttpGet]
        [Route("InfomeQuorum/{id}")]
        [Authorize(Roles = "administrador,moderador")]
        public async Task<IActionResult> InfomeQuorum(int id, [FromServices] IReportService r, [FromServices] IAsambleaService a, [FromServices] IConjuntoService c)
        {
            Asamblea asamblea = await a.GetAsamblea(Convert.ToInt32(id));
            Conjunto conjunto = await c.GetConjunto(asamblea.IdConjunto);
            DataTable dt = await r.GetInfomeQuorum(id, conjunto.IdConjunto);

            string logo = "default.png";
            if (conjunto.Logo != null)
            {
                logo = conjunto.Logo;
            }

            string mimetype = "";
            int extencion = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\InfomeQuorum.rdlc";

            string imagenParam = "";
            var imagenPath = $"{this._webHostEnvironment.WebRootPath}\\logos\\{logo}";
            using (var b=new Bitmap(imagenPath))
            {
                using (var ms=new MemoryStream()) 
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imagenParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Descripcion", asamblea.Descripcion);
            parameters.Add("Conjunto", conjunto.Nombre);
            parameters.Add("Fecha", asamblea.FechaInicio.Day + "-" + meses[asamblea.FechaInicio.Month - 1] + "-" + asamblea.FechaInicio.Year);
            parameters.Add("logo", imagenParam);

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("Quorum", dt);
            var result = localReport.Execute(RenderType.Pdf, extencion, parameters, mimetype);

            return File(result.MainStream, "application/pdf");
        }
    }
}
