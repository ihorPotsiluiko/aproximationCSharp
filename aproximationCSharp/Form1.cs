using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace aproximationCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            y1Box.Text = 3.ToString();
            y2Box.Text = 5.ToString();
            y3Box.Text = 1.ToString();
            y4Box.Text = 3.ToString();
            y5Box.Text = 7.ToString();

            DrawPlot();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DrawPlot()
        {
            float[] xValues = new float[] { 1, 2, 3, 4, 5 };
            float[] yValues;
            try
            {
                yValues = new float[]
                {
                (float) (Convert.ToDouble(y1Box.Text)),
                (float) (Convert.ToDouble(y2Box.Text)),
                (float) (Convert.ToDouble(y3Box.Text)),
                (float) (Convert.ToDouble(y4Box.Text)),
                (float) (Convert.ToDouble(y5Box.Text))
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            LaGrng lg = new LaGrng(xValues, yValues);
            Mnq mng = new Mnq(xValues, yValues);

            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "{F2}";
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisX.Interval = 1;

            chart.Series.Add("NMQ");
            chart.Series[0].ChartType = SeriesChartType.Line;

            chart.Series.Add("LaGrange");
            chart.Series[1].ChartType = SeriesChartType.Line;
            for (float i = 0f; i < 6; i += 0.1f)
            {
                chart.Series[0].Points.AddXY(i, mng.MnqAproxFunction(i));
                chart.Series[1].Points.AddXY(i, lg.LagrngAproxFunction(i));
            }

            chart.Series.Add("Points");
            chart.Series[2].ChartType = SeriesChartType.Point;
            for (int i = 0; i < 5; i++)
            {
                chart.Series[2].Points.AddXY(xValues[i], yValues[i]);
            }
        }

        private void plotButton_Click(object sender, EventArgs e)
        {
            DrawPlot();
        }
    }
}
