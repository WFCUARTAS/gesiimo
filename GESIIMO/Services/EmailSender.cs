using GESIIMO.Services.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GESIIMO.Data;
using Microsoft.AspNetCore.Identity;
using GESIIMO.Model;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using System;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Net.Mime;
using System.Collections.Generic;

namespace GESIIMO.Services
{
    public class EmailSender : IEmailSender
    {
        private UserManager<ApplicationUser> _userManager;
        private IConjuntoService conjuntoService;
        // Our private configuration variables
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private SmtpClient smt;

        List<string> meses = new List<string>() { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };

        private readonly SqlConfiguration configuration;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        // Get our parameterized configuration
        public EmailSender(string host, int port, bool enableSSL, string userName, string password, SqlConfiguration configuration)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
            this.configuration = configuration;
            
            smt = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {           
            return smt.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }

        public async Task<int> CitacionMasiva(IEnumerable<EnvioCorreo> propietarios, Asamblea asamblea, Conjunto conjunto, string url)
        {
            foreach(var propietario in propietarios)
            { 
                await CitacionIndividual(propietario, asamblea, conjunto, url);
            }
            return 0;
        }

        public async Task<int> CitacionIndividual(EnvioCorreo propietario, Asamblea asamblea, Conjunto conjunto, string url)
        {
            string codeUsuario = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(propietario.IdUsuario));
            string codeAsamblea = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(""+asamblea.IdAsamblea));

            //string html = "Señor(a): "+propietario.Propietario+" , Para ingresar a la Asamblea  <a href='" + url + "ActualizarDatos/" + codeUsuario +"/"+ codeAsamblea + "'>HAGA CLIK AQUI</a> .";
            string ruta = url + "ActualizarDatos/" + codeUsuario + "/" + codeAsamblea;


            AlternateView html =await GetHtml(propietario, asamblea, conjunto,ruta);

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(propietario.Email);
            mailMessage.Subject = "Citación Asamblea";
            mailMessage.IsBodyHtml = true;
            mailMessage.AlternateViews.Add(html);
            mailMessage.From = new System.Net.Mail.MailAddress(userName);

            await smt.SendMailAsync(mailMessage);

            if (!propietario.IndCorreoEnviado)
            {
                using SqlConnection db = dbConnection();
                string sql = "dbo.sp_CitacionAsamblea_SET";

                var parametros = new
                {
                    IdCitacionAsamblea = 0,
                    idAsamblea = asamblea.IdAsamblea,
                    idUsuario = propietario.IdUsuario,
                    Action = "POST"
                };

                return await db.ExecuteAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            }

            return 0;
        }



        public async Task<AlternateView> GetHtml(EnvioCorreo propietario, Asamblea asamblea, Conjunto conjunto, string ruta)
        {
            string logo = "";
            if (conjunto.Logo == null)
            {
                logo = "wwwroot/logos/default.png";
            }
            else
            {
                logo = "wwwroot/logos/" + conjunto.Logo;
            }
            string banner = "wwwroot/imagen/banner_default.jpg";
            var mailMessage = new MailMessage();

            string html = @"
                <div style='width: 60%; margin-left: auto; margin-right: auto; '>
                    <blockquote>
                                <p><strong>
                                    <img style='display: block; margin-left: auto; margin-right: auto; width:100px; height:100px;' src='cid:LogoConjunto' alt='Logo' />
                                </strong></p>
                    </blockquote> 
                    <p><strong>
                        <img style='display: block; margin-left: auto; margin-right: auto;width:100%; ' src='cid:banner' alt='banner' />
                    </strong>
                    </p><h3>
                        <span style='color: #008080;'>
                            *NombrePropietario* est&aacute; cordialmente invitad@ a la Asamblea Virtual de su Conjunto Residencial *Conjunto*.
                        </span>
                    </h3>
                    <p> 
                        <span style='color: #000000;'>
                            Hemos organizado todo para que&nbsp;pueda disfrutar de esta asamblea desde la comodidad de su hogar de manera f&aacute;cil y pr&aacute;ctica. La asamblea se llevar&aacute; a cabo el d&iacute;a&nbsp;<strong>*Fecha*</strong><strong>&nbsp;</strong>conforme a la ley*.</span></p><p> 
                        <strong>ORDEN DEL D&Iacute;A:</strong>
                    </p>
                    <p>*OrdenDia*</p> 
                    <p style='text-align: left;'>Use el bot&oacute;n a continuaci&oacute;n para ingresar a la asamblea.</p> 
                    <p>&nbsp;</p>
                    <table style='border-collapse: collapse; width: 53.3928%; height: 33px; margin-left: auto; margin-right: auto;' border='1'>
                        <tbody>
                            <tr style='height: 33px;'>
                                <td style='width: 100%; height: 33px; background-color: #1a8989; border-style: hidden; text-align: center;'>
                                    <span style='color: #0000ff;'>
                                        <span style='color: #ffffff;'><strong>
                                            <a style='color: #ffffff;' href='*URL*' target='_blank'><b>Clic aqu&iacute; para ingresar</b></a>
                                        </strong></span>
                                    </span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p>&nbsp;</p> 
                    
                    
                    <table style='margin-left: auto; margin-right: auto; ' border='0'  cellspacing='0'>
                        <tbody>
                            <tr >
                                <td style=' background-color: #aeaaaa;' valign='top'>
                                    <span style='color: #ffffff;'>
                                        <br /><br />
                                        En caso de que el aplicativo no funcione de forma adecuada en su dispositivo movil se recomienda iniciar la sesión desde la app de Mozilla Firefox, si esta no se encuentra instalada lo invitamos a realizarla la descarga
                                        <a style='color: #ffffff;' href='https://play.google.com/store/apps/details?id=org.mozilla.firefox' target='_blank'><b>Playstore </b></a> o 
                                        <a style='color: #ffffff;' href='https://apps.apple.com/co/app/navegador-web-firefox/id989804926' target='_blank'><b>App store</b></a>
                                    </span>

                                    <br /><br />

                                    <span style='color: #ffffff;'>
                                        *A pesar de que la Ley 675 de 2001 ya permit&iacute;a las reuniones no presenciales en propiedad horizontal, en la pr&aacute;ctica se hac&iacute;a casi imposible cumplir con las reglas que establec&iacute;a para su celebraci&oacute;n, por cuanto las decisiones se invalidaban en caso que alguno de los propietarios no participara en la comunicaci&oacute;n simult&aacute;nea o sucesiva.
                                    </span>
                                    <br />
                                    <span style='color: #ffffff;'>
                                        <br /><br />
                                        No obstante, la situaci&oacute;n cambi&oacute; con la expedici&oacute;n del Decreto 398 de marzo 13 de 2020, que adiciona el Decreto &Uacute;nico del Sector Comercio, Industria y Turismo, en lo referente al desarrollo de reuniones no presenciales de las juntas de socios, asambleas generales de accionistas o juntas directivas en sociedades comerciales sujetas al r&eacute;gimen de la Ley 222 de 1995; cuya aplicaci&oacute;n se hizo extensiva a todas las personas jur&iacute;dicas, sin excepci&oacute;n, quienes estar&aacute;n facultadas para aplicar las reglas previstas en los art&iacute;culos 1&deg; y 2&deg; del decreto en la realizaci&oacute;n de reuniones no presenciales de sus &oacute;rganos colegiados.
                                    </span>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                </div>
                ";
            if (asamblea.OrdenDia == null) asamblea.OrdenDia = "";
            asamblea.OrdenDia = asamblea.OrdenDia.Replace("\n", "<br>");
            html = html.Replace("*NombrePropietario*", propietario.Propietario);
            html = html.Replace("*Conjunto*", conjunto.Nombre);
            html = html.Replace("*Fecha*", asamblea.FechaInicio.Day+"-"+meses[asamblea.FechaInicio.Month-1] +""+asamblea.FechaInicio.ToString("-yyyy HH:mm tt"));
            html = html.Replace("*OrdenDia*", asamblea.OrdenDia);
            html = html.Replace("*URL*", ruta);

            LinkedResource log = new LinkedResource(logo);
            log.ContentId = "LogoConjunto";
            
            LinkedResource ban = new LinkedResource(banner);
            ban.ContentId = "banner";

            AlternateView AV = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(log);
            AV.LinkedResources.Add(ban);
            
            return AV;
        }

        
    }


}
