using System;
using System.Collections.Generic;
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
            Assert.IsFalse(spec.IsSatisfiedBy("1234567-"));
        }

        [TestMethod]
        public void HandlesTooLongBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy("1234567-89"));
        }

        [TestMethod]
        public void HasCorrectBusinessIdLength()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy("0737546-2"));
        }

        [TestMethod]
        public void HandlesNullBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(null));
        }

        [TestMethod]
        public void HandlesEmptyBusinessId()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(""));
        }

        [TestMethod]
        public void HasHyphenInCorrectPlace()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy("0737546-2"));
        }

        [TestMethod]
        public void DoesNotHaveHyphenInCorrectPlace()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy("123456-79"));
        }

        [TestMethod]
        public void HasNumericFirstPart()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy("0737546-2"));
        }

        [TestMethod]
        public void HandlesNonNumericFirstPart()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy("0x37546-2"));
        }

        [TestMethod]
        public void HandlesNonNumericCheckDigit()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy("0737546-x"));
        }

        [TestMethod]
        public void HasCorrectCheckDigit()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification();
            Assert.IsTrue(spec.CheckCorrectCheckDigit("0737546-2"));
        }

        [TestMethod]
        public void DoesNotAllowModOne()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification();
            Assert.IsFalse(spec.CheckCorrectCheckDigit("1802465-1"));
        }

        [TestMethod]
        public void FindsFalseCheckDigits()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification();

            /*
             * Normally unit tests should just have one assert per test method 
             * but in this case it is acceptable to have many asserts. 
             */
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-0"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-1"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-3"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-4"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-5"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-6"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-7"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-8"));
            Assert.IsFalse(spec.CheckCorrectCheckDigit("0737546-9"));
        }

        [TestMethod]
        public void CheckReasonsForDissatisfaction()
        {
            ISpecification<string> spec = new BusinessIdentifierSpecification();
            bool ok = spec.IsSatisfiedBy("123456-79");
            IEnumerable<string> reasons = spec.ReasonsForDissatisfaction;

            int reasonsCount = 0;
            foreach (var reason in reasons)
            {
                /*
                 * This gives additional information to the test output.
                 * Not mandatory to but sometimes it is helpful that you can check 
                 * the test output afterwards.
                 */
                Console.WriteLine(reason);
                reasonsCount++;
            }
            Assert.IsTrue(reasonsCount > 0);
        }

    }
}
