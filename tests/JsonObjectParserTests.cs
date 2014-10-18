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
    }
}
