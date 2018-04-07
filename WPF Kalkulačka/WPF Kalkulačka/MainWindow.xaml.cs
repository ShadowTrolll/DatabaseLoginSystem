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

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        bool st = true;
        bool cont = false;
        double minp = new double();
        double inp1 = new double();
        double inp2 = new double();
        string sminp = string.Empty;
        string sinp1 = string.Empty;
        string sinp2 = string.Empty;
        string x = string.Empty;
        string toDo = string.Empty;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        x = (sender as Button).Content.ToString();
        if (st)
        { 
                sinp1 += x;
                lbl.Content = sinp1;
        }  else if (!cont)
        {
                sinp2 += x;
                lbl2.Content = sinp1;
                lbl.Content += sinp2;
        } else
        {
            sminp += x;
            lbl.Content = lbl2.Content.ToString() + toDo + sminp;
        }
        }
        private void Button_Clickequals(object sender, RoutedEventArgs e)
        {
            if (!cont)
            {
                if (double.TryParse(sinp2, out inp2))
                {
                    lbl2.Content = res(inp1, inp2, toDo).ToString();
                    lbl.Content = lbl2.Content.ToString();
                } else sinp2 = "";
            } else
            {
                if (double.TryParse(sminp, out minp))
                {
                    lbl.Content = res(double.Parse(lbl2.Content.ToString()), minp, toDo);
                    lbl2.Content = lbl.Content;
                }
                sminp = "";
            }
        }
        private void Button_Clickmdpm(object sender, RoutedEventArgs e)
        {
            toDo = (sender as Button).Content.ToString();
            if (st)
            {
                if (double.TryParse(sinp1, out inp1))
                {
                    st = false;
                }
            } else if (!cont)
            {
                if (double.TryParse(sinp2, out inp2))
                {
                    cont = true;
                }
            } else
            {
                double.TryParse(sminp, out minp);
            }
            lbl.Content += toDo;
        }
        private static double res(double i, double y, string toDo)
        {
            double res2 = new double();
            switch (toDo)
            {
                case "*":
                    res2 = i * y;
                    break;
                case "/":
                    res2 = i / y;
                    break;
                case "-":
                    res2 = i - y;
                    break;
                case "+":
                    res2 = i + y;
                    break;
                default:
                    res2 = 0.00;
                    break;
            }
            return res2;
        }

        private void Button_Clickc(object sender, RoutedEventArgs e)
        {
            st = true;
            cont = false;
            minp = new double();
            inp1 = new double();
            inp2 = new double();
            sminp = string.Empty;
            sinp1 = string.Empty;
            sinp2 = string.Empty;
            x = string.Empty;
            toDo = string.Empty;
            lbl.Content = string.Empty;
            lbl2.Content = string.Empty;
        }
    }
}
