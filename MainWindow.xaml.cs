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
using System.Threading;
using QuickGraph;
using GraphSharp.Controls;
using GraphSharp.Algorithms;
using GraphSharp.Converters;
using GraphSharp.Helpers;
using System.ComponentModel;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged    
    {
        public int newVertex;

      
        List<BidirectionalGraph<object, IEdge<object>>> Graphs;

        private BidirectionalGraph<object, IEdge<object>> _graphToVisualize;
        
        public BidirectionalGraph<object, IEdge<object>> GraphToVisualize {
            get {
                return _graphToVisualize;
            }
            set
            {
                if (_graphToVisualize != value)
                {
                    _graphToVisualize = value;
                    NotifyChanged("GraphToVisualize");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e) {
            List<string> data = new List<string>();
            data.Add("Circular");
            data.Add("ISOM");
            data.Add("FR");
            data.Add("Tree");
            data.Add("LinLog");
            data.Add("CompoundFDP");
            data.Add("KK");
            data.Add("BoundedFR");
            data.Add("EfficientSugiyama");
            var combobox = sender as ComboBox;
            combobox.ItemsSource = data;
            combobox.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e) {
            var comboBox = sender as ComboBox;
            string value = comboBox.SelectedItem as string;
            graphLayout.SetValue(GraphLayout.LayoutAlgorithmTypeProperty, value);
        }

        private void ComboBoxGraph_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            int value = comboBox.SelectedIndex;
            GraphToVisualize = Graphs[value].Clone();
        }

        private void ComboBoxGraph_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> data = new List<int>();
            data.Add(0);
            data.Add(1);
            data.Add(2);
            SetGraphs();
            GraphToVisualize = Graphs[0].Clone();
            var combobox = sender as ComboBox;
            combobox.ItemsSource = data;
            combobox.SelectedIndex = 0;
        }

        private void SetGraphs() {
            Graphs = new List<BidirectionalGraph<object, IEdge<object>>>();
            var g = new BidirectionalGraph<object, IEdge<object>>();

            string[] vertices = new string[8];

            for (int i = 0; i < 8; ++i)
            {
                vertices[i] = i.ToString();
                g.AddVertex(vertices[i]);
            }

            g.AddEdge(new Edge<object>(vertices[0], vertices[1]));
            g.AddEdge(new Edge<object>(vertices[2], vertices[3]));
            g.AddEdge(new Edge<object>(vertices[3], vertices[5]));
            g.AddEdge(new Edge<object>(vertices[2], vertices[4]));
            g.AddEdge(new Edge<object>(vertices[4], vertices[6]));
            g.AddEdge(new Edge<object>(vertices[4], vertices[7]));
            Graphs.Add(g);

            BidirectionalGraph<object, IEdge<object>> temp = new BidirectionalGraph<object, IEdge<object>>();
            temp.AddVertex("0");
            temp.AddVertex("1");
            temp.AddVertex("2");
            temp.AddVertex("3");
            temp.AddVertex("4");
            temp.AddVertex("5");
            temp.AddVertex("6");
            temp.AddVertex("7"); 
            temp.AddEdge(new Edge<object>("0", "1"));
            temp.AddEdge(new Edge<object>("1", "2"));
            temp.AddEdge(new Edge<object>("2", "3"));
            temp.AddEdge(new Edge<object>("3", "4"));
            temp.AddEdge(new Edge<object>("4", "5"));
            temp.AddEdge(new Edge<object>("5", "6"));
            temp.AddEdge(new Edge<object>("6", "7"));
            temp.AddEdge(new Edge<object>("7", "0"));

            Graphs.Add(temp);

            BidirectionalGraph<object, IEdge<object>> temp2 = new BidirectionalGraph<object, IEdge<object>>();
            temp2.AddVertex("0");
            temp2.AddVertex("1");
            temp2.AddVertex("2");
            temp2.AddVertex("3");
            temp2.AddVertex("4");
            temp2.AddVertex("5");
            temp2.AddVertex("6");
            temp2.AddEdge(new Edge<object>("0", "1"));
            temp2.AddEdge(new Edge<object>("1", "2"));
            temp2.AddEdge(new Edge<object>("2", "3"));
            temp2.AddEdge(new Edge<object>("3", "0"));
            temp2.AddEdge(new Edge<object>("1", "4"));
            temp2.AddEdge(new Edge<object>("1", "5"));
            temp2.AddEdge(new Edge<object>("5", "6"));
            temp2.AddEdge(new Edge<object>("6", "3"));

            Graphs.Add(temp2);
    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newVertex = _graphToVisualize.Vertices.Count();
            Random rnd = new Random(DateTime.Now.Millisecond);
            _graphToVisualize.AddVertex(newVertex.ToString());
            var v1 = rnd.Next(0, _graphToVisualize.Vertices.Count()-1);
            _graphToVisualize.AddEdge(new Edge<object>(v1.ToString(), newVertex.ToString()));
        }

        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
