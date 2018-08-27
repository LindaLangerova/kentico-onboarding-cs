using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Contract.Models;

namespace TodoApp.Contract.Tests.Utilities.Comparers
{
    public static class EqualConstraintsExtensions
    {
        public static EqualConstraint UsingItemModelComparer(this EqualConstraint constraint)
            => constraint.Using(ItemModelComparer.Instance.Value);

        private class ItemModelComparer : IEqualityComparer<Item>
        {
            private ItemModelComparer() { }

            public static Lazy<ItemModelComparer> Instance
                => new Lazy<ItemModelComparer>(() => new ItemModelComparer());

            public bool Equals(Item x, Item y)
                => x?.Id == y?.Id 
                && x?.Text == y?.Text 
                && x?.LastChange == y?.LastChange
                && x?.CreatedAt == y?.CreatedAt;

            public int GetHashCode(Item obj)
                => obj.Id.GetHashCode();
        }
    }
}
