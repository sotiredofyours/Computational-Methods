namespace Lab5;

public class LinearSystemSolver
{
    public static double[] KhaletskyMethod(double[,] matrix, double[] resultVector)
    {
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);
        var pMatrix = new double[rows,columns];
        var cMatrix = new double[rows,columns];
        for (int i = 0; i < rows; i++)
        {
            pMatrix[i, 0] = matrix[i, 0];
        }

        for (int i = 0; i < columns; i++)
        {
            cMatrix[0, i] = matrix[0, i] / pMatrix[0, 0];
        }
        CalculateColumn(matrix, pMatrix, cMatrix, 1);
        CalculateRow(matrix, pMatrix, cMatrix, 1);
        CalculateColumn(matrix, pMatrix, cMatrix, 2);
        CalculateRow(matrix, pMatrix, cMatrix, 2);

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
        for (int i = rows - 2; i > 0; i--)
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
}