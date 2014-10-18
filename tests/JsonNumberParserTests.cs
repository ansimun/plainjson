using System;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonNumberParserTests
    {
        [Test()]
        public void ParseIntegralNumbers()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(3.0, testInstance.Parse("3"));
            Assert.AreEqual(-1.0, testInstance.Parse("-1"));
            Assert.AreEqual(0.0, testInstance.Parse("0"));
            Assert.AreEqual(3000000E14, testInstance.Parse("3000000E14"));
            Assert.AreEqual(-123e2, testInstance.Parse("-123e2"));
        }

        [Test()]
        public void ParseFloatingPointNumbers()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(1.0000000000000012, testInstance.Parse("1.0000000000000012"));
            Assert.AreEqual(-324.99, testInstance.Parse("-324.99"));
        }

        [Test()]
        public void ParseMaxDouble()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(double.MaxValue, testInstance.Parse("1.7976931348623157E+308"));
        }

        [Test()]
        public void ParseMinDouble()
        { 
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(double.MinValue, testInstance.Parse("-1.7976931348623157E+308"));
        }

        [Test()]
        public void ParseEpsilon()
        {
            JsonParser testInstance = new JsonParser();
            Assert.AreEqual(double.Epsilon, testInstance.Parse("4.94065645841247e-324"));
        }
    }
}
