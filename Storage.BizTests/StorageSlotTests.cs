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
                Size = 4,
                TypeName="CAR"
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
        [Test]
        public void ShouldReturnSameObject()
        {
            // Arrange
            TestStorable expected = item1;
            TestStorable actual;

            // Act
            sut.Add(item1);
            actual = sut.Peek(item1.RegistrationNumber);

            // Assert
            Assert.That(Is.ReferenceEquals(actual,expected));
        }
        [Test]
        public void ShouldBeOccupiedBy4()
        {
            // Arrange
            int expected = item1.Size;
            int actual;

            // Act
            sut.Add(item1);
            actual = sut.Occupied();

            // Assert
            Assert.That(actual,Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeOccupiedBy8()
        {
            // Arrange
            int expected = 8;
            int actual;

            // Act
            sut.Add(item1);
            sut.Add(item2);
            actual = sut.Occupied();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpace0()
        {
            // Arrange
            int expected = 0;
            int actual;

            // Act
            sut.Add(item1);
            sut.Add(item2);
            actual = sut.FreeSpace();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpace4()
        {
            // Arrange
            int expected = 4;
            int actual;

            // Act
            sut.Add(item1);
            actual = sut.FreeSpace();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpace8()
        {
            // Arrange
            int expected = 8;
            int actual;

            // Act
            actual = sut.FreeSpace();

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpaces2()
        {
            // Arrange
            int expected =2;
            int actual;
            int size = 4;

            // Act
            actual = sut.FreeSpaces(size);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpaces1()
        {
            // Arrange
            int expected = 1;
            int actual;
            int size = 4;

            // Act
            sut.Add(item1);
            actual = sut.FreeSpaces(size);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldBeFreeSpaces0()
        {
            // Arrange
            int expected = 0;
            int actual;
            int size = 4;

            // Act
            sut.Add(item1);
            sut.Add(item2);
            actual = sut.FreeSpaces(size);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ShouldGetEmptySlotDetails()
        {
            // Arrange
            StorageSlotDetail expected = new StorageSlotDetail()
            {
                FreeSpace = 8,
                OccupiedSpace = 0,
                SlotNumber = 1,
                Size = 8,
                StorageItemDetails = new List<StorageItemDetail>(),
            };
            StorageSlotDetail actual;


            // Act
            actual = sut.GetSlotDetails();

            // Assert
            Assert.That(actual.FreeSpace, Is.EqualTo(expected.FreeSpace));
            Assert.That(actual.OccupiedSpace, Is.EqualTo(expected.OccupiedSpace));
            Assert.That(actual.SlotNumber, Is.EqualTo(expected.SlotNumber));
            Assert.That(actual.Size, Is.EqualTo(expected.Size));
            Assert.That(actual.StorageItemDetails.Count, Is.EqualTo(expected.StorageItemDetails.Count));
        }
        [Test]
        public void ShouldGetStorageItemDetails()
        {
            // Arrange
            StorageItemDetail expected = new StorageItemDetail()
            {
                Size = 4,
                TimeStamp = DateTime.Now,
                RegistrationNumber = "ABC123",
                StorageSlotNumber = 1,
                TypeName = "CAR",
            };
            List<StorageItemDetail> actual;


            // Act
            sut.Add(item1);
            actual = sut.GetStorageItemDetailsReport();

            // Assert
            Assert.That(actual[0].Size, Is.EqualTo(expected.Size));
            Assert.That(actual[0].RegistrationNumber, Is.EqualTo(expected.RegistrationNumber));
            Assert.That(actual[0].StorageSlotNumber, Is.EqualTo(expected.StorageSlotNumber));
            Assert.That(actual[0].TypeName, Is.EqualTo(expected.TypeName));
        }
        [Test]
        public void ShouldGetEmptyStorageItemDetailsList()
        {
            // Arrange
            List<StorageItemDetail> actual;


            // Act
            actual = sut.GetStorageItemDetailsReport();

            // Assert
            Assert.That(actual.Count, Is.EqualTo(0));
        }
    }
}
