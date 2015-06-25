using System;
using System.Configuration;
using System.Linq;
using CacheAspect;
using CacheAspect.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCache
{
    /// <summary>
    /// Summary description for ConfigBased
    /// </summary>
    [TestClass]
    public class ConfigBased
    {
        public ConfigBased()
        {
            //TODO change this to config driven
            CacheService.Cache = new MemoryCache();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestLargeObjects()
        {
            Assert.IsTrue(CacheService.Cache is MemoryCache);

            byte[] b1 = GetRandomBlock(16);
            byte[] b2 = GetRandomBlock(16);
            Assert.IsTrue(b1.SequenceEqual(b2));
            CacheService.Cache.Clear();
            byte[] b3 = GetRandomBlock(16);
            Assert.IsFalse(b1.SequenceEqual(b3), "Sequence should not be the same");
        }

        [TestMethod]
        public void TestLargeObjectsParameter()
        {
            byte[] b1 = GetRandomBlock(16);
            byte[] b2 = GetRandomBlock(16);
            byte[] b3 = GetRandomBlock(17);
            Assert.IsTrue(b1.SequenceEqual(b2));
            Assert.IsFalse(b1.SequenceEqual(b3));
            CacheService.Cache.Clear();
        }

        [Cache.Cacheable]
        private byte[] GetRandomBlock(int blocks = 1)
        {
            var r = new Random(DateTime.Now.Millisecond);
            byte[] buffer = new byte[1048576 * blocks];
            r.NextBytes(buffer);
            return buffer;
        }
    }
}
