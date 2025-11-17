using System;
using System.Collections.Generic;
using System.Linq;

public interface IModular
{
    public double Module();
}

public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
{
    private double re;
    private double im;

    public double Re { get => re; set => re = value; }
    public double Im { get => im; set => im = value; }

    public ComplexNumber(double re, double im)
    {
        this.re = re;
        this.im = im;
    }

    public override string ToString()
    {
        string sign = im >= 0 ? "+" : "-";
        return $"{re} {sign} {Math.Abs(im)}i";
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re + b.re, a.im + b.im);

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re - b.re, a.im - b.im);

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);

    public static ComplexNumber operator -(ComplexNumber a)
        => new ComplexNumber(-a.re, -a.im);

    public object Clone() => new ComplexNumber(re, im);

    public bool Equals(ComplexNumber other)
    {
        if (other == null) return false;
        return re == other.re && im == other.im;
    }

    public override bool Equals(object obj)
        => obj is ComplexNumber other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(re, im);

    public static bool operator ==(ComplexNumber a, ComplexNumber b)
        => a?.Equals(b) ?? b is null;

    public static bool operator !=(ComplexNumber a, ComplexNumber b)
        => !(a == b);

    public double Module()
        => Math.Sqrt(re * re + im * im);

    public int CompareTo(ComplexNumber other)
    {
        if (other == null) return 1;
        return this.Module().CompareTo(other.Module());
    }
}

class Program
{
    static void PrintCollection<T>(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
    }

    static void Main()
    {
        Console.WriteLine("=== ZADANIE 2 - TABLICA ===");

        ComplexNumber[] complexArray = new ComplexNumber[]
        {
            new ComplexNumber(3, 4),
            new ComplexNumber(1, 2),
            new ComplexNumber(5, -1),
            new ComplexNumber(-2, -3),
            new ComplexNumber(0, 5)
        };

        Console.WriteLine("\n2a. Tablica liczb zespolonych:");
        foreach (var number in complexArray)
        {
            Console.WriteLine(number);
        }

        Array.Sort(complexArray);
        Console.WriteLine("\n2b. Posortowana tablica według modułu:");
        foreach (var number in complexArray)
        {
            Console.WriteLine($"{number} (moduł: {number.Module():F2})");
        }

        Console.WriteLine($"\n2c. Minimum: {complexArray.Min()}");
        Console.WriteLine($"Maksimum: {complexArray.Max()}");

        var filteredArray = complexArray.Where(c => c.Im >= 0).ToArray();
        Console.WriteLine("\n2d. Liczby z nieujemną częścią urojoną:");
        foreach (var number in filteredArray)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\n=== ZADANIE 3 - LISTA ===");

        List<ComplexNumber> complexList = new List<ComplexNumber>
        {
            new ComplexNumber(3, 4),
            new ComplexNumber(1, 2),
            new ComplexNumber(5, -1),
            new ComplexNumber(-2, -3),
            new ComplexNumber(0, 5),
            new ComplexNumber(2, 1)
        };

        Console.WriteLine("\n3. Lista liczb zespolonych:");
        PrintCollection(complexList);

        complexList.RemoveAt(1);
        Console.WriteLine("\n3a. Po usunięciu drugiego elementu:");
        PrintCollection(complexList);

        var minElement = complexList.Min();
        complexList.Remove(minElement);
        Console.WriteLine("\n3b. Po usunięciu najmniejszego elementu:");
        PrintCollection(complexList);

        complexList.Clear();
        Console.WriteLine("\n3c. Po wyczyszczeniu listy:");
        Console.WriteLine($"Liczba elementów: {complexList.Count}");

        Console.WriteLine("\n=== ZADANIE 4 - ZBIÓR (HashSet) ===");

        HashSet<ComplexNumber> complexSet = new HashSet<ComplexNumber>();
        ComplexNumber z1 = new ComplexNumber(6, 7);
        ComplexNumber z2 = new ComplexNumber(1, 2);
        ComplexNumber z3 = new ComplexNumber(6, 7);
        ComplexNumber z4 = new ComplexNumber(1, -2);
        ComplexNumber z5 = new ComplexNumber(-5, 9);

        complexSet.Add(z1);
        complexSet.Add(z2);
        complexSet.Add(z3);
        complexSet.Add(z4);
        complexSet.Add(z5);

        Console.WriteLine("\n4a. Zawartość zbioru (HashSet):");
        foreach (var number in complexSet)
        {
            Console.WriteLine(number);
        }
        Console.WriteLine($"Liczba elementów w zbiorze: {complexSet.Count} (z3 nie został dodany - duplikat z1)");

        Console.WriteLine($"\n4b. Operacje na zbiorze:");
        Console.WriteLine($"Minimum: {complexSet.Min()}");
        Console.WriteLine($"Maksimum: {complexSet.Max()}");

        var sortedSet = complexSet.OrderBy(c => c.Module());
        Console.WriteLine("Posortowany zbiór:");
        foreach (var number in sortedSet)
        {
            Console.WriteLine($"{number} (moduł: {number.Module():F2})");
        }

        var filteredSet = complexSet.Where(c => c.Im >= 0);
        Console.WriteLine("Przefiltrowany zbiór (nieujemna część urojona):");
        foreach (var number in filteredSet)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("\n=== ZADANIE 5 - SŁOWNIK ===");

        Dictionary<string, ComplexNumber> complexDict = new Dictionary<string, ComplexNumber>
        {
            ["z1"] = z1,
            ["z2"] = z2,
            ["z3"] = z3,
            ["z4"] = z4,
            ["z5"] = z5
        };

        Console.WriteLine("\n5a. Wszystkie elementy słownika:");
        foreach (var kvp in complexDict)
        {
            Console.WriteLine($"Klucz: {kvp.Key}, Wartość: {kvp.Value}");
        }

        Console.WriteLine("\n5b. Klucze:");
        foreach (var key in complexDict.Keys)
        {
            Console.WriteLine(key);
        }

        Console.WriteLine("\nWartości:");
        foreach (var value in complexDict.Values)
        {
            Console.WriteLine(value);
        }

        Console.WriteLine($"\n5c. Czy istnieje klucz 'z6'? {complexDict.ContainsKey("z6")}");

        Console.WriteLine($"\n5d. Minimum: {complexDict.Values.Min()}");
        Console.WriteLine($"Maksimum: {complexDict.Values.Max()}");

        var filteredDict = complexDict.Where(kvp => kvp.Value.Im >= 0);
        Console.WriteLine("\nPrzefiltrowany słownik (nieujemna część urojona):");
        foreach (var kvp in filteredDict)
        {
            Console.WriteLine($"Klucz: {kvp.Key}, Wartość: {kvp.Value}");
        }

        complexDict.Remove("z3");
        Console.WriteLine("\n5e. Po usunięciu elementu 'z3':");
        foreach (var kvp in complexDict)
        {
            Console.WriteLine($"Klucz: {kvp.Key}, Wartość: {kvp.Value}");
        }

        if (complexDict.Count >= 2)
        {
            string secondKey = complexDict.Keys.ElementAt(1);
            complexDict.Remove(secondKey);
            Console.WriteLine($"\n5f. Po usunięciu drugiego elementu ('{secondKey}'):");
            foreach (var kvp in complexDict)
            {
                Console.WriteLine($"Klucz: {kvp.Key}, Wartość: {kvp.Value}");
            }
        }

        complexDict.Clear();
        Console.WriteLine($"\n5g. Po wyczyszczeniu słownika: {complexDict.Count} elementów");

        Console.WriteLine("\n=== ZADANIE 6 - KOLEJKA I STOS ===");

        Queue<ComplexNumber> queue = new Queue<ComplexNumber>();
        queue.Enqueue(new ComplexNumber(1, 1));
        queue.Enqueue(new ComplexNumber(2, 2));
        queue.Enqueue(new ComplexNumber(3, 3));

        Console.WriteLine("\nKolejka (FIFO):");
        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            Console.WriteLine($"Zdjęto z kolejki: {item}");
        }

        Stack<ComplexNumber> stack = new Stack<ComplexNumber>();
        stack.Push(new ComplexNumber(1, 1));
        stack.Push(new ComplexNumber(2, 2));
        stack.Push(new ComplexNumber(3, 3));

        Console.WriteLine("\nStos (LIFO):");
        while (stack.Count > 0)
        {
            var item = stack.Pop();
            Console.WriteLine($"Zdjęto ze stosu: {item}");
        }

        Console.WriteLine("\n=== TEST OPERATORA NEGACJI ===");
        ComplexNumber testNumber = new ComplexNumber(3, -4);
        ComplexNumber negated = -testNumber;
        Console.WriteLine($"Original: {testNumber}");
        Console.WriteLine($"Po negacji: {negated}");

        Console.WriteLine("\n=== KONIEC ===");
    }
}