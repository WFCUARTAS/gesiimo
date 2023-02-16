using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.BarChart.Axes;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;


namespace GESIIMO.Model
{
    public class Votacion
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; }
        public int TipoGrafica { get; set; }
        public int IdOpcionPregunta { get; set; }
        public string Opcion { get; set; }
        public double Coeficiente { get; set; }
        public int Cantidad { get; set; }
        public string color { get; set; }

        public PieConfig config_torta = new PieConfig
        {
            Options = new PieOptions
            {
                Responsive = true,
                Title = new OptionsTitle
                {
                    Display = true,
                    Text = "Grafica",
                    FontSize = 20
                }
            }
        };

        public BarConfig config_barras = new BarConfig
        {
            Options = new BarOptions
            {
                Responsive = true,
                Legend = new Legend
                {
                    Position = ChartJs.Blazor.Common.Enums.Position.Top
                },
                Title = new OptionsTitle
                {
                    Display = true,
                    Text = "Grafica",
                    FontSize = 20
                },
                Scales = new BarScales
                {
                    XAxes = new List<CartesianAxis>
                    {
                        new BarCategoryAxis
                        {
                            Stacked = true,
                            MaxBarThickness = 50
                        }
                    },
                    YAxes = new List<CartesianAxis>
                    {
                        new BarLinearCartesianAxis
                        {
                            Ticks = new LinearCartesianTicks
                            {
                                BeginAtZero = true
                            },
                            Stacked=true
                        }
                    }
                }

            }
        };

    }
}
