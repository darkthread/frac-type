using MathNet.Numerics.LinearAlgebra;
public class MathNetDemo
{
    public static void Solve()
    {
/*
聯立方程組
7x - 2y + 3z = 12
2x - y + 4z = 12
-x + 3y - z = 2
*/             
        var matrix = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {7, -2, 3},
            {2, -1, 4},
            {-1, 3, -1}
        });
        var vector = Vector<double>.Build.Dense(new double[] {12, 12, 2});
        var result = matrix.Solve(vector);
        Console.WriteLine($"x = {result[0]}, y = {result[1]}, z = {result[2]}");
    }
}