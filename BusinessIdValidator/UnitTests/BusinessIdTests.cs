using BusinessIdValidator;
using BusinessIdValidator.Identifier;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BusinessIdTests
    {


        private IdentifierSpecificationFactory<BusinessId> factory = new IdentifierSpecificationFactory<BusinessId>();
        

        [TestMethod]
        public void HandlesTooShortBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("1234567-")));
        }

        [TestMethod]
        public void HandlesTooLongBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("12345678-89")));
        }

        [TestMethod]
        public void HasCorrectBusinessIdLength()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy(new BusinessId("0737546-2")));
        }

        [TestMethod]
        public void HandlesNullBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(null));
        }

        [TestMethod]
        public void HandlesNullBusinessIdsId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId(null)));
        }

        [TestMethod]
        public void HandlesEmptyBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("")));
        }

        [TestMethod]
        public void HandlesWhiteSpaceBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId(" ")));
        }

        [TestMethod]
        public void HasHyphenInCorrectPlace()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy(new BusinessId("0737546-2")));
        }

        [TestMethod]
        public void DoesNotHaveHyphenInCorrectPlace()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("123456-79")));
        }

        [TestMethod]
        public void HasNumericFirstPart()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy(new BusinessId("0737546-2")));
        }

        [TestMethod]
        public void HandlesNonNumericFirstPart()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0x37546-2")));
        }

        [TestMethod]
        public void HandlesNonNumericCheckDigit()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-x")));
        }

        [TestMethod]
        public void HasCorrectCheckDigit()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsTrue(spec.IsSatisfiedBy(new BusinessId("0737546-2")));
        }

        [TestMethod]
        public void HandlesIdWithoutCheckDigit()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("07375462")));
        }

        [TestMethod]
        public void HandlesShortBusinessId()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0-0")));
        }


        [TestMethod]
        public void DoesNotAllowModOne()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("1802465-1")));
        }

        [TestMethod]
        public void FindsFalseCheckDigits()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();

            /*
             * Normally unit tests should just have one assert per test method 
             * but in this case it is acceptable to have many asserts. 
             */
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-0")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-1")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-3")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-4")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-5")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-6")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-7")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-8")));
            Assert.IsFalse(spec.IsSatisfiedBy(new BusinessId("0737546-9")));
        }

        [TestMethod]
        public void CheckReasonsForDissatisfaction()
        {
            ISpecification<BusinessId> spec = factory.GetSpecification();
            bool ok = spec.IsSatisfiedBy(new BusinessId("123456-79"));
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

        [TestMethod]
        public void InspectCorrectCheckDigitHandlesNullValues()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification(new int[] { 7, 9, 10, 5, 8, 4, 2 });
            spec.InspectCorrectCheckDigit(null);
        }

        [TestMethod]
        public void InspectCorrectCheckDigitHandlesNullId()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification(new int[] { 7, 9, 10, 5, 8, 4, 2 });
            spec.InspectCorrectCheckDigit(new BusinessId(null));
        }

        [TestMethod]
        public void InspectCorrectCheckDigitHandlesEmptyId()
        {
            BusinessIdentifierSpecification spec = new BusinessIdentifierSpecification(new int[] { 7, 9, 10, 5, 8, 4, 2 });
            spec.InspectCorrectCheckDigit(new BusinessId(""));
        }

        [TestMethod]
        public void TryNotImplementedVatIdSpecExceptionHandling()
        {
            try
            {
                IdentifierSpecificationFactory<VatId> factory = new IdentifierSpecificationFactory<VatId>();
                ISpecification<VatId> spec = factory.GetSpecification();
            }
            catch (NotImplementedException ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void TestBusinessIdFormat()
        {
           BusinessId bi = new BusinessId("1234567-8");
           Assert.IsNotNull(bi.Format);
        }

        [TestMethod]
        public void TestBusinessIdSetter()
        {
            BusinessId bi = new BusinessId("1234567-8");
            bi.Id = "9876543-1";
            Assert.IsNotNull(bi.Id);
        }
    }
}
