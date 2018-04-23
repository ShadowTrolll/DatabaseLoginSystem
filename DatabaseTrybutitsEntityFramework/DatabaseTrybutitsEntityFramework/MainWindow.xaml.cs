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

namespace DatabaseTrybutitsEntityFramework
{
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.Data1ConnectionString);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Message(Brush custombrush, string txt)
        {
            signinout.Foreground = custombrush;
            signinout.Content = txt;
        }

        private readonly string salt = "1234567890";
        Regex regex = new Regex("(CREATE TABLE)|(DROP TABLE)|(DROP INDEX)|(CREATE INDEX)|(ALTER TABLE)|(ALTER DATABASE)|(CREATE DATABASE)|(INSERT INTO)|( )|(INNER JOIN)|(OUTER JOIN)|(UPDATE)|(;)|(-)|(')|(')", RegexOptions.IgnoreCase);
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameBox.Text) || !string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                if (regex.IsMatch(usernameBox.Text))
                {
                    Message(Brushes.Red, "Foreign username");
                } else if (regex.IsMatch(passwordBox.Password))
                {
                    Message(Brushes.Red, "Foreign password.");
                }
                else
                {
                    UserData user = dc.UserData.FirstOrDefault(x => x.UserName == usernameBox.Text);
                    if ((sender as Button).Name.Equals("loginButton"))
                    {
                        if (user != null)
                        {
                            if (Encryption.Encrypt(usernameBox.Text, (user.UserID + Encryption.EncPass(salt))).Equals(user.UserPass)) Message(Brushes.Green, "Login Succesful");
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
                                    UserID = dc.UserData.Count(),
                                    UserName = usernameBox.Text,
                                    UserPass = Encryption.Encrypt(passwordBox.Password, (dc.UserData.Count() + "xyz"))

                                };
                                try
                                {
                                    dc.UserData.InsertOnSubmit(newuser);
                                    dc.SubmitChanges();
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
}
