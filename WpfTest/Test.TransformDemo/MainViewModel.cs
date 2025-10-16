using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test.TransformDemo
{
    public class MainViewModel
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle {  get; set; }
        public Thickness Region { get; set; }
        public MainViewModel()
        {
            Width = 150;
            Height = 30;
            Angle = 35;
            Region = new Thickness(0,100,0,0);
        }
    }
}
