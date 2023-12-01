namespace AOC.util;

public class NamedNodeGraph
{
    private readonly Dictionary<string, NamedNode> _names;

    public NamedNodeGraph()
    {
        _names = new Dictionary<string, NamedNode>();
    }

    public bool AddNode(NamedNode namedNode)
    {
        if (Contains(namedNode.Name)) return false;
        _names[namedNode.Name] = namedNode;
        return true;
    }

    public bool Contains(string name)
    {
        return _names.ContainsKey(name);
    }

    public NamedNode TryGetOrAdd(string name)
    {
        if (!Contains(name))
            AddNode(new NamedNode(name));
        return _names[name];
    }
    
    public NamedNode Get(string name)
    {
        return _names[name];
    }

    public void AddNodesAndEdge(string name1, string name2)
    {
        var node1 = TryGetOrAdd(name1);
        var node2 = TryGetOrAdd(name2);
        node1.AddConnection(node2);
    }
}