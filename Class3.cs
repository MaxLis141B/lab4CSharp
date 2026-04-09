using System;

class MatrixDouble
{
    protected double[,] DArray;
    protected uint n, m;
    protected int codeError;
    protected static int num_mf = 0;

    public MatrixDouble()
    {
        n = 1;
        m = 1;
        DArray = new double[1, 1];
        DArray[0, 0] = 0;
        num_mf++;
    }

    public MatrixDouble(uint rows, uint cols)
    {
        n = rows;
        m = cols;
        DArray = new double[n, m];
        num_mf++;
    }

    public MatrixDouble(uint rows, uint cols, double initValue)
    {
        n = rows;
        m = cols;
        DArray = new double[n, m];
        AssignValue(initValue);
        num_mf++;
    }

    ~MatrixDouble()
    {
        Console.WriteLine($"Матрицю видалено. (Поточна кількість: {--num_mf})");
    }

    public uint Rows => n;
    public uint Cols => m;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    public double this[uint i, uint j]
    {
        get
        {
            if (i >= n || j >= m) { codeError = -1; return 0; }
            return DArray[i, j];
        }
        set
        {
            if (i >= n || j >= m) codeError = -1;
            else DArray[i, j] = value;
        }
    }

    public double this[uint k]
    {
        get
        {
            uint i = k / m;
            uint j = k % m;
            if (i >= n || j >= m) { codeError = -1; return 0; }
            return DArray[i, j];
        }
        set
        {
            uint i = k / m;
            uint j = k % m;
            if (i >= n || j >= m) codeError = -1;
            else DArray[i, j] = value;
        }
    }

    public void Input()
    {
        Console.WriteLine($"Введіть елементи матриці ({n}x{m}):");
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                Console.Write($"Елемент [{i},{j}]: ");
                if (double.TryParse(Console.ReadLine(), out double val))
                    DArray[i, j] = val;
                else
                    DArray[i, j] = 0;
            }
        }
    }

    public void Output()
    {
        Console.WriteLine("Матриця:");
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
                Console.Write($"{DArray[i, j]}\t");
            Console.WriteLine();
        }
    }

    public void AssignValue(double value)
    {
        for (uint i = 0; i < n; i++)
            for (uint j = 0; j < m; j++)
                DArray[i, j] = value;
    }

    public static int GetMatrixCount() => num_mf;

    public static MatrixDouble operator ++(MatrixDouble mat)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] + 1;
        return res;
    }

    public static MatrixDouble operator --(MatrixDouble mat)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] - 1;
        return res;
    }

    public static bool operator true(MatrixDouble mat)
    {
        if (mat.n == 0 || mat.m == 0) return false;
        foreach (double val in mat.DArray) if (val != 0) return true;
        return false;
    }

    public static bool operator false(MatrixDouble mat)
    {
        if (mat.n == 0 || mat.m == 0) return true;
        foreach (double val in mat.DArray) if (val != 0) return false;
        return true;
    }

    public static bool operator !(MatrixDouble mat) => mat.n != 0 && mat.m != 0;

    public static MatrixDouble operator ~(MatrixDouble mat)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++)
            {
                long bits = BitConverter.DoubleToInt64Bits(mat[i, j]);
                res[i, j] = BitConverter.Int64BitsToDouble(~bits);
            }
        return res;
    }

    public static MatrixDouble operator +(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++) res[i, j] = m1[i, j] + m2[i, j];
        return res;
    }

    public static MatrixDouble operator +(MatrixDouble mat, double scalar)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] + scalar;
        return res;
    }

    public static MatrixDouble operator -(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++) res[i, j] = m1[i, j] - m2[i, j];
        return res;
    }

    public static MatrixDouble operator -(MatrixDouble mat, double scalar)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] - scalar;
        return res;
    }

    public static MatrixDouble operator *(MatrixDouble m1, MatrixDouble m2)
    {
        if (m1.m != m2.n) throw new ArgumentException("Розміри матриць не співпадають для множення.");
        MatrixDouble res = new MatrixDouble(m1.n, m2.m);
        for (uint i = 0; i < res.n; i++)
            for (uint j = 0; j < res.m; j++)
                for (uint k = 0; k < m1.m; k++)
                    res[i, j] += m1[i, k] * m2[k, j];
        return res;
    }

    public static VectorDouble operator *(MatrixDouble mat, VectorDouble vec)
    {
        if (mat.m != vec.Size) throw new ArgumentException("Кількість стовпців матриці має дорівнювати розміру вектора.");
        VectorDouble res = new VectorDouble(mat.n);
        for (uint i = 0; i < mat.n; i++)
            for (uint k = 0; k < mat.m; k++)
                res[i] += mat[i, k] * vec[k];
        return res;
    }

    public static MatrixDouble operator *(MatrixDouble mat, double scalar)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] * scalar;
        return res;
    }

    public static MatrixDouble operator /(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++) res[i, j] = m1[i, j] / (m2[i, j] == 0 ? 1 : m2[i, j]);
        return res;
    }

    public static MatrixDouble operator /(MatrixDouble mat, double scalar)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] / scalar;
        return res;
    }

    public static MatrixDouble operator %(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++) res[i, j] = m2[i, j] == 0 ? m1[i, j] : m1[i, j] % m2[i, j];
        return res;
    }

    public static MatrixDouble operator %(MatrixDouble mat, uint scalar)
    {
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++) res[i, j] = mat[i, j] % scalar;
        return res;
    }

    public static MatrixDouble operator |(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) | BitConverter.DoubleToInt64Bits(m2[i, j]));
        return res;
    }

    public static MatrixDouble operator |(MatrixDouble mat, double scalar)
    {
        long sBits = BitConverter.DoubleToInt64Bits(scalar);
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(mat[i, j]) | sBits);
        return res;
    }

    public static MatrixDouble operator ^(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) ^ BitConverter.DoubleToInt64Bits(m2[i, j]));
        return res;
    }

    public static MatrixDouble operator ^(MatrixDouble mat, double scalar)
    {
        long sBits = BitConverter.DoubleToInt64Bits(scalar);
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(mat[i, j]) ^ sBits);
        return res;
    }

    public static MatrixDouble operator &(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];
        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) & BitConverter.DoubleToInt64Bits(m2[i, j]));
        return res;
    }

    public static MatrixDouble operator &(MatrixDouble mat, double scalar)
    {
        long sBits = BitConverter.DoubleToInt64Bits(scalar);
        MatrixDouble res = new MatrixDouble(mat.n, mat.m);
        for (uint i = 0; i < mat.n; i++)
            for (uint j = 0; j < mat.m; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(mat[i, j]) & sBits);
        return res;
    }

    public static MatrixDouble operator >>(MatrixDouble m1, ushort shift)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        int safeShift = shift & 63;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) >> safeShift);
        return res;
    }

    public static MatrixDouble operator <<(MatrixDouble m1, ushort shift)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        int safeShift = shift & 63;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++)
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) << safeShift);
        return res;
    }
    public static MatrixDouble operator >>(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];

        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++)
            {
                int shift = (int)Math.Abs(m2[i, j]) & 63;
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) >> shift);
            }
        return res;
    }

    public static MatrixDouble operator <<(MatrixDouble m1, MatrixDouble m2)
    {
        MatrixDouble res = new MatrixDouble(m1.n, m1.m);
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) res[i, j] = m1[i, j];

        uint r = Math.Min(m1.n, m2.n); uint c = Math.Min(m1.m, m2.m);
        for (uint i = 0; i < r; i++)
            for (uint j = 0; j < c; j++)
            {
                int shift = (int)Math.Abs(m2[i, j]) & 63;
                res[i, j] = BitConverter.Int64BitsToDouble(BitConverter.DoubleToInt64Bits(m1[i, j]) << shift);
            }
        return res;
    }
    public static bool operator ==(MatrixDouble m1, MatrixDouble m2)
    {
        if (ReferenceEquals(m1, m2)) return true;
        if (m1 is null || m2 is null || m1.n != m2.n || m1.m != m2.m) return false;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) if (m1[i, j] != m2[i, j]) return false;
        return true;
    }

    public static bool operator !=(MatrixDouble m1, MatrixDouble m2) => !(m1 == m2);

    public static bool operator >(MatrixDouble m1, MatrixDouble m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) if (m1[i, j] <= m2[i, j]) return false;
        return true;
    }

    public static bool operator <(MatrixDouble m1, MatrixDouble m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) if (m1[i, j] >= m2[i, j]) return false;
        return true;
    }

    public static bool operator >=(MatrixDouble m1, MatrixDouble m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) if (m1[i, j] < m2[i, j]) return false;
        return true;
    }

    public static bool operator <=(MatrixDouble m1, MatrixDouble m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;
        for (uint i = 0; i < m1.n; i++)
            for (uint j = 0; j < m1.m; j++) if (m1[i, j] > m2[i, j]) return false;
        return true;
    }

    public override bool Equals(object? obj) => obj is MatrixDouble other && this == other;

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + n.GetHashCode();
        hash = hash * 31 + m.GetHashCode();
        for (uint i = 0; i < n; i++)
            for (uint j = 0; j < m; j++)
                hash = hash * 31 + DArray[i, j].GetHashCode();
        return hash;
    }
}