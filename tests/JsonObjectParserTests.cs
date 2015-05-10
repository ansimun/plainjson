using System;
using System.Collections;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonObjectParserTests
    {
        [Test()]
        public void ParseEmptyObject()
        {
            JsonParser parser = new JsonParser();
            Hashtable result = parser.Parse("{}") as Hashtable;
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test()]
        public void ParseObject() 
        {
            JsonParser parser = new JsonParser();
            Hashtable result = parser.Parse("{\"Hallo\":0,\"Welt\":1}") as Hashtable;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("Hallo"));
            Assert.AreEqual(0.0, result["Hallo"]);
            Assert.IsTrue(result.ContainsKey("Welt"));
            Assert.AreEqual(1.0, result["Welt"]);
        }        

        [Test()]
        public void ParsePrettifiedObject()
        {
            JsonParser testInstance = new JsonParser();
            Hashtable result = testInstance.Parse("{"
                + "\n  \"A\": true,"
                + "\n  \"B\": 123.4e7,"
                + "\n  \"C\": ["
                + "\n    \"test\","
                + "\n    20"
                + "\n  ]"
                + "\n}") as Hashtable;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("A"));
            Assert.AreEqual(true, (bool)result["A"]);
            Assert.IsTrue(result.ContainsKey("B"));
            Assert.AreEqual(123.4e7, (double)result["B"]);
            Assert.IsTrue(result.ContainsKey("C"));
            Assert.AreEqual(2, (result["C"] as IList).Count);
            Assert.AreEqual("test", (result["C"] as IList)[0]);
            Assert.AreEqual(20.0, (result["C"] as IList)[1]);
        }
    }
}
