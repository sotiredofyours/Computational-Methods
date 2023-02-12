namespace Lab5;

public class LinearSystemSolver
{
    private static double eps => Math.Pow(10, -4);
    // @TODO: refactor, add summary
    public static double[] KholetskyMethod(double[,] matrix, double[] resultVector)
    {
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);
        var pMatrix = new double[rows, columns];
        var cMatrix = new double[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            pMatrix[i, 0] = matrix[i, 0];
        }

        for (int i = 0; i < columns; i++)
        {
            cMatrix[0, i] = matrix[0, i] / pMatrix[0, 0];
        }

        for (int i = 0; i < columns; i++)
        {
            CalculateColumn(matrix, pMatrix, cMatrix, i);
            CalculateRow(matrix, pMatrix, cMatrix, i);   
        }

        var yMatrix = new double[rows];
        yMatrix[0] = resultVector[0] / pMatrix[0, 0];
        for (int i = 1; i < rows; i++)
        {
            var b = resultVector[i];
            var sum = 0d;
            for (int k = 0; k < i; k++)
            {
                sum += pMatrix[i, k] * yMatrix[k];
            }

            yMatrix[i] = (b - sum) / pMatrix[i, i];
        }

        var result = new double[rows];
        result[rows - 1] = yMatrix[rows - 1];
        for (int i = rows - 2; i >= 0; i--)
        {
            var sum = 0d;
            for (int k = i; k < rows; k++)
            {
                sum += cMatrix[i, k] * result[k];
            }

            result[i] = yMatrix[i] - sum;
        }

        return result;
    }

    private static void CalculateColumn(double[,] matrix, double[,] pMatrix, double[,] cMatrix, int column)
    {
        for (int i = column; i < matrix.GetLength(0); i++)
        {
            var a = matrix[i, column];
            var sum = 0d;
            for (int k = 0; k < i; k++)
            {
                sum += pMatrix[i, k] * cMatrix[k, column];
            }

            pMatrix[i, column] = a - sum;
        }
    }

    private static void CalculateRow(double[,] matrix, double[,] pMatrix, double[,] cMatrix, int row)
    {
        for (int i = row; i < matrix.GetLength(0); i++)
        {
            var a = matrix[row, i];
            var sum = 0d;
            for (int j = 0; j < i; j++)
            {
                sum += pMatrix[row, j] * cMatrix[j, i];
            }

            cMatrix[row, i] = (a - sum) / pMatrix[row, row];
        }
    }

    public static double[] SeidelMethod(double[,] matrix, double[] resultVector)
    {
        
        double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];
        var resultMatrix = new double[resultVector.GetLength(0)];
        for (var i = 0; i < a.GetLength(0); i++)
        for (var j = 0; j < a.GetLength(1) - 1; j++)
            a[i, j] = matrix[i, j];

        for (var i = 0; i < a.GetLength(0); i++)
            a[i, a.GetLength(1) - 1] = resultVector[i];
        
        var previousValues = new double[matrix.GetLength(0)];
        for (var i = 0; i < previousValues.GetLength(0); i++) previousValues[i] = 0.0;
        while (true)
        {
            // Введем массив значений неизвестных на текущей итерации
            var currentValues = new double[a.GetLength(0)];

            // Посчитаем значения неизвестных на текущей итерации
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                // Инициализируем i-ую неизвестную значением
                // свободного члена i-ой строки матрицы
                currentValues[i] = a[i, a.GetLength(0)];

                // Вычитаем сумму по всем отличным от i-ой неизвестным
                for (var j = 0; j < a.GetLength(0); j++)
                {
                    if (j < i) currentValues[i] -= a[i, j] * currentValues[j];
                    if (j > i) currentValues[i] -= a[i, j] * previousValues[j];
                }
                currentValues[i] /= a[i, i];
            }
            var differency = 0.0;

            for (var i = 0; i < a.GetLength(0); i++)
                differency += Math.Abs(currentValues[i] - previousValues[i]);

            // Если необходимая точность достигнута, то завершаем процесс
            if (differency < eps)
                break;

            // Переходим к следующей итерации, так
            // что текущие значения неизвестных
            // становятся значениями на предыдущей итерации

            previousValues = currentValues;
        }

        // результат присваиваем матрице результатов.
        resultMatrix = previousValues;
        return resultMatrix;
    }
}