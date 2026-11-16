#nullable disable
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AplikacjaMobilna
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int wynikCalkowityGry = 0;
        private readonly Random random = new Random();
        private readonly Image[] kosciImages;

        public MainWindow()
        {
            InitializeComponent();
            kosciImages = new Image[] { imgKostka1, imgKostka2, imgKostka3, imgKostka4, imgKostka5 };
            ResetujGre();
        }

        /*
        ************************************************
        nazwa: LiczPunkty
        opis: Oblicza sumę punktów uzyskanych w rzucie kośćmi według zasad gry. 
              Punkty są sumą oczek tych kości, których wartości wystąpiły co najmniej 2 razy.
        parametry: kostki - tablica 5 liczb całkowitych reprezentująca wylosowane oczka na poszczególnych kostkach
        zwracany typ i opis: int - łączna suma punktów zdobytych w danym losowaniu
        autor: 00000000000
        ************************************************
        */
        public static int LiczPunkty(int[] kostki)
        {
            int sumaPunktow = 0;
            int[] liczniki = new int[7]; // indeksy 1-6

            foreach (int oczka in kostki)
            {
                if (oczka >= 1 && oczka <= 6)
                {
                    liczniki[oczka]++;
                }
            }

            for (int i = 1; i <= 6; i++)
            {
                if (liczniki[i] >= 2)
                {
                    sumaPunktow += i * liczniki[i];
                }
            }

            return sumaPunktow;
        }

        /*
        ************************************************
        nazwa: PobierzObrazek
        opis: Pomocnicza metoda ładująca bitmapę z pliku graficznego lub zasobów aplikacji.
        parametry: nazwaPliku - nazwa pliku obrazu kości (np. question.jpg, k1.jpg)
        zwracany typ i opis: BitmapImage - załadowany obiekt obrazu
        autor: 00000000000
        ************************************************
        */
        private BitmapImage PobierzObrazek(string nazwaPliku)
        {
            try
            {
                string sciezka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nazwaPliku);
                if (File.Exists(sciezka))
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(sciezka, UriKind.Absolute);
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.EndInit();
                    return bmp;
                }
                return new BitmapImage(new Uri(nazwaPliku, UriKind.RelativeOrAbsolute));
            }
            catch
            {
                return new BitmapImage(new Uri(nazwaPliku, UriKind.RelativeOrAbsolute));
            }
        }

        private void ResetujGre()
        {
            wynikCalkowityGry = 0;
            txtWynikLosowania.Text = "Wynik tego losowania: 0";
            txtWynikGry.Text = "Wynik gry: 0";

            BitmapImage imgQuestion = PobierzObrazek("question.jpg");
            foreach (var img in kosciImages)
            {
                img.Source = imgQuestion;
            }
        }

        private void BtnRzuc_Click(object sender, RoutedEventArgs e)
        {
            int[] rzuty = new int[5];

            for (int i = 0; i < 5; i++)
            {
                rzuty[i] = random.Next(1, 7);
                kosciImages[i].Source = PobierzObrazek($"k{rzuty[i]}.jpg");
            }

            int wynikTegoLosowania = LiczPunkty(rzuty);
            wynikCalkowityGry += wynikTegoLosowania;

            txtWynikLosowania.Text = $"Wynik tego losowania: {wynikTegoLosowania}";
            txtWynikGry.Text = $"Wynik gry: {wynikCalkowityGry}";
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetujGre();
        }
    }
}
