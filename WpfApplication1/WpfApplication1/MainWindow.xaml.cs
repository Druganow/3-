using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //вспомогательное
        string text_Name = "";
        int X, Y, N;
        string[] piese = new string[7];
        ImageExtended[,] image;
        bool ISVybor = false;
        ImageExtended imag1;
        int Fps;
        bool[] level=new bool [4];
        int[,,] MS = new int[16, 4, 4];
        int[,] MK = new int [16, 4];
        int Kolvohod;

        //Объявлкение "масок"
        void MSin()
        {
            for (int i = 0; i < 16; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 4; k++) MS[i, j, k] = 0;
            MS[0, 0, 0] = 1;
            MS[0, 0, 1] = 1;
            MS[0, 1, 2] = 1;
            MK[0, 0] = 2;
            MK[0, 1] = 3;

            MS[1,0,2] = 1;
            MS[1,1,0] = 1;
            MS[1,1,1] = 1;
            MK[1,0] = 2;
            MK[1,1] = 3;

            MS[2,0,0] = 1;
            MS[2,0,2] = 1;
            MS[2,1,1] = 1;
            MK[2,0] = 2;
            MK[2,1] = 3;

            MS[3,0,1] = 1;
            MS[3,0,2] = 1;
            MS[3,1,0] = 1;
            MK[3,0] = 2;
            MK[3,1] = 3;

            MS[4,0,0] = 1;
            MS[4,1,1] = 1;
            MS[4,1,2] = 1;
            MK[4,0] = 2;
            MK[4,1] = 3;

            MS[5,0,1] = 1;
            MS[5,1,0] = 1;
            MS[5,1,2] = 1;
            MK[5,0] = 2;
            MK[5,1] = 3;

            MS[6,0,1] = 1;
            MS[6,1,1] = 1;
            MS[6,2,0] = 1;
            MK[6,0] = 3;
            MK[6,1] = 2;

            MS[7,0,0] = 1;
            MS[7,1,0] = 1;
            MS[7,2,1] = 1;
            MK[7,0] = 3;
            MK[7,1] = 2;

            MS[8,0,0] = 1;
            MS[8,1,1] = 1;
            MS[8,2,1] = 1;
            MK[8,0] = 3;
            MK[8,1] = 2;

            MS[9,0,1] = 1;
            MS[9,1,1] = 1;
            MS[9,2,1] = 1;
            MK[9,0] = 3;
            MK[9,1] = 2;

            MS[10,0,1] = 1;
            MS[10,1,0] = 1;
            MS[10,2,1] = 1;
            MK[10,0] = 3;
            MK[10,1] = 2;

            MS[11,0,0] = 1;
            MS[11,1,1] = 1;
            MS[11,2,0] = 1;
            MK[11,0] = 3;
            MK[11,1] = 2;

            MS[12,0,0] = 1;
            MS[12,0,1] = 1;
            MS[12,0,3] = 1;
            MK[12,0] = 1;
            MK[12,1] = 4;

            MS[13,0,0] = 1;
            MS[13,0,2] = 1;
            MS[13,0,3] = 1;
            MK[13,0] = 1;
            MK[13,1] = 4;

            MS[14,0,0] = 1;
            MS[14,1,0] = 1;
            MS[14,3,0] = 1;
            MK[14,0] = 4;
            MK[14,1] = 1;

            MS[15,0,0] = 1;
            MS[15,2,0] = 1;
            MS[15,3,0] = 1;
            MK[15,0] = 4;
            MK[15,1] = 1;
        }

        public MainWindow()
        {
            InitializeComponent();
            New_game();
        }

        //Создание новой игры
        public void New_game()
        {
            grid1.Children.Clear();
            for (int i = 1; i < 4; i++) level[i] = false;
            level[0] = true;
            var window = new Window1();
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                if (window.lvl == 1) X = Y = 4;
                else if (window.lvl == 2) X = Y = 6;
                else X = Y = 8;
                if (window.lvl == 1)
                    text_Name = "Easy_level_records";
                else if (window.lvl == 2)
                    text_Name = "Average_level_records";
                else if (window.lvl == 3)
                    text_Name = "Heavy_level_records";
                N = 5;
                MSin();
                butt();
            }
            else
            {
                Close();
            }
        }

        //Создание поля
        private void butt()
        {
            piese[1] = @"/Resources/Фишка1.png";
            piese[2] = @"/Resources/Фишка2.png";
            piese[3] = @"/Resources/Фишка3.png";
            piese[4] = @"/Resources/Фишка4.png";
            piese[0] = @"/Resources/Фишка5.png";
            image = new ImageExtended[X, Y];
            Field field;
            field = new Field(X, Y, N);
            field.Generation(piese, image, grid1);
            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++)image[i, j].Height = image[i, j].Width = 50;
            grid1.Margin = new Thickness(290 - X * 25, 240 - Y * 25, 0, 0);
            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) image[i, j].PreviewMouseDown += new MouseButtonEventHandler(FieldClick);
            while (Poisk() > 0)
            {
                int abc = Poisk();
                Opadanie();
                Zamena();
            }
            Kolvohod = 15;
            Textbox1.Content = Kolvohod;

        }

       //Обработка нажатия по фишке
        async void FieldClick(object sender, MouseButtonEventArgs e)
        {
            Textbox1.Content = Kolvohod;
                ImageExtended imag = (ImageExtended)sender;
                if (ISVybor)
                {
                    Swap(imag, imag1);
                    await Task.Delay(500);
                    int abc = Poisk();
                    if (abc == 0) Swap(imag, imag1);
                    else
                    {
                        Kolvohod--;
                        while (abc > 0)
                        {
                            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) if (image[i, j].isCek) image[i, j].Opacity = 0.1;
                            Fps += abc;
                            Textbox.Content = Fps;
                            await Task.Delay(500);
                            Opadanie();
                            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) image[i, j].Opacity = 1;
                            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) if (image[i, j].isCek) image[i, j].Opacity = 0.1;
                            await Task.Delay(500);
                            Zamena();
                            for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) image[i, j].Opacity = 1;
                            abc = Poisk();
                        }
                    }

                }

                else
                {
                    imag1 = imag;
                    imag.Opacity = 0.5;
                    ISVybor = true;
                }
                if (!analyz())
                {

                    for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++)
                        {
                            image[i, j].isCek = true;
                            image[i, j].Opacity = 0.1;
                        }
                    await Task.Delay(500);
                    do
                    {
                        int abc = Poisk();
                        Opadanie();
                        Zamena();
                    } while (Poisk() > 0 && analyz());
                    for (int i = 0; i < X; i++) for (int j = 0; j < Y; j++) image[i, j].Opacity = 1;

                }

            if (Kolvohod == 0)
            {
                string Name = "";
                var name = new Name();
                name.labelTB.Content = "Поздравляю, Ваш счет: " + Fps;
                name.ShowDialog();
                if (name.DialogResult.HasValue && name.DialogResult.Value)
                {
                    Name = name.nameNewFile;
                }
                if (Name != "")
                {
                    var listWinners = new List<Player>();
                    using (StreamReader recordsRead = new StreamReader(text_Name + ".txt"))
                    {
                        while (!recordsRead.EndOfStream)
                        {
                            var str = new string[2];
                            str = recordsRead.ReadLine().Split('\t');
                            try
                            {
                                listWinners.Add(new Player(str[0], int.Parse(str[1])));
                            }
                            catch { }
                        }
                    }
                    var winner = new Player(Name, Fps);
                    listWinners.Add(winner);
                    using (StreamWriter recordsWrite = new StreamWriter(text_Name + ".txt"))
                    {
                        foreach (var i in listWinners)
                            recordsWrite.WriteLine(i.ToString());
                    }
                }
                
                    New_game();
               
            }
            Textbox1.Content = Kolvohod;
        }

        //Обмен фишками
        void Swap(ImageExtended im1, ImageExtended im2)
        {
            if (Sosedi(im1, im2))
            {
                int temp;
                im1.Source = new BitmapImage(new Uri(piese[im2.Cod], UriKind.Relative));
                im2.Source = new BitmapImage(new Uri(piese[im1.Cod], UriKind.Relative));
                temp = im1.Cod;
                im1.Cod = im2.Cod;
                im2.Cod = temp;
            }

            ISVybor = false;
            im2.Opacity = 1;
        }
        
        //проверка:являются ли фишки соседними
        bool Sosedi(ImageExtended a, ImageExtended b)
        {
            if (Math.Abs(a.X - b.X) == 1 || Math.Abs(a.Y - b.Y) == 1) return true;
            else return false;
        }
    
        //поиск нужных комбинаций фишек и их пометка
        int Poisk()
        {
            int Ochki = 0;
            int ChBlock = -1;
            int Anlz = 0;
            for (int i = 0; i < X; i++)
            {
                int NChBlock = 0;
                for (int j = 0; j < Y; j++)
                {
                    if (j == 0) ChBlock =image[i, j].Cod;
                    if (image[i, j].Cod == ChBlock) NChBlock++;
                    else if (NChBlock > 2)
                    {
                        Anlz++;
                        for (int l = 1; l <= NChBlock ; l++)
                        {
                            image[i, j - l].isCek=true;
                        }
                        Ochki += NChBlock;
                        NChBlock = 1;
                        ChBlock = image[i, j].Cod;
                    }
                    else
                    {
                        ChBlock = image[i, j].Cod;
                        NChBlock = 1;
                    }

                    if (j == Y-1 && NChBlock > 2)
                    {
                        Anlz++; 
                        for (int l = 0; l < NChBlock; l++)
                        {
                            image[i , j- l].isCek = true; 
                        }
                        Ochki += NChBlock;
                    }
                }
            }
            for (int j = 0; j < Y; j++)
            {
                int NChBlock = 0;
                for (int i = 0; i < X; i++)
                {
                    if (i == 0) ChBlock = image[i, j].Cod;
                    if (image[i, j].Cod == ChBlock) NChBlock++;
                    else if (NChBlock > 2)
                    {
                        Anlz++;
                        for (int l = 1; l <= NChBlock; l++)
                        {
                            image[i - l, j].isCek = true;
                        }
                        Ochki += NChBlock;
                        NChBlock = 1;
                        ChBlock = image[i, j].Cod;
                    }
                    else
                    {
                        ChBlock = image[i, j].Cod;
                        NChBlock = 1;
                    }

                    if (i == X-1 && NChBlock > 2)
                    {
                        Anlz++;
                        for (int l = 0; l < NChBlock; l++)
                        {
                            image[i- l, j ].isCek = true;
                        }
                        Ochki += NChBlock;
                    }
                }
                
            }
            return Ochki;
        }

        //удаление помеченных фишек и смещение их вниз
        void Opadanie()
        {
            for (int i = 0; i < X; i++)
            {
                int NHole = 0;
                int YHole = 0;
                for (int j = Y-1; j >= 0; j--)
                {
                    if (image[i, j].isCek)
                    {
                        NHole++;
                        if (NHole == 1) YHole = j;
                    }
                    if (!image[i, j].isCek && NHole > 0)
                    {
                        image[i, YHole].Cod = image[i, j].Cod;
                        image[i, YHole].Source = new BitmapImage(new Uri(piese[image[i, j].Cod], UriKind.Relative));
                        image[i, YHole].isCek= image[i, j].isCek;
                        YHole--;
                        image[i, j].isCek = !image[i, j].isCek;
                    }
                }
            }
            
            
        }

        //анализх поля на возможность сделать следующий ход
        bool analyz()
        {
            for(int i =0; i<N; i++)
            {
                for(int m=0; m<16; m++)
                {
                     for(int x=0;x<X-MK[m,0]+1;x++)
                     {
                        for(int y=0;y<Y-MK[m,1]+1; y++)
                        {
                            int Nmask = 0;
                            for(int mx=0;mx<MK[m,0];mx++)
                            {
                                for (int my = 0; my < MK[m, 1]; my++)
                                    if (MS[m, mx, my]== 1 && image[x + mx, y + my].Cod == i) Nmask++;
                            }
                            if (Nmask == 3) return true;
                        }
                     }
                }
            }
            return false;
        }

        //замена пустых полей на новые фишки
        void Zamena()
        {
            Random s = new Random();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    
                    if(image[i,j].isCek)
                    {
                        image[i, j].isCek = false;
                        int cod = s.Next(N);
                        image[i, j].Cod = cod;
                        image[i, j].Source = new BitmapImage(new Uri(piese[cod], UriKind.Relative));
                    }
                }
            }
        }
    }
}
