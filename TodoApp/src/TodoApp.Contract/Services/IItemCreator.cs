using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services
{
    public interface IItemCreator
    {
        bool SetItem(ref Item sampleItem);
    }
}
