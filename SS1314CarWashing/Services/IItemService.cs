using SS1314CarWashing.Models;

namespace SS1314CarWashing.Services;

public interface IItemService
{
    public Task<List<Item>> GetAll();
    public Task<Item> Get(Guid Id);
    public Task<bool> Delete(Guid Id);
    public Task<bool> Update(Item item);
    public Task<bool> Save(Item item);
}
