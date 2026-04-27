using System;
using System.Collections.Generic;

namespace PatientManagement
{
    public struct Patient
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string MedicalCardNumber { get; set; }
        public string InsurancePolicyNumber { get; set; }

        public Patient(string fullName, string address, string medicalCardNumber, string insurancePolicyNumber)
        {
            FullName = fullName;
            Address = address;
            MedicalCardNumber = medicalCardNumber;
            InsurancePolicyNumber = insurancePolicyNumber;
        }

        public override readonly string ToString() =>
            $"{FullName}, Адреса: {Address}, Медкартка: {MedicalCardNumber}, Поліс: {InsurancePolicyNumber}";
    }

    public class PatientClass
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string MedCardNumber { get; set; }
        public string InsuranceNumber { get; set; }

        public override string ToString() =>
            $"[Class] {Surname} {Name} {Patronymic} | Адреса: {Address} | Мед.картка: {MedCardNumber} | Поліс: {InsuranceNumber}";
    }

    public record PatientRecord(
        string Surname,
        string Name,
        string Patronymic,
        string Address,
        string MedCardNumber,
        string InsuranceNumber
    );

    public static class PatientTask
    {
        public static void RunClassVariant()
        {
            List<PatientClass> patients = new List<PatientClass>
            {
                new PatientClass { Surname = "Іванов", Name = "Іван", Patronymic = "Петрович", Address = "м. Київ, вул. 1", MedCardNumber = "MC001", InsuranceNumber = "INS111" },
                new PatientClass { Surname = "Петрова", Name = "Марія", Patronymic = "Сергіївна", Address = "м. Львів, вул. 2", MedCardNumber = "MC002", InsuranceNumber = "INS222" },
                new PatientClass { Surname = "Сидоров", Name = "Олег", Patronymic = "Миколайович", Address = "м. Одеса, вул. 3", MedCardNumber = "MC003", InsuranceNumber = "INS333" }
            };

            PrintClassList(patients);
            patients.RemoveAll(p => p.MedCardNumber == "MC002");
            Console.WriteLine("-- Після видалення MC002 --");
            PrintClassList(patients);

            var newP1 = new PatientClass { Surname = "Коваленко", Name = "Анна", Patronymic = "Вікторівна", Address = "м. Харків, вул. 4", MedCardNumber = "MC004", InsuranceNumber = "INS444" };
            var newP2 = new PatientClass { Surname = "Бондаренко", Name = "Дмитро", Patronymic = "Олександрович", Address = "м. Дніпро", MedCardNumber = "MC005", InsuranceNumber = "INS555" };
            patients.Insert(0, newP2);
            patients.Insert(0, newP1);
            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintClassList(patients);
        }

        public static void RunTupleVariant()
        {
            List<(string Surname, string Name, string Patronymic, string Address, string MedCard, string Insurance)> patients =
                new List<(string, string, string, string, string, string)>
                {
                    ("Іванов", "Іван", "Петрович", "м. Київ, вул. 1", "MC001", "INS111"),
                    ("Петрова", "Марія", "Сергіївна", "м. Львів, вул. 2", "MC002", "INS222"),
                    ("Сидоров", "Олег", "Миколайович", "м. Одеса, вул. 3", "MC003", "INS333")
                };

            PrintTupleList(patients);
            patients.RemoveAll(p => p.MedCard == "MC002");
            Console.WriteLine("-- Після видалення MC002 --");
            PrintTupleList(patients);

            var newP1 = ("Коваленко", "Анна", "Вікторівна", "м. Харків, вул. 4", "MC004", "INS444");
            var newP2 = ("Бондаренко", "Дмитро", "Олександрович", "м. Дніпро", "MC005", "INS555");
            patients.Insert(0, newP2);
            patients.Insert(0, newP1);
            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintTupleList(patients);
        }

        public static void RunRecordVariant()
        {
            List<PatientRecord> patients = new List<PatientRecord>
            {
                new PatientRecord("Іванов", "Іван", "Петрович", "м. Київ, вул. 1", "MC001", "INS111"),
                new PatientRecord("Петрова", "Марія", "Сергіївна", "м. Львів, вул. 2", "MC002", "INS222"),
                new PatientRecord("Сидоров", "Олег", "Миколайович", "м. Одеса, вул. 3", "MC003", "INS333")
            };

            PrintRecordList(patients);
            patients.RemoveAll(p => p.MedCardNumber == "MC002");
            Console.WriteLine("-- Після видалення MC002 --");
            PrintRecordList(patients);

            var newP1 = new PatientRecord("Коваленко", "Анна", "Вікторівна", "м. Харків, вул. 4", "MC004", "INS444");
            var newP2 = new PatientRecord("Бондаренко", "Дмитро", "Олександрович", "м. Дніпро", "MC005", "INS555");
            patients.Insert(0, newP2);
            patients.Insert(0, newP1);
            Console.WriteLine("-- Після додавання 2 пацієнтів на початок --");
            PrintRecordList(patients);
        }

        public static void RunStructVariant()
        {
            Patient[] patients =
            {
                new Patient("Іваненко Іван Іванович", "м. Львів", "MC-100", "POL-900"),
                new Patient("Петренко Петро Петрович", "м. Київ", "MC-101", "POL-901"),
                new Patient("Сидоренко Сидір Сидорович", "м. Дніпро", "MC-102", "POL-902")
            };

            patients = RemoveByMedicalCard(patients, "MC-101");
            patients = AddTwoToStart(
                patients,
                new Patient("Новий Пацієнт 1", "м. Одеса", "MC-200", "POL-990"),
                new Patient("Новий Пацієнт 2", "м. Харків", "MC-201", "POL-991"));

            Console.WriteLine("Тест структури Patient (struct):");
            foreach (Patient p in patients)
            {
                Console.WriteLine(p);
            }
        }

        private static Patient[] RemoveByMedicalCard(Patient[] source, string cardNumber)
        {
            if (source == null || source.Length == 0) return Array.Empty<Patient>();

            int removeIndex = -1;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].MedicalCardNumber == cardNumber)
                {
                    removeIndex = i;
                    break;
                }
            }

            if (removeIndex < 0) return source;

            Patient[] result = new Patient[source.Length - 1];
            int dst = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (i == removeIndex) continue;
                result[dst++] = source[i];
            }

            return result;
        }

        private static Patient[] AddTwoToStart(Patient[] source, Patient first, Patient second)
        {
            int oldLength = source?.Length ?? 0;
            Patient[] result = new Patient[oldLength + 2];
            result[0] = first;
            result[1] = second;
            if (oldLength > 0) Array.Copy(source!, 0, result, 2, oldLength);
            return result;
        }

        private static void PrintClassList(List<PatientClass> list)
        {
            foreach (PatientClass p in list) Console.WriteLine(p);
        }

        private static void PrintTupleList(List<(string Surname, string Name, string Patronymic, string Address, string MedCard, string Insurance)> list)
        {
            foreach (var p in list)
            {
                Console.WriteLine($"[Tuple] {p.Surname} {p.Name} {p.Patronymic} | Адреса: {p.Address} | Мед.картка: {p.MedCard} | Поліс: {p.Insurance}");
            }
        }

        private static void PrintRecordList(List<PatientRecord> list)
        {
            foreach (PatientRecord p in list) Console.WriteLine(p);
        }
    }
}