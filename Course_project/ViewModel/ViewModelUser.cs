using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace Course_project
{
    public class ViewModelUser : INotifyPropertyChanged
    {
        
        //окно
        private Window authorizationWindow;

        #region Обозреваемые данные

        //необходимый набор данных
        private User_System user = new User_System();
        private UserInfo userInfo = new UserInfo();
        private RelayCommand addUserCommand;
        private RelayCommand enterCommand;
        private RelayCommand registerWinndowCommand;

        //виидимые коллекции для привязки к view
        public ObservableCollection<User_System> users;
        public ObservableCollection<UserInfo> userInfos;
      


        #endregion

        #region Команды

       /* public RelayCommand EnterSystem
        {
            get
            {
                return enterCommand ??
                    (enterCommand = new RelayCommand(obj =>
                    {
                        Enter();
                    }));
            }
        }*/
        public RelayCommand AddUser
        {
            get
            {
                return addUserCommand ??
                    (addUserCommand = new RelayCommand(obj =>
                    {
                        AddNewUser();
                    }));
            }
        }

        public RelayCommand GoToRegisterWindow
        {
            get
            {
                return registerWinndowCommand ??
                    (registerWinndowCommand = new RelayCommand(obj =>
                    {
                        GoToRegister();
                    }));
            }
        }

        //binding- свойства
        public User_System AddingUser
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("AddingUser");
            }
        }

        public UserInfo AddingUserInfo
        {
            get => userInfo;

            set
            {
                userInfo = value;
                OnPropertyChanged("AddingUserInfo");
            }
        }

        #endregion

        #region Методы команд


        //методы комманд
        public void AddNewUser()
        {
            try
            {

                userInfo.Login_User = user.Login_User;
              
                 
                    if (TestContext.getContext().User_System.ToList().Find(x => x.Login_User == AddingUser.Login_User) == null)
                    {
                        string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{9,}$";

                        if (!Regex.IsMatch(user.Password_User, pattern))
                        {
                            MessageBox.Show("Пароль обязательно должен содержать цифры и буквы латинского алфавита в верхнем и нижнем " +
                                "регистре, а также не быть короче 9-ти символо", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            TestContext.getContext().User_System.Add(user);
                            TestContext.getContext().UserUnfoes.Add(userInfo);
                            TestContext.getContext().SaveChanges();

                            MessageBox.Show("Регистрация завершена. Вы можете войти в систему через окно авторизации.");
                            this.authorizationWindow.Close();
                        }
                    }
                    else
                    {

                        MessageBox.Show("Error");
                    }
                

               
            }


            catch (Exception e)
            {
                MessageBox.Show("" + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        

         
            
        }

        public void GoToRegister()
        {
            Register window = new Register();
            window.Show();
        }

      
        #endregion

        //конструктор
        public ViewModelUser( Window authorization)
        {
            authorizationWindow = authorization;
            users = new ObservableCollection<User_System>();
            userInfos = new ObservableCollection<UserInfo>();
            userInfos.Add(userInfo);
            users.Add(user);
            
        }

        #region Реализация интерфейса


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
