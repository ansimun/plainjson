using System;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonBooleanParserTests
    {
        [Test()]
        public void ParseTrueValue()
        {
            JsonParser testInstance = new JsonParser();
            Assert.IsTrue((bool)testInstance.Parse("true"));
        }

        [Test()]
        public void ParseFalseValue()
        {
            JsonParser testInstance = new JsonParser();
            Assert.IsFalse((bool)testInstance.Parse("false"));
        }

        [Test()]
        [ExpectedException(typeof(FormatException))]
        public void ParseInvalidTrueValue()
        {
            JsonParser testInstance = new JsonParser();
            testInstance.Parse("True");
        }
    }
}
