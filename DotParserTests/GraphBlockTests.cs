using DotParser.LexicalAnalysis;
using DotParser.SemanticAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class GraphBlockTests
{
    private Lexeme comma => new Comma.Factory().GetLexeme(",");

    private Lexeme edge(string type) => new DotParser.LexicalAnalysis.Edge.Factory().GetLexeme(type);

    private Lexeme bracket(string type) => new Bracket.Factory().GetLexeme(type);

    private Word word(string word) => new Word(word);

    private Lexeme equal => new Equal.Factory().GetLexeme("=");

    private Lexeme sep => new AlienSeparator();

    [TestMethod]
    public void Digraph()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("digraph"));
        Add(sep);
        Add(word("abads"));
        Add(bracket("{"));

        new OperatorCreater(input).MakeOperators();

        GraphBlock actual = (GraphBlock)new GraphBlock.Factory().GetBlock(input[0]);
        GraphBlock expected = new GraphBlock(GraphBlock.Sort.Digraph);

        Assert.AreEqual(expected.Type, actual.Type);
    }

    [TestMethod]
    public void Graph()
    {
        List<Element> input = new List<Element>();
        void Add(Lexeme lexeme) { input.Add(new Element(input.Count > 0 ? input.Last() : null, lexeme)); }
        Add(word("graph"));
        Add(sep);
        Add(word("abads"));
        Add(bracket("{"));

        new OperatorCreater(input).MakeOperators();

        GraphBlock actual = (GraphBlock)new GraphBlock.Factory().GetBlock(input[0]);
        GraphBlock expected = new GraphBlock(GraphBlock.Sort.Graph);

        Assert.AreEqual(expected.Type, actual.Type);
    }
}
