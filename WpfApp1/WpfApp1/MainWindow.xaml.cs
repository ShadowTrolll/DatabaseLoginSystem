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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string x2 = " ";
        double x3 = new double();
        string td = " ";
        public static double ret(double a, double b, string toDo)
        {
            double res = new double();
            switch (toDo)
            {
                case "*":
                    res = a * b;
                    break;
                case "/":
                    res = a / b;
                    break;
                case "-":
                    res = a - b;
                    break;
                case "+":
                    res = a + b;
                    break;
                default:
                    res = 0.00;
                    break;
            }
            return res;

        }
        double y = 0.123427;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string x = (sender as Button).Content.ToString();
            while (!x.Equals("="))
            { 
                if (x.Equals("*") || x.Equals("/") || x.Equals("+") || x.Equals("-"))
                {
                    if (y == 0.123427)
                    { 
                        if (double.TryParse(x2, out y))
                        {
                            lbl2.Content = y;
                            td = x;
                        }
                    }

            
                } else lbl.Content += x;
            x2 = x;
            }
            if (double.TryParse(x2, out x3))
            { 
                lbl.Content = ret(x3, y, td);
            }
        }
    }
}
