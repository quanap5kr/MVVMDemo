using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    public class Line
    {
        public Point From { get; set; }

        public Point To { get; set; }
        public Brush Stroke { get; set; }
        public int StrokeThickness { get; set; }
    }
}
