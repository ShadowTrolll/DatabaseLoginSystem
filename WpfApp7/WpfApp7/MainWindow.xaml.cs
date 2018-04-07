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

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int rows = 0;
        int colums = 0;
        string tempRowDefiniton = string.Empty;
        string tempColumnDefinition = string.Empty;
        string tempButtonDefinition = string.Empty;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(Rws.Text, out rows) && Int32.TryParse(Clums.Text, out colums))
            {
                System.IO.File.WriteAllText(@"C:\Users\Guest\gridgen\grid.txt", GridGen(customname.Text, customonclick.Text));
                this.IsEnabled = false;
                State.Foreground = Brushes.Green;
                State.Content = "Done";
            } else 
            {
                State.Content = "Error";
                State.Foreground = Brushes.Red;
            }
        }
        private string GridGen(string c, string d)
        {
            if (c.Equals("")) c = "G";
            if (d.Equals("")) d = "Button_Click";
            string gridcode = string.Empty;
            int currentRow = 0;
            int currentColumn = 0;
            for (int i = 0; i < rows; i++) tempRowDefiniton += "<RowDefinition/> \n";
            for (int i = 0; i < colums; i++) tempColumnDefinition += "<ColumnDefinition/> \n";
            if (dobuttons.IsChecked == true)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int y = 0; y < colums; y++)
                    {
                        tempButtonDefinition += "<Button Grid.Column=\"" + currentColumn + "\" Grid.Row=\"" + currentRow + "\" Click=\"" + d + "\" Name=\"" + c + currentRow + "x" + currentColumn + "\">" + (currentRow + 1) + "-" + (currentColumn + 1) + "</Button> \n";
                        currentColumn++;
                    }
                    currentColumn = 0;
                    currentRow++;
                }
            }
            gridcode = "<Grid.ColumnDefinitions> \n" + tempColumnDefinition + "</Grid.ColumnDefinitions> \n<Grid.RowDefinitions> \n" + tempRowDefiniton + "</Grid.RowDefinitions> \n" + tempButtonDefinition;
            return gridcode;
        }
    }
}
