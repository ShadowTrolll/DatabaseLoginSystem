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
using System.Windows.Shapes;
using DatabaseTrybutitsEntityFramework.Models;
using System.Text.RegularExpressions;

namespace DatabaseTrybutitsEntityFramework
{
    public partial class NdWindow : Window
    {
        UserData loggedUser = new UserData();
        public void GetLoggedUser(UserData lgduser)
        {
            loggedUser = lgduser;
            RenderStuff();
        }
        public void RenderStuff()
        {
            showLoggedUser.Content = loggedUser.UserName;
        }
        public NdWindow()
        {
            InitializeComponent();
        }
        Regex rx = getRegex.GetRegex();
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private readonly string salt = "1234567890";
        private void savechanges(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currPassBox.Password) || !string.IsNullOrWhiteSpace(currPass4Name.Password) || !string.IsNullOrWhiteSpace(newPassBox.Password) || !string.IsNullOrWhiteSpace(newUsernameBox.Text))
            {
                if (rx.IsMatch(currPass4Name.Password) || rx.IsMatch(currPassBox.Password) || rx.IsMatch(newPassBox.Password) || rx.IsMatch(newUsernameBox.Text))
                {
                    stateInfoLbl.Foreground = Brushes.Red;
                    stateInfoLbl.Content = "Banned credentials";
                }
                else
                {
                    using (Data1Context context = new Data1Context())
                    {
                        bool madeChanges = false;
                        stateInfoLbl.Content = "";
                        if (Encryption.Encrypt(currPass4Name.Password, (currPass4Name.Password + Encryption.EncPass(salt)), loggedUser.UserPass).Equals(loggedUser.UserPass))
                        {
                            loggedUser.UserName = newUsernameBox.Text;
                            context.Update(loggedUser);
                            stateInfoLbl.Foreground = Brushes.Green;
                            stateInfoLbl.Content += " Username changed succesfully!";
                            madeChanges = true;
                        }
                        if (Encryption.Encrypt(currPassBox.Password, (currPassBox.Password + Encryption.EncPass(salt)), loggedUser.UserPass).Equals(loggedUser.UserPass))
                        {
                            loggedUser.UserPass = Encryption.Encrypt(newPassBox.Password, (newPassBox.Password + Encryption.EncPass(salt)));
                            context.Update(loggedUser);
                            stateInfoLbl.Foreground = Brushes.Green;
                            stateInfoLbl.Content += " Password changed succesfuly!";
                            madeChanges = true;
                        }
                        if (madeChanges)
                        {
                            context.SaveChanges();
                            currPass4Name.Password = null;
                            newPassBox.Password = null;
                            currPassBox.Password = null;
                            newUsernameBox.Text = null;
                            RenderStuff();
                        }
                    }
                }
            }
            else
            {
                stateInfoLbl.Foreground = Brushes.Red;
                stateInfoLbl.Content = "Invalid password";
            }
        }
        private void logout(object sender, RoutedEventArgs e)
        {
            loggedUser = null;
            MainWindow mainwin = new MainWindow();
            mainwin.Show();
            this.Close();
        }
    }
}
