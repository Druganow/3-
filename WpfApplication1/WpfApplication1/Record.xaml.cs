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
using System.IO;
namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Record.xaml
    /// </summary>
    public partial class Record : Window
    {
        string text_Name="";

        public Record()
        {
            InitializeComponent();
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void easyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            text_Name = "Easy_level_records";
            Label.Content = "";
            var listWinners = new List<Player>();
            using (StreamReader records = new StreamReader(text_Name + ".txt"))
            {
                while (!records.EndOfStream)
                {
                    var str = new string[2];
                    str = records.ReadLine().Split('\t');
                    try
                    {
                        
                        listWinners.Add(new Player(str[0], int.Parse(str[1])));
                    }
                    catch { }
                }
            }
            var result = from player in listWinners
                         orderby player.Point descending
                         select player;


            for (int i = 0; i < result.Count(); i++)
            {
                Label.Content += ((i + 1) + "  " + result.ElementAt(i).ToString() + "\n");
            }
        }

        private void averageRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            text_Name = "Average_level_records";
            Label.Content = "";
            var listWinners = new List<Player>();
            using (StreamReader records = new StreamReader(text_Name + ".txt"))
            {
                while (!records.EndOfStream)
                {
                    var str = new string[2];
                    str = records.ReadLine().Split('\t');
                    try
                    {
                        listWinners.Add(new Player(str[0], int.Parse(str[1])));
                    }
                    catch { }
                }
            }
            var result = from player in listWinners
                         orderby player.Point descending
                         select player;


            for (int i = 0; i < result.Count(); i++)
            {
                Label.Content += ((i + 1) + "  " + result.ElementAt(i).ToString() + "\n");
            }
        }

        private void heavyRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            text_Name = "Heavy_level_records";
            Label.Content = "";
            var listWinners = new List<Player>();
            using (StreamReader records = new StreamReader(text_Name + ".txt"))
            {
                while (!records.EndOfStream)
                {
                    var str = new string[2];
                    str = records.ReadLine().Split('\t');
                    try
                    {
                        listWinners.Add(new Player(str[0], int.Parse(str[1])));
                    }
                    catch { }
                }
            }
            var result = from player in listWinners
                         orderby player.Point descending
                         select player;


            for (int i = 0; i < result.Count(); i++)
            {
                Label.Content += ((i + 1) + "  " + result.ElementAt(i).ToString() + "\n");
            }
        }
    }
}
