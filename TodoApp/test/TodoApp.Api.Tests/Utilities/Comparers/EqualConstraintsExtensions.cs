using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Contract.Models;

namespace TodoApp.Api.Tests.Utilities.Comparers
{
    internal static class EqualConstraintsExtensions
    {
        public static EqualConstraint UsingItemModelComparer(this EqualConstraint constraint)
            => constraint.Using(ItemModelComparer.Instance.Value);

        private class ItemModelComparer : IEqualityComparer<Item>
        {
            public static Lazy<ItemModelComparer> Instance => new Lazy<ItemModelComparer>(() => new ItemModelComparer());

            private ItemModelComparer() { }

            public bool Equals(Item x, Item y)
                => x?.Id == y?.Id && x?.Text == y?.Text;

            public int GetHashCode(Item obj)
                => obj.Id.GetHashCode();
        }
    }
}