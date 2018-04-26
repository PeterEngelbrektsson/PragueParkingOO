using System;
using NUnit.Framework;
using MyCompany.Storage.Biz;
using MyCompany.Storage.BizTests;

namespace Storage.BizTests
{
    [TestFixture]
    public class StorageOccupiedCountTests
    {
        // Arrange
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
        TestStorable item3 = new TestStorable()
        {
            RegistrationNumber = "ABC987",
            Size = 4,
        };
        Storage<TestStorable> sut;

        [SetUp]
        public void RunBeforeEachTest()
        {
            int size = 10;
            sut = new Storage<TestStorable>(size);
        }

        [Test]
        public void ShouldGetOneOccupiedCount()
        {
            // Arrange
            int acutal;
            // Act
            sut.Add(item);
            acutal = sut.OccupiedCount();

            // Assert
            Assert.That(acutal.Equals(1));

        }

        [Test]
        public void ShouldGetTwoOccupiedCount()
        {
            // Arrange
            int acutal;

            // Act
            sut.Add(item);
            sut.Add(item2);
            acutal = sut.OccupiedCount();

            // Assert
            Assert.That(acutal.Equals(2));

        }
        [Test]
        public void ShouldGetThreeOccupiedCount()
        {
            // Arrange
            int acutal;

            // Act
            sut.Add(item);
            sut.Add(item2);
            sut.Add(item3);
            acutal = sut.OccupiedCount();

            // Assert
            Assert.That(acutal.Equals(3));

        }
    }
}
