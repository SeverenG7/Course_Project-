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

namespace Course_project
{
    public class ViewModelResult : INotifyPropertyChanged
    {

        #region Обозреваемые данные

        private ObservableCollection<Result> results;

        public ObservableCollection<Result> Results
        {
            get => results;

            set
            {
                results = value;
                OnPropertyChanged("Results");
            }
        }

        private ObservableCollection<UserInfo> students;
        public ObservableCollection<UserInfo> Students
        {
            get => students;

            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        private ResultUser selectedStudent;
        public ResultUser SelectedStudent
        {
            get => selectedStudent;

            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        private ObservableCollection<Result> selectedResults;
        public ObservableCollection<Result> SelectedResults
        {
            get => selectedResults;

            set
            {
                selectedResults = value;
                OnPropertyChanged("SelectedResults");
            }
        }

        private Result selectedResult;

        public Result SelectedResult
        {
            get => selectedResult;

            set
            {
                selectedResult = value;
                OnPropertyChanged("SelectedResult");
            }

        }


        private ObservableCollection<Test> testsDB;

        public ObservableCollection<Test> TestsDB
        {
            get => testsDB;

            set
            {
                testsDB = value;
                OnPropertyChanged("TestsDB");
            }
        }

        private UserInfo groupSort  = new UserInfo();

        public UserInfo GroupSort
        {
            get => groupSort;

            set
            {
                groupSort = value;
                OnPropertyChanged("GroupSort");
            }
        }

        private ObservableCollection<UserInfo> sortedStudents;

        public ObservableCollection<UserInfo> SortedStudents
        {
            get => sortedStudents;

            set
            {
                sortedStudents = value;
                OnPropertyChanged("SortedStudents");
            }
        }


        //комбинированная коллекция для полного просмотра информации
        private ObservableCollection<ResultUser> dataResultsFull;

        public ObservableCollection<ResultUser> DataResultsFull
        {
            get => dataResultsFull;

            set
            {
                dataResultsFull = value;
                OnPropertyChanged("DataResultsFull");
            }

        }

        private Test selectedTest;

        public Test SelectedTest
        {
            get => selectedTest;

            set
            {
                selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        

     



        #endregion

        #region Команды

        private RelayCommand showResultCommand;

        public RelayCommand ShowResultCommand
        {
            get => showResultCommand ?? (showResultCommand =
                new RelayCommand(obj => ShowResult()));

        }

        private RelayCommand sortByGroupcommand;

        public RelayCommand SortByGroupCommand
        {
            get => sortByGroupcommand ?? (sortByGroupcommand = new RelayCommand(
                obj => SortByGroup()));
        }

        private RelayCommand showResultTestCommand;

        public RelayCommand ShowResultTestCommand
        {
            get => showResultTestCommand ?? (showResultTestCommand = new
                RelayCommand(obj => ShowResultTest()));
        }

        private RelayCommand addPermissionCommand;

        public RelayCommand AddPermissionCommand
        {
            get => addPermissionCommand ?? (addPermissionCommand = new RelayCommand(
                obj => AddPermission()));
          
        }

        private RelayCommand removePermissionCommand;

        public RelayCommand RemovePermissionCommand
        {
            get => removePermissionCommand ?? (removePermissionCommand =
                new RelayCommand(obj => RemovePermission()));

        }

        private RelayCommand addGroupPremissionCommand;

        public RelayCommand AddGroupPremissionCommand
        {
            get => addGroupPremissionCommand ??(addGroupPremissionCommand = new RelayCommand(
                obj => AddGroupPremission()));
        }

        #endregion

        #region Методы команд

        private void ShowResult()
        {
            SelectedResults.Clear();
            if (SelectedStudent != null)
            {
                foreach (Result result in Results)
                {
                    if (result.Login_User == SelectedStudent.Login_User)
                    {
                        SelectedResults.Add(result);
                    }
                }
            }

            else
            {
                MessageBox.Show("Для выполнения этой функции сначала выберите студента из предоставленного списка студентов!" 
                    ,"Информация" , MessageBoxButton.OK ,MessageBoxImage.Asterisk);
            }
        }

        private void SortByGroup()
        {
            //SortedStudents.Clear();
            DataResultsFull.Clear();

            if (GroupSort.Group_User == 0)
            {
                foreach (UserInfo ui in Students)
                {
                    DataResultsFull.Add(new ResultUser
                    {
                        Login_User = ui.Login_User,
                        FirstName_User = ui.FirstName_User
                    ,
                        LastName_User = ui.LastName_User,
                        Group_User = ui.Group_User
                    });
                }

            }
            else
            {
                foreach (UserInfo ui in Students)
                {
       
                    if (ui.Group_User == GroupSort.Group_User)
                    {
                        DataResultsFull.Add(new ResultUser
                        {
                            Login_User = ui.Login_User,
                            FirstName_User = ui.FirstName_User
                    ,
                            LastName_User = ui.LastName_User,
                            Group_User = ui.Group_User
                        });
                    }
                }
            }
        }

        private void ShowResultTest()
        {
            if (SelectedTest != null)
            {
                MessageBox.Show("" + SelectedTest.Name_Test);
                foreach (ResultUser ru in DataResultsFull)
                {
                    foreach (Result result in Results)
                    {
                        if (ru.Login_User == result.Login_User)
                        {
                            if (result.Name_Test == SelectedTest.Name_Test)
                            {
                                ru.Name_Test = SelectedTest.Name_Test;
                                ru.Score_Result = result.Score_Result;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите тест из списка , для того что проверить результаты группы по нему!");
            }
        }

        private void AddPermission()
        {

            using (TestContext context = new TestContext())
            {

                if (context.Results.ToList().
                    Find(x => x.Name_Test == SelectedTest.Name_Test &&
                    x.Login_User == SelectedStudent.Login_User) == null
                    )
                {

                  context.Results.Add(new Result
                    {
                        Login_User = SelectedStudent.Login_User,
                        Name_Test =
                    SelectedTest.Name_Test,
                        Description_Result = SelectedStudent.Description_Result,
                    });
         
                 
                    context.Results.Load();
                    Results = context.Results.Local;
                    context.SaveChanges();
                    context.Dispose();
                    MessageBox.Show("Разрешение успешно добавлено! При входе в систему он будет извещен " +
                       "о прохождении нового теста");
                    ShowResult();
                }

                else
                {
                    MessageBox.Show("Такое разрешение уже существует для текущего студента!");
                }
            }
        }

        private void RemovePermission()
        {
            
            if (SelectedResult != null)
            {
                using (TestContext context = new TestContext())
                {
                    SqlParameter idResult = new SqlParameter("@idResult", SelectedResult.ID_Result);
                   // context.Database.ExecuteSqlCommand(" delete from Result where ID_Result = @idResult", idResult);
                    context.Results.Attach(SelectedResult);
                    context.Results.Remove(SelectedResult);
        
                    context.Results.Load();
                    Results = context.Results.Local;
                    context.SaveChanges();
                    context.Dispose();
                    ShowResult();
                    MessageBox.Show("Удалено");
                }
            }
            else
            {
                MessageBox.Show("Выберите разрешение для удаления из списка разрешений студента");
            }
        }

        private void AddGroupPremission()
        {
            using (TestContext context = new TestContext())
            {
                List<UserInfo> students = context.UserUnfoes.Where(x => x.Group_User == GroupSort.Group_User).ToList();


                foreach (UserInfo ui in students)
                {
                    
                    
                        if (context.Results.ToList().
                            Find(x => x.Name_Test == SelectedTest.Name_Test &&
                            x.Login_User == ui.Login_User) == null
                            )
                        {

                            context.Results.Add(new Result
                            {
                                Login_User = ui.Login_User,
                                Name_Test =
                              SelectedTest.Name_Test,
                            });


                            context.Results.Load();
                            Results = context.Results.Local;
                            context.SaveChanges();
                     
           
                        }

                        else
                        {
                            MessageBox.Show("Такое разрешение уже существует для  студента " + ui.Login_User + " Оно не будет измененно" +
                                "а останется прежним.");
                        }
                }
            }
            MessageBox.Show("Разрешения на прохождение " + SelectedTest.Name_Test+ " группе " + GroupSort.Group_User +" " +
                "выданы!");

        }

        #endregion

        public ViewModelResult()
        {
            Results = new ObservableCollection<Result>();
            TestContext.getContext().Results.Load();
            Results = TestContext.getContext().Results.Local;
            SelectedResults = new ObservableCollection<Result>();

            TestsDB = new ObservableCollection<Test>();
            TestContext.getContext().Tests.Load();
            TestsDB = TestContext.getContext().Tests.Local;

     

            Students = new ObservableCollection<UserInfo>();
            TestContext.getContext().UserUnfoes.Load();
            Students = TestContext.getContext().UserUnfoes.Local;

            SortedStudents = new ObservableCollection<UserInfo>();
            DataResultsFull = new ObservableCollection<ResultUser>();




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

