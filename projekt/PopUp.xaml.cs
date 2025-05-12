using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace projekt
{
    /// <summary>
    /// Logika interakcji dla klasy PopUp.xaml
    /// </summary>
    public partial class PopUp : Window
    {
        private List<TextBox> wymagane = new List<TextBox>();
        public PopUp()
        {
            InitializeComponent();
            
            wymagane.Add(strName);
            wymagane.Add(strSurname);
            wymagane.Add(strBdate);
            wymagane.Add(strAdress);
            wymagane.Add(strCity);
            wymagane.Add(strPostCode);  
        }
        private bool isinputted()
        {
            return
                   !string.IsNullOrWhiteSpace(strName.Text) ||
                   !string.IsNullOrWhiteSpace(strSecName.Text) ||
                   !string.IsNullOrWhiteSpace(strSurname.Text) ||
                   !string.IsNullOrWhiteSpace(strBdate.Text) ||
                   !string.IsNullOrWhiteSpace(strPnumber.Text) ||
                   !string.IsNullOrWhiteSpace(strAdress.Text) ||
                   !string.IsNullOrWhiteSpace(strCity.Text) ||
                   !string.IsNullOrWhiteSpace(strPostCode.Text);
        }

        


        private void Anuluj_Click(object sender, RoutedEventArgs e)

        {
            if (isinputted())
            {
                var result = MessageBox.Show("Czy na pewno chcesz zamknąć?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            DialogResult = false;  
            Close();
        }
        
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            bool poprawne = true;

            foreach (var pole in wymagane)
            {
                if (string.IsNullOrWhiteSpace(pole.Text))
                {
                    pole.BorderBrush = Brushes.Red;
                    pole.BorderThickness = new Thickness(2);
                    uwaga.Foreground = Brushes.Red;
                    poprawne = false;
                }
                else
                {
                    pole.ClearValue(TextBox.BorderBrushProperty);
                    pole.ClearValue(TextBox.BorderThicknessProperty);
                    uwaga.ClearValue(TextBox.ForegroundProperty);  
                }
            }

            string PhoneNumber = strPnumber.Text.Replace(" ", "");
            if (PhoneNumber.Length != 9 || PhoneNumber.Length != 0)
            {
                warning.Content = "Zły format, za mało lub za dużo cyfr (powinno byc 9)";
                warning.Foreground = Brushes.Red;
                poprawne = false;
            }
            else
            {
                warning.Content = "";
            }



            if (!poprawne)
            {
                return;
            }

            DialogResult = true;  
            Close();
        }
        
        
    }
}
