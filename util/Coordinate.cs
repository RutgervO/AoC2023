namespace AOC.util;

public readonly struct Coordinate : IComparable<Coordinate>, IComparable
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinate((int X, int Y) coordinates)
    {
        X = coordinates.X;
        Y = coordinates.Y;
    }

    public Coordinate(Coordinate coordinate)
    {
        X = coordinate.X;
        Y = coordinate.Y;
    }

    public Coordinate Add(Coordinate other)
    {
        return new Coordinate(X + other.X, Y + other.Y);
    }
    
    public Coordinate Add(Direction other)
    {
        return new Coordinate(X + other.X, Y + other.Y);
    }

    public Coordinate Subtract(Coordinate other)
    {
        return new Coordinate(X - other.X, Y - other.Y);
    }

    public Coordinate AbsMax(int max)
    {
        return new Coordinate(Math.Min(Math.Max(X, -max), max), Math.Min(Math.Max(Y, -max), max));
    }

    public bool IsInverse(Coordinate other)
    {
        return (X + other.X == 0) && (Y + other.Y == 0);
    }
    
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
    public bool Equals(Coordinate p)
    {
        // If run-time types are not exactly the same, return false.
        if (GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return X == p.X && Y == p.Y;
    }

    public override int GetHashCode() => (X, Y).GetHashCode();

    public static bool operator ==(Coordinate lhs, Coordinate rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Coordinate lhs, Coordinate rhs) => !(lhs == rhs);

    public override string ToString()
    {
        return $"({X},{Y})";
    }
    public int CompareTo(Coordinate other)
    {
        var xComparison = X.CompareTo(other.X);
        if (xComparison != 0) return xComparison;
        return Y.CompareTo(other.Y);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        return obj is Coordinate other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Coordinate)}");
    }

    public static bool operator <(Coordinate left, Coordinate right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Coordinate left, Coordinate right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Coordinate left, Coordinate right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Coordinate left, Coordinate right)
    {
        return left.CompareTo(right) >= 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is Coordinate other && Equals(other);
    }

}