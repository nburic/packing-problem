using PackingLibrary;
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


namespace PackingProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void onCalculateClick(object sender, RoutedEventArgs e)
        {
            tb_output.Text = "";
            canvas.Children.Clear();

            string input = "Btn Calculate was clicked!";
            tb_output.Text = input;

            tb_output.Text = input + "\n" + input.StartsWithUpper() +"\n";

            int circleRadius = Convert.ToInt32(tb_radius.Text.ToString());
            int borderDistance = Convert.ToInt32(tb_border_distance.Text.ToString());

            List<PackingCircles.Circle> circles = PackingCircles.Calculate(
                Convert.ToInt32(tb_radius.Text.ToString()), 
                Convert.ToInt32(tb_circle_distance.Text.ToString()), 
                Convert.ToInt32(tb_border_distance.Text.ToString()),
                Convert.ToInt32(tb_width.Text.ToString()),
                Convert.ToInt32(tb_height.Text.ToString()));

            tb_output.Text += circles.Count.ToString() + "\n";

            border.Width = Convert.ToInt32(tb_width.Text.ToString());
            border.Height = Convert.ToInt32(tb_height.Text.ToString());
            canvas.Width = Convert.ToInt32(tb_width.Text.ToString());
            canvas.Height = Convert.ToInt32(tb_height.Text.ToString());

            foreach (PackingCircles.Circle c in circles)
            {
                tb_output.Text += c.ToString() + " ";
                Ellipse ellipse = new Ellipse();
                ellipse.Width = circleRadius * 2;
                ellipse.Height = circleRadius * 2;
                SolidColorBrush blackBrush = new SolidColorBrush();
                blackBrush.Color = Colors.Black;
                ellipse.Stroke = blackBrush;
                ellipse.StrokeThickness = 1;

                Canvas.SetLeft(ellipse, c.x - circleRadius + borderDistance / 2);
                Canvas.SetTop(ellipse, c.y - circleRadius + borderDistance / 2);

                canvas.Children.Add(ellipse);
            }
        }
    }
}
