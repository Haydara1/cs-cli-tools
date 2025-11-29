using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace CMatrix.Classes;

internal partial class Matrix
{
    #region Operators

    /// <summary>
    /// Matrix addition
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.n != b.n || a.m != b.m)
            throw new ArgumentException("Non matching dimensions for addition");

        Matrix c = new(a.n, a.m);

        for(int i  = 0; i < a.n; i++)
            for(int j =  0; j < b.n; j++)
                c.coef[i, j] = (a.coef[i, j] + b.coef[i, j]);

        return c;
    }

    /// <summary>
    /// Matrix multiplication by a scalar
    /// </summary>
    /// <param name="x"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Matrix operator *(float x, Matrix b)
    {
        Matrix a = new(b.n, b.m);

        for(int i =0; i < b.n; i++)
            for(int j = 0; j < b.m; j++)
                a.coef[i, j] = b.coef[i, j] * x;

        return a;
    }

    // By commutativity of scalars and matrices.
    public static Matrix operator *(Matrix a, float x) => x * a;

    public static Matrix operator -(Matrix a) => -1 * a;

    public static Matrix operator -(Matrix a, Matrix b) => a + (-b);

    // Matrix multiplication

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.m != b.n) throw new ArgumentException("Dimension mismatch in matrix multiplication");

        Matrix c = new(a.n, b.m);

        for (int i = 0; i < c.n; i++)
        {
            for (int j = 0; j < c.m; j++)
            {
                float result = 0;

                for (int k = 0; k < a.m; k++)
                    result += a.coef[i, k] * b.coef[k, j];

                c.coef[i, j] = result;
            }
        }

        return c;
    }

    // Transpose
    public static Matrix operator !(Matrix matrix)
    {
        Matrix output = new(matrix.m, matrix.n);

        for(int i = 0; i < output.n; i++)
            for(int j = 0; j < output.m; j++)
                output.coef[j, i] = matrix.coef[i, j];

        return output; 
    }

    #endregion

    #region Comparison operators

    public override bool Equals(object obj)
    {
        if (obj is not Matrix other)
            return false;

        if (n != other.n ||
            m != other.m)
            return false;

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                if (coef[i, j] != other.coef[i, j])
                    return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Matrix a, Matrix b) => a.Equals(b); 

    public static bool operator !=(Matrix a, Matrix b) => !a.Equals(b);

    #endregion
}
