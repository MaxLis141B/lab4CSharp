using System;

public static class PatientArrayUtils
{
    public static Patient[] RemoveByMedicalCard(Patient[] source, string cardNumber)
    {
        if (source == null || source.Length == 0)
        {
            return Array.Empty<Patient>();
        }

        int removeIndex = -1;
        for (int i = 0; i < source.Length; i++)
        {
            if (string.Equals(source[i].MedicalCardNumber, cardNumber, StringComparison.Ordinal))
            {
                removeIndex = i;
                break;
            }
        }

        if (removeIndex < 0)
        {
            return source;
        }

        Patient[] result = new Patient[source.Length - 1];
        int dst = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (i == removeIndex)
            {
                continue;
            }

            result[dst++] = source[i];
        }

        return result;
    }

    public static Patient[] AddTwoToStart(Patient[] source, Patient first, Patient second)
    {
        int oldLength = source?.Length ?? 0;
        Patient[] result = new Patient[oldLength + 2];
        result[0] = first;
        result[1] = second;

        if (oldLength > 0)
        {
            Array.Copy(source!, 0, result, 2, oldLength);
        }

        return result;
    }
}
