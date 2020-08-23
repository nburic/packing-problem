using PackingLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            int circleRadius;
            int borderDistance;
            int circleDistance;
            int height;

            try
            {
                circleRadius = Convert.ToInt32(tb_radius.Text);
                borderDistance = Convert.ToInt32(tb_border_distance.Text);
                circleDistance = Convert.ToInt32(tb_circle_distance.Text);
                height = Convert.ToInt32(tb_height.Text);
            } 
            catch (Exception ex)
            {
                tb_output.Text = ex.Message;
                return;
            }
            
            if (height > PackingCircles.width)
            {
                tb_output.Text = "Width has to be greater than height for optimal solution.";
                return;
            }
             

            canvas.Width = PackingCircles.width;
            canvas.Height = height;

            List<PackingCircles.Circle> circles;

            try
            {
                circles = PackingCircles.Calculate(
                    circleRadius,
                    circleDistance,
                    borderDistance,
                    height);

            } catch (Exception ex) {
                tb_output.Text = ex.Message;
                return;
            }

            tb_output.Text += "Number of circles: " + circles.Count.ToString() + "\n";
            tb_output.Text += "Coordinates:\n";

            foreach (PackingCircles.Circle c in circles)
            {
                Ellipse ellipse = createEllipse(c, height);

                tb_output.Text += c.ToString() + "\n";

                canvas.Children.Add(ellipse);
            }
        }

        private Ellipse createEllipse(PackingCircles.Circle c, int areaHeight)
        {
            Ellipse ellipse = new Ellipse();

            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            ellipse.Width = c.r * 2;
            ellipse.Height = c.r * 2;
            ellipse.Stroke = blackBrush;
            ellipse.StrokeThickness = 1;

            Canvas.SetLeft(ellipse, c.coords.X - c.r);
            Canvas.SetTop(ellipse, areaHeight - c.coords.Y - c.r);

            return ellipse;
        }
    }
}
