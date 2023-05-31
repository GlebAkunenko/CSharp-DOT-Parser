using DotParser.LexicalAnalysis;
using DotParser.SemanticAnalysis;
using DotParser.SyntaxAnalysis;

namespace DotParser.Graphs;

public static class RawGraphParser
{
    private static List<Lexeme> AnaliseLexical(string code)
    {
        var lexemes = LexemeScanner.ScanLexemes(code);
        lexemes = LexemeScanner.RemoveUselessWhiteSpaces(lexemes);
        List<Lexeme> result = new List<Lexeme>(lexemes.Length);
        result.AddRange(lexemes);
        return result;
    }

    private static List<Element> AnaliseSyntax(List<Lexeme> lexemes)
    {
        ElementsCreater elementsCreater = new ElementsCreater();
        List<Element> result = elementsCreater.Create(lexemes);
        OperatorCreater operatorCreater = new OperatorCreater(result);
        operatorCreater.MakeOperators();
        return result;
    }

    private static List<Block> AnaliseSemantic(List<Element> elements)
    {
        BlockCreater blockCreater = new BlockCreater();
        return blockCreater.CreateBlocks(elements);
    }

    public static RawGraph Parse(string code)
    {
        List<Lexeme> lexemes = AnaliseLexical(code);
        List<Element> elements = AnaliseSyntax(lexemes);
        List<Block> blocks = AnaliseSemantic(elements);

        return new RawGraphCreater().Create(blocks);
    }
}
