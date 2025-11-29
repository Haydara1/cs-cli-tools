using System.Text;

namespace CMatrix.Classes;

internal partial class Matrix
{
    // Matrix dimensions
    public int n, m;

    // Matrix coefficients
    internal float[,] coef;

    /// <summary>
    /// Creates an empty Matrix of dimensions N*M
    /// </summary>
    /// <param name="N">Matrix rows</param>
    /// <param name="M">Matrix columns</param>
    internal Matrix(int N, int M)
    {
         n = N;
         m = M;

        coef = new float[n, m];
    }

    internal Matrix(string s)
    {

    }

    /// <summary>
    /// Transforms an integer array to an N*M matrix
    /// </summary>
    /// <param name="numbers">Integer array containing the matrix coefficients</param>
    /// <param name="N">Array rows</param>
    internal Matrix(float[] numbers, int N, int M)
    {
        n = N;
        m = M;

        int len = numbers.Length;

        coef = new float[n, m];

        for (int i = 0; i < n; i++)
            for(int j = 0; j < m; j++)
                coef[i, j] = i * m + j < len ? numbers[i * m + j] : 0;
    }

    internal Matrix(float[,] numbers)
    {
        n = numbers.GetLength(0);
        m = numbers.GetLength(1);

        coef = numbers;
    }

    internal Matrix(Matrix[] matrices)
    {
        // Check if the matrices are rows
        if (matrices[0].n == 1)
        {
            n = matrices.Length;
            m = matrices[0].m;

            coef = new float[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    coef[i, j] = matrices[i].coef[0, j];
        }
        // Check if the matrices are columns
        else if (matrices[0].m == 1)
        {
            m = matrices.Length;
            n = matrices[0].n;

            coef = new float[n, m];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    coef[i, j] = matrices[i].coef[j, 0];
        }
        else throw new ArgumentException("Different size matrices can't build a matrix");
    }

    /// <summary>
    /// Returns a string representation of the given matrix.
    /// </summary>
    /// <param name="matrix">The wanted represented matrix</param>
    /// <returns></returns>
    public override string ToString()
    {
        int[] colWidth = new int[m];
        for (int j = 0; j < m; j++)
        {
            int max = 0;

            for (int i = 0; i < n; i++)
                max = Math.Max(coef[i, j].ToString().Length + 1, max);
            
            colWidth[j] = max;
        }

        
        StringBuilder sb = new();
        
        for (int i = 0; i < n; i++)
        {
            sb.Append('|');

            for (int j = 0; j < m; j++)
            {
                string cell = coef[i, j].ToString().PadLeft(colWidth[j]);
                sb.Append(cell);

                if (j < m - 1)
                    sb.Append(' ');
            }

            
            sb.Append("|\n");
        }

        return sb.ToString();
    }

}

