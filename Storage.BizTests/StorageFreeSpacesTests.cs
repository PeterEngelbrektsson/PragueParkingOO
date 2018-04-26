using System;
using NUnit.Framework;
using MyCompany.Storage.Biz;
using MyCompany.Storage.BizTests;

namespace Storage.BizTests
{
    [TestFixture]
    public class StorageFreeSpacesTests
    {
        // Arrange
        TestStorable item;
        TestStorable item2;
        TestStorable item3;
        TestStorable item4;
        Storage<TestStorable> sut;

        [SetUp]
        public void RunBeforeEachTest()
        {
            int size = 10;
            sut = new Storage<TestStorable>(size);
             item = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4,
            };
            item2 = new TestStorable()
            {
                RegistrationNumber = "ABC432",
                Size = 4,
            };
            item3 = new TestStorable()
            {
                RegistrationNumber = "ABC987",
                Size = 4,
            };
            item4 = new TestStorable()
            {
                RegistrationNumber = "ABC444",
                Size = 4,
            };
        }

        [Test]
        public void ShouldGet8FreeSpaces()
        {
            // Arrange
            int acutal;
            // Act
            sut.Add(item);
            sut.Add(item2);
            acutal = sut.FindFreePlace(4);

            // Assert
            Assert.That(acutal.Equals(8));

        }

        [Test]
        public void ShouldGet7FreeSpaces()
        {
            // Arrange
            int acutal;
            // Act
            sut.Add(item);
            sut.Add(item2);
            sut.Add(item3);
            acutal = sut.FindFreePlace(4);

            // Assert
            Assert.That(acutal.Equals(7));

        }
        [Test]
        public void ShouldGet6FreeSpaces()
        {
            // Arrange
            int acutal;
            // Act
            sut.Add(item);
            sut.Add(item2);
            sut.Add(item3);
            sut.Add(item4);
            acutal = sut.FindFreePlace(4);

            // Assert
            Assert.That(acutal.Equals(7));

        }
    }
}
