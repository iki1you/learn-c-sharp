using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace Names
{
    internal static class Charts
    {
        public static void ShowHistogram(HistogramData stats)
        {
            // Графики строятся сторонней библиотекой ZedGraph.
            // Примеры можно найти тут https://jenyay.net/Programming/ZedGraph или тут https://web.archive.org/web/20051127161423/http://zedgraph.sourceforge.net/samples.html
            // Не бойтесь экспериментировать с кодом самостоятельно!

            var chart = new ZedGraphControl
            {
                Dock = DockStyle.Fill
            };
            if (stats.Values.Length == 0)
                chart.GraphPane.Title.Text = "Нет статистики за заданный год";
            else
            {
                chart.GraphPane.Title.Text = stats.Title;
                chart.GraphPane.AddPieSlices(stats.Values, stats.Names);
            }
            chart.AxisChange();
            // Form — это привычное нам окно программы.
            // Это одна из главных частей подсистемы под названием Windows Forms http://msdn.microsoft.com/ru-ru/library/ms229601.aspx
            var form = new Form
            {
                Text = stats.Title,
                Size = new Size(800, 600)
            };
            form.Controls.Add(chart);
            form.ShowDialog();
        }

        private static Color GetColor(double value, double avgHeat, double sigma)
        {
            var sigmaValue = (value - avgHeat) / sigma;
            var colorValue = Math.Min(255, (int) (200 * Math.Abs(sigmaValue)));
            var color = sigmaValue >= 0
                ? Color.FromArgb(255, 255 - colorValue, 255, 255 - colorValue)
                : Color.FromArgb(255, 255, 255 - colorValue, 255 - colorValue);
            return color;
        }
    }
}