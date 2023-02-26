using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotParser.Parse;
using Attribute = DotParser.DOT.Attribute;
using System.Reflection.Metadata;
using System.Reflection;

namespace DotParser.Parse.Tests;

[TestClass]
public class AttributesParserTests
{
    [TestMethod]
    public void FromString_2attr_parseSuccess()
    {
        var parser = new AttributesParser();
        var input = "[shape=box, info = 123]";
        
        Attribute[] externed = new Attribute[] {
            new Attribute("shape", "box"),
            new Attribute("info", "123")
        };

        Attribute[] actual = parser.FromString(input);

        CollectionAssert.AreEqual(externed, actual);
    }

    [TestMethod]
    public void FromString_1attr_parseSuccess()
    {
        var parser = new AttributesParser();
        var input = "[shape = box]";

        Attribute[] externed = new Attribute[] {
            new Attribute("shape", "box")
        };

        Attribute[] actual = parser.FromString(input);

        CollectionAssert.AreEqual(externed, actual);
    }

    [ExpectedException(typeof(DotSyntaxException))]
    [TestMethod]
    public void FromString_wrongSyntax_exeptionExpected()
    {
        string input = "[shape = box, info==true]";
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
