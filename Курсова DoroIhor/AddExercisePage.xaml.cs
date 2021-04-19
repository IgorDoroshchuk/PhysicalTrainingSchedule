using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для AddExercisePage.xaml
    /// </summary>
    public partial class AddExercisePage : Window, INotifyPropertyChanged
    {
        private Models.Excercise ex;
        public Models.Excercise Ex
        {
            get => ex;
            set
            {
                ex = value;
                OnPropertyChanged(nameof(Ex));
            }
        }
        public AddExercisePage()
        {
            InitializeComponent();
            Ex = new Models.Excercise() { Count = 0, Name = "", Time = "", Weight = 0, Repeats = 0 };
            DataContext = Ex;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               double d = Ex.Weight + Ex.Count + Ex.Repeats;
               DialogResult = true;
            }
            catch (Exception)
            {
                DialogResult = false;
            }
        }
    }
}
