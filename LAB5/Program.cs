using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace PracaZPlikami
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko}, Oceny: [{string.Join(", ", Oceny)}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Zadanie 2
            ZapiszLinieDoPliku();

            // Zadanie 3
            OdczytajLinieZPliku();

            // Zadanie 4
            DopiszLinieDoPliku();

            // Zadanie 6
            SerializujStudentowDoJson();

            // Zadanie 7
            DeserializujStudentowZJson();

            // Zadanie 8
            SerializujStudentowDoXml();

            // Zadanie 9
            DeserializujStudentowZXml();

            // Zadanie 10 i 11
            AnalizujIrisCsv();

            // Zadanie 12
            FiltrujIrisCsv();

            Console.WriteLine("Wszystkie zadania zakończone. Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        // Zadanie 2
        static void ZapiszLinieDoPliku()
        {
            Console.WriteLine("\n=== Zadanie 2: Zapisywanie linii do pliku ===");
            List<string> linie = new List<string>();

            Console.WriteLine("Wprowadź kilka linii tekstu (wpisz 'koniec' aby zakończyć):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input?.Trim().ToLower() == "koniec") break;
                linie.Add(input);
            }

            File.WriteAllLines("dane.txt", linie);
            Console.WriteLine("Zapisano do pliku dane.txt");
        }

        // Zadanie 3
        static void OdczytajLinieZPliku()
        {
            Console.WriteLine("\n=== Zadanie 3: Odczyt linii z pliku ===");
            if (File.Exists("dane.txt"))
            {
                string[] linie = File.ReadAllLines("dane.txt");
                Console.WriteLine("Zawartość pliku dane.txt:");
                foreach (var linia in linie)
                {
                    Console.WriteLine(linia);
                }
            }
            else
            {
                Console.WriteLine("Plik dane.txt nie istnieje.");
            }
        }

        // Zadanie 4
        static void DopiszLinieDoPliku()
        {
            Console.WriteLine("\n=== Zadanie 4: Dopisywanie do istniejącego pliku ===");
            List<string> noweLinie = new List<string>();

            Console.WriteLine("Wprowadź nowe linie (wpisz 'koniec' aby zakończyć):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input?.Trim().ToLower() == "koniec") break;
                noweLinie.Add(input);
            }

            File.AppendAllLines("dane.txt", noweLinie);
            Console.WriteLine("Dopisywanie zakończone.");
        }

        // Zadanie 6
        static void SerializujStudentowDoJson()
        {
            Console.WriteLine("\n=== Zadanie 6: Serializacja do JSON ===");

            var studenci = new List<Student>
            {
                new Student { Imie = "Anna", Nazwisko = "Kowalska", Oceny = new List<int> { 5, 4, 5, 3 } },
                new Student { Imie = "Jan", Nazwisko = "Nowak", Oceny = new List<int> { 4, 4, 5, 5 } },
                new Student { Imie = "Piotr", Nazwisko = "Wiśniewski", Oceny = new List<int> { 3, 5, 4, 4 } }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(studenci, options);

            File.WriteAllText("studenci.json", json);
            Console.WriteLine("Zapisano studentów do studenci.json");
        }

        // Zadanie 7
        static void DeserializujStudentowZJson()
        {
            Console.WriteLine("\n=== Zadanie 7: Deserializacja z JSON ===");
            if (File.Exists("studenci.json"))
            {
                string json = File.ReadAllText("studenci.json");
                var studenci = JsonSerializer.Deserialize<List<Student>>(json);

                Console.WriteLine("Odczytani studenci:");
                foreach (var s in studenci)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                Console.WriteLine("Plik studenci.json nie istnieje.");
            }
        }

        // Zadanie 8
        static void SerializujStudentowDoXml()
        {
            Console.WriteLine("\n=== Zadanie 8: Serializacja do XML ===");

            var studenci = new List<Student>
            {
                new Student { Imie = "Maria", Nazwisko = "Zielińska", Oceny = new List<int> { 5, 5, 4, 5 } },
                new Student { Imie = "Tomasz", Nazwisko = "Szymański", Oceny = new List<int> { 4, 3, 4, 5 } }
            };

            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var writer = new StreamWriter("studenci.xml"))
            {
                serializer.Serialize(writer, studenci);
            }
            Console.WriteLine("Zapisano studentów do studenci.xml");
        }

        // Zadanie 9
        static void DeserializujStudentowZXml()
        {
            Console.WriteLine("\n=== Zadanie 9: Deserializacja z XML ===");
            if (File.Exists("studenci.xml"))
            {
                var serializer = new XmlSerializer(typeof(List<Student>));
                using (var reader = new StreamReader("studenci.xml"))
                {
                    var studenci = (List<Student>)serializer.Deserialize(reader);
                    Console.WriteLine("Odczytani studenci:");
                    foreach (var s in studenci)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            else
            {
                Console.WriteLine("Plik studenci.xml nie istnieje.");
            }
        }

        // Zadania 10 i 11
        static void AnalizujIrisCsv()
        {
            Console.WriteLine("\n=== Zadania 10 i 11: Analiza iris.csv ===");
            string plik = "Iris.csv"; 

            if (!File.Exists(plik))
            {
                Console.WriteLine($"Plik {plik} nie został znaleziony.");
                return;
            }

            var linie = File.ReadAllLines(plik).Skip(1); 
            var dane = linie.Select(l => l.Split(',')).ToList();

            // Zadanie 10
            Console.WriteLine("Pierwsze 5 wierszy:");
            foreach (var wiersz in dane.Take(5))
            {
                Console.WriteLine(string.Join(" | ", wiersz));
            }

            double[] sumy = new double[4];
            int licznik = dane.Count;

            foreach (var wiersz in dane)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (double.TryParse(wiersz[i].Replace('.', ','), out double val)) 
                        sumy[i] += val;
                    else if (double.TryParse(wiersz[i], out val))
                        sumy[i] += val;
                }
            }

            Console.WriteLine("\nŚrednie wartości kolumn numerycznych:");
            string[] nazwyKolumn = { "sepal length", "sepal width", "petal length", "petal width" };
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{nazwyKolumn[i]}: {sumy[i] / licznik:F3}");
            }
        }

        // Zadanie 12
        static void FiltrujIrisCsv()
        {
            Console.WriteLine("\n=== Zadanie 12: Filtrowanie iris.csv ===");
            string plikWe = "Iris.csv";
            string plikWy = "iris_filtered.csv";

            if (!File.Exists(plikWe))
            {
                Console.WriteLine($"Plik {plikWe} nie został znaleziony.");
                return;
            }

            var wszystkieLinie = File.ReadAllLines(plikWe);
            var naglowek = wszystkieLinie[0];
            var dane = wszystkieLinie.Skip(1);

            var wyfiltrowane = dane
                .Where(l =>
                {
                    var kolumny = l.Split(',');
                    if (double.TryParse(kolumny[0].Replace('.', ','), out double sepalLength))
                        return sepalLength < 5;
                    return false;
                })
                .Select(l =>
                {
                    var kolumny = l.Split(',');
                    return $"{kolumny[0]},{kolumny[1]},{kolumny[4]}";
                });

            var wynik = new List<string> { "sepal length,sepal width,class" };
            wynik.AddRange(wyfiltrowane);

            File.WriteAllLines(plikWy, wynik);
            Console.WriteLine($"Zapisano wyfiltrowany zbiór do {plikWy}");
        }
    }
}
