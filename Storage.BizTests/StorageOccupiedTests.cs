using System;
using NUnit.Framework;
using MyCompany.Storage.Biz;
using MyCompany.Storage.BizTests;
using System.Collections.Generic;

namespace Storage.BizTests
{
    [TestFixture]
    public class StorageOccupiedTests
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
        public void ShouldGet1DetailsReport()
        {
            // Arrange
            List<StorageSlotDetail> acutal;
            StorageItemDetail expectedDetail = new StorageItemDetail();
            expectedDetail.RegistrationNumber = item.RegistrationNumber;
            expectedDetail.Size = item.Size;
            List<StorageItemDetail> expectedDetailList = new List<StorageItemDetail>();
            expectedDetailList.Add(expectedDetail);
            StorageSlotDetail expected = new StorageSlotDetail();
            expected.Size = 4;
            expected.SlotNumber = 0;    // Expected to be stored in first free position
            expected.OccupiedSpace = 4;
            expected.FreeSpace = 0;
            expected.StorageItemDetails = expectedDetailList;

            // Act
            sut.Add(item);
            acutal = sut.Occupied();

            // Assert
            Assert.That(acutal.Count.Equals(1));
            Assert.That(acutal[0].Size.Equals(expected.Size));
            Assert.That(acutal[0].SlotNumber.Equals(expected.SlotNumber));
            Assert.That(acutal[0].OccupiedSpace.Equals(expected.OccupiedSpace));
            Assert.That(acutal[0].FreeSpace.Equals(expected.FreeSpace));
            Assert.That(acutal[0].StorageItemDetails.Count.Equals(1));
            Assert.That(acutal[0].StorageItemDetails[0].RegistrationNumber.Equals(expected.StorageItemDetails[0].RegistrationNumber));
            Assert.That(acutal[0].StorageItemDetails[0].Size.Equals(expected.StorageItemDetails[0].Size));
            Assert.That(acutal[0].StorageItemDetails[0].TimeStamp,Is.EqualTo(DateTime.Now).Within(10).Seconds);
        }
        [Test]
        public void ShouldGet2DetailsReports()
        {
            // Arrange
            List<StorageSlotDetail> acutal;
            StorageItemDetail expectedDetail = new StorageItemDetail();
            expectedDetail.RegistrationNumber = item.RegistrationNumber;
            expectedDetail.Size = item.Size;
            List<StorageItemDetail> expectedDetailList = new List<StorageItemDetail>();
            expectedDetailList.Add(expectedDetail);
            StorageSlotDetail expected = new StorageSlotDetail();
            expected.Size = 4;
            expected.SlotNumber = 0;    // Expected to be stored in first free position
            expected.OccupiedSpace = 4;
            expected.FreeSpace = 0;
            expected.StorageItemDetails = expectedDetailList;

            StorageItemDetail expectedDetail2 = new StorageItemDetail();
            expectedDetail2.RegistrationNumber = item2.RegistrationNumber;
            expectedDetail2.Size = item2.Size;
            List<StorageItemDetail> expectedDetailList2 = new List<StorageItemDetail>();
            expectedDetailList2.Add(expectedDetail2);
            StorageSlotDetail expected2 = new StorageSlotDetail();
            expected2.Size = 4;
            expected2.SlotNumber = 1;    // Expected to be stored in first free position
            expected2.OccupiedSpace = 4;
            expected2.FreeSpace = 0;
            expected2.StorageItemDetails = expectedDetailList2;

            // Act
            sut.Add(item);
            sut.Add(item2);
            acutal = sut.Occupied();

            // Assert
            Assert.That(acutal.Count.Equals(2));
            Assert.That(acutal[0].Size.Equals(expected.Size));
            Assert.That(acutal[0].SlotNumber.Equals(expected.SlotNumber));
            Assert.That(acutal[0].OccupiedSpace.Equals(expected.OccupiedSpace));
            Assert.That(acutal[0].FreeSpace.Equals(expected.FreeSpace));
            Assert.That(acutal[0].StorageItemDetails.Count.Equals(1));
            Assert.That(acutal[0].StorageItemDetails[0].RegistrationNumber.Equals(expected.StorageItemDetails[0].RegistrationNumber));
            Assert.That(acutal[0].StorageItemDetails[0].Size.Equals(expected.StorageItemDetails[0].Size));
            Assert.That(acutal[0].StorageItemDetails[0].TimeStamp, Is.EqualTo(DateTime.Now).Within(10).Seconds);

            Assert.That(acutal[1].Size.Equals(expected2.Size));
            Assert.That(acutal[1].SlotNumber.Equals(expected2.SlotNumber));
            Assert.That(acutal[1].OccupiedSpace.Equals(expected2.OccupiedSpace));
            Assert.That(acutal[1].FreeSpace.Equals(expected2.FreeSpace));
            Assert.That(acutal[1].StorageItemDetails.Count.Equals(1));
            Assert.That(acutal[1].StorageItemDetails[0].RegistrationNumber.Equals(expected2.StorageItemDetails[0].RegistrationNumber));
            Assert.That(acutal[1].StorageItemDetails[0].Size.Equals(expected2.StorageItemDetails[0].Size));
            Assert.That(acutal[1].StorageItemDetails[0].TimeStamp, Is.EqualTo(DateTime.Now).Within(10).Seconds);
        }
    }
}
