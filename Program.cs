Console.WriteLine(@"解聯立方程組
=================
7x - 2y + 3z = 12
2x - y + 4z = 12
-x + 3y - z = 2");
Console.WriteLine("\n使用 MathNet 程式庫");
MathNetDemo.Solve();
Console.WriteLine("\n分數矩陣");
FracMatrixDemo.GaussianEliminationDemo();

// 使用自訂分數型別進行數學運算
Frac a = new Frac(1, 2);
Frac b = new Frac("3/4");
Frac c = new Frac(2);
Frac d = "2/3";
Frac e = 3;
Frac f = "4/6";

Console.WriteLine($"a = {a}"); // 1 / 2
Console.WriteLine($"正號 +a = {a}"); // 1 / 2
Console.WriteLine($"負號 -a = {-a}"); // -1 / 2
Console.WriteLine($"加法 {a} + {b} = {a + b}"); // 5 / 4
Console.WriteLine($"減法 {a} - {b} = {a - b}"); // -1 / 4
Console.WriteLine($"乘法 {a} * {b} = {a * b}"); // 3 / 8
Console.WriteLine($"除法 {a} / {b} = {a / b}"); // 2 / 3
Console.WriteLine($"乘整數 {b} * -3 = {a * -3}"); // -3 / 2
Console.WriteLine($"除整數 {b} / 3 = {a / 3}"); // 1 / 6
Console.WriteLine($"大於 {b} > {d} = {b > d}"); // True
Console.WriteLine($"小於 {b} < {d} = {b < d}"); // False
Console.WriteLine($"等於 {d} == {f} = {d == f}"); // True
Console.WriteLine($"大於等於 {d} >= {f} = {d >= f}"); // True
Console.WriteLine($"相等 {d}.Equals({f}) = {d.Equals(f)}"); // True
Console.WriteLine($"指定分母(9) {d} == {d.WithDenominator(9)}"); // 6 / 9
try {
    Console.WriteLine($"指定分母(7) {d} == {d.WithDenominator(7)}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
// -2 * 1/3 + 3/4 = 1/12
Frac g = -2 * (Frac)"1/3" + new Frac(3, 4);
Console.WriteLine($"-2 * 1/3 + 3/4 = {g}"); // 1 / 12