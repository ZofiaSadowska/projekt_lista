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
using System.Security.Policy;
using System.Globalization;

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
        public string? m_strBdate { get; set; } 
        public string? m_strPnumber { get; set; }
        public string? m_strAdress { get; set; }
        public string? m_strCity { get; set; }
        public string? m_strPostCode { get; set; } 
        public Osoba()
        {
            m_strPESEL = "00000000000";
            m_strName = "";
            m_strSecName = "";
            m_strSurname = "";
            m_strBdate = "";
            m_strPnumber = "";
            m_strAdress = "";
            m_strCity = "";
            m_strPostCode = "";

        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                            uczen.m_strBdate = columns.ElementAtOrDefault(4);
                            uczen.m_strPnumber = columns.ElementAtOrDefault(5);
                            uczen.m_strAdress = columns.ElementAtOrDefault(6);
                            uczen.m_strCity = columns.ElementAtOrDefault(7);
                            uczen.m_strPostCode = columns.ElementAtOrDefault(8);
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
            bool? result = nowy.ShowDialog();  

            if (result == true)
            {
                Osoba uczen = new();
                Random random = new();
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                string strName = ti.ToTitleCase(nowy.strName.Text.Trim().ToLower());
                string strSurname = ti.ToTitleCase(nowy.strSurname.Text.Trim().ToLower());
                string strSecName = ti.ToTitleCase(nowy.strSecName.Text.Trim().ToLower());
                string strAdress = ti.ToTitleCase(nowy.strAdress.Text.Trim().ToLower());
                string strCity = ti.ToTitleCase(nowy.strCity.Text.Trim().ToLower());

                string strPnumber = nowy.strPnumber.Text.Replace(" ", "");
                string czesci  = $"{strPnumber.Substring(0, 3)} {strPnumber.Substring(3, 3)} {strPnumber.Substring(6, 3)}";
                strPnumber = "+48" + " " + czesci;


                uczen.m_strPESEL = random.Next().ToString();
                uczen.m_strName = strName;
                uczen.m_strSurname = strSurname;
                uczen.m_strSecName = strSecName;
                uczen.m_strBdate = nowy.strBdate.Text.Trim();
                uczen.m_strPnumber = strPnumber;
                uczen.m_strAdress = strAdress;
                uczen.m_strCity = strCity;
                uczen.m_strPostCode = nowy.strPostCode.Text.Trim();

                lista.Items.Add(uczen);
            }
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