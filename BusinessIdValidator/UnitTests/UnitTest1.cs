using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessIdValidator;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void HandlesTooShortBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("1234567-");
            Assert.AreEqual(ok, false);            
        }

        [TestMethod]
        public void HandlesTooLongBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("1234567-89");
            Assert.AreEqual(ok, false);
        }

        [TestMethod]
        public void HasCorrectBusinessIdLength()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("1234567-8");
            Assert.AreEqual(ok, true);
        }

        [TestMethod]
        public void HandlesNullBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy(null);
            Assert.AreEqual(ok, false);
        }

        [TestMethod]
        public void HandlesEmptyBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("");
            Assert.AreEqual(ok, false);
        }

        [TestMethod]
        public void HasHyphenInCorrectPlace()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("1234567-9");
            Assert.AreEqual(ok, true);
        }

        [TestMethod]
        public void DoesNotHaveHyphenInCorrectPlace()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("123456-79");
            Assert.AreEqual(ok, false);
        }

    }
}
