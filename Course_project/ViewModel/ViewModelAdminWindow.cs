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
    public class ViewModelAdminWindow : INotifyPropertyChanged
    {


        #region Системные данные для выборок

        //кол-во ответов
        private ObservableCollection<string> number_answers = new ObservableCollection<string>()
        { "1","2","3","4","5","0" , "Любое"};

        public ObservableCollection<string> Number_Answers
        {
            get => number_answers;
            set
            {
                number_answers = value;
                OnPropertyChanged("Number_Answers");
            }
        }

        //выбранное значение кол-ва ответов
        private string selectedNumberAnswers;

        public string SelectedNumberAnswers
        {
            get => selectedNumberAnswers;

            set
            {
                selectedNumberAnswers = value;
                OnPropertyChanged("SelectedNumberAnswers");
            }
        }

        //кол-во верных ответов в вопросе
        private string number_correct_ans;

        public string SelectedCorrectAnswers
        {
            get => number_correct_ans;

            set
            {
                number_correct_ans = value;
                OnPropertyChanged("SelectedCorrectAnswers");
            }

        }

        //кол-во вопросов по соответствующим критериям
        private int numberQuestions;

        public int NumberQuestions
        {
            get => numberQuestions;

            set
            {
                numberQuestions = value;
                OnPropertyChanged("NumberQuestions");
            }
        }

        #endregion


        #region Обозреваемые данные 

        //обозрвеваемые коллекции
        private ObservableCollection<Property> properties;
        public ObservableCollection<Property> Properties
        {
            get => properties;

            set
            {
                properties = value;
                OnPropertyChanged("Properties");
            }

        }

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get => questions;

            set
            {
                questions = value;
                OnPropertyChanged("Questions");
            }
        }

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

        private ObservableCollection<Test> tests;
        public ObservableCollection<Test> Tests
        {
            get => tests;

            set
            {
                tests = value;
                OnPropertyChanged("Tests");
            }
        }

        //выбранное свойство для вопросов
        private Property propertyQuestionSelected;
        public Property PropertyQuestionSelected
        {
            get
            {
                return propertyQuestionSelected ??
                     (propertyQuestionSelected = new Property());
            }

            set
            {
                propertyQuestionSelected = value;
                OnPropertyChanged("PropertyQuestionSelected");
            }
        }

        //выбранное свойство для тестов
        private Property propertyTestSelected;
        public Property PropertyTestSelected
        {
            get
            {
                return propertyTestSelected ??
                    (propertyTestSelected = new Property());
            }

            set
            {
                propertyTestSelected = value;
                OnPropertyChanged("PropertyTestSelected");
            }

        }

        //выбранный вопрос
        private Question selectedQuestion;
        public Question SelectedQuestion
        {
            get => selectedQuestion ??
                (selectedQuestion = new Question());

            set
            {
                selectedQuestion = value;
                Mediator.selectedQuestion = value;
                OnPropertyChanged("SelectedQuestion");
            }

        }

        //добавляемый тест
        private Test addingTest;
        public Test AddingTest
        {
            get
            {
                return addingTest ?? (addingTest = new Test());
            }
            set
            {
                addingTest = value;
                OnPropertyChanged("AddingTest");
            }
        }

        //коллекция и данные для загружаемых тестов (да , я знаю , что можно сделать на основе существующих , но мне оч лень)

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

        //выбранное свойство для тестов

        private Property propertyTestSearch;

        public Property PropertyTestSearch
        {
            get => propertyTestSearch;

            set
            {
                propertyTestSearch = value;
                OnPropertyChanged("PropertyTestSearch");
            }

        }






        #endregion


        #region Команды

        private RelayCommand searchQuestionsCommand;
        public RelayCommand SearchQuestionsCommand
        {
            get
            {
                return searchQuestionsCommand ??
                    (searchQuestionsCommand = new RelayCommand(obj =>
                    {
                        SearchQuestions();
                    }));

            }
        }

        private RelayCommand constructorQuestionsCommand;
        public RelayCommand ConstructorQuestionsCommand
        {
            get
            {
                return constructorQuestionsCommand ??
                    (constructorQuestionsCommand = new RelayCommand(obj =>
                    {
                        ConstructorQuestions();
                    }));
            }
        }

        private RelayCommand testParamsCommand;
        public RelayCommand TestParamsCommand
        {
            get
            {
                return testParamsCommand ?? (testParamsCommand =
                    new RelayCommand(obj => TestParams()));
            }
        }

        private RelayCommand addQuestionToTestCommand;

        public RelayCommand AddQuestionToTestCommand
        {
            get => addQuestionToTestCommand ??
                (addQuestionToTestCommand = new RelayCommand(obj =>
                AddQuestionToTest()));
        }

        private RelayCommand addTestCommand;

        public RelayCommand AddTestCommand
        {
            get => addTestCommand ??
                (addTestCommand = new RelayCommand(obj =>
                AddTest()));
        }

        public RelayCommand updateQuestionCommand;

        public RelayCommand UpdateQuestionCommand
        {
            get => updateQuestionCommand ??
                (updateQuestionCommand = new RelayCommand(
                    obj => UpdateQuestion()));
        }

        private RelayCommand searchTestsCommand;
        public RelayCommand SearchTestsCommand
        {
            get => searchTestsCommand ?? (
                searchTestsCommand = new RelayCommand(obj =>
                SearchTests()));


        }


        private RelayCommand editSelectedTestCommand;

        public RelayCommand EditSelectedTestCommand
        {
            get => editSelectedTestCommand ??
                (editSelectedTestCommand = new RelayCommand(
                    obj => EditSelectedTest()));

        }

        private RelayCommand removeQuestionFromDBCommand;

        public RelayCommand RemoveQuestionFromDBCommand
        {
            get => removeQuestionFromDBCommand ?? (
                removeQuestionFromDBCommand = new RelayCommand(obj
                    => RemoveQuestionFromDB()));
        }

        private RelayCommand resultCommand;
        public RelayCommand ResultCommand
        {
            get => resultCommand ??
                (resultCommand = new RelayCommand(obj =>
               GoToResult()));
        }

        private RelayCommand generateTestCommand;

        public RelayCommand GenerateTestCommand
        {
            get => generateTestCommand ?? (
                generateTestCommand = new RelayCommand(obj =>
                GenerateTest()));
        }

        private RelayCommand removeTestFromDBCommand;

        public RelayCommand RemoveTestFromDBCommand
        {
            get => removeTestFromDBCommand ??
                (removeTestFromDBCommand = new RelayCommand(
                    obj => RemoveTestFromDB()));
        }

        private RelayCommand removeQuestionFromTestCommand;

        public RelayCommand RemoveQuestionFromTestCommand
        {
            get => removeQuestionFromTestCommand ??
                (removeQuestionFromTestCommand = new RelayCommand(
                    obj => RemoveQuestionFromTest()));
        }

        private RelayCommand refreshTestCommand;

        public RelayCommand RefreshTestCommand
        {
            get => refreshTestCommand ?? (refreshTestCommand =
                new RelayCommand(obj => RefreshTest()));
        }

        private RelayCommand generateGroupTestCommand;

        public RelayCommand GenerateGroupTestCommand
        {
            get => generateGroupTestCommand ??
                (generateGroupTestCommand = new RelayCommand(obj =>
               GenerateGroupTest()));
        }

        private RelayCommand aboutAppCommand;

        public RelayCommand AboutAppCommand
        {
            get => aboutAppCommand ?? (aboutAppCommand = new RelayCommand
                (obj => MessageBox.Show("Программное обеспечение для прохождения и/или генерации тестов по теме" +
                    "'Объектно-ориентированное программирование' , а также их" +
                    " прохождению . \n Версия 1.0 © 2018. \n Автор: Черняк Николай, ИСиТ-2  ")));
        }


        #endregion


        #region Методы команд

        private void SearchQuestions()
        {

            Questions = new ObservableCollection<Question>();
            /*  TestContext.getContext().Questions.Load();
              Questions = TestContext.getContext().Questions.Local;*/

            List<Question> list = TestContext.getContext().Questions.ToList();

            List<Question> resultQuestions = new List<Question>();

            //ниже - удивительная магия
            var TypeQuestion = new Tuple<bool, List<Question>, string>(false, list, "");
            var NumberAnswers = new Tuple<bool, List<Question>, string>(false, list, "");
            var CorrectAnswers = new Tuple<bool, List<Question>, string>(false, list, "");
            try
            {
                TypeQuestion = new Tuple<bool, List<Question>, string>(
                    PropertyQuestionSelected.ID_Property != 17,
                   list.ToList().FindAll(question => question.ID_Property == PropertyQuestionSelected.ID_Property),
                   "Тип вопроса");


                NumberAnswers = new Tuple<bool, List<Question>, string>(Int32.Parse(SelectedNumberAnswers) != 0,
                    list.ToList().FindAll(question => question.Number_Variant == Int32.Parse(SelectedNumberAnswers)),
                    "Кол-во вариантов ответа(-ов) на вопрос");


                CorrectAnswers = new Tuple<bool, List<Question>, string>(Int32.Parse(SelectedCorrectAnswers) != 0,
                 list.Where(x => x.Answers.Where(answer => answer.Correct == true).Count() == Int32.Parse(SelectedCorrectAnswers)).ToList(),
                 "Кол-во верных ответов в вопросе");

            }

            catch (FormatException e)
            {

            }

            finally
            {
                TypeQuestion = new Tuple<bool, List<Question>, string>(PropertyQuestionSelected != null ||
                   PropertyQuestionSelected.ID_Property != 17,
                  list.ToList().FindAll(question => question.ID_Property == PropertyQuestionSelected.ID_Property),
                  "Тип вопроса");

            }



            List<Tuple<bool, List<Question>, string>> indicators = new List<Tuple<bool, List<Question>, string>>();

            indicators.Add(TypeQuestion);
            indicators.Add(NumberAnswers);
            indicators.Add(CorrectAnswers);


            foreach (Tuple<bool, List<Question>, string> param in indicators)
            {
                if (param.Item1 == true)

                {
                    resultQuestions = param.Item2;

                }
            }

            if (!indicators.TrueForAll(x => x.Item1 == false))
            {

                if (PropertyQuestionSelected.ID_Property != 17)
                {
                    resultQuestions.RemoveAll(x => x.ID_Property != PropertyQuestionSelected.ID_Property);
                }
                if (SelectedNumberAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Number_Variant != Int32.Parse(SelectedNumberAnswers));
                }
                if (SelectedCorrectAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Answers.Where(answer => answer.Correct == true).Count()
                    != Int32.Parse(SelectedCorrectAnswers));
                }
            }
            else
            {
                if (PropertyQuestionSelected.ID_Property != 17)
                {
                    resultQuestions.RemoveAll(x => x.ID_Property != PropertyQuestionSelected.ID_Property);
                }
                if (SelectedNumberAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Number_Variant != Int32.Parse(SelectedNumberAnswers));
                }
                if (SelectedCorrectAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Answers.Where(answer => answer.Correct == true).Count()
                    != Int32.Parse(SelectedCorrectAnswers));
                }
            }

            if (PropertyQuestionSelected.ID_Property == 17 && SelectedNumberAnswers == Number_Answers.Last() &&
                SelectedCorrectAnswers == Number_Answers.Last())
            {
                resultQuestions = list;
            }



            //конец магии
            Questions.Clear();


            foreach (Question que in resultQuestions)
            {
                Questions.Add(que);
            }

            MessageBox.Show("По заданным параметрам найденно:  " + Questions.Count + " вопрос(-ов)");



        }

        private void ConstructorQuestions()
        {
            ConstructorQuestionView window = new ConstructorQuestionView();
            window.Show();
        }

        private void TestParams()
        {
            try
            {
                if (AddingTest.Name_Test == null ||
                    PropertyTestSelected == null ||
                    (AddingTest.Questions_Number == 0 || AddingTest.Questions_Number > 20))
                {
                    MessageBox.Show("Введите все параметры теста, для его корректного" +
                        "создания ", "Предупреждение", MessageBoxButton.OK,
                        MessageBoxImage.Asterisk);
                }
                else
                {
                    using (TestContext context = new TestContext())
                    {
                        if (context.Tests.ToList().Find(x => x.Name_Test == AddingTest.Name_Test) == null)
                        {
                            TestQuestions.Clear();
                            AddingTest.ID_Property = PropertyTestSelected.ID_Property;
                            MessageBox.Show("Параметры теста успешно заданы! Теперь вы можете добавлять вопросы в тест из меню ниже.");
                            TestContext.getContext().Tests.Add(AddingTest);
                            TestContext.getContext().SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Тест с таким именем уже сущетсвует!", "Предупреждение", MessageBoxButton.OK,
                      MessageBoxImage.Asterisk);
                        }
                    }


                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                MessageBox.Show("Тест с таким именем уже существует!");
            }
        }

        private void AddQuestionToTest()
        {
            if (AddingTest.Questions_Number != 0)
            {
             
                if (AddingTest.Questions.Count < AddingTest.Questions_Number && (AddingTest.Questions.ToList()
                    .Find(x => x.ID_Question == SelectedQuestion.ID_Question) == null))
                {
                    AddingTest.Questions.Add(SelectedQuestion);
                    TestQuestions.Add(SelectedQuestion);
                }
                else
                {
                    MessageBox.Show("В тесте не может быть больше заданного количества вопросов , а также " +
                        "повторяющиеся вопросы");
                }
            }
            else
            {
                if (SelectedTest != null)
                {
              
                    if (TestQuestions.Count < SelectedTest.Questions_Number && (SelectedTest.Questions.ToList()
                   .Find(x => x.ID_Question == SelectedQuestion.ID_Question) == null))
                    {
                        AddingTest.Questions.Add(SelectedQuestion);
                        TestQuestions.Add(SelectedQuestion);
                    }
                    else
                    {
                        MessageBox.Show("В тесте не может быть больше заданного количества вопросов , а также " +
                            "повторяющиеся вопросы");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите тест для добавления вопросов!");
                }
            }
        }
        
        private void AddTest()
        {
          int IdTest = TestContext.getContext().Tests.ToList().LastOrDefault().ID_Test;

            try
            {

                if (TestQuestions.First() != null)
                {
                    foreach (Question que in TestQuestions)
                    {
                        SqlParameter idTest = new SqlParameter("@idTest", TestContext.getContext().Tests.ToList().LastOrDefault().ID_Test);
                        SqlParameter idQuestion = new SqlParameter("@idQuestion", que.ID_Question);
                        TestContext.getContext().Database.ExecuteSqlCommand(
                            "insert into Test_Question(ID_Test,ID_Question) values (@idTest,@idQuestion)",
                          idTest, idQuestion);
                    }

                    Test test = TestContext.getContext().Tests.ToList().Find(x => x.ID_Test == IdTest);
                    foreach (Question que in test.Questions)
                    {
                        test.Max_score += que.Score;
                    }

                    test.Min_score = test.Max_score * 0.4f;

                    TestContext.getContext().SaveChanges();


                    MessageBox.Show("Тест успешно добавлен!");
                }

                else
                {
                    MessageBox.Show("Нельзя добавить пустой тест!");
                }
            }

            catch (System.InvalidOperationException e)
            {
                MessageBox.Show("Нельзя добавить пустой тест!");
            }
        }

        private void UpdateQuestion()
        {
            if (Mediator.selectedQuestion != null)
            {
        
                QuestionView window = new QuestionView();
                window.Show();
            }
            else
            {
                MessageBox.Show("Выберите вопрос , для его редактирования!");
            }
        }

        private void SearchTests()
        {
            TestsDB = new ObservableCollection<Test>();
            List<Test> list = TestContext.getContext().Tests.ToList();

            List<Test> resultTests = new List<Test>();

            //ниже - удивительная магия
            var TypeTest = new Tuple<bool, List<Test>, string>(false, list, "");
          //  var NumberAnswers = new Tuple<bool, List<Question>, string>(false, list, "");
          //  var CorrectAnswers = new Tuple<bool, List<Question>, string>(false, list, "");
            try
            {
                TypeTest = new Tuple<bool, List<Test>, string>(
                    PropertyTestSearch.ID_Property != 17,
                   list.ToList().FindAll(test => test.ID_Property == PropertyTestSearch.ID_Property),
                   "Тип теста");


               /* NumberAnswers = new Tuple<bool, List<Question>, string>(Int32.Parse(SelectedNumberAnswers) != 0,
                    list.ToList().FindAll(question => question.Number_Variant == Int32.Parse(SelectedNumberAnswers)),
                    "Кол-во вариантов ответа(-ов) на вопрос");


                CorrectAnswers = new Tuple<bool, List<Question>, string>(Int32.Parse(SelectedCorrectAnswers) != 0,
                 list.Where(x => x.Answers.Where(answer => answer.Correct == true).Count() == Int32.Parse(SelectedCorrectAnswers)).ToList(),
                 "Кол-во верных ответов в вопросе");*/

            }

            catch (FormatException e)
            {

            }

          /*  finally
            {
                TypeQuestion = new Tuple<bool, List<Question>, string>(PropertyQuestionSelected != null ||
                   PropertyQuestionSelected.ID_Property != 10,
                  list.ToList().FindAll(question => question.ID_Property == PropertyQuestionSelected.ID_Property),
                  "Тип вопроса");

            }*/



            List<Tuple<bool, List<Test>, string>> indicators = new List<Tuple<bool, List<Test>, string>>();

            indicators.Add(TypeTest);
           /* indicators.Add(NumberAnswers);
            indicators.Add(CorrectAnswers);*/


            foreach (Tuple<bool, List<Test>, string> param in indicators)
            {
                if (param.Item1 == true)

                {
                    resultTests = param.Item2;

                }
            }

            if (!indicators.TrueForAll(x => x.Item1 == false))
            {

                if (PropertyTestSearch.ID_Property != 17)
                {
                    resultTests.RemoveAll(x => x.ID_Property != PropertyTestSearch.ID_Property);
                }
             /*   if (SelectedNumberAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Number_Variant != Int32.Parse(SelectedNumberAnswers));
                }
                if (SelectedCorrectAnswers != Number_Answers.Last())
                {
                    resultQuestions.RemoveAll(x => x.Answers.Where(answer => answer.Correct == true).Count()
                    != Int32.Parse(SelectedCorrectAnswers));
                }*/
            }
            else
            {

                if (PropertyTestSearch.ID_Property != 17)
                {
                    resultTests.RemoveAll(x => x.ID_Property != PropertyTestSearch.ID_Property);
                }

                /* if (SelectedNumberAnswers != Number_Answers.Last())
                 {
                     resultQuestions.RemoveAll(x => x.Number_Variant != Int32.Parse(SelectedNumberAnswers));
                 }
                 if (SelectedCorrectAnswers != Number_Answers.Last())
                 {
                     resultQuestions.RemoveAll(x => x.Answers.Where(answer => answer.Correct == true).Count()
                     != Int32.Parse(SelectedCorrectAnswers));
                 }*/
            }

           if (PropertyTestSearch.ID_Property == 17)
            {
                resultTests = list;
            }



            //конец магии
            TestsDB.Clear();


            foreach (Test test in resultTests)
            {
                TestsDB.Add(test);
            }
        
            MessageBox.Show("По запросу найдено " + TestsDB.Count + " тест(-ов)");

        }

        private void EditSelectedTest()
        {
            if (SelectedTest != null)
            {
                TestQuestions.Clear();
                foreach (Question que in SelectedTest.Questions)
                {
                    TestQuestions.Add(que);

                }
            }

            else
            {
                MessageBox.Show("Выберите тест для его редактирования!");
            }
        }

        private void RemoveQuestionFromDB()
        {
            try
            {
                if (SelectedQuestion != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить вопрос из базы данных? Это действие нельзя будет отменить.", "Предупреждение" +
                         "", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        foreach (Test test in SelectedQuestion.Tests)
                        {
                            test.Questions_Number--;
                        }

                        SqlParameter idQuestion = new SqlParameter("@idQuestion", SelectedQuestion.ID_Question);
                        TestContext.getContext().Database.ExecuteSqlCommand(" exec DeleteQuestion @idQuestion", idQuestion);
                        /* foreach (Answer answer in SelectedQuestion.Answers)
                         {
                             TestContext.getContext().Answers.Remove(answer);
                             TestContext.getContext().SaveChanges();
                         }*/

                        TestContext.getContext().Questions.Remove(SelectedQuestion);
                        TestContext.getContext().SaveChanges();
                        MessageBox.Show("Вопрос удален");
                        SearchQuestions();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите вопрос для его удаления!");
                }
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                using (TestContext context = new TestContext())
                {
                    context.Questions.Attach(SelectedQuestion);
                    context.Questions.Remove(SelectedQuestion);
                    context.SaveChanges();
                    context.Dispose();
                    MessageBox.Show("Вопрос удален");
                    SearchQuestions();
                }

            }
            catch (System.InvalidOperationException e)
            {

            }


        }

        private void GoToResult()
        {
            ResultView window = new ResultView();
            window.Show();
        }

        private void RemoveTestFromDB()
        {
            try
            {
                if (SelectedTest != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы " +
                        "точно хотите удалить тест из базы данных?  Вместе с тестом будут " +
                        "удалены и его результаты учащихся .Это действие нельзя будет отменить.", "Предупреждение" +
                         "", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                       

                            SqlParameter idTest = new SqlParameter("@idTest", SelectedTest.ID_Test);
                            SqlParameter NameTest = new SqlParameter("@NameTest", SelectedTest.Name_Test);
                            TestContext.getContext().Database.ExecuteSqlCommand(" exec DeleteTest @idTest, @NameTest", idTest, NameTest);
                            TestContext.getContext().Tests.Remove(SelectedTest);
                            TestContext.getContext().SaveChanges();
                            MessageBox.Show("Тест удален");
                            SearchTests();
                            TestQuestions.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите тест для его удаления!");
                }
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                try
                {
                    using (TestContext context = new TestContext())
                    {
                        if (SelectedTest.Questions.ToList().SequenceEqual(TestQuestions))
                        {
                            TestQuestions.Clear();
                        }
    
                        context.Tests.Attach(SelectedTest);
                        context.Tests.Remove(SelectedTest);
                        context.SaveChanges();
                        context.Dispose();
                        MessageBox.Show("Тест удален");
                        SearchTests();
                    }
                }

                catch(System.InvalidOperationException exp)
                {

              
                }

            }

        }


        private void RemoveQuestionFromTest()
        {
            if (SelectedQuestion != null && SelectedTest != null)
            {
                SelectedTest.Questions.Remove(SelectedQuestion);

                SelectedTest.Max_score -= SelectedQuestion.Score;

                SelectedTest.Min_score = SelectedTest.Max_score * 0.4f;

                EditSelectedTest();

                TestContext.getContext().SaveChanges();
            }
                
        }

        private void RefreshTest()
        {
            if (SelectedTest != null)
            {
               
                //вопросы для основной части - 80%
                List<Question> questions_1 = TestContext.getContext().Questions.ToList().FindAll(
                    x => x.ID_Property == SelectedTest.ID_Property);
                //случайные вопросы - 20%
                List<Question> questions_2 = TestContext.getContext().Questions.ToList();



              
                //выделение вопросов , которые уже были в этом тесте
                foreach (Question que in questions_1.ToArray())
                {
                    foreach (Question qu in SelectedTest.Questions)
                    {
                        if (que.ID_Question == qu.ID_Question)
                        {
                            questions_1.Remove(qu);
                        }
                    }
                }

                SelectedTest.Questions.Clear();


                double number = SelectedTest.Questions_Number * 0.8;

                if (Math.Round(number) > questions_1.Count)
                {
                    MessageBoxResult result = MessageBox.Show("В базе данных недостаточно вопросов, чтобы сгенерировать 80%" +
                          " теста  по конкретно заданому параметру. " +
                          "Вы можете добавить вопросы по этой тематике в меню ниже",
                          "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);

                }

                else
                {
                  
                    //рандомная выборка из вопросов по заданной теме (80% вопросов)
                    Random rand = new Random();
                    List<int> AllQuestions = new List<int>();


                    foreach (Question que in questions_1)
                    {
                        AllQuestions.Add(que.ID_Question);
                    }

                    List<int> RandomQuestions = AllQuestions.OrderBy(x => rand.Next()).ToList();

                    int[] idQuestions = RandomQuestions.ToArray();

                    Array.Resize(ref idQuestions, (int)Math.Round(number));

                    for (int i = 0; i < idQuestions.Length; i++)
                    {
                        SelectedTest.Questions.Add(questions_1.Find(x => x.ID_Question == idQuestions[i]));
                    }
                    //рандомная генерация остальных вопросов

                    Random rand_2 = new Random();

                    AllQuestions.Clear();
                    RandomQuestions.Clear();

                    foreach (Question que in questions_2)
                    {
                        AllQuestions.Add(que.ID_Question);
                    }

                    RandomQuestions = AllQuestions.OrderBy(x => rand_2.Next()).ToList();

                    foreach (Question que in questions_2)
                    {
                        AllQuestions.Add(que.ID_Question);
                    }

                    idQuestions = RandomQuestions.ToArray();

                    Array.Resize(ref idQuestions, (int)Math.Round(SelectedTest.Questions_Number * 0.2));

                    for (int i = 0; i < idQuestions.Length; i++)
                    {
                        SelectedTest.Questions.Add(questions_2.Find(x => x.ID_Question == idQuestions[i]));
                    }



                    if (SelectedTest.Questions.Count < AddingTest.Questions_Number)
                    {
                        MessageBox.Show("Алгоритм сгенерировал 80% теста по заданной теме, но не хватило вопросов из других тем." +
                            "В связи с этим количество вопросов меньше заданного. Пожалуйста , добавьте больше вопросов ," +
                            "для корректной работы алгоритма.");

                        AddingTest.Questions_Number = AddingTest.Questions.Count;
                    }

                    else
                    {

                        MessageBox.Show("Тест успешно обновлен!");
                    }
                    foreach (Question que in AddingTest.Questions)
                    {
                        AddingTest.Max_score += que.Score;
                    }

                    AddingTest.Min_score = AddingTest.Max_score * 0.4f;
                    try
                    {

                        TestContext.getContext().SaveChanges();
                        TestQuestions.Clear();


                        EditSelectedTest();
                    }

                    catch (System.InvalidOperationException e)
                    {

                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                    {

                    }
               
                }
            }
            else
            {
                MessageBox.Show("Выберите тест для его обновления");
            }
        }

        private void GenerateGroupTest()
        {


            if (AddingTest.Name_Test == null ||
                PropertyTestSelected == null ||
                (AddingTest.Questions_Number == 0 || AddingTest.Questions_Number > 20))
            {
                MessageBox.Show("Введите все параметры теста, для его корректного" +
                    "создания ", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }

            else
            {
                Property groupProp = PropertyTestSelected;
                string Name_Test = AddingTest.Name_Test;
                int number = AddingTest.Questions_Number;


                for (int i = 0; i < 3; i++)
                {

                    AddingTest.Name_Test = null;
                    AddingTest.Name_Test += " " + Name_Test + " Вариант № " + (i + 1);
                    AddingTest.Property = groupProp;
                    AddingTest.Questions_Number = number;
                    GenerateTest();


                }
                MessageBox.Show("Сгенерировано 3 варианта теста. Найти их вы можете в меню поиска слева.");
            }
        }

        private void GenerateTest()
        {
            if (AddingTest.Name_Test == null ||
                PropertyTestSelected == null ||
                (AddingTest.Questions_Number == 0 || AddingTest.Questions_Number > 20))

            {
                MessageBox.Show("Введите все параметры теста, для его корректного" +
                    "создания ", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Asterisk);
            }

            else
            {
                try
                {

                    AddingTest.ID_Property = PropertyTestSelected.ID_Property;
                    //вопросы для основной части - 80%
                    List<Question> questions_1 = TestContext.getContext().Questions.ToList().FindAll(
                        x => x.ID_Property == PropertyTestSelected.ID_Property);
                    //случайные вопросы - 20%
                    List<Question> questions_2 = TestContext.getContext().Questions.ToList();

                    double number = AddingTest.Questions_Number * 0.8;

                    if (Math.Round(number) > questions_1.Count)
                    {
                        MessageBoxResult result = MessageBox.Show("В базе данных" +
                            " недостаточно вопросов, чтобы сгенерировать 80%" +
                              " теста  по конкретно заданому параметру. " +
                              "Вы можете добавить вопросы по этой тематике в меню ниже",
                              "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);

                    }

                    else
                    {
                        TestContext.getContext().Tests.Add(AddingTest);
                        TestContext.getContext().SaveChanges();
                        //рандомная выборка из вопросов по заданной теме (80% вопросов)
                        Random rand = new Random();
                        List<int> AllQuestions = new List<int>();

                        foreach (Question que in questions_1)
                        {
                            AllQuestions.Add(que.ID_Question);
                        }

                        List<int> RandomQuestions = AllQuestions.OrderBy(x => rand.Next()).ToList();

                        int[] idQuestions = RandomQuestions.ToArray();

                        Array.Resize(ref idQuestions, (int)Math.Round(number));

                        for (int i = 0; i < idQuestions.Length; i++)
                        {
                            AddingTest.Questions.Add(questions_1.Find(x => x.ID_Question == idQuestions[i]));
                        }
                        //рандомная генерация остальных вопросов

                        Random rand_2 = new Random();

                        AllQuestions.Clear();
                        RandomQuestions.Clear();

                        foreach (Question que in questions_2)
                        {
                            AllQuestions.Add(que.ID_Question);
                        }
                        RandomQuestions = AllQuestions.OrderBy(x => rand_2.Next()).ToList();

                        foreach (Question que in questions_2)
                        {
                            AllQuestions.Add(que.ID_Question);
                        }

                        idQuestions = RandomQuestions.ToArray();

                        Array.Resize(ref idQuestions, (int)Math.Round(AddingTest.Questions_Number * 0.2));

                        for (int i = 0; i < idQuestions.Length; i++)
                        {
                            AddingTest.Questions.Add(questions_2.Find(x => x.ID_Question == idQuestions[i]));
                        }

                        if (AddingTest.Questions.Count < AddingTest.Questions_Number)
                        {
                            MessageBox.Show("Алгоритм сгенерировал 80% теста по заданной теме, но не хватило вопросов из других тем." +
                                "В связи с этим количество вопросов меньше заданного. Пожалуйста , добавьте больше вопросов ," +
                                "для корректной работы алгоритма.");

                            AddingTest.Questions_Number = AddingTest.Questions.Count;
                        }

                        else
                        {

                            MessageBox.Show("Тест успешно сгенерирован!");
                        }
                        foreach (Question que in AddingTest.Questions)
                        {
                            AddingTest.Max_score += que.Score;
                        }

                        AddingTest.Min_score = AddingTest.Max_score * 0.4f;

                        TestContext.getContext().SaveChanges();
                        TestQuestions.Clear();

                        SelectedTest = AddingTest;
                        EditSelectedTest();
                        AddingTest = null;
                    }
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    MessageBox.Show("В базе данных уже есть тест с таким названием, пожалуйста , выберите другое");
                }
            }
        }
        #endregion


        public ViewModelAdminWindow()
        {
 
            //получение контекста
            TestContext.getContext();
            //заполнение всех полей для типов вопросов/тестов
            Properties = new ObservableCollection<Property>();
            Properties.Add(propertyQuestionSelected);
            Properties.Add(propertyTestSelected);
            Properties.Add(propertyTestSearch);
            TestContext.getContext().Properties.Load();
            Properties = TestContext.getContext().Properties.Local;
            //для нового теста
            tests = new ObservableCollection<Test>();
            tests.Add(addingTest);
            testQuestions = new ObservableCollection<Question>();

            //Начальные значения в поисковых окнах
            PropertyQuestionSelected = Properties.ToList().Last();
            SelectedCorrectAnswers = Number_Answers.Last();
            SelectedNumberAnswers = Number_Answers.Last();
            PropertyTestSearch = Properties.ToList().Last();
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            TestContext.getContext().Dispose();

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
