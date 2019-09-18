using NetworkFlow.Layers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point firstPosition = new Point(30, 30);
        private Point secondPosition = new Point(100, 100);
        private bool isDragging = false;
        private int indexCurrentLine= -1;
        private int numberOfNode = 0;
        public ObservableCollection<Line> CurrentConnection { get; private set; }
        public ObservableCollection<Line> Connections { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            CurrentConnection = new ObservableCollection<Line>();
            Connections = new ObservableCollection<Line>();

            //    CurrentConnection = new ObservableCollection<Line>
            //{
            //    new Line { From = new Point(100, 20), To = new Point(180, 180), Stroke = Brushes.Green, StrokeThickness=1},
            //    new Line { From = new Point(180, 180), To = new Point(20, 180),  Stroke = Brushes.Red, StrokeThickness=2 },
            //    new Line { From = new Point(20, 180), To = new Point(100, 20),  Stroke = Brushes.GhostWhite, StrokeThickness=3 },
            //    new Line { From = new Point(20, 50), To = new Point(180, 150),  Stroke = Brushes.BlueViolet, StrokeThickness=4 }
            //};

            DataContext = this;
            // for handle mouse event
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(GridNetwork_MouseLeftButtonUp);
            //this.MouseMove += new MouseEventHandler(GridNetwork_MouseMove);
            //this.MouseLeftButtonDown += new MouseButtonEventHandler(GridNetwork_MouseLeftButtonDown);
            //this.MouseRightButtonDown += new MouseButtonEventHandler(GridNetwork_MouseRightButtonDown);
        }

        private void GridNetwork_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDragging == false)
            {
                Debug.WriteLine("Down left mouse on network canvas");
                Debug.WriteLine("BEGIN making connection");
                var clickPosition = e.GetPosition(net_canvas);
                Debug.WriteLine("Down position: " + clickPosition.X.ToString() + "," + clickPosition.Y.ToString());
                firstPosition = clickPosition;
            }
        }

        private void GridNetwork_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                Debug.WriteLine("Moving mouse on network canvas");
                Debug.WriteLine("MAKING connection");
                var currentPosition = e.GetPosition(net_canvas);
                Debug.WriteLine("Current position: " + currentPosition.X.ToString() + "," + currentPosition.Y.ToString());
                secondPosition = currentPosition;
                var currentLine = new Line { From = new Point(firstPosition.X, firstPosition.Y), To = new Point(secondPosition.X, secondPosition.Y), Stroke = Brushes.Green, StrokeThickness = 10 };
                if (indexCurrentLine >= 1)
                {
                    Debug.WriteLine(indexCurrentLine);
                    CurrentConnection.RemoveAt(indexCurrentLine-1);
                }
                CurrentConnection.Add(currentLine);

            }
            indexCurrentLine = CurrentConnection.Count() - 1;
        }


        private void GridNetwork_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Up left mouse on network canvas");
            var clickPosition = e.GetPosition(net_canvas);
            Debug.WriteLine("Up position: " + clickPosition.X.ToString() + "," + clickPosition.Y.ToString());
            secondPosition = clickPosition;
            if (isDragging == false)
            {
                isDragging = true;
                Debug.WriteLine("BEGIN making connection");
                //currentLine = new Line { From = new Point(firstPosition.X, firstPosition.Y), To = new Point(secondPosition.X, secondPosition.Y), Stroke = Brushes.Green, StrokeThickness = 10 };
            }
            else
            {
                Debug.WriteLine("COMPLETE making connection");
                isDragging = false;

                Connections.Add(new Line
                {
                    From = new Point(firstPosition.X, firstPosition.Y),
                    To = new Point(secondPosition.X, secondPosition.Y),
                    Stroke = Brushes.Red,
                    StrokeThickness = 3
                });

                //drawBezier(new Point(firstPosition.X, firstPosition.Y), new Point(secondPosition.X, firstPosition.Y), new Point(secondPosition.X, secondPosition.Y));
                //indexCurrentLine = CurrentConnection.Count() - 1;
            }
        }

        
        private void GridNetwork_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.Write("Number of CurrentConnection: " + CurrentConnection.Count().ToString());
            Debug.Write("Number of Connections: " + Connections.Count().ToString());
            Connections.Clear();
            CurrentConnection.Clear();
            indexCurrentLine = CurrentConnection.Count() - 1;
            //net_canvas.Children.Clear();
        }
        /// <summary>
        /// Draw bezier curver for smooth connection on canvas
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="point3">The third point</param>
        private void drawBezier(Point point1, Point point2, Point point3)
        {
            BezierSegment bezier = new BezierSegment()
            {
                Point1 = new Point(0.5*point1.X + 0.5*point3.X, point1.Y),
                Point2 = point2,
                Point3 = point3,
                IsStroked = true
            };

            PathFigure figure = new PathFigure();
            figure.StartPoint = point1;
            figure.Segments.Add(bezier);

            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.Data = new PathGeometry(new PathFigure[] { figure });

            net_canvas.Children.Add(path);
            
        }

        /// <summary>
        /// Add CNN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_CNN(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_CNN();
            var newNode = new LayerCNN();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }
        /// <summary>
        /// Add FC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_FC(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_FC();
            //net_canvas.Children.Add(new LayerFC());
            var newNode = new LayerFC();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }
        /// <summary>
        /// Add Drop Out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_DropOut(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_DropOut();
            //net_canvas.Children.Add(new LayerDropOut());
            var newNode = new LayerDropOut();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }
        /// <summary>
        /// Add Rnn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_RNN(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_RNN();
            //net_canvas.Children.Add(new LayerRNN());
            var newNode = new LayerRNN();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }
        /// <summary>
        /// Add Flatten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Flatten(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_Flatten();
            //net_canvas.Children.Add(new LayerFlatten());
            var newNode = new LayerFlatten();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }
        /// <summary>
        /// Add Pooling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Pooling(object sender, RoutedEventArgs e)
        {
            //network_canvas.Add_Pooling();
            //net_canvas.Children.Add(new LayerPooling());
            var newNode = new LayerPooling();
            newNode.seq_id = numberOfNode + 1;
            numberOfNode++;
            net_canvas.Children.Add(newNode);
        }

    }
}
