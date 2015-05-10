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
        [ExpectedException(typeof(FormatException))]
        public void ParseEmptyJsonString()
        {
            JsonParser testInstance = new JsonParser();
            testInstance.Parse("");
        }
    }
}
