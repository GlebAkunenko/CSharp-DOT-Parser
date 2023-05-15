using DotParser.LexicalAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class OperatorCreaterTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Word word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void Edge()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(comma);
        Add(word("b"));
        Add(edge("->"));
        Add(word("c"));

        var creater = new OperatorCreater(input);
        creater.MakeOperators();

        Assert.IsTrue(input.Count == 1);
        EdgeOperator eo = (EdgeOperator)input[0];
        Assert.AreEqual(null, eo.Left);
        Assert.AreEqual(null, eo.Right);
        CommaOperator c0 = (CommaOperator)eo.First;
        Assert.AreEqual(word("a"), c0.First.Lexeme);
        Assert.AreEqual(word("b"), c0.Second.Lexeme);
        Assert.AreEqual(word("c"), eo.Second.Lexeme);
    }

    [TestMethod]
    public void Edge_attributes()
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
        Add(sep);
        Add(word("k2"));
        Add(equal);
        Add(word("v2"));
        Add(comma);
        Add(word("k3"));
        Add(equal);
        Add(word("v3"));
        Add(bracket("]"));

        List<Tuple<Word, Word>> expectedAttrbiutes = new List<Tuple<Word, Word>>() {
            new Tuple<Word, Word>(word("k1"), word("v1")),
            new Tuple<Word, Word>(word("k2"), word("v2")),
            new Tuple<Word, Word>(word("k3"), word("v3"))
        };

        var creater = new OperatorCreater(input);
        creater.MakeOperators();

        Assert.IsTrue(input.Count == 2);

        EdgeOperator edge_op = (EdgeOperator)input[0];
        List<Tuple<Word, Word>> actualAttributes = ((AttrubuteOperator)input[1]).GetAttributes();

        Assert.AreEqual(word("a"), edge_op.First.Lexeme);
        Assert.AreEqual(word("b"), edge_op.Second.Lexeme);

        Assert.AreEqual(expectedAttrbiutes[0], actualAttributes[0]);
        Assert.AreEqual(expectedAttrbiutes[1], actualAttributes[1]);
        Assert.AreEqual(expectedAttrbiutes[2], actualAttributes[2]);
    }

    [TestMethod]
    public void Nodes_edges()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }

        // a, b, c
        // n, m -> u, v
        Add(word("a"));
        Add(comma);
        Add(word("b"));
        Add(comma);
        Add(word("c"));
        Add(sep);
        Add(word("n"));
        Add(comma);
        Add(word("m"));
        Add(edge("->"));
        Add(word("u"));
        Add(comma);
        Add(word("v"));

        var creater = new OperatorCreater(input);
        creater.MakeOperators();

        Assert.IsTrue(input.Count == 3);

        CommaOperator nodes = (CommaOperator)input[0];
        EdgeOperator edge1 = (EdgeOperator)input[2];

        Assert.AreEqual(word("a"), ((CommaOperator)nodes.First).First.Lexeme);
        Assert.AreEqual(word("b"), ((CommaOperator)nodes.First).Second.Lexeme);
        Assert.AreEqual(word("c"), nodes.Second.Lexeme);
        Assert.AreEqual(word("n"), ((CommaOperator)edge1.First).First.Lexeme);
        Assert.AreEqual(word("m"), ((CommaOperator)edge1.First).Second.Lexeme);
        Assert.AreEqual(word("u"), ((CommaOperator)edge1.Second).First.Lexeme);
        Assert.AreEqual(word("v"), ((CommaOperator)edge1.Second).Second.Lexeme);
    }

    [TestMethod]
    public void Node_edge()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }

        // a
        // u -> v
        Add(word("a"));
        Add(sep);
        Add(word("u"));
        Add(edge("->"));
        Add(word("v"));

        var creater = new OperatorCreater(input);
        creater.MakeOperators();

        Assert.IsTrue(input.Count == 3);

        Assert.AreEqual(word("a"), input[0].Lexeme);
        Assert.AreEqual(word("u"), ((EdgeOperator)input[2]).First.Lexeme);
        Assert.AreEqual(word("v"), ((EdgeOperator)input[2]).Second.Lexeme);

    }
}