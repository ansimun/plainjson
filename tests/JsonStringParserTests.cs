using System;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonStringParserTests
    {
        [Test()]
        public void ParseEmptyString()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual("", testInstance.Parse("\"\""));
        }

        [Test()]
        public void ParseSimpleString()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual("Hallo", testInstance.Parse("\"Hallo\""));
        }

        [Test()]
        public void ParseStringWithLeadingAndTrailingWhitespaces()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(" HubaBuba\n \t", testInstance.Parse("\" HubaBuba\\n \\t\""));
        }

        [Test()]
        public void ParseStringWithEscapes()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual("\"-\\-/-\b-\f-\n-\r-\t", 
                testInstance.Parse("\"\\\"-\\\\-\\/-\\b-\\f-\\n-\\r-\\t\""));
        }

        [Test()]
        public void ParseStringWithUnicodeEscapes()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual("Hallo\u047AWelt", testInstance.Parse("\"Hallo\\u047aWelt\""));
        }

        [Test()]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenDelimiterOfStringIsMissing()
        {
            JsonParser testInstance = new JsonParser();
            testInstance.Parse("Hallo\"");
        }

        [Test()]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenStringContainsInvalidEscapes()
        {
            JsonParser testInstance = new JsonParser();
            testInstance.Parse("\"Hallo\\m\"");
        }

        [Test()]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenStringContainsInvalidUnicodeSequence()
        {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("\"\\uZZZZ\"");
        }

        [Test()]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsWhenStringContainsTruncatedUnicodeSequence()
        {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("\"\\u012");
        }
    }
}
