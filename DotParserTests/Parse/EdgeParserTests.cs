using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = DotParser.DOT.Attribute;
using System.Reflection;
using DotParser.DOT;

namespace DotParser.Parse.Tests;

[TestClass]
public class EdgeParserTests
{
    [TestMethod]
    public void TryWithdrawAttributes_attrs_success()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -> b -> b1 [color = blue,shape=box]";

        MethodInfo method = edgesParser.GetType().GetMethod("TryWithdrawAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
        object[] args = new object[] { line, null, null };
        
        bool        actualResult  = (bool)method.Invoke(edgesParser, args);
        string      actualOutput  = (string)args[1];
        Attribute[] actrualAttr    = (Attribute[])args[2];


        bool        expectedResult = true;
        string      expectedOutput = "a -> b -> b1;";
        Attribute[] expectedAttr   = new Attribute[] {
            new Attribute("color", "blue"),
            new Attribute("shape", "box")
        };

        Assert.AreEqual(expectedResult, actualResult);
        Assert.AreEqual(expectedOutput, actualOutput, $"Output string after withdraw attributes is '{actualOutput}', but expected '{expectedOutput}'");
        CollectionAssert.AreEqual(expectedAttr, actrualAttr);
    }

    [TestMethod]
    public void TryWithdrawAttributes_noAttr_nothing()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -> b -> b1;";

        MethodInfo method = edgesParser.GetType().GetMethod("TryWithdrawAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
        object[] args = new object[] { line, null, null };

        bool actualResult = (bool)method.Invoke(edgesParser, args);
        string actualOutput = (string)args[1];
        Attribute[] actrualAttr = (Attribute[])args[2];

        bool expectedResult = false;
        string expectedOutput = line;
        Attribute[] expectedAttr = null;

        Assert.AreEqual(expectedResult, actualResult);
        Assert.AreEqual(expectedOutput, actualOutput);
        Assert.AreEqual(expectedAttr, actrualAttr);
    }

    [ExpectedException(typeof(TargetInvocationException))]
    [TestMethod]
    public void TryWithdrawAttributes_incorrectAttr_exception()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -> b -> b1 [shape];";

        MethodInfo method = edgesParser.GetType().GetMethod("TryWithdrawAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
        object[] args = new object[] { line, null, null };

        method.Invoke(edgesParser, args);
    }

    [ExpectedException(typeof(TargetInvocationException))]
    [TestMethod]
    public void TryWithdrawAttributes_incorrectAttr2_exception()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -> b -> b1 [shape==box];";

        MethodInfo method = edgesParser.GetType().GetMethod("TryWithdrawAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
        object[] args = new object[] { line, null, null };

        method.Invoke(edgesParser, args);
    }

    [TestMethod]
    public void ParseFromString_acacb_accacbExpected()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -> c -> a->c -> b;";

        Edge[] expected = new Edge[] {
            new Edge("a", "c"),
            new Edge("c", "a"),
            new Edge("c", "b")
        };

        Edge[] actual = edgesParser.ParseFromString(line);

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_acb_accacbbcExpected()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), false);
        string line = "a -- c -- b;";

        Edge[] expected = new Edge[] {
            new Edge("a", "c"),
            new Edge("c", "a"),
            new Edge("c", "b"),
            new Edge("b", "c")
        };

        Edge[] actual = edgesParser.ParseFromString(line);

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_wrongSep_zeroEdges()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), true);
        string line = "a -- c -- b;";

        Edge[] actual = edgesParser.ParseFromString(line);
        Edge[] expected = new Edge[0];

        CollectionAssert.AreEqual(expected, actual);
    }

    [ExpectedException(typeof(DotSyntaxException))]
    [TestMethod]
    public void ParseFromString_noSimicolom_syntaxErrorExpected()
    {
        var edgesParser = new EdgesParser(new AttributesParser(), false);
        string line = "a -- c -- b";
        edgesParser.ParseFromString(line);
    }
}