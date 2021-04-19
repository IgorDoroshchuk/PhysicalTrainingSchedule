using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace DoroIhor.Models
{
    public class UserProfile : INotifyPropertyChanged
    {
        private string _name;
        private double _weight, _height;
        private ObservableCollection<Day> _days;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                try
                {
                    _weight = value;
                    
                }
                catch (Exception)
                {
                    _weight = 1337;
                }
                OnPropertyChanged(nameof(Weight));
                OnPropertyChanged(nameof(BMI));
            }
        }
        public double Height
        {
            get => _height;
            set
            {
                try
                {
                    _height = value;

                }
                catch (Exception)
                {
                    _height = 1337;
                }
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(BMI));
            }
        }
        public double BMI
        {
            get => Weight / Math.Pow((Height / 100), 2);
        }
        public ObservableCollection<Day> Days
        {
            get => _days;
            set
            {
                _days = value;
                OnPropertyChanged(nameof(Days));
            }
        }
        public void SaveToXML()
        {
            try
            {
                if (Days == null) Days = new ObservableCollection<Day>();
                var xDoc = new XDocument();
                var userXElem = new XElement("User");
                userXElem.Add(
                    new XAttribute("Name", Name),
                    new XAttribute("Weight", Weight),
                    new XAttribute("Height", Height)
                    );
                foreach(var day in Days)
                {
                    var Day = new XElement("Day");
                    Day.Add(new XAttribute("Date", day.Date.ToString()));
                    foreach(var ex in day.Excercises)
                    {
                        var Exercise = new XElement("Exercise");
                        Exercise.Add(
                            new XAttribute("Name", ex.Name),
                            new XAttribute("Count", ex.Count),
                            new XAttribute("Repeats", ex.Repeats),
                            new XAttribute("Weight", ex.Weight),
                            new XAttribute("Time", ex.Time)
                        );
                        Day.Add(Exercise);
                    }
                    userXElem.Add(Day);
                }
                xDoc.Add(userXElem);
                SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "XML File | *.xml", Title="Сохранение нового пользователя", FileName=$"{Name} Profile.xml"};
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName != "")
                {
                    xDoc.Save(saveFileDialog.FileName);
                    return;
                }
                xDoc.Save($"{Name}.xml");
            }
            catch (Exception) { }
        }

        public void LoadFromXML()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog() { 
                    Filter = "XML File | *.xml",
                    Title = "Чтение пользователя" 
                };
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    XDocument xDoc = XDocument.Load(openFileDialog.FileName);
                    var tempUser = new UserProfile() {
                        Name = xDoc.Element("User").Attribute("Name").Value,
                        Weight = Double.Parse(xDoc.Element("User").Attribute("Weight").Value),
                        Height = Double.Parse(xDoc.Element("User").Attribute("Height").Value)
                    };
                    
                    var tempDays = new ObservableCollection<Day>();
                    foreach(var day in xDoc.Element("User").Elements("Day"))
                    {
                        var tempDay = new Day() { 
                            Date = DateTime.Parse(day.Attribute("Date").Value), 
                            Excercises = new ObservableCollection<Excercise>() 
                        };
                        
                        foreach (var ex in day.Elements("Exercise"))
                        {
                            var tempEx = new Excercise()
                            {
                                Count = Int32.Parse(ex.Attribute("Count").Value),
                                Name = ex.Attribute("Name").Value,
                                Weight = Double.Parse(ex.Attribute("Weight").Value),
                                Time = ex.Attribute("Time").Value,
                                Repeats = Convert.ToInt32(ex.Attribute("Repeats").Value)
                            };
                            tempDay.Excercises.Add(tempEx);
                        }
                        tempDays.Add(tempDay);
                    }
                    tempUser.Days = tempDays;
                    Name = tempUser.Name;
                    Days = tempUser.Days;
                    Weight = tempUser.Weight;
                    Height = tempUser.Height;
                }
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
