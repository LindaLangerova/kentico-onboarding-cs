using TodoApp.Contract.Models;

namespace TodoApp.Services.ItemServices
{
    public interface IItemCreator
    {
        bool SetItem(ref Item sampleItem);
    }
}
