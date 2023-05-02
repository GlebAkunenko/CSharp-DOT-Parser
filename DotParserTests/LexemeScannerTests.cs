using DotParser.LexicalAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class LexemeScannerTests
{
    [TestMethod]
    public void ScanLexemes_keywords_attributes()
    {
        string input = "node [shape= box]\n";

        Lexeme[] actual = LexemeScanner.ScanLexemes(input);

        Lexeme[] expected = new Lexeme[] {
            new Word("node"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Bracket.Factory().GetLexeme("["),
            new Word("shape"),
            new Equal.Factory().GetLexeme("="),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Word("box"),
            new Bracket.Factory().GetLexeme("]"),
            new WhiteSpace.Factory().GetLexeme("\n")
        };

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanLexemes_nodes_edge_nodes()
    {
        string input = "a1,a2, a -> b,b1";

        Lexeme[] actual = LexemeScanner.ScanLexemes(input);

        Lexeme[] expected = new Lexeme[] {
            new Word("a1"),
            new Comma.Factory().GetLexeme(","),
            new Word("a2"),
            new Comma.Factory().GetLexeme(","),
            new WhiteSpace.Factory().GetLexeme(""),
            new Word("a"),
            new WhiteSpace.Factory().GetLexeme(""),
            new Edge.Factory().GetLexeme("->"),
            new WhiteSpace.Factory().GetLexeme(""),
            new Word("b"),
            new Comma.Factory().GetLexeme(","),
            new Word("b1")
        };

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanLexemes_oneKeyword()
    {
        string input = "digraph";
        Word actual = (Word)LexemeScanner.ScanLexemes(input)[0];

        Assert.AreEqual(Keyword.Digraph, actual.Keyword);
        Assert.AreEqual(true, actual.IsKeyword);
    }

    [TestMethod]
    public void ScanLexemes_ManySpaces()
    {
        string input = "  a\n \t-- \n b\n\n";

        Lexeme[] actual = LexemeScanner.ScanLexemes(input);

        Lexeme[] expected = new Lexeme[] {
            new WhiteSpace.Factory().GetLexeme(""),
            new Word("a"),
            new WhiteSpace.Factory().GetLexeme(""),
            new Edge.Factory().GetLexeme("--"),
            new WhiteSpace.Factory().GetLexeme(""),
            new Word("b"),
            new WhiteSpace.Factory().GetLexeme("")
        };

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScanLexemes_UseQuotes()
    {
        string input = "    \"start\" -> \"edit message\" -> \" --    \ncool -- \"\r\n";

        Lexeme[] actual = LexemeScanner.ScanLexemes(input);

        Lexeme[] expected = new Lexeme[] {
            new WhiteSpace.Factory().GetLexeme(" "),
            new Word("start"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Edge.Factory().GetLexeme("->"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Word("edit message"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Edge.Factory().GetLexeme("->"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Word(" --    \ncool -- "),
            new WhiteSpace.Factory().GetLexeme("")
        };

        CollectionAssert.AreEqual(expected, actual);
    }

    public void RemoveUselessSpaces()
    {
        Lexeme[] input = new Lexeme[] {
            new Word("a"),                                  //0
            new Comma.Factory().GetLexeme(","),             //1
            new WhiteSpace.Factory().GetLexeme("\n"),       //2
            new Word("b"),                                  //3
            new Comma.Factory().GetLexeme(","),             //4
            new WhiteSpace.Factory().GetLexeme("\n"),       //5
            new Word("c"),                                  //6
            new WhiteSpace.Factory().GetLexeme(" "),        //7
            new Edge.Factory().GetLexeme("->"),             //8
            new WhiteSpace.Factory().GetLexeme(" "),        //9
            new Word("u"),                                  //10
            new Comma.Factory().GetLexeme(","),             //11
            new WhiteSpace.Factory().GetLexeme("\n"),       //12
            new Word("v"),                                  //13
            new WhiteSpace.Factory().GetLexeme(" "),        //14
            new Bracket.Factory().GetLexeme("["),           //15
            new WhiteSpace.Factory().GetLexeme("\t"),       //16
            new Word("shape"),                              //17
            new WhiteSpace.Factory().GetLexeme(" "),        //18
            new Equal.Factory().GetLexeme("="),             //19
            new WhiteSpace.Factory().GetLexeme(" "),        //20
            new Word("box"),                                //21
            new Comma.Factory().GetLexeme(","),             //22
            new WhiteSpace.Factory().GetLexeme("\t"),       //23
            new Word("color"),                              //24
            new WhiteSpace.Factory().GetLexeme(" "),        //25
            new Equal.Factory().GetLexeme("="),             //26
            new WhiteSpace.Factory().GetLexeme(" "),        //27
            new Word("blue"),                               //28
            new WhiteSpace.Factory().GetLexeme("\n"),       //29
            new Bracket.Factory().GetLexeme("]"),           //30
            new WhiteSpace.Factory().GetLexeme("\n")        //31
        };

        Lexeme[] actual = LexemeScanner.RemoveUselessWhiteSpaces(input);

        Lexeme[] expected = new Lexeme[] {
            input[0],
            input[1],
            input[3],
            input[4],
            input[6],
            input[8],
            input[10],
            input[11],
            input[13],
            input[15],
            input[16],
            input[17],
            input[18],
            input[19],
            input[20],
            input[21],
            input[22],
            input[23],
            input[24],
            input[25],
            input[26],
            input[27],
            input[28],
            input[29],
            input[30],
            input[31]
        };

        CollectionAssert.AreEqual(expected, actual);
    }
}
