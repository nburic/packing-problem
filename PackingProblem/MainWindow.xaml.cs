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

            int circleRadius = Convert.ToInt32(tb_radius.Text);
            int borderDistance = Convert.ToInt32(tb_border_distance.Text);
            int circleDistance = Convert.ToInt32(tb_circle_distance.Text);
            int width = Convert.ToInt32(tb_width.Text);
            int height = Convert.ToInt32(tb_height.Text);

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
                Ellipse ellipse = createEllipse(c);

                tb_output.Text += c.ToString() + " ";

                canvas.Children.Add(ellipse);
            }
        }

        private Ellipse createEllipse(PackingCircles.Circle c)
        {
            Ellipse ellipse = new Ellipse();

            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            ellipse.Width = c.r * 2;
            ellipse.Height = c.r * 2;
            ellipse.Stroke = blackBrush;
            ellipse.StrokeThickness = 1;

            Canvas.SetLeft(ellipse, c.coords.X - c.r);
            Canvas.SetTop(ellipse, c.coords.Y - c.r);

            return ellipse;
        }
    }
}
