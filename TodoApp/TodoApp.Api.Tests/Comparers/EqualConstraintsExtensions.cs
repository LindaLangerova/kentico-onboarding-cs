using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Api.Models;

namespace TodoApp.Api.Tests.Comparers
{
    internal static class EqualConstraintsExtensions
    {
        public static EqualConstraint UsingItemModelComparer(this EqualConstraint constraint)
            => constraint.Using(ItemModelComparer.Instance.Value);

        private class ItemModelComparer : IEqualityComparer<ItemModel>
        {
            public static Lazy<ItemModelComparer> Instance => new Lazy<ItemModelComparer>(() => new ItemModelComparer());

            private ItemModelComparer() { }

            public bool Equals(ItemModel x, ItemModel y)
                => x?.Id == y?.Id && x?.Text == y?.Text;

            public int GetHashCode(ItemModel obj)
                => obj.Id.GetHashCode();
        }
    }
}