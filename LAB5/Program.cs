using System;
using System.IO;

class Program
{
    static void ZapiszDoPliku()
    {
        Console.WriteLine("Podaj liczbe tekstuw, kture chcesz wprowadzić: ");
        int liczbaTekstuw;

        while (!int.TryParse(Console.ReadLine(), out liczbaTekstuw) || liczbaTekstuw <= 0)
        {
            Console.WriteLine("Prosze podać poprawnu liczbu większą od 0");
        }

        using (StreamWriter writer = new StreamWriter("wprowadzone_teksty.txt"))
        {
            for (int i = 1; i <= liczbaTekstuw; i++)
            {
                Console.WriteLine($"Podaj tekst nr {i}:");
                string tekst = Console.ReadLine();
                writer.WriteLine(tekst);
            }
        }

        Console.WriteLine("Wszystkie wpisane informacje zostały zapisane do pliku");
    }

    static void Main(string[] args)
    {
        ZapiszDoPliku();
    }
}