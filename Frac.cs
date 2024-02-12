public readonly struct Frac
{
    private readonly int num;
    private readonly int den;

    // 標準建構式：分子、分母
    public Frac(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
        }
        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }
        num = numerator;
        den = denominator;
    }
    // ValueTuple 建構式
    public Frac((int numerator, int denominator) frac) : this(frac.numerator, frac.denominator) { }
    // 由字串解析分子分母
    static Func<string, (int numerator, int denominator)> ParseFrac = fracStr =>
    {
        var parts = fracStr.Split('/');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid fraction string.", nameof(fracStr));
        }
        try
        {
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }
        catch
        {
            throw new ApplicationException($"Failed to convert string to farction - {fracStr}");
        }
    };
    // 使用字串建立分數
    public Frac(string fracStr) : this(ParseFrac(fracStr)) { }
    public static implicit operator Frac(string fracStr) => new Frac(fracStr);
    // 使用整數建立分數
    public Frac(int number) : this(number, 1) { }
    public static implicit operator Frac(int number) => new Frac(number);

    // 正、負、加減乘除
    public static Frac operator +(Frac a) => a;
    public static Frac operator -(Frac a) => new Frac(-a.num, a.den);
    public static Frac operator +(Frac a, Frac b)
        => new Frac(a.num * b.den + b.num * a.den, a.den * b.den).Simplify();
    public static Frac operator -(Frac a, Frac b)
        => a + (-b);
    public static Frac operator *(Frac a, Frac b)
        => new Frac(a.num * b.num, a.den * b.den).Simplify();
    public static Frac operator /(Frac a, Frac b)
    {
        if (b.num == 0)
        {
            throw new DivideByZeroException();
        }
        return new Frac(a.num * b.den, a.den * b.num).Simplify();
    }

    // 指定分母
    public Frac WithDenominator(int newDenominator)
    {
        // 若無法整除，則無法指定分母
        if (newDenominator == 0 || newDenominator % den != 0)
        {
            throw new ArgumentException($"Cannot specify the denominator - {newDenominator}.", nameof(newDenominator));
        }
        return new Frac(num * newDenominator / den, newDenominator);
    }

    public override string ToString() =>
        den == 1 ? num.ToString() : $"{num}/{den}";

    // 約分
    public Frac Simplify()
    {
        int gcd = Gcd(num, den);
        return new Frac(num / gcd, den / gcd);
    }
    // 通分
    public static (Frac, Frac) CommonDenominator(Frac a, Frac b)
    {
        int lcm = Lcm(a.den, b.den);
        return (new Frac(a.num * lcm / a.den, lcm), new Frac(b.num * lcm / b.den, lcm));
    }
    // 最大公約数
    private static int Gcd(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        return Gcd(b, a % b);
    }
    // 最小公倍数
    private static int Lcm(int a, int b)
    {
        return a * b / Gcd(a, b);
    }

    // 相等比較 (先約分再比較) 
    // 註：Equals、GetHashCode、==、!= 要一起實作
    public override bool Equals(object? obj) => obj is Frac other
        && this.Simplify().ToString() == other.Simplify().ToString();
    public override int GetHashCode() => Simplify().ToString().GetHashCode();
    public static bool operator ==(Frac a, Frac b) => a.Equals(b);
    public static bool operator !=(Frac a, Frac b) => !a.Equals(b);

    // 大小比較 (先通分再比較) 註：大於小放要一起實作
    public static bool operator <(Frac a, Frac b) =>
        CommonDenominator(a, b).Item1.num < CommonDenominator(a, b).Item2.num;
    public static bool operator >(Frac a, Frac b) =>
        CommonDenominator(a, b).Item1.num > CommonDenominator(a, b).Item2.num;
    public static bool operator <=(Frac a, Frac b) => a < b || a == b;
    public static bool operator >=(Frac a, Frac b) => a > b || a == b;

}
