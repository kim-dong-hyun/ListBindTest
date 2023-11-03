using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListBindTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel = new MainViewModel();
        private Person personTest = new Person();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;

            personTest.Name = "Test";
            personTest.Age = 100;
            canvasTest.DataContext = personTest;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Person p = new Person();
            p.Name = personTest.Name;
            p.Age = personTest.Age;
            
            //viewModel.People.Add(p);
            viewModel.People.Add(personTest);            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //personTest.Name = personTest.Name + "X";
            Person p = viewModel.SelectedItem;
            p.Age++;
        }
    }
#if false
    public class Person
    {
        private string _name;
        private int _age;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Console.WriteLine($"Name : {_name}");
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                Console.WriteLine($"Age : {_age}");
            }
        }
    }
#else
    public class Person : INotifyPropertyChanged
    {
        private string _name;
        private int _age;

        public string Name
        {
            get {  return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
#endif
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _people;
        private Person _selectedItem;

        public MainViewModel()
        {
            _people = new ObservableCollection<Person>();
            _people.Add(new Person { Name = "Alice", Age = 25 });
            _people.Add(new Person { Name = "Bob", Age = 30 });
            _people.Add(new Person { Name = "Charlie", Age = 35 });
        }

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                //OnPropertyChanged(nameof(People));
            }
        }

        public Person SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
