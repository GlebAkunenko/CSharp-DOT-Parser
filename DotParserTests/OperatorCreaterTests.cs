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

    private Lexeme word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void Modify_edge()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("a"));
        Add(comma);
        Add(word("b"));
        Add(edge("->"));
        Add(word("c"));

        var creater = new OperatorCreater(input);
        creater.Modify();

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
    public void Modify_edge_attributes()
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

        var creater = new OperatorCreater(input);
        creater.Modify();

        Assert.IsTrue(input.Count == 4);
        Assert.IsInstanceOfType(input[0], typeof(EdgeOperator));
        Assert.AreEqual(bracket("["), input[1].Lexeme);

        EqualSeparator attr = (EqualSeparator)input[2];
        EqualSeparator part1 = (EqualSeparator)attr.First;
        EqualOperator kv1 = (EqualOperator)part1.First;
        EqualOperator kv2 = (EqualOperator)part1.Second;
        EqualOperator kv3 = (EqualOperator)attr.Second;

        Assert.AreEqual(word("k1"), kv1.First.Lexeme);
        Assert.AreEqual(word("v1"), kv1.Second.Lexeme);
        Assert.AreEqual(word("k2"), kv2.First.Lexeme);
        Assert.AreEqual(word("v2"), kv2.Second.Lexeme);
        Assert.AreEqual(word("k3"), kv3.First.Lexeme);
        Assert.AreEqual(word("v3"), kv3.Second.Lexeme);
    }
}