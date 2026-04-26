using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement
{

    public class PatientClass
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string MedCardNumber { get; set; }
        public string InsuranceNumber { get; set; }

        public override string ToString()
        {
            return $"[Class] {Surname} {Name} {Patronymic} | Адреса: {Address} | Мед.картка: {MedCardNumber} | Поліс: {InsuranceNumber}";
        }
    }


    public record PatientRecord(
        string Surname, 
        string Name, 
        string Patronymic, 
        string Address, 
        string MedCardNumber, 
        string InsuranceNumber
    );

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            
        }


        static void RunClassVariant()
        {

            List<PatientClass> patients = new List<PatientClass>
            {
                new PatientClass { Surname = "Іванов", Name = "Іван", Patronymic = "Петрович", Address = "м. Київ, вул. 1", MedCardNumber = "MC001", InsuranceNumber = "INS111" },
                new PatientClass { Surname = "Петрова", Name = "Марія", Patronymic = "Сергіївна", Address = "м. Львів, вул. 2", MedCardNumber = "MC002", InsuranceNumber = "INS222" },
                new PatientClass { Surname = "Сидоров", Name = "Олег", Patronymic = "Миколайович", Address = "м. Одеса, вул. 3", MedCardNumber = "MC003", InsuranceNumber = "INS333" }
            };

            PrintList(patients);


            Console.Write("Введіть номер медичної картки для видалення (напр. MC002): ");
            string cardToDelete = Console.ReadLine();
            

            patients.RemoveAll(p => p.MedCardNumber == cardToDelete);
            Console.WriteLine($"-- Після видалення {cardToDelete} --");
            PrintList(patients);


            var newP1 = new PatientClass { Surname = "Коваленко", Name = "Анна", Patronymic = "Вікторівна", Address = "м. Харків, вул. 4", MedCardNumber = "MC004", InsuranceNumber = "INS444" };
            var newP2 = new PatientClass { Surname = "Бондаренко", Name = "Дмитро", Patronymic = "Олександрович", Address = "м. Дніпро, вул. 5", MedCardNumber = "MC005", InsuranceNumber = "INS555" };

            patients.Insert(0, newP2); 
            patients.Insert(0, newP1);

            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintList(patients);
        }


        static void RunTupleVariant()
        {
            List<(string Surname, string Name, string Patronymic, string Address, string MedCard, string Insurance)> patients = new List<(string, string, string, string, string, string)>
            {
                ("Іванов", "Іван", "Петрович", "м. Київ, вул. 1", "MC001", "INS111"),
                ("Петрова", "Марія", "Сергіївна", "м. Львів, вул. 2", "MC002", "INS222"),
                ("Сидоров", "Олег", "Миколайович", "м. Одеса, вул. 3", "MC003", "INS333")
            };

            PrintTupleList(patients);

            Console.Write("Введіть номер медичної картки для видалення (напр. MC002): ");
            string cardToDelete = Console.ReadLine();

            patients.RemoveAll(p => p.MedCard == cardToDelete);
            Console.WriteLine($"-- Після видалення {cardToDelete} --");
            PrintTupleList(patients);

            var newP1 = ("Коваленко", "Анна", "Вікторівна", "м. Харків, вул. 4", "MC004", "INS444");
            var newP2 = ("Бондаренко", "Дмитро", "Олександрович", "м. Дніпро, вул. 5", "MC005", "INS555");

            patients.Insert(0, newP2);
            patients.Insert(0, newP1);

            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintTupleList(patients);
        }

        static void RunRecordVariant()
        {
            List<PatientRecord> patients = new List<PatientRecord>
            {
                new PatientRecord("Іванов", "Іван", "Петрович", "м. Київ, вул. 1", "MC001", "INS111"),
                new PatientRecord("Петрова", "Марія", "Сергіївна", "м. Львів, вул. 2", "MC002", "INS222"),
                new PatientRecord("Сидоров", "Олег", "Миколайович", "м. Одеса, вул. 3", "MC003", "INS333")
            };

            PrintRecordList(patients);

            // 2. Видалення
            Console.Write("Введіть номер медичної картки для видалення (напр. MC002): ");
            string cardToDelete = Console.ReadLine();

            patients.RemoveAll(p => p.MedCardNumber == cardToDelete);
            Console.WriteLine($"-- Після видалення {cardToDelete} --");
            PrintRecordList(patients);

            var newP1 = new PatientRecord("Коваленко", "Анна", "Вікторівна", "м. Харків, вул. 4", "MC004", "INS444");
            var newP2 = new PatientRecord("Бондаренко", "Дмитро", "Олександрович", "м. Дніпро, вул. 5", "MC005", "INS555");

            patients.Insert(0, newP2);
            patients.Insert(0, newP1);

            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintRecordList(patients);
        }

        static void PrintList(List<PatientClass> list)
        {
            foreach (var p in list) Console.WriteLine(p);
        }

        static void PrintTupleList(List<(string Surname, string Name, string Patronymic, string Address, string MedCard, string Insurance)> list)
        {
            foreach (var p in list)
            {
                Console.WriteLine($"[Tuple] {p.Surname} {p.Name} {p.Patronymic} | Адреса: {p.Address} | Мед.картка: {p.MedCard} | Поліс: {p.Insurance}");
            }
        }

        static void PrintRecordList(List<PatientRecord> list)
        {
            foreach (var p in list) Console.WriteLine(p); 
    }
}