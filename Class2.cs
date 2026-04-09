using System;
using System.Linq;

class VectorDouble
{
    protected double[] FArray;
    protected uint num;
    protected int codeError;
    protected static uint num_vd = 0;

    public VectorDouble()
    {
        num = 1;
        FArray = new double[1];
        FArray[0] = 0;
        num_vd++;
    }

    public VectorDouble(uint size)
    {
        num = size;
        FArray = new double[num];
        num_vd++;
    }

    public VectorDouble(uint size, double initValue)
    {
        num = size;
        FArray = new double[num];
        for (int i = 0; i < num; i++)
        {
            FArray[i] = initValue;
        }
        num_vd++;
    }

    ~VectorDouble()
    {
        Console.WriteLine($"Вектор видалено. (Поточна кількість: {--num_vd})");
    }

    public uint Size => num;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    public double this[uint index]
    {
        get
        {
            if (index >= num)
            {
                codeError = -1;
                return 0;
            }
            return FArray[index];
        }
        set
        {
            if (index >= num)
            {
                codeError = -1;
            }
            else
            {
                FArray[index] = value;
            }
        }
    }

    public void Input()
    {
        Console.WriteLine($"Введіть {num} дійсних чисел:");
        for (uint i = 0; i < num; i++)
        {
            Console.Write($"Елемент [{i}]: ");
            if (double.TryParse(Console.ReadLine(), out double val))
                FArray[i] = val;
            else
                Console.WriteLine("Помилка введення. Встановлено 0.");
        }
    }

    public void Output()
    {
        Console.WriteLine("Вектор: [" + string.Join(", ", FArray) + "]");
    }

    public void AssignValue(double value)
    {
        for (int i = 0; i < num; i++)
        {
            FArray[i] = value;
        }
    }

    public static uint GetVectorCount()
    {
        return num_vd;
    }

    public static VectorDouble operator ++(VectorDouble v)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] + 1;
        return res;
    }

    public static VectorDouble operator --(VectorDouble v)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] - 1;
        return res;
    }

    public static bool operator true(VectorDouble v)
    {
        return v.num != 0 || v.FArray.All(x => x != 0);
    }

    public static bool operator false(VectorDouble v)
    {
        return v.num == 0 && !v.FArray.All(x => x != 0);
    }

    public static bool operator !(VectorDouble v)
    {
        return v.num != 0;
    }

    public static int[] operator ~(VectorDouble v)
    {
        int[] res = new int[v.num];
        for (uint i = 0; i < v.num; i++) res[i] = (int)Math.Truncate(v[i]);
        return res;
    }

    public static VectorDouble operator +(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++) res[i] = v1[i] + v2[i];
        return res;
    }

    public static VectorDouble operator +(VectorDouble v, double scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] + scalar;
        return res;
    }

    public static VectorDouble operator -(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        for (uint i = 0; i < v1.num; i++) res[i] = v1[i];
        for (uint i = 0; i < minSize; i++) res[i] = v1[i] - v2[i];
        return res;
    }

    public static VectorDouble operator -(VectorDouble v, double scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] - scalar;
        return res;
    }

    public static VectorDouble operator *(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++) res[i] = v1[i] * v2[i];
        return res;
    }

    public static VectorDouble operator *(VectorDouble v, double scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] * scalar;
        return res;
    }

    public static VectorDouble operator /(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        for (uint i = 0; i < v1.num; i++) res[i] = v1[i];
        for (uint i = 0; i < minSize; i++) res[i] = v1[i] / (v2[i] == 0 ? 1 : v2[i]); 
        return res;
    }

    public static VectorDouble operator /(VectorDouble v, double scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] / scalar;
        return res;
    }

    public static VectorDouble operator %(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        for (uint i = 0; i < v1.num; i++) res[i] = v1[i];
        for (uint i = 0; i < minSize; i++) res[i] = v2[i] == 0 ? v1[i] : v1[i] % v2[i];
        return res;
    }

    public static VectorDouble operator %(VectorDouble v, double scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++) res[i] = v[i] % scalar;
        return res;
    }

    public static VectorDouble operator |(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) | BitConverter.DoubleToInt64Bits(v2[i]));
        return res;
    }

    public static VectorDouble operator |(VectorDouble v, byte scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v[i]) | scalar);
        return res;
    }

    public static VectorDouble operator ^(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) ^ BitConverter.DoubleToInt64Bits(v2[i]));
        return res;
    }

    public static VectorDouble operator ^(VectorDouble v, byte scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v[i]) ^ scalar);
        return res;
    }

    public static VectorDouble operator &(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) & BitConverter.DoubleToInt64Bits(v2[i]));
        return res;
    }

    public static VectorDouble operator &(VectorDouble v, byte scalar)
    {
        VectorDouble res = new VectorDouble(v.num);
        for (uint i = 0; i < v.num; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v[i]) & scalar);
        return res;
    }

    public static VectorDouble operator >>(VectorDouble v1, uint shift)
    {
        VectorDouble res = new VectorDouble(v1.num);
        int safeShift = (int)(shift & 63);
        for (uint i = 0; i < v1.num; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) >> safeShift);
        return res;
    }

    public static VectorDouble operator <<(VectorDouble v1, uint shift)
    {
        VectorDouble res = new VectorDouble(v1.num);
        int safeShift = (int)(shift & 63);
        for (uint i = 0; i < v1.num; i++)
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) << safeShift);
        return res;
    }

    public static VectorDouble operator >>(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++)
        {
            int shift = (int)Math.Abs(v2[i]) & 63;
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) >> shift);
        }
        return res;
    }

    public static VectorDouble operator <<(VectorDouble v1, VectorDouble v2)
    {
        uint minSize = Math.Min(v1.num, v2.num);
        VectorDouble res = new VectorDouble(Math.Max(v1.num, v2.num));
        VectorDouble bigger = v1.num >= v2.num ? v1 : v2;
        for (uint i = 0; i < res.num; i++) res[i] = bigger[i];
        for (uint i = 0; i < minSize; i++)
        {
            int shift = (int)Math.Abs(v2[i]) & 63;
            res[i] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(v1[i]) << shift);
        }
        return res;
    }

    public static bool operator ==(VectorDouble v1, VectorDouble v2)
    {
        if (ReferenceEquals(v1, v2)) return true;
        if (v1 is null || v2 is null || v1.num != v2.num) return false;
        for (uint i = 0; i < v1.num; i++) if (v1[i] != v2[i]) return false;
        return true;
    }

    public static bool operator !=(VectorDouble v1, VectorDouble v2) => !(v1 == v2);

    public static bool operator >(VectorDouble v1, VectorDouble v2)
    {
        if (v1.num != v2.num) return false;
        for (uint i = 0; i < v1.num; i++) if (v1[i] <= v2[i]) return false;
        return true;
    }

    public static bool operator <(VectorDouble v1, VectorDouble v2)
    {
        if (v1.num != v2.num) return false;
        for (uint i = 0; i < v1.num; i++) if (v1[i] >= v2[i]) return false;
        return true;
    }

    public static bool operator >=(VectorDouble v1, VectorDouble v2)
    {
        if (v1.num != v2.num) return false;
        for (uint i = 0; i < v1.num; i++) if (v1[i] < v2[i]) return false;
        return true;
    }

    public static bool operator <=(VectorDouble v1, VectorDouble v2)
    {
        if (v1.num != v2.num) return false;
        for (uint i = 0; i < v1.num; i++) if (v1[i] > v2[i]) return false;
        return true;
    }

    public override bool Equals(object? obj) => obj is VectorDouble other && this == other;

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + num.GetHashCode();
        for (uint i = 0; i < num; i++)
        {
            hash = hash * 31 + FArray[i].GetHashCode();
        }
        return hash;
    }
}