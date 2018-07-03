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
    /// Логика взаимодействия для ConstructorTestsView.xaml
    /// </summary>
    public partial class AdminWindowView : Window
    {
        public AdminWindowView()
        {
            InitializeComponent();
            ViewModelAdminWindow win = new ViewModelAdminWindow();
            DataContext = win;
            Closing += win.OnWindowClosing;
        }

        public ViewModelAdminWindow ViewModelAdminWindow
        {
            get => default(ViewModelAdminWindow);
            set
            {
            }
        }
    }
}
