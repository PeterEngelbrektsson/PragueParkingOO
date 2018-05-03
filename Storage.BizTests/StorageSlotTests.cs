using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MyCompany.Storage.Biz;
using MyCompany.Storage.BizTests;
using MyCompany.Storage;

namespace Storage.BizTests
{
    [TestFixture]
    public class StorageSlotTests
    {

        StorageSlot<TestStorable> sut;
        TestStorable item1,item2,item3,item1B;

        [SetUp]
        public void RunBeforeEachTest()
        {
            int size = 8;
            int number = 1;
            sut = new StorageSlot<TestStorable>(number,size);
            item1 = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4
            };
            item1B = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4
        };
            item2 = new TestStorable()
            {
                RegistrationNumber = "ABC234",
                Size = 4
            };
            item3 = new TestStorable()
            {
                RegistrationNumber = "ABC345",
                Size = 4
            };
        }
            

        [Test]
        public void ShouldContainStorableRegistrationNumber()
        {
            // Arrange
            bool expected = true;
            bool actual;

            // Act
            sut.Add(item1);
            actual = sut.Contains(item1.RegistrationNumber);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldThrowStorageSlotToFullForStoreableException()
        {
            // Arrange

            // Act
            sut.Add(item1);
            sut.Add(item2);

            // Assert
            Assert.Throws<StorageSlotToFullForStoreableException>(() => sut.Add(item3)); // Should throw exception
        }

        [Test]
        public void ShouldThrowRegistrationNumberAlreadyExistsException()
        {
            // Arrange

            // Act
            sut.Add(item1);

            // Assert
            Assert.Throws<RegistrationNumberAlreadyExistsException>(() => sut.Add(item1B)); // Should throw exception
        }

    }
}
