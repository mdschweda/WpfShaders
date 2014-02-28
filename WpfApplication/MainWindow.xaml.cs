using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        double[] lumarec601 = { 0.299, 0.587, 0.114 };

        double dot(double[] a, double[] b) {
            double scalar = 0;
            if (a.Length == b.Length)
                for (int i = 0; i < a.Length; i++)
                    scalar += a[i] * b[i];
            return scalar;
        }

        double max(double a, double b) {
            return a > b ? a : b;
        }

        double min(double a, double b) {
            return a < b ? a : b;
        }

        double[] hcy(double[] c, double[] lumacoeff) {
            double[] c2 = { c[3], c[3], c[3], c[3] };
            // Luma
            c2[2] = dot(lumacoeff, new double[] { c[0], c[1], c[2] });
            // Chroma
            double M = max(c[0], max(c[1], c[2]));
            c2[1] = M - min(c[0], min(c[1], c[2])); 
            // Hue
            if (c2[1] != 0) {
                if (M == c[0])
                    c2[0] = ((c[1] - c[2]) / c2[1]) % 6;
                else if (M == c[1])
                    c2[0] = (c[2] - c[0]) / c2[1] + 2;
                else
                    c2[0] = (c[0] - c[1]) / c2[1] + 4;
                if (c2[0] < 0)
                    c2[0] += 6;
            } else {
                c2[0] = 0;
            }
            return c2;
        }

        double[] hcy2(double[] c2) {
            double[] c = new double[] { c2[0], c2[1], c2[2] };
            double luma = 0.299 * c[0] + 0.587 * c[1] + 0.114 * c[2];
               
            double max = c[0];
            double min = c[0];
            if (c[1] > max) max = c[1];
            if (c[2] > max) max = c[2];
            if (c[1] < min) min = c[1];
            if (c[2] < min) min = c[2];
               
            double chroma = max;
            double im = 1 / (max - min);
            c[0] = (c[0] - min) * im;
            c[1] = (c[1] - min) * im;
            c[2] = (c[2] - min) * im;

            double hue;
            if (c[0] == 1) {
                hue = (c[1] - c[2]);
                if (hue < 0)
                    hue += 6;
            } else if (c[1] == 1)
                hue = 2 + (c[2] - c[0]);
            else
                hue = 4 + (c[0] - c[1]);

            return new double[] { hue, chroma, luma, c2[3] };
        }

        double abs(double x) {
            return x < 0 ? -x : x;
        }

        double[] rgb(double[] c, double[] lumacoeff) {
            double[] c2 = { 0, 0, 0, c[3] };
            double X = c[1] * (1 - abs(c[0] % 2 - 1)), m;
            if (0 <= c[0] && c[0] < 1) {
                c2[0] = c[1]; c2[1] = X; c2[2] = 0;
            } else if (1 <= c[0] && c[0] < 2) {
                c2[0] = X; c2[1] = c[1]; c2[2] = 0;
            } else if (2 <= c[0] && c[0] < 3) {
                c2[0] = 0; c2[1] = c[1]; c2[2] = X;
            } else if (3 <= c[0] && c[0] < 4) {
                c2[0] = 0; c2[1] = X; c2[2] = c[1];
            } else if (4 <= c[0] && c[0] < 5) {
                c2[0] = X; c2[1] = 0; c2[2] = c[1];
            } else if (5 <= c[0] && c[0] < 6) {
                c2[0] = c[1]; c2[1] = 0; c2[2] = X;
            }
            m = c[2] - dot(lumacoeff, new double[] { c2[0], c2[1], c2[2] });
            return new double[] { c2[0] + m, c2[1] + m, c2[2] + m, c[3] };
        }

        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // 51 153 ~17
            //double[] color = new double[] { 0.2, 0.6, 0.0667, 1 };
            double[] color = new double[] { 230 / 255.0, 230 / 255.0, 230 / 255.0, 1 };
                /*
                 * rgb = [179, 64, 156]
                 * rgb = [15, 70, 26]
                 * rgb = [23, 248, 125]
                 */
                double[] hcycolor = hcy(color, lumarec601);
                double[] convBack = rgb(hcycolor, lumarec601);

                System.Diagnostics.Debug.WriteLine("======================================================");
                System.Diagnostics.Debug.WriteLine("rgb = [{0:#}, {1:#}, {2:#}]", color[0] * 255, color[1] * 255, color[2] * 255);
                System.Diagnostics.Debug.WriteLine("-------------------------------------------------------");
                System.Diagnostics.Debug.WriteLine("hcy(rgb) = [{0:#.##}, {1:#.##}, {2:#.##}]", hcycolor[0], hcycolor[1], hcycolor[2]);
                System.Diagnostics.Debug.WriteLine("rgb(hcy) = [{0:#}, {1:#}, {2:#}]", convBack[0] * 255, convBack[1] * 255, convBack[2] * 255);

            /*hcycolor[0] *= 60;
            hcycolor[1] *= 100;
            hcycolor[2] *= 100;*/
            //double[] color2 = rgb(hcycolor, lumarec601);
        }

    }
}