using System;
using NUnit.Framework;
using Json;

namespace Tests.Serializer
{
    [TestFixture()]
    public class JsonNumberSerializerTests
    {
        [Test()]
        public void SerializeDouble()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("0.01", test.Serialize(0.01));
        }

        [Test()]
        public void SerializeInteger()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("123", test.Serialize(123));
        }

        [Test()]
        public void SerializeByte()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("255", test.Serialize((byte)255));
        }

        [Test()]
        public void SerializeNull()
        { 
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("null", test.Serialize(null));
        }

        [Test()]
        public void SerializeNan()
        {
            try
            {
                JsonSerializer testInstance = new JsonSerializer();
                testInstance.Serialize(double.NaN);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { 
            }
        }

        [Test()]
        public void SerializePositiveInfinity()
        {
            try
            {
                JsonSerializer testInstance = new JsonSerializer();
                testInstance.Serialize(double.PositiveInfinity);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { 
            }
        }

        [Test()]
        public void SerializeNegativeInfinity()
        {
            try
            {
                JsonSerializer testInstance = new JsonSerializer();
                testInstance.Serialize(double.NegativeInfinity);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { 
            }
        }

        [Test()]
        public void SerializeMaxDouble()
        { 
            JsonSerializer testInstance = new JsonSerializer();
            Assert.AreEqual("1.7976931348623157E+308", testInstance.Serialize(double.MaxValue));
        }
    }
}
