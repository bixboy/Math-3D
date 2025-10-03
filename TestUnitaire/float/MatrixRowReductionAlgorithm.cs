namespace TestUnitaire;

public static class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat m1, MatrixFloat m2)
    {
        int rows = m1.NbLines;

        MatrixFloat mat = new MatrixFloat(m1.ToArray2D());
        MatrixFloat rhs = new MatrixFloat(m2.ToArray2D());

        double tol = GlobalSettings.DefaultFloatingPointTolerance;

        for (int i = 0; i < rows; i++)
        {
            int pivotRow = i;
            for (int r = i; r < rows; r++)
            {
                if (Math.Abs(mat[r, i]) > Math.Abs(mat[pivotRow, i]))
                    pivotRow = r;   
            }

            if (Math.Abs(mat[pivotRow, i]) < tol)
                continue;

            mat.SwapLines(i, pivotRow);
            rhs.SwapLines(i, pivotRow);

            float pivotValue = mat[i, i];
            if (Math.Abs(pivotValue) > tol)
            {
                mat.MultiplyLine(i, 1f / pivotValue);
                rhs.MultiplyLine(i, 1f / pivotValue);
            }

            for (int r = 0; r < rows; r++)
            {
                if (r == i) 
                    continue;
                
                float factor = -mat[r, i];
                mat.AddMultipleOfLine(i, r, factor);
                rhs.AddMultipleOfLine(i, r, factor);
            }
        }

        return (mat, rhs);
    }
}