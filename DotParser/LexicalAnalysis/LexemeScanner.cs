using System.Globalization;

namespace DotParser.LexicalAnalysis;

public static class LexemeScanner
{
    private static List<Lexeme> GetRawLexemes(string code)
    {
        string current = "";
        List<Lexeme> result = new List<Lexeme>();

        for (int i = 0; i < code.Length; i++) {
            current += code[i];
            if (LexemeCreater.TryGetLexeme(current, out Lexeme lexeme)) {
                current = "";
                result.Add(lexeme);
            }
        }

        return result;
    }

    public static Lexeme[] ScanLexemes(string code)
    {
        List<Lexeme> input = GetRawLexemes(code);
        List<Lexeme> output = new List<Lexeme>();
        
        for(int i = 0; i < input.Count; i++) {

            if (i > 0) {
                if (input[i] is WhiteSpace && input[i - 1] is WhiteSpace)
                    continue;
            }

            if (input[i] is Symbol) {
                Word word = new Word();
                int j = i;
                for (; j < input.Count; j++) {
                    if (input[j] is not Symbol)
                        break;
                    word.AddSymbol((Symbol)input[j]);
                }
                output.Add(word);
                i = j - 1;
            }
            else
                output.Add(input[i]);
        }

        return output.ToArray();
    }
}