using System.Runtime.InteropServices;
using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public class AttributeScanner
{
    const string incorrect_string = "INCORRECTINCORRECT";

    private IAttributeParser _attributeParser;
    private string _code;
    private int _begin = -1;
    private int _end = -1;
    private string _attributes = incorrect_string;

    private Exception needCallException => new Exception("Need call FindAttributeString() before read properties");

    public string AttributeString => _attributes == incorrect_string ? throw needCallException : _attributes;

    public int StartIndex => _begin == -1 ? throw needCallException : _begin;

    public int EndIndex => _end == -1 ? throw needCallException : _end;

    public AttributeScanner(string code, IAttributeParser attributeParser)
    {
        _attributeParser = attributeParser;
        _code = code;
    }

    public bool HasAttributes(string str)
    {
        return _attributeParser.HasAttributes(str);
    }

    public bool HasAttributes() => HasAttributes(_code);

    public void FindAttributeString()
    {
        string attributes = "";
        int begin = 0;
        for(int i = 0; i < _code.Length; i++) {
            if (attributes != "")
                attributes += _code[i];
            if (_code[i] == ']')
                break;
            if (_code[i] == '[') {
                if (attributes != "")
                    throw new DotSyntaxException("Brackets error");
                attributes += _code[i];
                begin = i;
            }
        }
        _begin = begin;
        if (attributes.Length != 0)
            _end = _begin + attributes.Length - 1;
        else 
            _end = 0;
        _attributes = attributes;
    }
}