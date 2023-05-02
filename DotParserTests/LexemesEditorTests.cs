using DotParser.LexicalAnalysis;
using DotParser.SyntaxAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotParserTests;

[TestClass]
public class LexemesEditorTests
{
    [TestMethod]
    public void Edit_attributes()
    {
        // a -> b [shape = box color = blue, k=v]
        List<Lexeme> input = new List<Lexeme>() {
            /*0*/  new Word("a"),
            /*1*/  new Edge.Factory().GetLexeme("->"),
            /*2*/  new Word("b"),
            /*3*/  new Bracket.Factory().GetLexeme("["),
            /*4*/  new Word("shape"),
            /*5*/  new Equal.Factory().GetLexeme("="),
            /*6*/  new Word("box"),
            /*7*/  new Word("color"),
            /*8*/  new Equal.Factory().GetLexeme("="),
            /*9*/  new Word("blue"),
            /*10*/ new Semicolon.Factory().GetLexeme(","),
            /*11*/ new Word("k"),
            /*12*/ new Equal.Factory().GetLexeme("="),
            /*13*/ new Word("v"),
            /*14*/ new Bracket.Factory().GetLexeme("]")
        };

        new LexemesEditor().Edit(input);

        List<Lexeme> expected = new List<Lexeme>() {
            /*0*/  new Word("a"),
            /*1*/  new Edge.Factory().GetLexeme("->"),
            /*2*/  new Word("b"),
            /*3*/  new Bracket.Factory().GetLexeme("["),
            /*4*/  new Word("shape"),
            /*5*/  new Equal.Factory().GetLexeme("="),
            /*6*/  new Word("box"),
            /*7*/  new AlienSeparator(),
            /*8*/  new Word("color"),
            /*9*/  new Equal.Factory().GetLexeme("="),
            /*10*/ new Word("blue"),
            /*11*/ new Semicolon.Factory().GetLexeme(","),
            /*12*/ new Word("k"),
            /*13*/ new Equal.Factory().GetLexeme("="),
            /*14*/ new Word("v"),
            /*15*/ new Bracket.Factory().GetLexeme("]")
        };

        CollectionAssert.AreEqual(expected, input);
    }
}
