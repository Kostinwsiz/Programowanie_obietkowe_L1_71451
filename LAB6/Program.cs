using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}

public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}

public class Program
{
    public static void Main()
    {
        string connectionString =
            "Data Source=10.200.2.28;" +
            "Initial Catalog=studenci_{71451};" + 
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.");


            WyswietlStudentow(connection);
            WyswietlStudentaPoId(connection, 2);

            var allStudents = PobierzStudentowZOceniami(connection);
            foreach (var s in allStudents)
            {
                Console.WriteLine($"{s.StudentId} {s.Imie} {s.Nazwisko}");
                foreach (var oc in s.Oceny)
                    Console.WriteLine($"  {oc.Przedmiot} - {oc.Wartosc}");
            }

            DodajStudenta(connection, new Student { Imie = "Nowy", Nazwisko = "Student" });

            DodajOcene(connection, new Ocena { Wartosc = 3.5, Przedmiot = "matematyka", StudentId = 1 });

            UsunOcenyZGeografii(connection);

            AktualizujOcene(connection, 1, 4.0);
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc.Message);
        }
    }

    public static void WyswietlStudentow(SqlConnection connection)
    {
        string query = "SELECT student_id, imie, nazwisko FROM student";
        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();
        Console.WriteLine("Lista studentów:");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["student_id"]}, {reader["imie"]}, {reader["nazwisko"]}");
        }
        Console.WriteLine();
    }

    public static void WyswietlStudentaPoId(SqlConnection connection, int studentId)
    {
        string query = "SELECT imie, nazwisko FROM student WHERE student_id = @studentId";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@studentId", studentId);
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
            Console.WriteLine($"Student o ID {studentId}: {reader["imie"]} {reader["nazwisko"]}");
        else
            Console.WriteLine($"Brak studenta o ID {studentId}");
        Console.WriteLine();
    }

    public static List<Student> PobierzStudentowZOceniami(SqlConnection connection)
    {
        var students = new List<Student>();
        string query = @"
            SELECT s.student_id, s.imie, s.nazwisko,
                   o.ocena_id, o.wartosc, o.przedmiot
            FROM student s
            LEFT JOIN ocena o ON s.student_id = o.student_id
            ORDER BY s.student_id";

        using SqlCommand command = new SqlCommand(query, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            int id = (int)reader["student_id"];
            var student = students.Find(st => st.StudentId == id);
            if (student == null)
            {
                student = new Student
                {
                    StudentId = id,
                    Imie = (string)reader["imie"],
                    Nazwisko = (string)reader["nazwisko"]
                };
                students.Add(student);
            }

            if (reader["ocena_id"] != DBNull.Value)
            {
                student.Oceny.Add(new Ocena
                {
                    OcenaId = (int)reader["ocena_id"],
                    Wartosc = Convert.ToDouble(reader["wartosc"]),
                    Przedmiot = (string)reader["przedmiot"],
                    StudentId = id
                });
            }
        }

        return students;
    }

    public static void DodajStudenta(SqlConnection connection, Student student)
    {
        string query = "INSERT INTO student (imie, nazwisko) VALUES (@imie, @nazwisko)";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@imie", student.Imie);
        command.Parameters.AddWithValue("@nazwisko", student.Nazwisko);
        int rows = command.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Dodano studenta." : "Nie dodano studenta.");
        Console.WriteLine();
    }

    public static void DodajOcene(SqlConnection connection, Ocena ocena)
    {
        if (!CzyOcenaPoprawna(ocena.Wartosc))
        {
            Console.WriteLine("Nieprawidłowa wartość oceny!");
            Console.WriteLine();
            return;
        }

        string query = "INSERT INTO ocena (wartosc, przedmiot, student_id) VALUES (@wartosc, @przedmiot, @student_id)";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@wartosc", ocena.Wartosc);
        command.Parameters.AddWithValue("@przedmiot", ocena.Przedmiot);
        command.Parameters.AddWithValue("@student_id", ocena.StudentId);
        command.ExecuteNonQuery();
        Console.WriteLine("Dodano ocenę.");
        Console.WriteLine();
    }

    private static bool CzyOcenaPoprawna(double wartosc)
    {
        if (wartosc < 2.0 || wartosc > 5.0)
            return false;
        if (Math.Abs(wartosc * 2 - Math.Round(wartosc * 2)) > 0.0001)
            return false;
        if (wartosc == 2.5)
            return false;
        return true;
    }

    public static void UsunOcenyZGeografii(SqlConnection connection)
    {
        string query = "DELETE FROM ocena WHERE przedmiot = 'geografia'";
        using SqlCommand command = new SqlCommand(query, connection);
        int rows = command.ExecuteNonQuery();
        Console.WriteLine($"Usunięto {rows} ocen z geografii.");
        Console.WriteLine();
    }

    public static void AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
    {
        if (!CzyOcenaPoprawna(nowaWartosc))
        {
            Console.WriteLine("Nieprawidłowa wartość oceny do aktualizacji!");
            Console.WriteLine();
            return;
        }

        string query = "UPDATE ocena SET wartosc = @wartosc WHERE ocena_id = @ocenaId";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@wartosc", nowaWartosc);
        command.Parameters.AddWithValue("@ocenaId", ocenaId);
        int rows = command.ExecuteNonQuery();

        Console.WriteLine(rows > 0 ? "Zaktualizowano ocenę." : "Brak oceny do aktualizacji.");
        Console.WriteLine();
    }
}
