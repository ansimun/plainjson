using System;
using System.Collections;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonValueParserTests
    {
        [Test()]
        public void ParseNullValue()
        {
            JsonParser testInstance = new JsonParser();
            Assert.IsNull(testInstance.Parse("null"));
        }

        [Test()]
        public void ParseEmptyJsonString()
        {
            try
            {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("");
                Assert.Fail();
            }
            catch (FormatException)
            {
            }
        }

        [Test()]
        public void ParsePrettyObject()
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
