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
