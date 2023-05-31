using DotParser.Graphs;
using DotParser.LexicalAnalysis;
using DotParser.SemanticAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edge = DotParser.DOT.Edge;
using Node = DotParser.DOT.Node;
using Attribute = DotParser.DOT.Attribute;
using System.Net.Http.Headers;

namespace DotParserTests;

[TestClass]
public class RawGraphCreaterTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new DotParser.LexicalAnalysis.Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Word word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void a_to_b()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(edge("->"));
        Add(word("b"));

        new OperatorCreater(input).MakeOperators();

        EdgeOperator eo = (EdgeOperator)input[0];
        List<Block> blocks = new List<Block>() { new EdgeBlock(eo)};
        RawGraphCreater creater = new RawGraphCreater();

        RawGraph actual = creater.Create(blocks);

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "b")
            },
            Nodes = new List<Node>() {
                new Node("a"), new Node("b")
            }
        };

        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
    }

    [TestMethod]
    public void a1_a2_to_b1_b2()
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

        new OperatorCreater(input).MakeOperators();

        EdgeOperator eo = (EdgeOperator)input[0];
        List<Block> blocks = new List<Block>() { new EdgeBlock(eo) };
        RawGraphCreater creater = new RawGraphCreater();

        RawGraph actual = creater.Create(blocks);

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a1", "b1"),
                new Edge("a1", "b2"),
                new Edge("a2", "b1"),
                new Edge("a2", "b2")
            },
            Nodes = new List<Node>() {
                new Node("a1"), new Node("b1"),
                new Node("a2"), new Node("b2")
            }
        };

        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
    }

    [TestMethod]
    public void a_b_c_and_u_line_v()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(comma);
        Add(word("b"));
        Add(comma);
        Add(word("c"));
        Add(sep);
        Add(word("u"));
        Add(edge("--"));
        Add(word("v"));

        new OperatorCreater(input).MakeOperators();

        CommaOperator co = (CommaOperator)input[0];
        EdgeOperator eo = (EdgeOperator)input[2];
        List<Block> blocks = new List<Block>() { 
            new NodesBlock(co, null),
            new EdgeBlock(eo)
        };
        RawGraphCreater creater = new RawGraphCreater();

        RawGraph actual = creater.Create(blocks);

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("u", "v"), new Edge("v", "u")
            },
            Nodes = new List<Node>() {
                new Node("a"),
                new Node("b"),
                new Node("c"),
                new Node("u"),
                new Node("v")
            }
        };

        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
    }

    [TestMethod]
    public void a_b_attrs_c_and_a_line_c_attrs()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a")); Add(comma); Add(word("b"));
        Add(bracket("[")); Add(word("k1")); Add(equal); Add(word("v1")); Add(bracket("]"));
        Add(sep);
        Add(word("c"));
        Add(sep);
        Add(word("a")); Add(edge("--")); Add(word("c"));
        Add(bracket("[")); Add(word("k2")); Add(equal); Add(word("v2")); Add(sep); Add(word("k3")); Add(equal); Add(word("v3")); Add(bracket("]"));

        new OperatorCreater(input).MakeOperators();

        CommaOperator a_b = (CommaOperator)input[0];
        AttrubuteOperator attr1 = (AttrubuteOperator)input[1];
        Word c = (Word)input[3].Lexeme;
        EdgeOperator a_line_c = (EdgeOperator)input[5];
        AttrubuteOperator attr2 = (AttrubuteOperator)input[6];

        List<Block> blocks = new List<Block>() {
            new NodesBlock(a_b, attr1),
            new NodesBlock(c),
            new EdgeBlock(a_line_c, attr2)
        };
        RawGraphCreater creater = new RawGraphCreater();

        RawGraph actual = creater.Create(blocks);

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "c", new Attribute[] {new("k2", "k2"), new("k3", "v3")}), 
                new Edge("c", "a", new Attribute[] {new("k2", "v2"), new("k3", "v3")})
            },
            Nodes = new List<Node>() {
                new Node("a", new Attribute[] {new("k1", "v1")}),
                new Node("b", new Attribute[] {new("k1", "v1")}),
                new Node("c")
            }
        };

        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
    }

    [TestMethod]
    public void u_v_and_a_to_b_c()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("u")); Add(sep); Add(word("v"));
        Add(sep);
        Add(word("a")); Add(edge("->")); Add(word("b")); Add(comma); Add(word("c"));

        new OperatorCreater(input).MakeOperators();

        Word u = (Word)input[0].Lexeme;
        Word v = (Word)input[2].Lexeme;
        EdgeOperator a_to_b_c = (EdgeOperator)input[4];

        List<Block> blocks = new List<Block>() {
            new NodesBlock(u),
            new NodesBlock(v),
            new EdgeBlock(a_to_b_c)
        };
        RawGraphCreater creater = new RawGraphCreater();

        RawGraph actual = creater.Create(blocks);

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "b"),
                new Edge("a", "c")
            },
            Nodes = new List<Node>() {
                new Node("a"),
                new Node("b"),
                new Node("c"),
                new Node("u"),
                new Node("v")
            }
        };

        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
    }
}
