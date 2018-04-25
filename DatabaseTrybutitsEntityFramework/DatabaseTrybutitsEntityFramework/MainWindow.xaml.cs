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
using System.Text.RegularExpressions;
using DatabaseTrybutitsEntityFramework.Models;
using System.ComponentModel;

namespace DatabaseTrybutitsEntityFramework
{
    public partial class MainWindow : Window
    {
        NdWindow SecondWindow = new NdWindow();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Message(Brush custombrush, string txt)
        {
            signinout.Foreground = custombrush;
            signinout.Content = txt;
        }
        Regex rx = getRegex.GetRegex();
        private readonly string salt = "1234567890";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Data1Context context = new Data1Context())
            {
                if (!string.IsNullOrWhiteSpace(usernameBox.Text) || !string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    if (rx.IsMatch(usernameBox.Text))
                    {
                        Message(Brushes.Red, "Banned username");
                    }
                    else if (rx.IsMatch(passwordBox.Password))
                    {
                        Message(Brushes.Red, "Banned password");
                    }
                    else
                    {
                        Models.UserData user = context.UserData.Where(x => x.UserName == usernameBox.Text).FirstOrDefault();
                        if ((sender as Button).Name.Equals("loginButton"))
                        {
                            if (user != null)
                            {
                                if (Encryption.Encrypt(passwordBox.Password, (passwordBox.Password + Encryption.EncPass(salt)), user.UserPass).Equals(user.UserPass))
                                {
                                    Message(Brushes.Green, "Login Succesful");
                                    SecondWindow.Show();
                                    SecondWindow.GetLoggedUser(user);
                                    SecondWindow.Owner = this;
                                    this.Hide();
                                }
                                else Message(Brushes.Red, "Invalid Password");
                            }
                            else Message(Brushes.Red, "Invalid Username");
                        }
                        else if ((sender as Button).Name.Equals("registerButton"))
                        {
                            if (user == null)
                            {
                                if (passwordBox.Password.Length > 7 && passwordBox.Password.Length < 50 && usernameBox.Text.Length < 50)
                                {
                                    UserData newuser = new UserData
                                    {
                                        UserID = 0 + context.UserData.Count(),
                                        UserName = usernameBox.Text,
                                        UserPass = Encryption.Encrypt(passwordBox.Password, (passwordBox.Password + Encryption.EncPass(salt)))

                                    };
                                    try
                                    {
                                        context.Add(newuser);
                                        context.SaveChanges();
                                        Message(Brushes.Green, "Registration succesful");
                                    }
                                    catch (Exception exc)
                                    {
                                        Message(Brushes.Red, "ERROR: \n" + exc);
                                        loginButton.IsEnabled = false;
                                        registerButton.IsEnabled = false;
                                    }
                                }
                                else Message(Brushes.Red, "Password has to have at least 8 characters");
                            }
                            else Message(Brushes.Red, "Account with this name already exists");
                        }
                    }
                }
                else Message(Brushes.Red, "Input fields cannot be empty");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
