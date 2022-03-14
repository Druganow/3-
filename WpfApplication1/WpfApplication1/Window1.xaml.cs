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

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            if (easyRadioButton.IsChecked == true)
            {
                lvl = 1;
            }
            else if (averageRadioButton.IsChecked == true)
            {
                lvl = 2;
            }
            else if (hardRadioButton.IsChecked == true)
            {
                lvl = 3;
            }
            this.Close();
        }
        public int lvl
        {
            get; set;
        }
       

        private void Cancel_Click(object sender, MouseButtonEventArgs e)
        {
DialogResult = false;
            this.Close();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Record record = new Record();
            record.Show();
        }

        private void Image_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
