namespace TestUnitaire
{
    public static class GlobalSettings
    {
        public static double DefaultFloatingPointTolerance = 1e-6f;
    }

    public class MatrixFloat
    {
        private float[,] data;

        public int NbLines { get; }
        public int NbColumns { get; }

        public MatrixFloat(int nbLines, int nbColumns)
        {
            NbLines = nbLines;
            NbColumns = nbColumns;
            data = new float[nbLines, nbColumns];
        }

        public MatrixFloat(float[,] array)
        {
            NbLines = array.GetLength(0);
            NbColumns = array.GetLength(1);
            data = new float[NbLines, NbColumns];
            
            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                    data[i, j] = array[i, j];   
            }
        }

        public float this[int line, int col]
        {
            get => data[line, col];
            set => data[line, col] = value;
        }

        public float[,] ToArray2D()
        {
            float[,] copy = new float[NbLines, NbColumns];

            for (int i = 0; i < NbLines; i++)
            {
                for (int j = 0; j < NbColumns; j++)
                    copy[i, j] = data[i, j];   
            }
            
            return copy;
        }

        public void SwapLines(int i, int j)
        {
            if (i == j) return;
            for (int col = 0; col < NbColumns; col++)
            {
                (data[i, col], data[j, col]) = (data[j, col], data[i, col]);
            }
        }

        public void MultiplyLine(int line, float factor)
        {
            for (int j = 0; j < NbColumns; j++)
                data[line, j] *= factor;
        }

        public void AddMultipleOfLine(int sourceLine, int targetLine, float factor)
        {
            for (int j = 0; j < NbColumns; j++)
                data[targetLine, j] += factor * data[sourceLine, j];
        }
        
        public static MatrixFloat Identity(int n)
        {
            MatrixFloat id = new MatrixFloat(n, n);
            for (int i = 0; i < n; i++)
                id[i, i] = 1f;
            
            return id;
        }
        
        public MatrixFloat InvertByRowReduction()
        {
            return InvertByRowReduction(this);
        }

        public static MatrixFloat InvertByRowReduction(MatrixFloat m)
        {
            if (m.NbLines != m.NbColumns)
                throw new MatrixInvertException("Matrix must be square to be inverted.");

            int n = m.NbLines;

            MatrixFloat identity = Identity(n);
            var (reduced, inverse) = MatrixRowReductionAlgorithm.Apply(m, identity);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    float expected = (i == j) ? 1f : 0f;
                    
                    if (Math.Abs(reduced[i, j] - expected) > GlobalSettings.DefaultFloatingPointTolerance)
                        throw new MatrixInvertException("Matrix is singular and cannot be inverted.");
                }
            }

            return inverse;
        }

        public MatrixFloat SubMatrix(int removeRow, int removeCol)
        {
            return SubMatrix(this, removeRow, removeCol);
        }

        public static MatrixFloat SubMatrix(MatrixFloat m, int removeRow, int removeCol)
        {
            if (m.NbLines != m.NbColumns)
                throw new ArgumentException("SubMatrix only makes sense for square matrices.");

            int n = m.NbLines;
            MatrixFloat result = new MatrixFloat(n - 1, n - 1);

            int rDest = 0;
            for (int r = 0; r < n; r++)
            {
                if (r == removeRow) 
                    continue;

                int cDest = 0;
                for (int c = 0; c < n; c++)
                {
                    if (c == removeCol) 
                        continue;

                    result[rDest, cDest] = m[r, c];
                    cDest++;
                }
                
                rDest++;
            }

            return result;
        }


        public static float Determinant(MatrixFloat m)
        {
            if (m.NbLines != m.NbColumns)
                throw new ArgumentException("Matrix must be square to compute determinant.");

            int n = m.NbLines;

            if (n == 1)
                return m[0, 0];

            if (n == 2)
                return m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0];

            float det = 0f;
            for (int j = 0; j < n; j++)
            {
                MatrixFloat sub = m.SubMatrix(0, j);
                float cofactor = ((j % 2 == 0) ? 1f : -1f) * m[0, j] * Determinant(sub);
                det += cofactor;
            }

            return det;
        }
        
        
        public MatrixFloat Adjugate()
        {
            return Adjugate(this);
        }

        public static MatrixFloat Adjugate(MatrixFloat m)
        {
            if (m.NbLines != m.NbColumns)
                throw new ArgumentException("Adjugate is only defined for square matrices.");

            int n = m.NbLines;
            MatrixFloat cofactorMatrix = new MatrixFloat(n, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrixFloat sub = SubMatrix(m, i, j);

                    float sign = ((i + j) % 2 == 0) ? 1f : -1f;
                    cofactorMatrix[i, j] = sign * Determinant(sub);
                }
            }

            MatrixFloat adj = new MatrixFloat(n, n);
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    adj[i, j] = cofactorMatrix[j, i];
            }

            return adj;
        }
        
        
        public MatrixFloat InvertByDeterminant()
        {
            return InvertByDeterminant(this);
        }

        public static MatrixFloat InvertByDeterminant(MatrixFloat m)
        {
            if (m.NbLines != m.NbColumns)
                throw new MatrixInvertException("Matrix must be square to be inverted.");

            int n = m.NbLines;
            float det = Determinant(m);
            
            if (Math.Abs(det) < GlobalSettings.DefaultFloatingPointTolerance)
                throw new MatrixInvertException("Matrix is singular and cannot be inverted.");

            MatrixFloat adj = Adjugate(m);
            MatrixFloat inv = new MatrixFloat(n, n);
            
            float invDet = 1f / det;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    inv[i, j] = adj[i, j] * invDet;
            }

            return inv;
        }
    }
}