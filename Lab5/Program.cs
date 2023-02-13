using Lab5;

var k = 29;
var m = 6;

var eps = Math.Pow(10, -4);

var matrix = new double[,]
{
    {12 + k, 2, m/4, 1, 2},
    {4, 113+k, 1, m/10, m-4},
    {1, 2, -24-k, 3, 4},
    {1, 2/m, 4, 33+k, 4},
    {-1, 2, -3, 3+m, -44-k}
};

var result = new double[]
{
    1, 2, 3, 4, 5
};

var x = LinearSystemSolver.SeidelMethod(matrix, result);
var y = LinearSystemSolver.KholetskyMethod(matrix, result);

Console.WriteLine();
