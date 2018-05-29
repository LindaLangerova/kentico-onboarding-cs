using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public interface IItemCreator
    {
        Item SetItem(Item sampleItem);
    }
}
