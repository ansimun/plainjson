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
        public void ParseStringWithMissingDelimiter()
        {
            try
            {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("Hallo\"");
                Assert.Fail();
            }
            catch (FormatException)
            { 
            
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test()]
        public void ParseStringWithInvalidEscapes()
        {
            try
            {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("\"Hallo\\m\"");
                Assert.Fail();
            }
            catch (FormatException)
            { 
            }
        }

        [Test()]
        public void ParseStringWithInvalidUnicodeSequence()
        {
            try
            {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("\"\\uZZZZ\"");
                Assert.Fail();
            }
            catch (FormatException)
            { }
        }

        [Test()]
        public void ParseStringWithTruncatedUnicodeSequence()
        {
            try
            {
                JsonParser testInstance = new JsonParser();
                testInstance.Parse("\"\\u012");
                Assert.Fail();
            }
            catch (FormatException)
            { }
        } 
    }
}
