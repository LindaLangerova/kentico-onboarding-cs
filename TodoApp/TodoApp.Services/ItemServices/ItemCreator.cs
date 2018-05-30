using System;
using TodoApp.Services.IdServices;
using TodoApp.Contract.Models;

namespace TodoApp.Services.ItemServices
{
    public class ItemCreator : IItemCreator
    {
        private readonly IIdGenerator _idGenerator;

        public ItemCreator(IIdGenerator idGenerator)
            => _idGenerator = idGenerator;

        public bool SetItem(ref Item item)
        {
            if (!item.IsValidForCreating())
                return false;

            item.Id = _idGenerator.GenerateId();
            item.Text = item.Text;
            item.CreatedAt = DateTime.Now;
            item.LastChange = DateTime.Now;

            return true;
        }
    }
}
