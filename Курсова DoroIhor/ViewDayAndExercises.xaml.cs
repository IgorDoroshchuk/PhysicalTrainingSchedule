using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace DoroIhor
{
    /// <summary>
    /// Логика взаимодействия для ViewDayAndExercises.xaml
    /// </summary>
    public partial class ViewDayAndExercises : Window, INotifyPropertyChanged
    {
        private Models.Day _day;
        private Models.Excercise _chosenExercise;
        public Models.Day Day
        {
            get => _day;
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }
        public double Tonnage {
            get => ChosenExercise.Weight * ChosenExercise.Repeats * ChosenExercise.Count; 
        }
        public Models.Excercise ChosenExercise
        {
            get => _chosenExercise;
            set
            {
                _chosenExercise = value;
                OnPropertyChanged(nameof(ChosenExercise));
                OnPropertyChanged(nameof(Tonnage));
            }
        }
        public ViewDayAndExercises(Models.Day day)
        {
            InitializeComponent();
            Day = day;
            ChosenExercise = new Models.Excercise { Count = 0, Weight = 0, Repeats = 0, Name = "Не выбрано" };
            DataContext = this;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var chosen = lstBox.SelectedItem as Models.Excercise;
                ChosenExercise = chosen;
            }
            catch (Exception)
            {

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
