using DotParser.DOT;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotParser.Parse;

public class EdgesParser : IEdgesParser
{
    private bool _isDigraph;

    public EdgesParser(bool isDigraph)
    {
        _isDigraph = isDigraph;
    }

    private Node[] ReadNodes(string str)
    {
        string[] nodes = str.Split(',');
        Node[] result = new Node[nodes.Length];
        for (int i = 0; i < nodes.Length; i++)
            result[i] = new Node(nodes[i].Trim());
        return result;
    }

    private void ConnectNodes(Node[] from, Node[] to, List<Edge> container)
    {
        void TryAddEdge(Node u, Node v)
        {
            Edge e = new Edge(u, v);
            if (!container.Contains(e))
                container.Add(e);
        }

        foreach(Node u in from) {
            foreach (Node v in to) {
                TryAddEdge(u, v);
                if (!_isDigraph)
                    TryAddEdge(v, u);
            }
        }
    }

    public Edge[] ParseFromString(string input)
    {
        List<Edge> result = new List<Edge>();

        string separator = _isDigraph ? "->" : "--";
        string[] lexemes = input.Split(separator);

        for(int i = 0; i < lexemes.Length - 1; i++) {
            Node[] from = ReadNodes(lexemes[i]);
            Node[] to   = ReadNodes(lexemes[i+1]);
            ConnectNodes(from, to, result);
        }

        return result.ToArray();
    }
}
