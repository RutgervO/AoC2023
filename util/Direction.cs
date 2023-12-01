namespace AOC.util;

public readonly struct Direction
{
    public override bool Equals(object? obj)
    {
        return obj is Direction other && Equals(other);
    }

    public int X { get; }
    public int Y { get; }
    
    public Direction((int X, int Y) direction)
    {
        X = direction.X;
        Y = direction.Y;
    }

    public Direction(Direction direction)
    {
        X = direction.X;
        Y = direction.Y;
    }
    
    public Direction(string name)
    {
        X = 0;
        Y = 0;
        switch (name)
        {
            case "N": case "U": Y = -1; break;
            case "S": case "D": Y = 1; break;
            case "W": case "L": X = -1; break;
            case "E": case "R": X = 1; break;
            default:
                throw new ArgumentException($"Unknown direction $name.", name);
        }
    }

    public static IEnumerable<Direction> AllDirections()
    {
        return "NSWE".Select(c => new Direction(c.ToString()));
    }
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    public Coordinate ToCoordinate()
    {
        return new Coordinate(X, Y);
    }
    
    public bool Equals(Direction p)
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

    public static bool operator ==(Direction lhs, Direction rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Direction lhs, Direction rhs) => !(lhs == rhs);

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}