namespace AOC.util;

public readonly struct Coordinate3D
{
    public override bool Equals(object? obj)
    {
        return obj is Coordinate3D other && Equals(other);
    }

    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public Coordinate3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Coordinate3D((int X, int Y, int Z) coordinates)
    {
        X = coordinates.X;
        Y = coordinates.Y;
        Z = coordinates.Z;
    }

    public Coordinate3D(Coordinate3D coordinate)
    {
        X = coordinate.X;
        Y = coordinate.Y;
        Z = coordinate.Z;
    }

    public Coordinate3D Add(Coordinate3D other)
    {
        return new Coordinate3D(X + other.X, Y + other.Y, Z + other.Z);
    }

    public Coordinate3D Subtract(Coordinate3D other)
    {
        return new Coordinate3D(X - other.X, Y - other.Y, Z - other.Z);
    }

    public Coordinate3D AbsMax(int max)
    {
        return new Coordinate3D(Math.Min(Math.Max(X, -max), max),
            Math.Min(Math.Max(Y, -max), max),
            Math.Min(Math.Max(Z, -max), max));
    }
    public void Deconstruct(out int x, out int y, out int z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    public bool Equals(Coordinate3D p)
    {
        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (X == p.X) && (Y == p.Y) && (Z == p.Z);
    }

    public override int GetHashCode() => (X, Y, Z).GetHashCode();

    public static bool operator ==(Coordinate3D lhs, Coordinate3D rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Coordinate3D lhs, Coordinate3D rhs) => !(lhs == rhs);

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }
}