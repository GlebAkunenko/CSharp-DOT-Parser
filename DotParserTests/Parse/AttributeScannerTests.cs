using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse.Tests;

[TestClass]
public class AttributeScannerTests
{
    private static IEnumerable<object> data => new object[][] {
        new object[] { "start [shape=Mdiamond];\n", "[shape=Mdiamond]", 6, 21},
        new object[] { "a,\r\nb\r\n,\r\nc\r\n[shape\r\n=\r\nbox\r\n]", "[shape\r\n=\r\nbox\r\n]", 13, 29 },
        new object[] { "node  [ shape = box ]\r\na, b, c [color=blue]", "[ shape = box ]", 6, 20 },
        new object[] { "    a,\r\n    b\r\n    ,\r\n    c\r\n    [shape\r\n    =\r\n    box\r\n    ]\r\n    a -> b1, b2 -> c;", "[shape\r\n    =\r\n    box\r\n    ]", 33, 61 }
    };

    [TestMethod]
    public void FindAttributeString_noAttr_empty()
    {
        string input = "a -> b->c->  d;";
        AttributeScanner scanner = new AttributeScanner(input, new AttributesParser());
        scanner.FindAttributeString();

        string actualAttributs = scanner.AttributeString;
        int actualStartIndex = scanner.StartIndex;
        int actualEndIndex = scanner.EndIndex;

        string expectedAttributes = "";
        int expectedStartIndex = 0;
        int expectedEndIndex = 0;

        Assert.AreEqual(expectedAttributes, actualAttributs);
        Assert.AreEqual(expectedStartIndex, actualStartIndex);
        Assert.AreEqual(expectedEndIndex, actualEndIndex);
    }

    [TestMethod]
    [DynamicData(nameof(data))]
    public void FindAttributeString_FromDynamicDataTest(string input,
        string expectedAttributes, int expectedStartIndex, int expectedEndIndex)
    {
        AttributeScanner scanner = new AttributeScanner(input, new AttributesParser());
        scanner.FindAttributeString();

        string actualAttributes = scanner.AttributeString;
        int actualStartIndex = scanner.StartIndex;
        int actualEndIndex = scanner.EndIndex;

        Assert.AreEqual(expectedAttributes, actualAttributes);
        Assert.AreEqual(expectedStartIndex, actualStartIndex);
        Assert.AreEqual(expectedEndIndex, actualEndIndex);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Property_exception()
    {
        AttributeScanner scanner = new AttributeScanner("some string", new AttributesParser());
        string actaul = scanner.AttributeString;
    }

}