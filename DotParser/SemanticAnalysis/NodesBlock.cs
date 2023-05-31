using DotParser.DOT;
using DotParser.Graphs;
using DotParser.LexicalAnalysis;
using DotParser.SyntaxAnalysis;
using Attribute = DotParser.DOT.Attribute;
using Node = DotParser.DOT.Node;

namespace DotParser.SemanticAnalysis;

public class NodesBlock : Block
{
    private List<Word> _nodes;
    private AttrubuteOperator? _attrubutes;

    public NodesBlock(Word node, AttrubuteOperator? attrubutes)
    {
        _nodes = new List<Word>() { node };
        _attrubutes = attrubutes;
    }

    public NodesBlock(Word node) : this(node, null) { }

    public NodesBlock(CommaOperator nodes, AttrubuteOperator attrubutes)
    {
        _nodes = GetWordsFromComma(nodes);
        _attrubutes = attrubutes;
    }

    public NodesBlock(CommaOperator nodes) : this(nodes, null) { }

    private List<Word> GetWordsFromComma(CommaOperator comma)
    {
        if (comma.First.Lexeme is Word w1 && comma.Second.Lexeme is Word w2)
            return new List<Word>() { w1, w2 };

        var result = new List<Word>();
        if (comma.First is CommaOperator comma1)
            result.AddRange(GetWordsFromComma(comma1));
        result.Add((Word)comma.Second.Lexeme);
        return result;
    }

    private Attribute[] GetAttributes()
    {
        if (_attrubutes == null)
            return Array.Empty<Attribute>();
        List<Attribute> result = new List<Attribute>();
        foreach (var attribute in _attrubutes.GetAttributes())
            result.Add(new Attribute(attribute.Item1.Value, attribute.Item2.Value));
        return result.ToArray();
    }

    public Node[] GetNodes()
    {
        Node[] result = new Node[_nodes.Count];
        Attribute[] attributes = GetAttributes();
        for (int i = 0; i < result.Length; i++)
            result[i] = new Node(_nodes[i].Value, attributes);
        return result;
    }

    public override void ApplyToGraph(RawGraph graph)
    {
        graph.Nodes.AddRange(GetNodes());
    }

    public class Factory : BlockFactory
    {
        public override bool IsRelevant(Element element)
        {
            return
                (element.Lexeme is Word || element is CommaOperator) &&
                element.Right is AttrubuteOperator;
        }

        public override Block GetBlock(Element element)
        {
            AttrubuteOperator attrubutes = (AttrubuteOperator)element.Right;
            if (element.Lexeme is Word word)
                return new NodesBlock(word, attrubutes);
            if (element is CommaOperator comma)
                return new NodesBlock(comma, attrubutes);
            throw new Exception();
        }
    }
}
