using TodoApp.Contract.Models;

namespace Todo.App.Services.ItemServices
{
    public interface IItemCreator
    {
        bool SetItem(ref Item sampleItem);
    }
}
