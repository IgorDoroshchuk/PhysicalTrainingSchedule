using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Models.UserProfile user;
        public Models.UserProfile User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            User = new Models.UserProfile
            {
                Name = "Не выбрано",
                Height = 0,
                Weight = 0
            };
            DataContext = User;
        }
        private void Profile_Open(object source, EventArgs e)
        {
            var window = new UserControlPage(User);
            if(window.ShowDialog() == true)
            {
                User = window.User;
            }
        }
        
        private void Schedule_Open(object source, EventArgs e)
        {
            if(User.Name != "Не выбрано")
            {
                var window = new UserDaysPage(User);
                if(window.ShowDialog() == true)
                {
                    User = window.User;
                    User.SaveToXML();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
