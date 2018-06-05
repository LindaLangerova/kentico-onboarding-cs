using TodoApp.Contract.Models;

namespace TodoApp.Contract.Services
{
    public interface IItemCreator
    {
        Item SetItem(Item sampleItem);
    }
}
