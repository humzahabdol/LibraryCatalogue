using NUnit.Framework;
using System;

namespace LibraryCatalogue.UnitTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            Assert.That(true, Is.EqualTo(true));
        }
    }
}
