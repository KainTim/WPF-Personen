using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Personen;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int index = -1;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
    {
        SaveFileDialog ofd = new SaveFileDialog();
        ofd.Filter = "CSV Files|*.csv";
        ofd.ShowDialog();
        string result = "";
        foreach (Person person in ListBox.Items)
        {
            result += person.ToCsvString() + "\n";
        }
        File.WriteAllText(ofd.FileName, result);
    }
    private void MenuItem_Click_Load(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CSV Files|*.csv";
        ofd.Multiselect = false;
        ofd.ShowDialog();
        ListBox.Items.Clear();
        string v = File.ReadAllText(ofd.FileName);
        string[] lines = v.Split("\n");
        for (int i = 0; i < lines.Length - 1; i++)
        {
            ListBox.Items.Add(Person.Parse(lines[i]));
        }
    }
    private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }


    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        index = ListBox.SelectedIndex;
        object item = ListBox.SelectedItem;
        if (item != null)
        {
            if (item is Person)
            {
                Person person = (Person)item;

                firstName.Text = person.Firstname;
                lastName.Text = person.Lastname;
                Birthday.SelectedDate = person.Birthdate;
                CbxDriversL.IsChecked = person.HasDriversLicense;
                RBStudent.IsChecked = person.IsStudent;
                RBTeacher.IsChecked = !person.IsStudent;
            }
        }
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        ListBox.Items.Add(new Person
        {
            Firstname = firstName.Text,
            Lastname = lastName.Text,
            Birthdate = Birthday.SelectedDate ?? new DateTime(),
            HasDriversLicense = CbxDriversL.IsChecked ?? false,
            IsStudent = RBStudent.IsChecked ?? true,
        });
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (ListBox.SelectedItem != null)
        {
            ListBox.Items.Remove(ListBox.SelectedItem);
        }
    }
}