using System;
using System.Net.Http.Headers;

namespace DotParser.Parse.Dictionary;

public class DictionaryParser
{
    public Dictionary<string, string> ParseFromString(string str)
    {
        str = str.Trim();
        var result = new Dictionary<string, string>();

        Buffer keyBuffer = new Buffer(
            forbidden: null,
            ignore: new char[] {' ', '\t', '\n', ';', ','},
            switching: new char[] {'='}
        );
        Buffer valueBuffer = new Buffer(
            forbidden: new char[] {'='},
            ignore: new char[] {' ', ';', ',', '\t', '\n'},
            switching: new char[] {',', ';', ' ', '\t', '\n'}
        );
        Buffer currentBuffer = keyBuffer;

        void switchOnValue() => currentBuffer = valueBuffer;
        void switchOnKey() => currentBuffer = keyBuffer;
        void savePair()
        {
            string key = keyBuffer.GetValue();
            string value = valueBuffer.GetValue();
            if (key == "" || value == "")
                throw new ParseExeption("Empth field");
            result.Add(key, value);
            keyBuffer.Clear();
            valueBuffer.Clear();
        }

        keyBuffer.Switched += switchOnValue;
        valueBuffer.Switched += switchOnKey;
        valueBuffer.Switched += savePair;

        for (int i = 0; i < str.Length; i++) 
            currentBuffer.TakeChar(str[i]);

        savePair();

        keyBuffer.Switched -= switchOnValue;
        valueBuffer.Switched -= switchOnKey;

        return result;
    }

    public class ParseExeption : Exception
    {
        public ParseExeption(string message) : base(message) { }
    }

}
