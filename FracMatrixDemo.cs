public class FracMatrixDemo 
{
    public static void GaussianEliminationDemo() 
    {
/*
聯立方程組
7x - 2y + 3z = 12
2x - y + 4z = 12
-x + 3y - z = 2
*/        
        Frac[,] matrix = new Frac[3, 4] 
        {
            {7, -2, 3, 12},
            {2, -1, 4, 12},
            {-1, 3, -1, 2}
        };
        PrintMatrix(matrix, "原矩陣");
        // 排序將絕對值最大的列放在最上面
        SortRows(matrix);
        PrintMatrix(matrix, "排序後");
        // 將矩陣化為上三角矩陣
        Console.WriteLine("列階梯形矩陣");
        ToRowEchelonForm(matrix);
        // 將對角線以外係數調成 0
        Console.WriteLine("簡化列階梯形矩陣");
        ToDiagonal(matrix);
    }

    static void ToRowEchelonForm(Frac[,] matrix)
    {
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);
        for (int i = 0; i < rowCount; i++)
        {
            Console.WriteLine($"** 第 {i + 1} 列 **");
            // 將對角線係數調成 1
            var diag = matrix[i, i];
            for (int j = i; j < colCount; j++)
            {
                matrix[i, j] /= diag;
            }
            PrintMatrix(matrix, $"({i}, {i}) 改為 1");
            // 將對角線以下係數調成 0
            for (int k = i + 1; k < rowCount; k++)
            {
                var factor = matrix[k, i];
                for (int l = i; l < colCount; l++)
                {
                    matrix[k, l] -= matrix[i, l] * factor;
                    if (k == l && matrix[k, l] == 0) 
                    {
                        throw new ApplicationException("無限多組解");
                    }                    
                }
            }
            if (i < rowCount - 1)
                PrintMatrix(matrix, $"({i}, {i}) 以下改為 0");            
        }
    }

    static void ToDiagonal(Frac[,] matrix)
    {
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);
        for (int i = rowCount - 1; i > 0; i--)
        {
            Console.WriteLine($"** 第 {i + 1} 列 **");
            for (int j = i - 1; j >= 0; j--)
            {
                var factor = matrix[j, i];
                for (int k = i; k < colCount; k++)
                {
                    matrix[j, k] -= matrix[i, k] * factor;
                }
            }
            PrintMatrix(matrix, $"({i}, {i}) 以上改為 0");
        }
    }

    static void SortRows(Frac[,] matrix)
    {
        // 排序將對角線元素絕對值較大者放在最上面
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);
        for (int i = 0; i < rowCount; i++)
        {
            int maxRow = i;
            for (int j = i + 1; j < rowCount; j++)
            {
                if (matrix[j, i].Abs() > matrix[maxRow, i].Abs())
                {
                    maxRow = j;
                }
            }
            // 與最大列交換
            if (maxRow != i)
            {
                for (int k = 0; k < colCount; k++)
                {
                    var temp = matrix[i, k];
                    matrix[i, k] = matrix[maxRow, k];
                    matrix[maxRow, k] = temp;
                }
            }
        }

    }

    public static void PrintMatrix(Frac[,] matrix, string title)
    {
        var len = Math.Max(5, matrix.Cast<Frac>().Max(o => o.ToString().Length) + 2);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(title);
        Console.ForegroundColor = ConsoleColor.Yellow;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(len));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.ResetColor();
    }
}