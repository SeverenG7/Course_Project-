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
using Microsoft.Win32;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;


namespace Course_project
{
    public class ViewModelUserWindow
    {
        #region Обозреваемые данные

        private ObservableCollection<Test> currentTests;

        public ObservableCollection<Test> CurrentTests
        {
            get => currentTests;

            set
            {
                currentTests = value;
                OnPropertyChanged("CurrentTests");
            }
        }

        private User_System activeUser = new User_System();

        public User_System ActiveUser
        {
            get => activeUser;

            set
            {
                activeUser = Mediator.activeUser;
            }
        }

        private ObservableCollection<Result> userResults;

        public ObservableCollection<Result> UserResults
        {
            get => userResults;

            set
            {
                userResults = value;
                OnPropertyChanged("UserResults");
            }
        }

        //выбранный тест
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

        //загружаемые тесты
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


        //вопросы проходимого теста - выгруженные 
        private ObservableCollection<Question> testQuestions;

        public ObservableCollection<Question> TestQuestions
        {
            get
            {
                return testQuestions ??
                    (testQuestions = new ObservableCollection<Question>());
            }
            set
            {
                testQuestions = value;
                OnPropertyChanged("TestQuestions");
            }
        }

        //текущий пользователь

        private ObservableCollection<UserInfo> user;

        public ObservableCollection<UserInfo> User
        {
            get => user;

            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        //текущий результат 
        private Result currentResult;


        private ObservableCollection<Result> endedResults;

        public ObservableCollection<Result> EndedResults
        {
            get => endedResults;

            set
            {
                endedResults = value;
                OnPropertyChanged("EndedResults");
            }

        }




        #endregion


        #region Команды

        private RelayCommand startTestCommand;

        public RelayCommand StartTestCommand
        {
            get => startTestCommand ?? (startTestCommand = new RelayCommand(
                obj => StartTest()));
        }

        private RelayCommand endTestCommand;

        public RelayCommand EndTestCommand
        {
            get => endTestCommand ?? (endTestCommand = new RelayCommand(
                obj => EndTest()));
        }


        #endregion

        #region Методы команд 


        private void StartTest()
        {

           
            if (SelectedTest != null)
                {
                TestQuestions.Clear();
                foreach (Question que in SelectedTest.Questions)
                {
                    TestQuestions.Add(que);
                }

                foreach (Question question in TestQuestions)
                {

                    foreach (Answer answer in question.Answers)
                    {
                        answer.Correct = false;

                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите тест для прохождения , если таковой имеется!");
            }
        }

        private void EndTest()
        {

            if (SelectedTest != null)
            {
                
                double TestResult = 0;

                currentResult = UserResults.ToList().Find(x => x.Name_Test == SelectedTest.Name_Test);
                currentResult.Score_Result = 0;

                foreach (Question question in TestQuestions)
                {

                    if (question.Property.Difficult == "Обычный")
                    {
                        int counter = 0;
                        foreach (Answer answer in question.Answers)
                        {
                            bool check = answer.Correct;
                            using (TestContext context = new TestContext())
                            {
                                if (check == context.Answers.ToList().Find(x => x.ID_Answer == answer.ID_Answer).Correct)
                                {
                                    counter++;
                                }
                                context.Dispose();

                            }
                        }
                        if (counter == question.Number_Variant)
                        {
                            TestResult += question.Score;
                        }
                    }

                    else
                    {
                        int counter = 0;
                        foreach (Answer answer in question.Answers)
                        {
                            bool check = answer.Correct;
                            using (TestContext context = new TestContext())
                            {
                                if (check == context.Answers.ToList().Find(x => x.ID_Answer == answer.ID_Answer).Correct)
                                {
                                    counter++;
                                }
                                context.Dispose();

                            }
                        }
                        TestResult += question.Score;
                    }

                }

                currentResult.Score_Result = (int)Math.Round((TestResult * 100 / SelectedTest.Max_score));
                if (currentResult.Score_Result < 40)
                {
                    MessageBox.Show("Ваш результат : " + currentResult.Score_Result + " %" + "Вы не набарали минимальный балл");
                }

                else
                {
                    if (currentResult.Score_Result > 40 && currentResult.Score_Result < 80)
                    {
                        MessageBox.Show("Ваш результат : " + currentResult.Score_Result + " %" + "Вы набарали выше минимального балла!");
                    }
                    if (currentResult.Score_Result > 80)
                    {
                        MessageBox.Show("Ваш результат : " + currentResult.Score_Result + " %" + "Вы сдали тест на отлично!");
                    }

                }
                using (TestContext context = new TestContext())
                {
                     //context.Results.Attach(currentResult);
                    if (
                        context.Results.ToList().Find(x => x.Name_Test ==
                        SelectedTest.Name_Test) != null)
                    {
                        context.Results.ToList().Find(x => x.Name_Test ==
                        SelectedTest.Name_Test).Score_Result = currentResult.Score_Result;
                        context.SaveChanges();
                        context.Dispose();
                        MessageBox.Show("Результат сохранён");
                    }
                    else
                    {
                        MessageBox.Show("Result не найдет");
                    }

                    context.Dispose();
                }
         
                CurrentTests.Remove(SelectedTest);
                TestQuestions.Clear();
                EndedResults.Add(currentResult);
                currentResult = null;
                TestContext.getContext().SaveChanges();
            }
            else
            {
                MessageBox.Show("Выберите тест для прохождения!");
            }
        }


        #endregion

        public ViewModelUserWindow()
        {

            TestContext.getContext().Results.Load();
            UserResults = TestContext.getContext().Results.Local;
            TestContext.getContext().Tests.Load();
            TestsDB = TestContext.getContext().Tests.Local;
            CurrentTests = new ObservableCollection<Test>();
            User = new ObservableCollection<UserInfo>();



            User.Add(TestContext.getContext().UserUnfoes.ToList().Find(x => x.Login_User == Mediator.ActiveUser().Login_User));

            foreach (Result result in UserResults.ToArray())
            {
                if (result.Login_User != Mediator.activeUser.Login_User)
                {
                    UserResults.Remove(result);
                }
            }

            EndedResults = new ObservableCollection<Result>();

            foreach (Result result in UserResults)
            {
                if (result.Score_Result != null)
                {
                    EndedResults.Add(result);
                }
            }



            foreach (Test test in TestsDB)
            {
                if (UserResults.ToList().Find(x => x.Name_Test == test.Name_Test && x.Score_Result
                == null) != null)
                {

                    CurrentTests.Add(test);
                }
            }
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {

            MessageBox.Show("Removed");
        }
        //импровизационный код , осторожно

        private BitmapImage ConvertByteArrayToImage(byte[] array)
        {
            if (array != null)
            {
                using (var ms = new MemoryStream(array, 0, array.Length))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            return null;
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
