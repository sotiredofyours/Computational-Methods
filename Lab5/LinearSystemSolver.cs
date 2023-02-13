namespace Lab5;

public static class LinearSystemSolver
{
    /// <summary>
    /// Погрешность, допускаемая в итеративном методе
    /// </summary>
    private static double Tolerance => Math.Pow(10, -4);

    /// <summary>
    /// Матрица
    /// </summary>
    private static double[,] _matrix;
    
    /// <summary>
    /// Количество столбцов и строк в матрице
    /// </summary>
    private static int n => _matrix.GetLength(0);
 
    /// <summary>
    /// Решение СЛАУ методом Холецкого
    /// </summary>
    /// <param name="matrix">Матрица чисел</param>
    /// <param name="resultVector">Вектор-столбец - результат умножения матрицы на (x1, x2, ...)</param>
    /// <returns>Вектор-столбец значений x1, x2, ..</returns>
    public static double[] KholetskyMethod(double[,] matrix, double[] resultVector)
    {
        _matrix = matrix;
        var pMatrix = new double[n, n];
        var cMatrix = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            pMatrix[i, 0] = matrix[i, 0];
        }

        for (int i = 0; i < n; i++)
        {
            cMatrix[0, i] = matrix[0, i] / pMatrix[0, 0];
        }

        for (int i = 1; i < n; i++)
        {
            CalculateColumn(pMatrix, cMatrix, i);
            CalculateRow(pMatrix, cMatrix, i);   
        }

        var yMatrix = new double[n];
        yMatrix[0] = resultVector[0] / pMatrix[0, 0];
        
        for (int i = 1; i < n; i++)
        {
            var b = resultVector[i];
            var sum = 0d;
            for (int k = 0; k < i; k++)
            {
                sum += pMatrix[i, k] * yMatrix[k];
            }

            yMatrix[i] = (b - sum) / pMatrix[i, i];
        }

        var result = new double[n];
        
        result[n - 1] = yMatrix[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            var sum = 0d;
            for (int k = i; k < n; k++)
            {
                sum += cMatrix[i, k] * result[k];
            }

            result[i] = yMatrix[i] - sum;
        }

        return result;
    }
    
    /// <summary>
    /// Вспомогательный метод, вычисляющий столбец треугольной матрицы P. Где P - представление матрицы A = PC
    /// </summary>
    /// <param name="pMatrix">Матрица P</param>
    /// <param name="cMatrix">Матрица C</param>
    /// <param name="column">Номер столбца</param>
    private static void CalculateColumn( double[,] pMatrix, double[,] cMatrix, int column)
    {
        for (int i = column; i < n; i++)
        {
            var a = _matrix[i, column];
            var sum = 0d;
            for (int k = 0; k < i; k++)
            {
                sum += pMatrix[i, k] * cMatrix[k, column];
            }

            pMatrix[i, column] = a - sum;
        }
    }

    /// <summary>
    /// Вспомогательный метод, вычисляющий строку треугольной матрицы C. Где C - представление матрицы A = PC
    /// </summary>
    /// <param name="pMatrix">Матрица P</param>
    /// <param name="cMatrix">Матрица C</param>
    /// <param name="row">Номер строки</param>
    private static void CalculateRow(double[,] pMatrix, double[,] cMatrix, int row)
    {
        for (int i = row; i < n; i++)
        {
            var a = _matrix[row, i];
            var sum = 0d;
            for (int j = 0; j < i; j++)
            {
                sum += pMatrix[row, j] * cMatrix[j, i];
            }

            cMatrix[row, i] = (a - sum) / pMatrix[row, row];
        }
    }

    /// <summary>
    /// Решение СЛАУ методом Зейделя.
    /// </summary>
    /// <param name="matrix">Матрица с диагональным преобладанием</param>
    /// <param name="resultVector">Вектор-столбец - результат умножения матрицы на (x1, x2, ...)</param>
    /// <returns>Вектор-столбец значений x1, x2, ..</returns>
    public static double[] SeidelMethod(double[,] matrix, double[] resultVector)
    {
        _matrix = matrix;
        double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];
        for (var i = 0; i < a.GetLength(0); i++)
        for (var j = 0; j < a.GetLength(1) - 1; j++)
            a[i, j] = matrix[i, j];

        for (var i = 0; i < a.GetLength(0); i++)
            a[i, a.GetLength(1) - 1] = resultVector[i];
        
        var previousValues = new double[matrix.GetLength(0)];
        for (var i = 0; i < previousValues.GetLength(0); i++) previousValues[i] = 0.0;
        var approximationStep = 0; // номер приближения / итерации
        while (true)
        {
            approximationStep++;
            var currentValues = new double[a.GetLength(0)];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                currentValues[i] = a[i, a.GetLength(0)];

                for (var j = 0; j < a.GetLength(0); j++)
                {
                    if (j < i) currentValues[i] -= a[i, j] * currentValues[j];
                    if (j > i) currentValues[i] -= a[i, j] * previousValues[j];
                }
                currentValues[i] /= a[i, i];
            }
            var difference = 0.0;

            for (var i = 0; i < a.GetLength(0); i++)
                difference += Math.Abs(currentValues[i] - previousValues[i]);
            
            if (difference < Tolerance)
                break;
            
            previousValues = currentValues;
        }
        
        var resultMatrix = previousValues;
        return resultMatrix;
    }
}