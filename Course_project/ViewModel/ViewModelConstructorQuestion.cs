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
    public class ViewModelConstructorQuestion : INotifyPropertyChanged
    {
        Window questionWindow;
        //необходимые обозреваемые коллекции 
        #region Обозрвеваемые данные
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
        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {

            get => answers;
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }


        //необходимые элементы для загрузки для добавления
        private Property property = new Property();
        public Property SelectedProperty
        {
            get => property;

            set
            {
                property = value;
                OnPropertyChanged("SelectedProperty");
            }
        }

        private Question question = new Question();
        public Question AddingQuestion
        {
            get
            {
                return question ?? (new Question());
            }
            set
            {
                question = value;
                OnPropertyChanged("AddingQuestion");
            }
        }

        private Answer selectedAnswer = new Answer();
        public Answer SelectedAnswer
        {
            get => selectedAnswer;
            set
            {
                selectedAnswer = value;
                OnPropertyChanged("SelectedAnswer");
            }

        }

        #endregion

        #region Команды

        RelayCommand addQuestionCommand;
        public RelayCommand AddQuestionCommand
        {
            get
            {
                return addQuestionCommand ??
                    (addQuestionCommand = new RelayCommand(obj =>
                    {
                        AddQuestion();
                    }));
            }

        }

        RelayCommand removeAnswerCommand;

        public RelayCommand RemoveAnswerCommand
        {
            get => removeAnswerCommand ?? (removeAnswerCommand =
                new RelayCommand(obj => RemoveAnswer()));
        }

        RelayCommand addPictureCommand;

        public RelayCommand AddPictureCommand
        {
            get => addPictureCommand ?? (
                addPictureCommand = new RelayCommand(
                    obj => AddPicture()));
        }


        RelayCommand removePictureCommand;

        public RelayCommand RemovePictureCommand
        {
            get => removePictureCommand ??
                (removePictureCommand = new RelayCommand(
                    obj => RemovePicture()));
        }

        #endregion

        #region Методы команд
        private void AddQuestion()
        {
            if (Answers.Count <= 5 || Answers.ToList().FindAll(x =>
           x.Text_Answer == null) == null)
            {
                var coll = from prop in Properties where prop.Description_Property == SelectedProperty.Description_Property select prop;
                if (coll.Count() != 0)
                {
                    question.ID_Property = coll.First().ID_Property;
                    question.Number_Variant = answers.Count;


                    double Coefficient;


                    foreach (Answer ans in Answers)
                    {
                        question.Answers.Add(ans);
                    }

                    if (SelectedProperty.Difficult == "Обычный")
                    {

                        Coefficient = 0.4f;
                    }

                    else
                    {
                        Coefficient = 0.8f;
                    }


                    question.Score = Coefficient * question.Number_Variant;

                    TestContext.getContext().Questions.Add(question);
                    TestContext.getContext().SaveChanges();

               

                    questionWindow.Close();

                    ConstructorQuestionView window = new ConstructorQuestionView();

                    window.Show();



                    MessageBox.Show("Вопрос добавлен!");
                }
                else
                {
                    MessageBox.Show("Укажите все характеристики добавляемого вопроса");
                }
            }
            else
            {
                MessageBox.Show("В вопросе не может быть более 5-ти вариантов ответа. Пожалауйста," +
                  "отредактируйте вопрос");
            }
        }

        private void RemoveAnswer()
        {
            Answers.Remove(SelectedAnswer);
        }


        #endregion

        #region Работа с картинкой вопроса;
        //работа с картинками
        private void AddPicture()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter =
                "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

            if (openDialog.ShowDialog() == true)
            {
                Image_Picture = new BitmapImage(new Uri(openDialog.FileName));
                AddingQuestion.Picture = ConvertImageToByteArray(openDialog.FileName);
            }
        }
     

        private ImageSource image;
        public ImageSource Image_Picture
        {
            get => image;

            set
            {
                image = value;
                OnPropertyChanged("Image_Picture");
            }
        }

        public BitmapImage ConvertByteArrayToImage(byte[] array)
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
        private byte[] ConvertImageToByteArray(string fileName)
      {
         Bitmap bitMap = new Bitmap(fileName);
         ImageFormat bmpFormat = bitMap.RawFormat;
         var imageToConvert = System.Drawing.Image.FromFile(fileName);
         using (MemoryStream ms = new MemoryStream())
         {
            imageToConvert.Save(ms, bmpFormat);
            return ms.ToArray();
         }
      }
        private void RemovePicture()
        {
            if (AddingQuestion.Picture != null)
            {
                AddingQuestion.Picture = null;
                image = null;
            }

            else
            {
                MessageBox.Show("У вопроса нет картинки!");
            }
        }


        #endregion

        public ViewModelConstructorQuestion(Window window)
        {
            questionWindow = window;
            Answers = new ObservableCollection<Answer>();
            properties = new ObservableCollection<Property>();
            properties.Add(property);
            TestContext.getContext().Properties.Load();
            Properties = TestContext.getContext().Properties.Local;
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
