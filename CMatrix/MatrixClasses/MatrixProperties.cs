using CMatrix.MatrixClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CMatrix.Classes;

// A partial class that contains functions that calculate
// the mathematical properties of matrices
internal partial class Matrix
{
    internal static bool IsSquare(Matrix matrix) => matrix.n == matrix.m;

    public static float Tr(Matrix matrix)
    {
        if (!IsSquare(matrix)) 
            throw new ArgumentException("Trace isn't defined on non-square matrices");

        float trace = 0;

        for (int i = 0; i < matrix.m; i++)
            trace += matrix.coef[i, i];

        return trace;
    }

    public static float Det(Matrix matrix)
    {
        if (!IsSquare(matrix))
            throw new ArgumentException("Determinant isn't defined on non-square matrices");

        if (matrix.n == 1) return matrix.coef[0, 0];

        if (matrix.n == 2)
            return matrix.coef[0, 0] * matrix.coef[1, 1] - matrix.coef[0, 1] * matrix.coef[1, 0];

        float det = 0;

        for (int j = 0; j < matrix.m; j++)
        {
            // Minor of element (0, j)
            Matrix minor = Minor(matrix, 0, j);

            // Laplace expansion: alternating sign
            det += ((j % 2 == 0) ? 1 : -1) * matrix.coef[0, j] * Det(minor);
        }

        return det;
    }

    public static Matrix Inverse(Matrix matrix)
    {
        if (!IsInversible(matrix) || !IsSquare(matrix)) 
            throw new ArgumentException("Can't inverse this matrix");


        if (matrix.n == 1)
            return matrix;

        Matrix output = new(matrix.n, matrix.n);
        
        // Cayley-Hamilton decomposition
        if (matrix.n == 2)
        {
            output = Tr(matrix) * UsualMatrices.Identity(2, 2) - matrix;    
            return output * (1 / Det(matrix));
        }
        else if(matrix.n == 3)
        {
            Matrix squared = matrix * matrix;

            output = 0.5f * ((float)(Math.Pow(Tr(matrix), 2) - Tr(squared))) 
                * UsualMatrices.Identity(3, 3);
            output += squared - Tr(matrix) * matrix;

            return output * (1 / Det(matrix));
        }

            output = InvertByLUP(matrix);

            return output;
    }

    public static Matrix Minor(Matrix matrix, int r, int c)
    {
        Matrix minor = new(matrix.n - 1, matrix.m - 1);
        int mi = 0;

        for (int i = 0; i < matrix.n; i++)
        {
            if (i == r) continue;
            int mj = 0;
            for (int j = 0; j < matrix.m; j++)
            {
                if (j == c) continue;
                minor.coef[mi, mj] = matrix.coef[i, j];
                mj++;
            }
            mi++;
        }

        return minor;
    }

    public static bool IsInversible(Matrix matrix) => Det(matrix) != 0;

    public static void LUPDecomposition
        (Matrix A, out Matrix L, out Matrix U, out int[] P)
    {
        int n = A.n;
        U = A;
        L = new Matrix(n, n);
        P = new int[n];

        // Initialise array
        for (int i = 0; i < n; i++)
            P[i] = i;

        for (int k = 0; k < n; k++)
        {
            // Find pivot
            double max = 0;
            int pivot = k;

            for (int i = k; i < n; i++)
            {
                if (Math.Abs(U.coef[i, k]) > max)
                {
                    max = Math.Abs(U.coef[i, k]);
                    pivot = i;
                }
            }

            if (max == 0)
                throw new Exception("Matrix is singular.");

            // Swap rows in U
            for (int j = 0; j < n; j++)
            {
                (U.coef[k, j], U.coef[pivot, j]) = (U.coef[pivot, j], U.coef[k, j]);
            }

            // Swap permutation vector
            (P[k], P[pivot]) = (P[pivot], P[k]);

            // Swap rows in L below diagonal
            for (int j = 0; j < k; j++)
            {
                (L.coef[k, j], L.coef[pivot, j]) = (L.coef[pivot, j], L.coef[k, j]);
            }

            // Compute factors
            for (int i = k + 1; i < n; i++)
            {
                L.coef[i, k] = U.coef[i, k] / U.coef[k, k];

                for (int j = k; j < n; j++)
                    U.coef[i, j] -= L.coef[i, k] * U.coef[k, j];
            }
        }

        // Fill diag of L with 1s
        for (int i = 0; i < n; i++)
            L.coef[i, i] = 1;
    }

    public static Matrix InvertByLUP(Matrix A)
    {
        int n = A.n;

        // Decompose
        LUPDecomposition(A, out Matrix L, out Matrix U, out int[] P);

        Matrix inverse = new(n, n);

        // Solve for each column of identity
        for (int col = 0; col < n; col++)
        {
            // Create the permuted RHS vector (Pe_col)
            double[] b = new double[n];
            b[P[col]] = 1.0;

            // Forward substitution Ly = b
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = b[i];
                for (int j = 0; j < i; j++)
                    sum -= L.coef[i, j] * y[j];
                y[i] = sum;
            }

            // Back substitution Ux = y
            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = y[i];
                for (int j = i + 1; j < n; j++)
                    sum -= U.coef[i, j] * x[j];

                if (U.coef[i, i] == 0)
                    throw new Exception("Matrix is singular.");

                x[i] = sum / U.coef[i, i];
            }

            // Store x as a column of the inverse
            for (int i = 0; i < n; i++)
                inverse.coef[i, col] = (float)x[i];
        }

        return inverse;
    }


}
