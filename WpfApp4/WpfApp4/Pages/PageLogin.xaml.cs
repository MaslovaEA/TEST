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
using WpfApp4.ApplicationData;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void btnToComeIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = ProbaEntities.GetContext().Person.FirstOrDefault(x => x.login == txbLogin.Text && x.password == psbPassword.Password);
                if (userObj == null) 
                {
                    MessageBox.Show("Пользователь с таким логином и паролем не найден!","Ошибка авторизации",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                else
                {
                    AppFrame.MainFrame.Navigate(new MainMenu());
                    switch (userObj.idrole)
                    {
                        case 1:
                            Application.Current.MainWindow.Title = userObj.name + " (Менеджер)";
                            //MessageBox.Show("Здравствуйте, менеджер " + userObj.name +"!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 2:
                            Application.Current.MainWindow.Title = userObj.name + " (Продавец)";
                            //MessageBox.Show("Здравствуйте, продавец " + userObj.name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка:" + Ex.Message.ToString(), "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageCreateAcc());
        }
    }
}
