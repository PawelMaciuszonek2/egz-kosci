#nullable disable
using System;

namespace GraWKosci
{
    class Program
    {
        /*
        ************************************************
        nazwa: RzucKoscmi
        opis: Losuje określoną liczbę rzutów kośćmi sześciennymi (od 1 do 6 oczek)
              i zapisuje wylosowane wartości do tablicy.
        parametry: liczbaKostek - liczba kostek do wylosowania (z przedziału od 3 do 10)
        zwracany typ i opis: int[] - tablica z wylosowanymi wartościami oczek dla każdej kostki
        autor: 00000000000
        ************************************************
        */
        public static int[] RzucKoscmi(int liczbaKostek)
        {
            Random random = new Random();
            int[] wyniki = new int[liczbaKostek];
            for (int i = 0; i < liczbaKostek; i++)
            {
                wyniki[i] = random.Next(1, 7);
            }
            return wyniki;
        }

        /*
        ************************************************
        nazwa: LiczPunkty
        opis: Oblicza sumę punktów uzyskanych w rzucie kośćmi. Zgodnie z zasadami gry,
              punkty są sumą oczek tych liczb, które zostały wylosowane co najmniej dwa razy.
        parametry: kostki - tablica liczb całkowitych przechowująca wylosowane oczka na poszczególnych kostkach
        zwracany typ i opis: int - łączna liczba zdobytych punktów w danym rzucie
        autor: 00000000000
        ************************************************
        */
        public static int LiczPunkty(int[] kostki)
        {
            int sumaPunktow = 0;
            int[] liczniki = new int[7]; // indeksy 1-6 dla ścianek kostki

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

        static void Main(string[] args)
        {
            int liczbaKostek = 0;

            // Punkt 1: Pobiera z klawiatury liczbę kostek z przedziału <3, 10>
            do
            {
                Console.Write("Ile kostek chcesz rzucić?(3 - 10)\n");
                string wejscie = Console.ReadLine();
                if (int.TryParse(wejscie, out int n))
                {
                    if (n >= 3 && n <= 10)
                    {
                        liczbaKostek = n;
                    }
                }
            } while (liczbaKostek < 3 || liczbaKostek > 10);

            // Pętla powtarzania gry
            string kontynuacja = "t";
            while (kontynuacja.ToLower() == "t")
            {
                // Punkt 2: Losuje tyle liczb całkowitych z przedziału 1-6, ile podano z klawiatury
                int[] kostki = RzucKoscmi(liczbaKostek);

                // Punkt 3: Wypisuje ile wyrzucono oczek na każdej kostce
                for (int i = 0; i < kostki.Length; i++)
                {
                    Console.WriteLine($"Kostka {i + 1}: {kostki[i]}");
                }

                // Punkt 4: Liczy punkty i je wyświetla
                int punkty = LiczPunkty(kostki);
                Console.WriteLine($"Liczba uzyskanych punktów: {punkty}");

                // Punkt 5: Pyta grającego czy powtórzyć grę ('t' -> idź do punktu 2, 'n' -> koniec)
                Console.Write("Jeszcze raz? (t/n)\n");
                kontynuacja = Console.ReadLine()?.Trim().ToLower() ?? "n";
            }
        }
    }
}
