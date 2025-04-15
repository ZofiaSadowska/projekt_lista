using Microsoft.Win32;
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
using System.Collections.ObjectModel;
using System.IO;

namespace projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class Osoba
    {
        public string? m_strPESEL { get; set; }
        public string? m_strName { get; set; }
        public string? m_strSecName { get; set; }
        public string? m_strSurname { get; set; }
        public Osoba()
        {
            m_strPESEL = "00000000000";
            m_strName = "";
            m_strSecName = "";
            m_strSurname = "";
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dodaj_Click(object sender, RoutedEventArgs e)
        {

            //PopUp nowaLista = new PopUp();

            //nowaLista.ShowDialog();


            //var rnd = new Random();

            //lista.Items.Add(new { m_nID = rnd.Next(), m_strName = nowaLista.strName.Text, m_strSname = nowaLista.strSname.Text });

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            openFileDialog.Title = "Otwórz plik CSV";
            if (openFileDialog.ShowDialog() == true)
            {
                lista.Items.Clear();
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                string delimiter = ";";
                if (selectedFilterIndex == 1)
                {
                    delimiter = ",";
                }
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    foreach (var line in lines)
                    {
                        string[] columns = line.Split(delimiter);
                        if (columns != null)
                        {
                            Osoba uczen = new();
                            uczen.m_strPESEL = columns.ElementAtOrDefault(0);
                            uczen.m_strName = columns.ElementAtOrDefault(1);
                            uczen.m_strSecName = columns.ElementAtOrDefault(2);
                            uczen.m_strSurname = columns.ElementAtOrDefault(3);
                            lista.Items.Add(uczen);
                        }
                    }
                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            saveFileDialog.Title = "Zapisz jako plik CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ";";
                if (saveFileDialog.FilterIndex == 1)
                {
                    delimiter = ",";
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Osoba item in lista.Items)
                    {
                        var row = $"{item.m_strPESEL}{delimiter}{item.m_strName}" +
                        $"{delimiter}{item.m_strSecName}{delimiter}{item.m_strSurname}";
                        writer.WriteLine(row);
                    }
                }
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            PopUp nowy = new PopUp();
            nowy.ShowDialog();
            Osoba uczen = new();
            Random random = new();
            uczen.m_strPESEL = random.Next().ToString();
            uczen.m_strName = nowy.strName.Text;
            uczen.m_strSurname = nowy.strSurname.Text;
            uczen.m_strSecName = nowy.strSecName.Text;
            lista.Items.Add(uczen); 
        }
        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {
            ///funkcja menu „remove selected…”:
            while (lista.SelectedItems.Count > 0)
            {
                lista.Items.Remove(lista.SelectedItems[0]);
            }
        }
    }
}