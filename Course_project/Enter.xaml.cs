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

namespace Course_project
{
    /// <summary>
    /// Логика взаимодействия для Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        public Enter()
        {
            InitializeComponent();
            DataContext = new ViewModelUser(this);
           
        }



        #region Enter
        private void Enter_System(object sender, RoutedEventArgs e)
        {

            using (TestContext context = new TestContext())
            {
                if (context.User_System.ToList().Find(user => user.AdminRights == true) != null)
                {

                    if (context.User_System.ToList().Find(x => x.Login_User == Login.Text &&
                         x.Password_User == Password.Password) == null)
                    {
                        MessageBox.Show("Ошибка", "Проверьте поля логина и пароля!", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }

                    else
                    {
                        MessageBox.Show("Welcome!");
                        if (context.User_System.ToList().Find(x => x.Login_User == Login.Text)
                            .AdminRights == true)
                        {


                            Mediator.activeUser = TestContext.getContext().User_System.ToList().Find(x => x.Login_User == Login.Text);
                            context.Dispose();
                            AdminWindowView constructor = new AdminWindowView();
                            constructor.Show();
                            this.Close();


                        }
                        else
                        {
                            Mediator.activeUser = context.User_System.ToList().Find(x => x.Login_User == Login.Text);
                            context.Dispose();
                            UserWindowView testsSystem = new UserWindowView();
                            testsSystem.Show();
                            this.Close();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Поздравляем с первым запуском программы! Для регистрации ааккаунта администратора перейдите " +
                        "в окно регистрации по кнопке ниже.");
                }
            }

            #endregion



        }
    }
}
