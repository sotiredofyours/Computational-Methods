using NUnit.Framework;

namespace Lab5.Tests;

[DefaultFloatingPointTolerance(0.0001)]
public class Tests
{
    [Test]
    public void TestOnSmallMatrix()
    {
        var matrix = new double[,]
        {
            {-4, 1, 1},
            {1, -9, 3},
            {1, 2, -16}
        };
        var resultVector = new double[] { 2, 5, 13 };
        var expected = new double[] { -1, -1, -1 };

        var kholetskyMethod = LinearSystemSolver.KholetskyMethod(matrix, resultVector);
        var seidelMethod = LinearSystemSolver.SeidelMethod(matrix, resultVector);
        CollectionAssert.AreEqual(expected, kholetskyMethod);
        CollectionAssert.AreEqual(expected, seidelMethod);
    }

    [Test]
    public void TestOnMyMatrix()
    {
        
        var k = 29; // количество букв в ФИО
        var m = 6; // количество букв в имени
        
        var matrix = new double[,]
        {
            {12 + k, 2, m/4, 1, 2},
            {4, 113+k, 1, m/10, m-4},
            {1, 2, -24-k, 3, 4},
            {1, 2/m, 4, 33+k, 4},
            {-1, 2, -3, 3+m, -44-k}
        };
        
        var resultVector = new double[] { 1, 2, 3, 4, 5};

        var kholetskyMethod = LinearSystemSolver.KholetskyMethod(matrix, resultVector);
        var seidelMethod = LinearSystemSolver.SeidelMethod(matrix, resultVector);
        
        CollectionAssert.AreEqual(seidelMethod, kholetskyMethod);
    }
}