using CMatrix.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMatrix.MatrixClasses
{
    public static class UsualMatrices
    {
        internal static Matrix Zeros(int rows, int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix.coef[i, j] = 0;

            return matrix;
        }

        internal static Matrix Ones(int rows, int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix.coef[i, j] = 1;

            return matrix;
        }

        internal static Matrix Identity(int rows, int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix.coef[i, j] = i == j ? 1 : 0;

            return matrix;
        }

    }
}
