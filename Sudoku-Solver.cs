// Sudoku solver:

// The following solver uses bit-masking and backtracking.

// Define a function to check whether it is safe to put a number in a given position.
bool IsSafe(int[,] mat, int row, int col, int num)
{
    // Check if num exists in the row or the column:
    for (int i = 0; i < 9; i++)
    {
        if (mat[row, i] == num || mat[i, col] == num)
        {
            return false;
        }
    }
    
    // Check if num exist in the 3 by 3 box:
    int startRow = row - (row % 3);
    int startColumn = col - (col % 3);

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (mat[startRow + i, startColumn + j] == num)
            {
                return false;
            }
        }
    }

    return true;
}

// Define a function to solve the sudoku:
bool SolveRec(int[,] mat, int i, int j,
    int[] row, int[] col, int[] box)
{
    int n = mat.GetLength(0);
    
    // Reached the end:
    if (i == n - 1 && j == n)
    {
        return true;
    }
    
    // If reached the last column of the row, move to next row
    if (j == n)
    {
        i++;
        j = 0;
    }
    
    // If cell already occupied move to next cell
    if (mat[i, j] != 0)
    {
        return SolveRec(mat, i, j + 1, row, col, box);
    }

    for (int num = 1; num <= n; num++)
    {

        // If it is safe to place num at current position
        if (IsSafe(mat, i, j, num))
        {
            mat[i, j] = num;

            // Update masks for the corresponding row, column, and box
            row[i] |= (1 << num);
            col[j] |= (1 << num);
            box[i / 3 * 3 + j / 3] |= (1 << num);

            if (SolveRec(mat, i, j + 1, row, col, box))
                return true;

            // Unmask the number num in the corresponding row, column and box masks
            mat[i, j] = 0;
            row[i] &= ~(1 << num);
            col[j] &= ~(1 << num);
            box[i / 3 * 3 + j / 3] &= ~(1 << num);
        }
    }

    return false;
}

void SolveSudoku(int[,] mat)
{
    int n = mat.GetLength(0);
    int[] row = new int[n];
    int[] col = new int[n];
    int[] box = new int[n];

    // Set the bits in bitmasks for values that are initially present
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            if (mat[i, j] != 0) {
                row[i] |= (1 << mat[i, j]);
                col[j] |= (1 << mat[i, j]);
                box[(i / 3) * 3 + j / 3] |= (1 << mat[i, j]);
            }
        }
    }

    SolveRec(mat, 0, 0, row, col, box);
}

int[,] mat = {
    {0, 3, 0, 7, 0, 0, 0, 0, 0},
    {8, 9, 2, 0, 4, 0, 0, 0, 0},
    {0, 1, 0, 0, 0, 0, 9, 0, 0},
    {7, 0, 0, 4, 1, 0, 0, 0, 0},
    {0, 6, 0, 5, 0, 8, 0, 2, 0},
    {0, 0, 0, 0, 7, 9, 0, 0, 8},
    {0, 0, 6, 0, 0, 0, 0, 5, 0},
    {0, 0, 0, 0, 8, 0, 4, 6, 3},
    {0, 0, 0, 0, 0, 1, 0, 8, 0}
};

SolveSudoku(mat);

for (int i = 0; i < mat.GetLength(0); i++) {
    for (int j = 0; j < mat.GetLength(1); j++)
        Console.Write(mat[i, j] + " ");
    Console.WriteLine();
}

/* Output:
 * 6 3 5 7 9 2 8 4 1 
 * 8 9 2 1 4 3 5 7 6 
 * 4 1 7 8 6 5 9 3 2 
 * 7 2 8 4 1 6 3 9 5 
 * 1 6 9 5 3 8 7 2 4 
 * 5 4 3 2 7 9 6 1 8 
 * 9 8 6 3 2 4 1 5 7 
 * 2 5 1 9 8 7 4 6 3 
 * 3 7 4 6 5 1 2 8 9 
 */
