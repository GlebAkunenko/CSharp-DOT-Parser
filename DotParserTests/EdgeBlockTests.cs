using DotParser.LexicalAnalysis;
using DotParser.SemanticAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = DotParser.DOT.Attribute;
using Edge = DotParser.DOT.Edge;

namespace DotParserTests;

[TestClass]
public class EdgeBlockTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new DotParser.LexicalAnalysis.Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Word word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void a_to_b_attr()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(edge("->"));
        Add(word("b"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a", "b", new DotParser.DOT.Attribute[] {new("k1", "v1")})
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void a1_a2_to_b1_b2_attr()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a1"));
        Add(comma);
        Add(word("a2"));
        Add(edge("->"));
        Add(word("b1"));
        Add(comma);
        Add(word("b2"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a1", "b1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a1", "b2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a2", "b1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a2", "b2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
        };

        CollectionAssert.AreEquivalent(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void a_line_b()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(edge("--"));
        Add(word("b"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a", "b", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b", "a", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void a1_a2_line_b1_b2_attr()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a1"));
        Add(comma);
        Add(word("a2"));
        Add(edge("--"));
        Add(word("b1"));
        Add(comma);
        Add(word("b2"));
        Add(bracket("["));
        Add(word("k1"));
        Add(equal);
        Add(word("v1"));
        Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a1", "b1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a1", "b2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a2", "b1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("a2", "b2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b1", "a1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b1", "a2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b2", "a1", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
            new("b2", "a2", new DotParser.DOT.Attribute[] {new("k1", "v1")}),
        };

        CollectionAssert.AreEquivalent(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void a_to_b()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(edge("->"));
        Add(word("b"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a", "b")
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void a1_a2_to_b()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a1"));
        Add(comma);
        Add(word("a2"));
        Add(edge("->"));
        Add(word("b"));

        new OperatorCreater(input).MakeOperators();

        EdgeBlock e = (EdgeBlock)new EdgeBlock.Factory().GetBlock(input[0]);

        Edge[] actual = e.GetEdges();

        Edge[] expected = new Edge[] {
            new("a1", "b"),
            new("a2", "b")
        };

        CollectionAssert.AreEqual(expected, actual);
        AttributesAssert.AreEqual(expected, actual);
    }



}
