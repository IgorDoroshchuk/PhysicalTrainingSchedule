using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DoroIhor.Models
{
    public class Day : INotifyPropertyChanged
    {
        private ObservableCollection<Excercise> _excercises;
        private DateTime _date;
        public ObservableCollection<Excercise> Excercises
        {
            get => _excercises;
            set
            {
                _excercises = value;
                OnPropertyChanged(nameof(Excercises));
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                try
                {
                    _date = value;
                }
                catch (Exception)
                {
                    _date = DateTime.Now;
                }
                
            }
        }
        public void RemoveExcercise(Excercise e) {
            try
            {
                Excercises.Remove(e);
            }
            catch (Exception) { }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
