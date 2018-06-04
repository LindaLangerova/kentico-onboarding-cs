using System;
using NUnit.Framework;
using TodoApp.Contract.Models;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Validators;

namespace TodoApp.Services.Test.ItemServices
{
    internal class ItemValidatorTests : TestBase
    {
        private readonly Item _validItem1 = new Item {Text = "Make a coffee"};

        private readonly Item _validaItem2 =
            new Item {Id = Guid.Parse("c5cc89a0-ab8d-4328-9000-3da679ec02d3"), Text = "Make two coffees"};

        private readonly Item _notValidItem = new Item {Text = ""};

        [Test]
        public void IsValidForUpdating_ItemIsValid_TrueReturned()
        {
            var result = _validItem1.IsValidForUpdating();
            var result2 = _validaItem2.IsValidForUpdating();

            Assert.That(result, Is.EqualTo(true)).AndThat(result2, Is.EqualTo(true));
        }

        [Test]
        public void IsValidForUpdating_ItemIsNotValid_FalseReturned()
        {
            var result = _notValidItem.IsValidForUpdating();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValidForCreting_ItemIsValid_TrueReturned()
        {
            var result = _validItem1.IsValidForCreating();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValidForCreting_ItemIsNotValid_FalseReturned()
        {
            var result = _notValidItem.IsValidForUpdating();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValidForCreting_ItemIsValidOnlyForUpdating_FalseReturned()
        {
            var result = _validaItem2.IsValidForCreating();

            Assert.That(result, Is.EqualTo(false));
        }
    }
}
