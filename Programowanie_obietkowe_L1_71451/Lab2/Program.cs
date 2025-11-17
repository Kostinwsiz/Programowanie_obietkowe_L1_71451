using System;

class Zwierze
{
    protected string nazwa;

    public Zwierze(string imie)
    {
        nazwa = imie;
    }

    public virtual void DajGlos()
    {
        Console.WriteLine("...");
    }
}

class Pies : Zwierze
{
    public Pies(string imie) : base(imie)
    {
    }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi woof woof!");
    }
}

class Kot : Zwierze
{
    public Kot(string imie) : base(imie)
    { }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi miau miau!");
    }
}

class Waz : Zwierze
{
    public Waz(string imie) : base(imie) { }

    public override void DajGlos()
    {
        Console.WriteLine($"{nazwa} robi ssssssss!");
    }
}

abstract class Pracownik
{
    public abstract void Pracuj();
}

class Piekarz : Pracownik
{
    public override void Pracuj()
    {
        Console.WriteLine("Trwa pieczenie…");
    }
}

class A
{
    public A()
    {
        Console.WriteLine("To jest konstruktor A");
    }
}

class B : A
{
    public B() : base()
    {
        Console.WriteLine("To jest konstruktor B");
    }
}

class C : B
{
    public C() : base()
    {
        Console.WriteLine("To jest konstruktor C");
    }
}

class MainClass
{
    static void powiedz_cos(Zwierze zwierze)
    {
        Console.Write($"Typ obiektu: {zwierze.GetType().Name} - ");
        zwierze.DajGlos();
    }

    static void Main()
    {
        Zwierze zwierze = new Zwierze("Podstawowe zwierzę");
        Pies pies = new Pies("Burek");
        Kot kot = new Kot("Mruczek");
        Waz waz = new Waz("Pyton");

        Console.WriteLine("=== Wywołanie metody powiedz_cos() ===");

        powiedz_cos(zwierze);
        powiedz_cos(pies);
        powiedz_cos(kot);
        powiedz_cos(waz);

        Console.WriteLine("\n=== Bezpośrednie wywołanie DajGlos() ===");

        zwierze.DajGlos();
        pies.DajGlos();
        kot.DajGlos();
        waz.DajGlos();

        Console.WriteLine("\n=== Przykład użycia klasy Piekarz ===");

        Piekarz piekarz = new Piekarz();
        piekarz.Pracuj();

        Console.WriteLine("\n=== Zadanie 11: Próba utworzenia obiektu klasy Pracownik ===");


        Console.WriteLine("\n=== Zadanie 15: Kolejność wykonywania konstruktorów ===");

        Console.WriteLine("Tworzenie obiektu A:");
        A obiektA = new A();

        Console.WriteLine("\nTworzenie obiektu B:");
        B obiektB = new B();

        Console.WriteLine("\nTworzenie obiektu C:");
        C obiektC = new C();
    }
}