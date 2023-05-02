using DotParser.LexicalAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class ElementsCreaterTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Lexeme word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    [TestMethod]
    public void Create()
    {
        List<Lexeme> input = new List<Lexeme>() {
            word("a"), comma, word("b"), edge("->"),
            word("c"), comma, word("d"),
            bracket("["), word("k1"), equal, word("v1"),
            word("k2"), equal, word("v2"), comma,
            word("k3"), equal, word("v3"), bracket("]")
        };

        List<Element> actual = new ElementsCreater().Create(input);

        Assert.IsTrue(actual.Count == 20);
        Assert.AreEqual("a", actual[0].Lexeme.Value);
        Assert.AreEqual(",", actual[1].Lexeme.Value);
        Assert.AreEqual("b", actual[2].Lexeme.Value);
        Assert.AreEqual("->", actual[3].Lexeme.Value);
        Assert.AreEqual("c", actual[4].Lexeme.Value);
        Assert.AreEqual(",", actual[5].Lexeme.Value);
        Assert.AreEqual("d", actual[6].Lexeme.Value);
        Assert.AreEqual("[", actual[7].Lexeme.Value);
        Assert.AreEqual("k1", actual[8].Lexeme.Value);
        Assert.AreEqual("=", actual[9].Lexeme.Value);
        Assert.AreEqual("v1", actual[10].Lexeme.Value);
        Assert.IsInstanceOfType(actual[11].Lexeme, typeof(AlienSeparator));
        Assert.AreEqual("k2", actual[12].Lexeme.Value);
        Assert.AreEqual("=", actual[13].Lexeme.Value);
        Assert.AreEqual("v2", actual[14].Lexeme.Value);
        Assert.AreEqual(",", actual[15].Lexeme.Value);
        Assert.AreEqual("k3", actual[16].Lexeme.Value);
        Assert.AreEqual("=", actual[17].Lexeme.Value);
        Assert.AreEqual("v3", actual[18].Lexeme.Value);
        Assert.AreEqual("]", actual[19].Lexeme.Value);
    }
}
