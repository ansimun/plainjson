using System;
using System.Collections;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonArrayParserTests
    {
        [Test()]
        public void ParseNumberArray()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[0,1,2]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(0.0, result [0]);
            Assert.AreEqual(1.0, result [1]);
            Assert.AreEqual(2.0, result [2]);
        }

        [Test()]
        public void ParseEmptyArray()
        { 
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test()]
        public void ParseEmptyArrayIncludingWhitespace()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[ \t\n\r ]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test()]
        public void ParseStringArray()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[\"value1\",\"value2\"]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("value1", result [0]);
            Assert.AreEqual("value2", result [1]);
        }

        [Test()]
        public void ParseArrayWithContentOfDifferentTypes()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[123,\"value2\"]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(123.0, result [0]);
            Assert.AreEqual("value2", result [1]);
        }

        [Test()]
        public void ParseArrayWithWhitespaceBetweenValues()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[ 123  ,\"value\",   50.0 ,   \" \"  ]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(123.0, result [0]);
            Assert.AreEqual("value", result [1]);
            Assert.AreEqual(50.0, result [2]);
            Assert.AreEqual(" ", result [3]);
        }
    }
}
