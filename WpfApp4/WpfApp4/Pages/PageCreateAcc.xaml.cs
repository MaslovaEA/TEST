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
    /// Логика взаимодействия для PageCreateAcc.xaml
    /// </summary>
    public partial class PageCreateAcc : Page
    {
        public PageCreateAcc()
        {
            InitializeComponent();
            cmbRole.ItemsSource = ProbaEntities.GetContext().Role.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }

        private void psbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (psbPassword.Password != txbPass.Text)
            {
                btnCreate.IsEnabled = false;
                psbPassword.Background = Brushes.LightCoral;
                psbPassword.BorderBrush = Brushes.Red;                
            }
            else
            {
                btnCreate.IsEnabled = true;
                psbPassword.Background = Brushes.LightGreen;
                psbPassword.BorderBrush = Brushes.Green;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string _NameRole;
            int _idRole;

            if (ProbaEntities.GetContext().Person.Count(x => x.login == txbLog.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            try
            {
                _NameRole = cmbRole.Text;
                var RoleObj = ProbaEntities.GetContext().Role.FirstOrDefault(x => x.NameRole == _NameRole);
                _idRole = RoleObj.id;
                Person userObj = new Person()
                {
                    login = txbLog.Text,
                    name = txbName.Text,
                    password = txbPass.Text,
                    idrole = _idRole
                };
                ProbaEntities.GetContext().Person.Add(userObj);
                ProbaEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                //txbLog.Clear();
                //txbName.Clear();
                //txbPass.Clear();
                //psbPassword.Clear();
                AppFrame.MainFrame.Navigate(new PageLogin());
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
