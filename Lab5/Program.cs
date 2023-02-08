using Lab5;

var k = 29;
var m = 6;

var matrix = new double[,]
{
    {12 + k, 2, m/4, 1, 2},
    {4, 113+k, 1, m/10, m-4},
    {1, 2, -24-k, 3, 4},
    {1, 2/m, 4, 33+k, 4},
    {-1, 2, -3, 3+m, -44-k}
};
var matrix2 = new double[,]
{
    {-4, 1, 1},
    {1, -9, 3},
    {1, 2, -16}
};

var result = new double[]
{
    2, 5, 13
};

var matrix3 = new double[,]
{
    {3, -2},
    {5, 1}
};

var result1 = new double[]
{
    -6, 3
};
var x = LinearSystemSolver.KhaletskyMethod(matrix3, result1);