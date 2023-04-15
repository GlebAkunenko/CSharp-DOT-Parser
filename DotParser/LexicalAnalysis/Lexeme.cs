using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.LexicalAnalysis;

public class Lexeme
{
    public string Value { get; protected set; }

    protected Lexeme(string value)
    {
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Lexeme || obj is null)
            return false;
        return
            this.GetType() == obj.GetType() &&
            this.Value == ((Lexeme)obj).Value;
    }

    public static bool operator ==(Lexeme a, Lexeme b) => a.Equals(b);
    public static bool operator !=(Lexeme a, Lexeme b) => !(a == b);
}
