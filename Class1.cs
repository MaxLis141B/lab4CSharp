using System;

class ATriangle
{
    protected int a;
    protected int b;
    protected int c_color;

    public ATriangle(int sideA, int sideB, int color)
    {
        a = sideA;
        b = sideB;
        c_color = color;
    }

    public int SideA
    {
        get => a;
        set => a = value;
    }

    public int SideB
    {
        get => b;
        set => b = value;
    }

    public int Color => c_color;

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => a,
                1 => b,
                2 => c_color,
                _ => PrintIndexError()
            };
        }
        set
        {
            switch (index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                case 2: c_color = value; break;
                default: Console.WriteLine("Помилка: некоректний індекс."); break;
            }
        }
    }

    private static int PrintIndexError()
    {
        Console.WriteLine("Помилка: доступні індекси 0 (a), 1 (b), 2 (колір).");
        return 0;
    }

    public void PrintSides()
    {
        double hypotenuse = Math.Sqrt(a * a + b * b);
        Console.WriteLine($"Сторони: катети {a} та {b}, гіпотенуза {hypotenuse:F2}");
    }

    public double GetPerimeter()
    {
        double hypotenuse = Math.Sqrt(a * a + b * b);
        return a + b + hypotenuse;
    }

    public double GetArea() => 0.5 * a * b;

    public bool IsIsosceles() => a == b;

    public static ATriangle operator ++(ATriangle tri)
    {
        tri.a++;
        tri.b++;
        return tri;
    }

    public static ATriangle operator --(ATriangle tri)
    {
        tri.a--;
        tri.b--;
        return tri;
    }

    public static bool operator true(ATriangle tri) => tri.a > 0 && tri.b > 0;
    public static bool operator false(ATriangle tri) => tri.a <= 0 || tri.b <= 0;

    public static ATriangle operator +(ATriangle tri, int scalar) =>
        new ATriangle(tri.a + scalar, tri.b + scalar, tri.c_color);

    public static implicit operator string(ATriangle tri) =>
        $"{tri.a};{tri.b};{tri.c_color}";

    public static explicit operator ATriangle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FormatException("Порожній рядок для перетворення у ATriangle.");
        }

        string[] parts = value.Split(';', StringSplitOptions.TrimEntries);
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out int sideA) ||
            !int.TryParse(parts[1], out int sideB) ||
            !int.TryParse(parts[2], out int color))
        {
            throw new FormatException("Формат повинен бути: a;b;color");
        }

        return new ATriangle(sideA, sideB, color);
    }
}
