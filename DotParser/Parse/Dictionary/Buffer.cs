namespace DotParser.Parse.Dictionary;

internal class Buffer
{
    private string _value;
    private char[] _forbidden;
    private char[] _ignore;
    private char[] _switching;

    private bool _commaMode;

    public Buffer(char[] forbidden, char[] ignore, char[] switching)
    {
        _forbidden = forbidden ?? Array.Empty<char>();
        _ignore = ignore ?? Array.Empty<char>();
        _switching = switching ?? Array.Empty<char>();
        _value = "";
    }

    public void TakeChar(char symbol)
    {
        if (symbol == '\"') {
            _commaMode = !_commaMode;
            if (_commaMode && _value != "")
                throw new DictionaryParser.ParseExeption("Incorrect comma");
            return;
        }
        if (_commaMode) {
            _value += symbol;
            return;
        }

        if (_switching.Contains(symbol) && _value != "") {
            Switched();
            return;
        }
        if (_ignore.Contains(symbol))
            return;
        if (_forbidden.Contains(symbol))
            throw new DictionaryParser.ParseExeption("Founded forbidden symbol");
         _value += symbol;
    }

    public string GetValue() => _value;

    public void Clear() => _value = "";

    public event System.Action Switched;
}