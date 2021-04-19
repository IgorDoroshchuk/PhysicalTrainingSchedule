using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DoroIhor.Models
{
    public class Excercise : INotifyPropertyChanged
    {
        private string _name, _time;
        private double _weight;
        private int _count, _repeats;
        
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                try
                {
                    _weight = Convert.ToDouble(value);
                }
                catch (Exception)
                {
                    _weight = 1337;
                }
                OnPropertyChanged(nameof(Weight));
            }
        }
        public int Count
        {
            get => _count;
            set
            {
                try
                {
                    _count = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    _count = 1337;
                }
                OnPropertyChanged(nameof(Count));
            }
        }
        public int Repeats
        {
            get => _repeats;
            set
            {
                try
                {
                    _repeats = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    _repeats = 1337;
                }
                OnPropertyChanged(nameof(Repeats));
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
