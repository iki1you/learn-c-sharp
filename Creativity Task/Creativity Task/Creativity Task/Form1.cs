using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Creativity_Task
{
    
    public partial class Form1 : Form
    {
        private static readonly string dataFilePath = "names.txt";
        int procent1 = 0;
        int procent2 = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart1.Series[0].Points.Clear();
            if (inputName1.Text == "" || inputName2.Text == "") MessageBox.Show("Введите два имени");
            else LoadStatistic(ReadData(), inputName1.Text, inputName2.Text);

        }
        private void LoadStatistic(NameData[] names, string name1, string name2)
        {
            int countName1 = 0;
            int countName2 = 0;
            int[] arrayName1 = new int[31];
            int[] arrayName2 = new int[31];
            foreach (var item in names)
            {
                if (item.Name == name2)
                {
                    countName2++;
                    if (item.BirthDate.Day != 1) arrayName1[item.BirthDate.Day - 1]++;
                }
                if (item.Name == name1)
                {
                    countName1++;
                    if (item.BirthDate.Day != 1) arrayName2[item.BirthDate.Day - 1]++;
                }
                label1.Text = $"Найдено {countName1} людей с именем {name1}";
                label2.Text = $"Найдено {countName2} людей с именем {name2}";
            }
            procent1 = countName1/(countName1 + countName2);
            procent2 = countName2 / (countName1 + countName2);
            for (int i = 0; i < 30; i++)
            {
                chart2.Series[0].Points.Add(arrayName1[i], i-5);
                chart2.Series[1].Points.Add(arrayName2[i], i-5);
            }
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series[0].Points.AddY(countName1);
            chart1.Series[0].Points.AddY(countName2);
            chart1.Series[0].Points[0].LegendText = name1;
            chart1.Series[0].Points[1].LegendText = name2;


        }
        private static NameData[] ReadData()
        {
            var lines = File.ReadAllLines(dataFilePath);
            var names = new NameData[lines.Length];
            for (var i = 0; i < lines.Length; i++)
                names[i] = NameData.ParseFrom(lines[i]);
            return names;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
