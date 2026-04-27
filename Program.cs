class Program
{
    static void task1()
    {
        ATriangle tri = new ATriangle(3, 4, 255);
        Console.WriteLine("Початковий трикутник:");
        tri.PrintSides();

        Console.WriteLine($"\nКолір через індекс [2]: {tri[2]}");
        tri[0] = 6;

        tri++;
        tri = tri + 3;

        Console.WriteLine("\nПісля операцій (tri++ та tri + 3):");
        tri.PrintSides();
        Console.WriteLine($"Площа: {tri.GetArea()}");

        Console.WriteLine($"Чи рівнобедрений: {(tri.IsIsosceles() ? "Так" : "Ні")}");
        if (tri)
            Console.WriteLine("Об'єкт є коректним трикутником.");

        string serialized = tri;
        Console.WriteLine($"Серіалізований трикутник: {serialized}");
        ATriangle parsed = (ATriangle)serialized;
        Console.WriteLine("Відновлений з рядка:");
        parsed.PrintSides();

        ATriangle[] array = {
                new ATriangle(5, 5, 10),
                new ATriangle(3, 4, 20),
                new ATriangle(12, 12, 30)
            };

        int count = 0;
        foreach (var t in array)
            if (t.IsIsosceles()) count++;

        Console.WriteLine($"\nКількість рівнобедрених у масиві: {count}");
    }

    static void task2()
    {
        VectorDouble v1 = new VectorDouble(3, 2.5);
        VectorDouble v2 = new VectorDouble(3);

        v2[0] = 1.0;
        v2[1] = 2.0;
        v2[2] = 3.0;

        Console.WriteLine("Вектор 1 (ініціалізований 2.5):");
        v1.Output();

        Console.WriteLine("\nВектор 2 (заповнений через індексатор):");
        v2.Output();

        VectorDouble vSum = v1 + v2;
        Console.WriteLine("\nДодавання векторів (v1 + v2):");
        vSum.Output();

        VectorDouble vMult = v2 * 10.0;
        Console.WriteLine("\nМноження вектора на скаляр (v2 * 10.0):");
        vMult.Output();

        v2++;
        Console.WriteLine("\nІнкремент вектора (v2++):");
        v2.Output();

        Console.WriteLine($"\nРозмір вектора v1: {v1.Size}");
        Console.WriteLine($"Чи коректний вектор v1 (перевірка true/false): {(v1 ? "Так" : "Ні")}");
        Console.WriteLine($"Загальна кількість створених векторів: {VectorDouble.GetVectorCount()}");
    }

    static void task3()
    {
        MatrixDouble m1 = new MatrixDouble(2, 2, 3.0);
        MatrixDouble m2 = new MatrixDouble(2, 2);

        m2[0, 0] = 1.0;
        m2[0, 1] = 2.0;
        m2[2] = 3.0;
        m2[3] = 4.0;

        Console.WriteLine("Матриця 1 (заповнена 3.0):");
        m1.Output();

        Console.WriteLine("\nМатриця 2 (заповнена через 1D та 2D індексатори):");
        m2.Output();

        MatrixDouble mSum = m1 + m2;
        Console.WriteLine("\nСума матриць (m1 + m2):");
        mSum.Output();

        MatrixDouble mProd = m1 * m2;
        Console.WriteLine("\nМноження матриць (m1 * m2):");
        mProd.Output();

        VectorDouble vec = new VectorDouble(2);
        vec[0] = 10;
        vec[1] = 20;

        Console.WriteLine("\nВектор для множення на матрицю:");
        vec.Output();

        VectorDouble resVec = m2 * vec;
        Console.WriteLine("\nРезультат множення (Матриця 2 * Вектор):");
        resVec.Output();

        Console.WriteLine($"\nРозмір Матриці 2: {m2.Rows}x{m2.Cols}");
        Console.WriteLine($"Загальна кількість створених матриць: {MatrixDouble.GetMatrixCount()}");
    }

    static void task4()
    {
        Console.WriteLine("=== ВАРІАНТ 1: КЛАСИ ===");
        PatientManagement.PatientTask.RunClassVariant();

        Console.WriteLine("\n=== ВАРІАНТ 2: КОРТЕЖІ ===");
        PatientManagement.PatientTask.RunTupleVariant();

        Console.WriteLine("\n=== ВАРІАНТ 3: ЗАПИСИ ===");
        PatientManagement.PatientTask.RunRecordVariant();

        Console.WriteLine("\n=== ВАРІАНТ 4: СТРУКТУРИ ===");
        PatientManagement.PatientTask.RunStructVariant();
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== ВИКОНАННЯ ЗАВДАННЯ 1 ===");
        task1();
        Console.WriteLine("\n\n=== ВИКОНАННЯ ЗАВДАННЯ 2 ===");
        task2();
        Console.WriteLine("\n\n=== ВИКОНАННЯ ЗАВДАННЯ 3 ===");
        task3();
        Console.WriteLine("\n\n=== ВИКОНАННЯ ЗАВДАННЯ 4 (Patient) ===");
        task4();
    }
}
