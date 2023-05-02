using DotParser.LexicalAnalysis;
using System;
using System.Diagnostics.Contracts;

namespace DotParser.SyntaxAnalysis;

public class Element
{
    public Element? Left { get; set; }
    public Element? Right { get; set; }
    public Lexeme Lexeme { get; set; }

    public Element(Element? previous, Lexeme lexeme)
    {
        Lexeme = lexeme;
        if (previous != null) {
            previous.Right = this;
            Left = previous;
        }
    }
}