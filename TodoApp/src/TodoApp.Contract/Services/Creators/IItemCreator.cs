using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services.Creators
{
    public interface IItemCreator
    {
        Item SetItem(Item sampleItem);
    }
}
