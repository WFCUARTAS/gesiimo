using Dapper;
using GESIIMO.Data;
using GESIIMO.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using Syncfusion.OfficeChart;
using System.Linq;

namespace GESIIMO.Services
{
    public class ReportService : IReportService
    {
        private readonly SqlConfiguration configuration;

        List<string> meses = new List<string>() { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };

        public ReportService(SqlConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(configuration.ConnectionString);
        }

        public async Task<DataTable> GetInformeVotaciones(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Informe_Votaciones";

            var parametros = new
            {
                idAsamblea = idAsamblea    
            };
            var dt = new DataTable();
            var result = await db.ExecuteReaderAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            dt.Load(result);
            return dt;
        }

        public async Task<DataTable> GetInfomeActualizacionDatos(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Informe_ActualizacionDatos";

            var parametros = new
            {
                idAsamblea = idAsamblea
            };
            var dt = new DataTable();
            var result = await db.ExecuteReaderAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            dt.Load(result);
            return dt; 
        }

        public async Task<DataTable> GetInfomeQuorum(int idAsamblea, int idConjunto)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Informe_Quorum";

            var parametros = new
            {
                idAsamblea = idAsamblea,
                idConjunto = idConjunto
            };
            var dt = new DataTable();
            var result = await db.ExecuteReaderAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            dt.Load(result);
            return dt;
        }

        public async Task<IEnumerable<InformeAsamblea>> GetInformeAsamblea(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Informe_Asamblea";

            var parametros = new
            {
                IdAsamblea = idAsamblea
            };

            return await db.QueryAsync<InformeAsamblea>(sql, parametros, null, null, CommandType.StoredProcedure);
        }

        public async Task<DataTable> GetInfomePoderes(int idAsamblea)
        {
            SqlConnection db = dbConnection();
            string sql = "dbo.sp_Informe_Poderes";

            var parametros = new
            {
                idAsamblea = idAsamblea
            };
            var dt = new DataTable();
            var result = await db.ExecuteReaderAsync(sql, parametros, null, null, CommandType.StoredProcedure);
            dt.Load(result);
            return dt;
        }

        public async Task<MemoryStream> GetInfomeAsamblea(int idAsamblea)
        {
            AsambleaService asambleaService = new AsambleaService(configuration);
            ConjuntoService conjuntoService = new ConjuntoService(configuration);


            Asamblea asamblea = await asambleaService.GetAsamblea(idAsamblea);
            Conjunto conjunto = await conjuntoService.GetConjunto(asamblea.IdConjunto);

            Quorum quorum = await asambleaService.GetQuorumInforme(asamblea.IdAsamblea);



            //Creating a new document
            WordDocument document = new WordDocument();
            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LeftIndent = 30;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Titulo1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 18f;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 10;
            style.ParagraphFormat.AfterSpacing = 20;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Red;

            style = document.AddParagraphStyle("Titulo2") as WParagraphStyle;
            style.ApplyBaseStyle("Titulo1");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 14f;
            style.ParagraphFormat.AfterSpacing = 20;
            style.ParagraphFormat.LeftIndent = 15;
            style.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Red;

            style = document.AddParagraphStyle("Titulo3") as WParagraphStyle;
            style.ApplyBaseStyle("Titulo2");
            style.CharacterFormat.FontName = "Calibri Light";
            style.ParagraphFormat.LeftIndent = 30;
            style.CharacterFormat.FontSize = 14f;
            style.CharacterFormat.Bold = true;
            style.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Black;


            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            //Appends paragraph
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Titulo1");
            WTextRange textRange = paragraph.AppendText("INFORME DE VOTACIONES") as WTextRange;


            //DATOS CONJUNTO
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            textRange = paragraph.AppendText("CONJUNTO: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange = paragraph.AppendText(conjunto.Nombre) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = false;

            //DATOS FECHA
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 20;
            textRange = paragraph.AppendText("FECHA: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange = paragraph.AppendText(asamblea.FechaInicio.Day + "-" + meses[asamblea.FechaInicio.Month - 1] + "-" + asamblea.FechaInicio.Year) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = false;

            //TITULO QUORUM
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Titulo2");
            textRange = paragraph.AppendText("1. QUORÚM") as WTextRange;


            //texto quorum
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Normal");
            textRange = paragraph.AppendText("Se inicia registro a las "+asamblea.FechaInicioReal.ToString("hh:mm tt")+ " y se finaliza a las "+asamblea.FechaFin.ToString("hh:m tt")+" del día "+ asamblea.FechaInicioReal.ToString("dd-MMMM-yyyy")+ " con una asistencia de "+quorum.CantidadAsistentes+ " apartamentos lo cual corresponde a un "+quorum.QuorumAsistentes+" %.") as WTextRange;

            //Grafica quorum
            WChart pieChart_Quorum = paragraph.AppendChart(446, 270);
            pieChart_Quorum.ChartType = OfficeChartType.Pie;
            pieChart_Quorum.ChartTitle = "";
            pieChart_Quorum.Legend.TextArea.Size = 14;
            pieChart_Quorum.Legend.Position = OfficeLegendPosition.Top;

            ///GRAFICA QUORUM
            pieChart_Quorum.ChartData.SetValue(1, 1, "Asistentes");
            pieChart_Quorum.ChartData.SetValue(1, 2, quorum.QuorumAsistentes);

            pieChart_Quorum.ChartData.SetValue(2, 1, "Ausentes");
            pieChart_Quorum.ChartData.SetValue(2, 2, quorum.QuorumAusentes);


            //Create a new chart series with the name “Sales”
            IOfficeChartSerie pieSeries_quorum = pieChart_Quorum.Series.Add("Sales");
            pieSeries_quorum.Values = pieChart_Quorum.ChartData[1, 2, 2, 2];
            //Setting data label
            pieSeries_quorum.DataPoints.DefaultDataPoint.DataLabels.IsPercentage = true;
            pieSeries_quorum.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;
            //Setting background color
            pieChart_Quorum.ChartArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
            pieChart_Quorum.PlotArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
            pieChart_Quorum.ChartArea.Border.LinePattern = OfficeChartLinePattern.None;
            pieChart_Quorum.PrimaryCategoryAxis.CategoryLabels = pieChart_Quorum.ChartData[1, 1, 2, 1];


            //TITULO VOTACIONES
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Titulo2");
            textRange = paragraph.AppendText("2. VOTACIONES") as WTextRange;



            IEnumerable<IGrouping<int, InformeAsamblea>> PreguntasGroup = new List<IGrouping<int, InformeAsamblea>>();
            IEnumerable<InformeAsamblea> preguntas = await GetInformeAsamblea(asamblea.IdAsamblea);

            PreguntasGroup = from item in preguntas group item by item.IdPregunta;

            int cont = 0;
            foreach (var group in PreguntasGroup)
            {
                cont++;
                //TITULO PREGUNTA
                paragraph = section.AddParagraph();
                paragraph.ApplyStyle("Titulo3");
                textRange = paragraph.AppendText("2."+cont+" "+ group.First().Pregunta) as WTextRange;

                //texto pregunta
                paragraph = section.AddParagraph();
                paragraph.ApplyStyle("Normal");
                textRange = paragraph.AppendText(group.First().InformePregunta) as WTextRange;

                if (group.First().TipoGrafica==5)
                {
                      //Grafica quorum
                    WChart pieChart = paragraph.AppendChart(446, 270);
                    pieChart.ChartType = OfficeChartType.Pie;
                    pieChart.ChartTitle = "";
                    pieChart.Legend.TextArea.Size = 14;
                    pieChart.Legend.Position = OfficeLegendPosition.Top;

                    int cont1 = 0;
                    double coeficiente_sum = 0;
                    foreach (var g in group)
                    {
                        pieChart.ChartData.SetValue(cont1 + 1, 1, g.Opcion);
                        pieChart.ChartData.SetValue(cont1 + 1, 2, g.Coeficiente);
                        coeficiente_sum = coeficiente_sum + g.Coeficiente;
                        cont1++;
                    }
                    //pieChart.ChartData.SetValue(cont1 + 1, 1, "Sin Respuesta");
                    //pieChart.ChartData.SetValue(cont1 + 1, 2, 100 - coeficiente_sum);
                    //cont1++;

                    //Create a new chart series with the name “Sales”
                    IOfficeChartSerie pieSeries = pieChart.Series.Add("");
                    pieSeries.Values = pieChart.ChartData[1, 2, cont1, 2];
                    //Setting data label
                    pieSeries.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                    pieSeries.DataPoints.DefaultDataPoint.DataLabels.Position = OfficeDataLabelPosition.Outside;
                    pieSeries.DataPoints.DefaultDataPoint.DataLabels.NumberFormat = "00.00 \\% ";
                    //Setting background color
                    pieChart.ChartArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
                    pieChart.PlotArea.Fill.ForeColor = Syncfusion.Drawing.Color.FromArgb(242, 242, 242);
                    pieChart.ChartArea.Border.LinePattern = OfficeChartLinePattern.None;
                    pieChart.PrimaryCategoryAxis.CategoryLabels = pieChart.ChartData[1, 1, cont1, 1];
                }
                else
                {
                    //Grafica quorum
                    WChart pieChart = paragraph.AppendChart(446, 270);
                    pieChart.ChartType = OfficeChartType.Column_Stacked;
                    pieChart.ChartTitle = "";
                    pieChart.Legend.TextArea.Size = 14;
                    pieChart.Legend.Position = OfficeLegendPosition.Top;

                    int cont1 = 0;
                    foreach (var g in group)
                    {
                        //pieChart.ChartData.SetValue(cont1 + 1, 1, g.Opcion);
                        pieChart.ChartData.SetValue(cont1 + 1, cont1 +  2, g.Coeficiente);

                        //Create a new chart series with the name “Sales”
                        IOfficeChartSerie pieSeries = pieChart.Series.Add(g.Opcion);
                        //Setting data label
                        pieSeries.Values = pieChart.ChartData[1, cont1 + 2, group.Count(), cont1 + 2]; ;
                        pieSeries.SerieType = OfficeChartType.Column_Stacked;
                        pieSeries.DataPoints.DefaultDataPoint.DataLabels.IsValue = true;
                        pieSeries.DataPoints.DefaultDataPoint.DataLabels.NumberFormat = "00.00 \\%  ";

                        cont1++;
                    }

                    pieChart.PrimaryCategoryAxis.CategoryLabels = pieChart.ChartData[1, 1, group.Count(), 1];

                    
                }


            }


            //Saves the Word document to MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            //Closes the Word document
            document.Close();
            stream.Position = 0;


            return stream;
        }
    }
}
