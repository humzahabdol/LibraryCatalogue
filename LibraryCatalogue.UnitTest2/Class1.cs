using System.Configuration;
using LibraryInfoCatalogue.Helper.BusinessClass;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace LibraryCatalogue.UnitTest2
{
    [TestFixture]
    public class Class1
    {
        private readonly BookHelper _bookHelper;
        public Class1()
        {
            _bookHelper = new BookHelper();
        }
        [Test]
        public void Should_return_true()
        {
            Assert.That(true,Is.EqualTo(true));
        }

        [Test]
        public void Should_return_keyword_with_no_space()
        {
            string s = "hello world";
           
            var t = _bookHelper.SplitKeywords(s);
            Assert.That(t.Count == 1);
            Assert.That(t[0],Is.EqualTo("helloworld"));
        }

        [Test]
        public void SplitKeyword_Should_Have_Length_of_Two()
        {
            string s = "hello ; world";

            var t = _bookHelper.SplitKeywords(s);


            Assert.That(t.Count,Is.EqualTo(2));
        }
        
    }
}
