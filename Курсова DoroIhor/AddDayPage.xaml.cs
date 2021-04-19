using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для AddDayPage.xaml
    /// </summary>
    public partial class AddDayPage : Window, INotifyPropertyChanged
    {
        private Models.Day day;
        public Models.Day Day
        {
            get => day;
            set
            {
                day = value;
                OnPropertyChanged(nameof(Day)); 
            }
        }
        public AddDayPage()
        {
            InitializeComponent();
            Day = new Models.Day() { Excercises = new ObservableCollection<Models.Excercise>(), Date = DateTime.Now };
            DataContext = Day;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Add_Ex(object sender, RoutedEventArgs e)
        {
            var window = new AddExercisePage();
            if(window.ShowDialog() == true)
            {
                Day.Excercises.Add(window.Ex);
            }
        }
        private void Remove_Ex(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstBox.SelectedItem == null) return;
                Day.Excercises.Remove(lstBox.SelectedItem as Models.Excercise);
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
