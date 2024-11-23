using System;

public struct Vector3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3D(double x, double y, double z)
    {
        try
        {
            if (x < 0 || y < 0 || z < 0)
                throw new ArgumentOutOfRangeException("Координати не повинні бути від'ємними.");
            X = x;
            Y = y;
            Z = z;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($" {ex.Message}");
            X = Y = Z = 0;
        }
        finally
        {
            Console.WriteLine(" Роботу завершено.");
        }
    }

    public override string ToString()
    {
        return $"Vector3D: (X: {X:F2}, Y: {Y:F2}, Z: {Z:F2})";
    }

    //операції з векторами
    public static Vector3D operator +(Vector3D v1, Vector3D v2)
    {
        return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3D operator -(Vector3D v1, Vector3D v2)
    {
        return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static double operator *(Vector3D v1, Vector3D v2)
    {
        return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    }

    public static Vector3D operator *(Vector3D v, double scalar)
    {
        return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
    }

    public static bool operator ==(Vector3D v1, Vector3D v2)
    {
        return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
    }

    public static bool operator !=(Vector3D v1, Vector3D v2)
    {
        return !(v1 == v2);
    }

    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector3D)
        {
            Vector3D v = (Vector3D)obj;
            return this == v;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (X, Y, Z).GetHashCode();
    }
}
public class Program
{
    public static Vector3D[] ReadArray(int n)
    {
        Vector3D[] arr = new Vector3D[n];
        for (int i = 0; i < n; i++)
        {
            try
            {
                Console.WriteLine($"\n Введіть координати векторів {i + 1}:");
                Console.Write("  X: ");
                double x = double.Parse(Console.ReadLine());
                Console.Write("  Y: ");
                double y = double.Parse(Console.ReadLine());
                Console.Write("  Z: ");
                double z = double.Parse(Console.ReadLine());
                arr[i] = new Vector3D(x, y, z);
            }
            catch (FormatException)
            {
                Console.WriteLine(" Неправильно введене значення, повторіть спробу");
                i--;
            }
        }
        return arr;
    }
    public static void DisplayVector(Vector3D v)
    {
        Console.WriteLine(v.ToString());
        Console.WriteLine($"  Довжина: {v.Length():F2}");
    }
    public static void SortArray(ref Vector3D[] arr)
    {
        Array.Sort(arr, (v1, v2) => v1.Length().CompareTo(v2.Length()));
    }

    public static void ModifyVector(ref Vector3D v)
    {
        Console.WriteLine(" Модифікація вектору");
        v.X += 1;
        v.Y += 2;
        v.Z += 3;
    }
    public static void MinMaxLength(Vector3D[] arr, out double minLen, out double maxLen)
    {
        minLen = double.MaxValue;
        maxLen = double.MinValue;
        foreach (var v in arr)
        {
            double len = v.Length();
            if (len < minLen) minLen = len;
            if (len > maxLen) maxLen = len;
        }
    }
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(" Введіть кількість векторів:");
        int n = int.Parse(Console.ReadLine());

        Vector3D[] arr = ReadArray(n);

        Console.WriteLine("\n Вектори:");
        foreach (var v in arr)
        {
            DisplayVector(v);
        }

        SortArray(ref arr);
        Console.WriteLine("\n Вектори, посортовані за довжиною:");
        foreach (var v in arr)
        {
            DisplayVector(v);
        }

        double minLen, maxLen;
        MinMaxLength(arr, out minLen, out maxLen);
        Console.WriteLine($"\n Мін довжина: {minLen:F2}, Макс довжина: {maxLen:F2}");

        if (arr.Length >= 2)
        {
            Vector3D sum = arr[0] + arr[1];
            Vector3D diff = arr[0] - arr[1];
            double dotProduct = arr[0] * arr[1]; //скалярний добуток

            Console.WriteLine("\n Введіть значення скаляра:");
            double scalar = double.Parse(Console.ReadLine());

            Vector3D scaledVector = arr[0] * scalar;

            Console.WriteLine("\n Результат додавання та віднімання векторів:");
            Console.WriteLine("  Додавання:");
            DisplayVector(sum);
            Console.WriteLine("  Віднімання:");
            DisplayVector(diff);

            Console.WriteLine($"\n Скалярний добуток перших двох векторів: {dotProduct:F2}");

            Console.WriteLine($"\n Вектор після множення на скаляр {scalar}:");
            DisplayVector(scaledVector);
        }
        else
        {
            Console.WriteLine("\n Повинно бути як мінімум два вектори.");
        }
    }
}