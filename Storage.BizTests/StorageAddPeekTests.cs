using System;
using NUnit.Framework;
using MyCompany.Storage.Biz;

namespace MyCompany.Storage.BizTests
{
    [TestFixture]
    public class StorageAddPeekTests
    {
     

        [Test]
        public void AddValidStorableAndPeekTest()
        {
            // Arrange
            int size = 10;
            Storage<TestStorable> sut = new Storage<TestStorable>(size);
            TestStorable item = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 1,
            };
            TestStorable acutal;
            int actualSlotNumber;
            // Act
            actualSlotNumber=sut.Add(item);
            acutal = sut.Peek(item.RegistrationNumber);

            // Assert
            Assert.That(acutal.RegistrationNumber.Equals(item.RegistrationNumber));
            Assert.That(acutal.Size.Equals(item.Size));
            Assert.That(acutal.TimeStamp,Is.EqualTo(DateTime.Now).Within(10).Seconds);
            Assert.That(acutal.Equals(item)); // Same ref

        }
        [Test]
        public void ShouldGiveStorageFullException()
        {
            // Arrange
            int size =1;
            Storage<TestStorable> sut = new Storage<TestStorable>(size);
            TestStorable item = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4,
            };
            TestStorable item2 = new TestStorable()
            {
                RegistrationNumber = "ABC432",
                Size = 4,
            };

            // Act
            sut.Add(item);
            
            // Assert
            Assert.Throws<StorageToFullForStoreableException>(()=>sut.Add(item2)); // Should throw exception
        }

        [Test]
        public void ShouldGiveRegistrationNumberAlreadyExistsException()
        {
            // Arrange
            int size = 1;
            Storage<TestStorable> sut = new Storage<TestStorable>(size);
            TestStorable item = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4,
            };
            TestStorable item2 = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4,
            };

            // Act
            sut.Add(item);

            // Assert
            Assert.Throws<RegistrationNumberAlreadyExistsException>(() => sut.Add(item2)); // Should throw exception
        }
    }
}
