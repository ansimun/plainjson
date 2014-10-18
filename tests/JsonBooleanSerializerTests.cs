using System;
using NUnit.Framework;
using Json;

namespace Tests.Serializer
{
    [TestFixture()]
    public class JsonBooleanSerializerTests
    {
        [Test()]
        public void SerializeTrueValue()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("true", test.Serialize(true));
        }

        [Test()]
        public void SerializeFalseValue()
        { 
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("false", test.Serialize(false));
        }
    }
}
