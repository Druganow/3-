using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    class Field
    {
        int x;
        int y;
        int countPiece;

        public Field(int X, int Y, int C)
        {
            x = X;
            y = Y;
            countPiece = C;
        }

        public Field(int C)
        {
            Random r = new Random();
            x = r.Next(5) + 6;
            y = r.Next(5) + 6;
        }

        public Field(int X, int Y)
        {
            Random r = new Random();
            x = X;
            y = Y;
            countPiece = r.Next(7) + 3;
        }

        public void Generation(string[] piece, ImageExtended[,] Image, Grid grid)
        {
            Random r = new Random();
            
            grid.Width = x * 50;
            grid.Height = y * 50;
            for (int i = 0; i < y; i++)
            {
                 grid.RowDefinitions.Add(new RowDefinition() { });
                grid.RowDefinitions[i].Height = new System.Windows.GridLength(50);
            }
            for (int j = 0; j <x; j++)
            {
               grid.ColumnDefinitions.Add(new ColumnDefinition() { });
                grid.ColumnDefinitions[j].Width = new System.Windows.GridLength(50);
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    ImageExtended imag = new ImageExtended();
                    int cod = r.Next(countPiece);
                    imag.Cod = cod;
                    imag.Source = new BitmapImage(new Uri(piece[cod], UriKind.Relative));
                    grid.Children.Add(imag);
                    Grid.SetColumn(imag, i);
                    Grid.SetRow(imag, j);
                    imag.X = i;
                    imag.Y = j;
                    Image[i, j] = imag;
                }
            }

        }
    }
}
