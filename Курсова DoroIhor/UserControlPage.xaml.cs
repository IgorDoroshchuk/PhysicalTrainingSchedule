using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для UserControlPage.xaml
    /// </summary>
    public partial class UserControlPage : Window, INotifyPropertyChanged
    {
        private Models.UserProfile u;
        public Models.UserProfile User
        {
            get => u;
            set
            {
                u = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public UserControlPage(Models.UserProfile user)
        {
            InitializeComponent();
            User = user;
            DataContext = User;
        }
        private void Save_User(object sender, EventArgs e)
        {
            User.SaveToXML();
            if (User.Name != "Не выбрано") DialogResult = true;
        }
        private void Load_User(object sender, EventArgs e)
        {
            User.LoadFromXML();
            if (User.Name != "Не выбрано") DialogResult = true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
