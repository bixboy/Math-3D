namespace TestUnitaire
{
    public class MatrixScalarZeroException : Exception
    {
        public MatrixScalarZeroException(string message = "Cannot multiply by zero.") : base(message) { }
    }

    public static class MatrixElementaryOperations
    {
        public static void SwapLines(MatrixInt m, int i, int j)
        {
            if (i < 0 || i >= m.NbLines || j < 0 || j >= m.NbLines)
                throw new IndexOutOfRangeException("Line index out of range.");

            if (i == j) return;

            for (int col = 0; col < m.NbColumns; col++)
            {
                int temp = m[i, col];
                m[i, col] = m[j, col];
                m[j, col] = temp;
            }
        }

        public static void SwapColumns(MatrixInt m, int i, int j)
        {
            if (i < 0 || i >= m.NbColumns || j < 0 || j >= m.NbColumns)
                throw new IndexOutOfRangeException("Column index out of range.");

            if (i == j) return;

            for (int row = 0; row < m.NbLines; row++)
            {
                int temp = m[row, i];
                m[row, i] = m[row, j];
                m[row, j] = temp;
            }
        }

        public static void MultiplyLine(MatrixInt m, int line, int factor)
        {
            if (factor == 0)
                throw new MatrixScalarZeroException();

            if (line < 0 || line >= m.NbLines)
                throw new IndexOutOfRangeException("Line index out of range.");

            for (int col = 0; col < m.NbColumns; col++)
            {
                m[line, col] *= factor;
            }
        }

        public static void MultiplyColumn(MatrixInt m, int col, int factor)
        {
            if (factor == 0)
                throw new MatrixScalarZeroException();

            if (col < 0 || col >= m.NbColumns)
                throw new IndexOutOfRangeException("Column index out of range.");

            for (int row = 0; row < m.NbLines; row++)
            {
                m[row, col] *= factor;
            }
        }

        public static void AddLineToAnother(MatrixInt m, int sourceLine, int targetLine, int factor)
        {
            if (sourceLine < 0 || sourceLine >= m.NbLines || targetLine < 0 || targetLine >= m.NbLines)
                throw new IndexOutOfRangeException("Line index out of range.");

            for (int col = 0; col < m.NbColumns; col++)
            {
                m[targetLine, col] += factor * m[sourceLine, col];
            }
        }

        public static void AddColumnToAnother(MatrixInt m, int sourceColumn, int targetColumn, int factor)
        {
            if (sourceColumn < 0 || sourceColumn >= m.NbColumns || targetColumn < 0 || targetColumn >= m.NbColumns)
                throw new IndexOutOfRangeException();

            for (int row = 0; row < m.NbLines; row++)
            {
                m[row, targetColumn] += factor * m[row, sourceColumn];   
            }
        }
    }
}