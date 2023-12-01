namespace AOC.util;

public readonly struct Coordinate
{
    public override bool Equals(object? obj)
    {
        return obj is Coordinate other && Equals(other);
    }

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

    public Coordinate Subtract(Coordinate other)
    {
        return new Coordinate(X - other.X, Y - other.Y);
    }

    public Coordinate AbsMax(int max)
    {
        return new Coordinate(Math.Min(Math.Max(X, -max), max), Math.Min(Math.Max(Y, -max), max));
    }
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
    public bool Equals(Coordinate p)
    {
        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (X == p.X) && (Y == p.Y);
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
}