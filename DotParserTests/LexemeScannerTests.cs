using DotParser.LexicalAnalysis;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DotParserTests;

[TestClass]
public class LexemeScannerTests
{
    [TestMethod]
    public void Keywords_attributes()
    {
        string input = "node [shape= box]\n";

        Lexeme[] actual = LexemeScanner.ScanLexemes(input);

        Lexeme[] expected = new Lexeme[] {
            new Word("node"),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Bracket.Factory().GetLexeme("["),
            new Word("shape"),
            new Equals.Factory().GetLexeme("="),
            new WhiteSpace.Factory().GetLexeme(" "),
            new Word("box"),
            new Bracket.Factory().GetLexeme("]"),
            new WhiteSpace.Factory().GetLexeme("\n")
        };

        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Nodes_edge_nodes()
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
    public void OneKeyword()
    {
        string input = "digraph";
        Word actual = (Word)LexemeScanner.ScanLexemes(input)[0];

        Assert.AreEqual(Keyword.Digraph, actual.Keyword);
        Assert.AreEqual(true, actual.IsKeyword);
    }

    [TestMethod]
    public void ManySpaces()
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
    public void UseQuotes()
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
}
