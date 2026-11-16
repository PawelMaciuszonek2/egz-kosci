using System;

namespace GraWKosci
{
    public partial class MainPage : ContentPage
    {
        private int wynikCalkowityGry = 0;
        private readonly Random random = new Random();
        private readonly Image[] kosciImages;

        public MainPage()
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
            int[] liczniki = new int[7];

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

        private void ResetujGre()
        {
            wynikCalkowityGry = 0;
            lblRollResult.Text = "Wynik tego losowania: 0";
            lblGameResult.Text = "Wynik gry: 0";

            foreach (var img in kosciImages)
            {
                img.Source = "question.jpg";
            }
        }

        private void OnRollDiceClicked(object sender, EventArgs e)
        {
            int[] rzuty = new int[5];

            for (int i = 0; i < 5; i++)
            {
                rzuty[i] = random.Next(1, 7);
                kosciImages[i].Source = $"k{rzuty[i]}.jpg";
            }

            int wynikTegoLosowania = LiczPunkty(rzuty);
            wynikCalkowityGry += wynikTegoLosowania;

            lblRollResult.Text = $"Wynik tego losowania: {wynikTegoLosowania}";
            lblGameResult.Text = $"Wynik gry: {wynikCalkowityGry}";
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            ResetujGre();
        }
    }
}
