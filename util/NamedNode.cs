namespace AOC.util;

public class NamedNode
{
    public HashSet<NamedNode> Connections { get; }
    public string Name { get; }

    public NamedNode(string name)
    {
        Connections = new HashSet<NamedNode>();
        Name = name;
    }

    public void AddConnection(NamedNode other)
    {
        Connections.Add(other);
        other.Connections.Add(this);
    }
}