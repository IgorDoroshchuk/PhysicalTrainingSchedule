using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для UserDaysPage.xaml
    /// </summary>
    public partial class UserDaysPage : Window, INotifyPropertyChanged
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
        public UserDaysPage(Models.UserProfile user)
        {
            InitializeComponent();
            User = user;
            DataContext = User;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Add_Day(object sender, EventArgs e)
        {
            var window = new AddDayPage();
            if(window.ShowDialog() == true)
            {
                User.Days.Add(window.Day);
            }

        }
        private void Remove_Day(object sender, EventArgs e)
        {
            try
            {
                if (lstBox.SelectedItem == null) return;
                var day = lstBox.SelectedItem as Models.Day;
                User.Days.Remove(day);
            }
            catch (Exception) { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(e.ClickCount == 2)
                {
                    var day = lstBox.SelectedItem as Models.Day;
                    var window = new ViewDayAndExercises(day);
                    window.Show();
                }   
            }
            catch (Exception) { }

        }
    }
}
