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

namespace _01_smtp
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == "rvckocqidlykxlga" || txtLogin.Text == "zaporojechs@gmail.com")
            {
                MainWindow dilog = new MainWindow();
                //dilog.myPassword= txtPassword.Text;
                //dilog.myEmail=  txtLogin.Text;
                dilog.Show();
            }
            else
                MessageBox.Show("Enter login or password error. ");
            
        }
    }
}
