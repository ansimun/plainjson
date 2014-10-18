using System;
using NUnit.Framework;
using Json;

namespace Tests.Serializer
{
    [TestFixture()]
    public class JsonStringSerializerTests
    {
        [Test()]
        public void SerializeSimpleString()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("\"Hallo Welt\"", test.Serialize("Hallo Welt"));
        }

        [Test()]
        public void SerializeStringWithEscapes()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("\"\\\"-\\\\-\\/-\\b-\\f-\\n-\\r-\\t\"", 
                test.Serialize("\"-\\-/-\b-\f-\n-\r-\t"));
        }

        [Test()]
        public void SerializeStringWithUnicodeSequence()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("\"\\u00df\"", test.Serialize("ß"));
            Assert.AreEqual("\"Hallo\\u047aWelt\"", test.Serialize("Hallo\u047AWelt"));
        }

        [Test()]
        public void SerializeSimpleChar()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("\"Z\"", test.Serialize('Z'));
        }

        [Test()]
        public void SerializeEmptyString()
        {
            JsonSerializer test = new JsonSerializer();
            Assert.AreEqual("\"\"", test.Serialize(""));
        }
    }
}
