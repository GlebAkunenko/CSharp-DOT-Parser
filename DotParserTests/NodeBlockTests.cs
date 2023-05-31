using DotParser.DOT;
using DotParser.LexicalAnalysis;
using DotParser.SemanticAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class NodeBlockTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new DotParser.LexicalAnalysis.Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Word word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void a_attr()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        NodesBlock n = (NodesBlock)new NodesBlock.Factory().GetBlock(input[0]);

        Node[] actual = n.GetNodes();

        Node[] expected = new Node[] {
            new("a", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    public void a_b_c_attr()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(comma);
        Add(word("b"));
        Add(comma);
        Add(word("c"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        NodesBlock n = (NodesBlock)new NodesBlock.Factory().GetBlock(input[0]);

        Node[] actual = n.GetNodes();

        Node[] expected = new Node[] {
            new("a", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("c", new DotParser.DOT.Attribute[] {new("k1", "v1")})
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }
}
