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
            canvas.Visibility = Visibility.Visible;

            int circleRadius = Convert.ToInt32(tb_radius.Text.ToString());
            int borderDistance = Convert.ToInt32(tb_border_distance.Text.ToString());
            int circleDistance = Convert.ToInt32(tb_circle_distance.Text.ToString());
            int width = Convert.ToInt32(tb_width.Text.ToString());
            int height = Convert.ToInt32(tb_height.Text.ToString());

            canvas.Width = width;
            canvas.Height = height;

            List<PackingCircles.Circle> circles = PackingCircles.Calculate(
                circleRadius,
                circleDistance,
                borderDistance,
                width,
                height);

            tb_output.Text += circles.Count.ToString() + "\n";

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

                Canvas.SetLeft(ellipse, c.x - circleRadius);
                Canvas.SetTop(ellipse, c.y - circleRadius);

                canvas.Children.Add(ellipse);
            }
        }
    }
}
