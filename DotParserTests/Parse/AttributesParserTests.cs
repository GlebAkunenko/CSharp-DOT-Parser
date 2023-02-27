using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotParser.Parse;
using Attribute = DotParser.DOT.Attribute;
using System.Reflection.Metadata;
using System.Reflection;
using DotParser.Parse.Dictionary;

namespace DotParser.Parse.Tests;

[TestClass]
public class AttributesParserTests
{
    private static Attribute[] successExpectedDataResult => new Attribute[] {
        new Attribute("shape", "box"),
        new Attribute("label", "Hello world"),
        new Attribute("info", "123"),
        new Attribute("key", "value")
    };

    private static IEnumerable<object[]> successExpectedData => new object[][] {
        new object[] {"[shape=box,label=\"Hello world\",info=123,key=value]", successExpectedDataResult},
        new object[] {"[shape =   box\nlabel  = \"Hello world\"\ninfo=123\n   key\n=\nvalue\n]", successExpectedDataResult},
        new object[] {" [ shape = box label = \"Hello world\" info = 123 key = value ] ", successExpectedDataResult},
        new object[] {"[ shape = box ; label = \"Hello world\" , info=123 \n, key = value]", successExpectedDataResult},
        new object[] {"[\t shape \n= box ;\t  label =\n \"Hello world\" , info=123 \n,\n key  = \tvalue]", successExpectedDataResult},
    };

    private static IEnumerable<object[]> parseExceptionExpectedData => new object[][] {
        new object[] {"[shape=bo=x,label=\"Hello world\",info=123,key=value]"},
        new object[] {"[shape=box,label=\"Hello world\",info=,key=value]"},
        new object[] {"[shape=box,label=\"Hello world\",info=123,key=]"},
        new object[] {"[shape=box,label=\"Hello w\"orld\",info=123,key=value]"},
    };

    private static IEnumerable<object[]> breaksExceptionExpectedData => new object[][] {
        new object[] {"shape=box,label=\"Hello world\",info=123,key=value]"},
        new object[] {"[shape=box,abel=\"Hello world\",info=123,key=value"}
    };

    [TestMethod]
    [DynamicData(nameof(successExpectedData))]
    public void FromString_FromDynamicDataTest(string input, Attribute[] expected)
    {
        var parser = new AttributesParser();
        Attribute[] actual = parser.FromString(input);
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    [DynamicData(nameof(parseExceptionExpectedData))]
    [ExpectedException(typeof(DictionaryParser.ParseExeption))]
    public void FromString_FromDynamicDataTest_ParseExeption(string input)
    {
        var parser = new AttributesParser();
        parser.FromString(input);
    }

    [TestMethod]
    public void HasAttributes_2attr_true()
    {
        string line = "a -> b->c->  d [color=blue,  info   = 134];";
        var parser = new AttributesParser();

        bool actual = parser.HasAttributes(line);

        bool expected = true;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HasAttributes_noAttr_false()
    {
        string line = "a -> b->c->  d;";
        var parser = new AttributesParser();

        bool actual = parser.HasAttributes(line);

        bool expected = false;
        Assert.AreEqual(expected, actual);
    }

    [ExpectedException(typeof(DotSyntaxException))]
    [TestMethod]
    public void HasAttributes_incorrect_exeption()
    {
        string line = "a -> b->c->  d [[color=blue,  info   = 134]";
        var parser = new AttributesParser();
        parser.HasAttributes(line);
    }
}
