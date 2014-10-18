using System;
using System.Collections;
using NUnit.Framework;
using Json;

namespace Tests.Parser
{
    [TestFixture()]
    public class JsonArrayParserTests
    {
        [Test()]
        public void ParseArray()
        {
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[0,1,2]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(0.0, result[0]);
            Assert.AreEqual(1.0, result[1]);
            Assert.AreEqual(2.0, result[2]);
        }

        [Test()]
        public void ParseEmptyArray()
        { 
            JsonParser testInstance = new JsonParser();
            ArrayList result = new ArrayList(testInstance.Parse("[]") as IList);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
