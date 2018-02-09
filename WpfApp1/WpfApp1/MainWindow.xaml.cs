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
        bool chain = false;
        string x2 = " ";
        double x3 = new double();
        string td = " ";
        double res2 = new double();
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

        double y = 0.12342723112154315346;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string x = (sender as Button).Content.ToString();
            if (!x.Equals("C"))
            {
                if (!x.Equals("="))
                { 
                    if (x.Equals("*") || x.Equals("/") || x.Equals("+") || x.Equals("-"))
                    {
                        if (y == 0.12342723112154315346)
                        { 
                            if (double.TryParse(x2, out y))
                            {
                                lbl2.Content = y;
                            }
                        }
                        td = x;
                    }
                    lbl.Content += x;
                    x2 = x;
                } else if (double.TryParse(x2, out x3) && !chain)
                { 
                    res2 = ret(x3, y, td);
                    lbl.Content = res2;
                    chain = true;
                } else if (double.TryParse(x2, out x3))
                {
                    lbl2.Content = res2;
                    lbl.Content = ret(res2, x3, td);
                }
            } else
            {
                chain = false;
                lbl.Content = "";
                lbl2.Content = "";
                y = 0.1234272311215431534;
            }

        }
    }
}
