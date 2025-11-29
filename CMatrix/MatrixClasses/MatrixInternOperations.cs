
namespace CMatrix.Classes;

internal partial class Matrix
{
    public Matrix Row(int r)
    {
        Matrix row = new(1, m);

        for (int i = 0; i < m; i++)
            row.coef[0, i] = coef[r, i];

        return row;
    }

    private Matrix Row(int r, int start)
    {
        Matrix row = new(1, m - start);

        for (int i = 0; i < m - start; i++)
            row.coef[0, i] = coef[r, i + start];

        return row;
    }

    public Matrix Column(int c) => (!this).Row(c);
    
}
