using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Json;

namespace Tests.Serializer
{
    [TestFixture()]
    public class JsonIDictionarySerializerTests
    {
        [Test()]
        public void SerializeEmptyHashtable()
        {
            JsonSerializer testInstance = new JsonSerializer();
            Assert.AreEqual("{}", testInstance.Serialize(new Hashtable()));
        }

        [Test()]
        public void SerializeHashtable()
        {
            JsonSerializer testInstance = new JsonSerializer();

            string expected1 = "{\"A\":5,\"B\":\"Hallo Welt\"}";
            string expected2 = "{\"B\":\"Hallo Welt\",\"A\":5}";

            Hashtable table = new Hashtable();
            table["A"] = 5;
            table["B"] = "Hallo Welt";

            string result = testInstance.Serialize(table);
            Assert.IsTrue(result.Equals(expected1) || result.Equals(expected2));
        }

        [Test()]
        public void SerializeDictionary()
        {
            JsonSerializer testInstance = new JsonSerializer();

            string expected = "{\"A\":5,\"B\":6,\"C\":10000}";

            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict["A"] = 5;
            dict["B"] = 6;
            dict["C"] = 10000;

            Assert.AreEqual(expected, testInstance.Serialize(dict));
        }

        [Test()]
        public void SerializeSortedList()
        {
            JsonSerializer testInstance = new JsonSerializer();

            string expected = "{\"A\":false,\"B\":true,\"C\":true}";

            SortedList<string, bool> list = new SortedList<string, bool>();
            list["A"] = false;
            list["B"] = true;
            list["C"] = true;

            Assert.AreEqual(expected, testInstance.Serialize(list));
        }

        [Test()]
        public void SerializeDictionaryWithArrayValue()
        {
            JsonSerializer testInstance = new JsonSerializer();

            Dictionary<string, IList> list = new Dictionary<string, IList>();
            list.Add("Bla", new object[5] {1,2,3,4,5});

            Assert.AreEqual("{\"Bla\":[1,2,3,4,5]}", testInstance.Serialize(list));
        }

        [Test()]
        public void SerializePrettyDictionaryWidthSmallIndentation()
        {
            JsonSerializer testInstance = new JsonSerializer();
            testInstance.Prettify = true;
            testInstance.Indentation = 2;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["Hallo"] = "Welt";
            dict["Ping"] = "Pong";

            string expected = "{"
                            + "\n  \"Hallo\": \"Welt\","
                            + "\n  \"Ping\": \"Pong\""
                            + "\n}";

            string result = testInstance.Serialize(dict);

            Assert.AreEqual(expected, result);
        }

        [Test()]
        public void SerializePrettyDictionaryWidthLargeIndentation()
        {
            string expected = "{"
                            + "\n    \"A\": \"Hallo Welt\","
                            + "\n    \"B\": ["
                            + "\n        1,"
                            + "\n        2"
                            + "\n    ],"
                            + "\n    \"C\": {"
                            + "\n        \"Ping\": \"Pong\""
                            + "\n    }"
                            + "\n}";

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["A"] = "Hallo Welt";
            dict["B"] = new object[2] { 1, 2 };
            Hashtable table = new Hashtable();
            table["Ping"] = "Pong";
            dict["C"] = table;

            JsonSerializer testInstance = new JsonSerializer();
            testInstance.Prettify = true;
            testInstance.Indentation = 4;

            string actual = testInstance.Serialize(dict);

            Assert.AreEqual(expected, actual);
        }
    }
}
