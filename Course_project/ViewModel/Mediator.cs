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
    public static class Mediator
    {
        public static Question selectedQuestion;
        public static User_System activeUser;

        public static Question SelectedQuestion()
        {
            return selectedQuestion ?? (selectedQuestion = new Question());
        }

        public static User_System ActiveUser()
        {
            return activeUser ?? (activeUser = new User_System());
        }

        
    }
}
