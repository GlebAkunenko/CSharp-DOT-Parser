using DotParser.DOT;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public class EdgesParser : IEdgesParser
{
    private IAttributeParser _attributeParser;
    private bool _isDigraph;

    public EdgesParser(IAttributeParser attributeParser, bool isDigraph)
    {
        _attributeParser = attributeParser;
        _isDigraph = isDigraph;
    }

    private int Count(string subject, char obj)
    {
        int result = 0;
        foreach (char c in subject)
            result += (obj == c) ? 1 : 0;
        return result;
    }

    private bool HasAttributes(string str)
    {
        int openBrackets = Count(str, '[');
        int closeBrackets = Count(str, ']');
        if (openBrackets != closeBrackets || openBrackets > 2)
            throw new DotSyntaxException("Brackets error");
        return openBrackets == 1;
    }

    private bool TryWithdrawAttributes(string given, out string result, out Attribute[] attributes)
    {
        if (HasAttributes(given)) {
            int begin = -1, end = -1;
            for(int i = 0; i < given.Length; i++) {
                if (given[i] == '[')
                    begin = i;
                if (given[i] == ']')
                    end = i;
            }
            if (begin >= end)
                throw new DotSyntaxException("Brackets error");
            string attributeString = "";
            for (int i = begin; i < end; i++)
                attributeString += given[i];

            attributes = _attributeParser.FromString(attributeString);

            result = "";
            for (int i = 0; i < begin - 1; i++)
                result += given[i];
            result += ";";

            return true;
        }
        else {
            result = given;
            attributes = null;
            return false;
        }
    }

    private string ArrangeString(string input)
    {
        if (!input.EndsWith(';'))
            throw new DotSyntaxException("The line must end with a semicolon");
        return input.Remove(input.Length - 1);
    }

    public Edge[] ParseFromString(string input)
    {
        input = ArrangeString(input);

        List<Edge> result = new List<Edge>();

        void TryAddEdge(Node u, Node v, Attribute[] attributes) {
            Edge edge = new Edge(u, v, attributes);
            if (!result.Contains(edge))
                result.Add(edge);
        }

        TryWithdrawAttributes(input, out string input2, out Attribute[] attributes);

        string separator = _isDigraph ? "->" : "--";
        string[] nodes = input2.Split(separator);

        for (int i = 0; i < nodes.Length - 1; i++) {
            Node u = new Node(nodes[i].Trim());
            Node v = new Node(nodes[i + 1].Trim());
            TryAddEdge(u, v, attributes);
            if (!_isDigraph)
                TryAddEdge(v, u, attributes);
        }

        return result.ToArray();
    }
}
