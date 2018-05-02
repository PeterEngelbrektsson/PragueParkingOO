﻿using System;
using NUnit.Framework;
using MyCompany.Storage.Biz;
using MyCompany.Storage.BizTests;
using System.Collections.Generic;

namespace Storage.BizTests
{
    [TestFixture]
    public class StorageOptimizerOptimizeInstructionsTests
    {
        // Arrange
        TestStorable item;
        TestStorable item2;
        TestStorable item3;
        TestStorable item4;
        TestStorable item5;
        TestStorable item6;
        TestStorable item7;
        TestStorable item8;
        TestStorable item9;
        TestStorable item10;
        Storage<TestStorable> storage;
        StorageOptimizer<TestStorable> sut;

        [SetUp]
        public void RunBeforeEachTest()
        {
            int size = 10;
            storage = new Storage<TestStorable>(size);
            sut = new StorageOptimizer<TestStorable>();
            item = new TestStorable()
            {
                RegistrationNumber = "ABC123",
                Size = 4,
                TypeName = "CAR",
            };
            item2 = new TestStorable()
            {
                RegistrationNumber = "ABC432",
                Size = 4,
                TypeName = "CAR",
            };
            item3 = new TestStorable()
            {
                RegistrationNumber = "ABC987",
                Size = 4,
                TypeName = "CAR",
            };
            item4 = new TestStorable()
            {
                RegistrationNumber = "ABC444",
                Size = 4,
                TypeName = "CAR",
            };
            item5 = new TestStorable()
            {
                RegistrationNumber = "BIKE1",
                Size = 1,
                TypeName = "BIKE",
            };
            item6 = new TestStorable()
            {
                RegistrationNumber = "BIKE2",
                Size = 1,
                TypeName = "BIKE",
            };
            item7 = new TestStorable()
            {
                RegistrationNumber = "BIKE3",
                Size = 1,
                TypeName = "BIKE",
            };
            item8 = new TestStorable()
            {
                RegistrationNumber = "BIKE4",
                Size = 1,
                TypeName = "BIKE",
            };
            item9 = new TestStorable()
            {
                RegistrationNumber = "BIKE5",
                Size = 1,
                TypeName = "BIKE",
            };
            item10 = new TestStorable()
            {
                RegistrationNumber = "BIKE6",
                Size = 1,
                TypeName = "BIKE",
            };
        }


        [Test]
        public void ShouldGetOneBikeFromLastMovementReport()
        {
            // Arrange
            storage.Add(item);      // slot 0

            storage.Add(item2);     // slot 1

            storage.Add(item5);     // slot 2
            storage.Add(item6);
            storage.Add(item7);
            storage.Add(item8);

            storage.Add(item9);     // slot 3
            storage.Add(item10);
            storage.Add(item3);

            storage.Move(item5.RegistrationNumber, 9);

            OptimizeMovementDetail expected = new OptimizeMovementDetail();
            expected.RegistrationNumber = item5.RegistrationNumber;
            expected.TypeName = item5.TypeName;
            expected.OldStorageSlotNumber = 9;
            expected.NewStorageSlotNumber = 2;

            // Act
            var actual = sut.GetOptimzeInstructions(storage);

            // Assert
            Assert.That(actual.Count, Is.EqualTo(1));
            Assert.That(actual[0].OldStorageSlotNumber, Is.EqualTo(expected.OldStorageSlotNumber));
            Assert.That(actual[0].NewStorageSlotNumber, Is.EqualTo(expected.NewStorageSlotNumber));
            Assert.That(actual[0].RegistrationNumber, Is.EqualTo(expected.RegistrationNumber));
            Assert.That(actual[0].TypeName, Is.EqualTo(expected.TypeName));
        }
   
    }
}
