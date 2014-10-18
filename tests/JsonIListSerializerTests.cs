using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Json;

namespace Tests.Serializer
{
    [TestFixture()]
    public class JsonIListSerializerTests
    {
        [Test()]
        public void SerializeEmptyList()
        {
            JsonSerializer testInstance = new JsonSerializer();
            Assert.AreEqual("[]", testInstance.Serialize(new List<bool>()));
        }

        [Test()]
        public void SerializeList()
        {
            JsonSerializer testInstance = new JsonSerializer();

            List<string> list = new List<string>();
            list.Add("Hallo");
            list.Add(" ");
            list.Add("Welt");
            list.Add("!");

            Assert.AreEqual("[\"Hallo\",\" \",\"Welt\",\"!\"]", testInstance.Serialize(list));
        }

        [Test()]
        public void SerializeArrayList()
        {
            JsonSerializer testInstance = new JsonSerializer();

            ArrayList arrayList = new ArrayList();
            arrayList.Add("Hallo");
            arrayList.Add(" ");
            arrayList.Add("Welt");
            arrayList.Add("!");

            Assert.AreEqual("[\"Hallo\",\" \",\"Welt\",\"!\"]", testInstance.Serialize(arrayList));
        }

        [Test()]
        public void SerializeArray()
        {
            JsonSerializer testInstance = new JsonSerializer();

            object[] array = new object[3] {"Mu","Ha","Ha" };

            Assert.AreEqual("[\"Mu\",\"Ha\",\"Ha\"]", testInstance.Serialize(array));
        }

        [Test()]
        public void SerializePrettyArray()
        {
            Dictionary<string, object[]> dict = new Dictionary<string, object[]>();
            dict["test"] = new object[3] { 1, 2, 3 };

            string expected = "{"
                            + "\n  \"test\": ["
                            + "\n    1,"
                            + "\n    2,"
                            + "\n    3"
                            + "\n  ]"
                            + "\n}";

            JsonSerializer testInstance = new JsonSerializer();
            testInstance.Prettify = true;
            testInstance.Indentation = 2;

            string actual = testInstance.Serialize(dict);

            Assert.AreEqual(expected, actual);
        }
    }
}
