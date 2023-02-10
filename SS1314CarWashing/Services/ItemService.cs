using Microsoft.EntityFrameworkCore;
using SS1314CarWashing.Models;

namespace SS1314CarWashing.Services
{
    public class ItemService : IItemService,IDisposable
    {
        private readonly AppDbContext _context;

        public ItemService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var item =await _context.Item.FindAsync(Id);
                if (item == null)
                {
                    return false;
                }
                _context.Item.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Item> Get(Guid Id)
           => await _context.Item.FindAsync(Id);

        public async Task<List<Item>> GetAll()
            => await _context.Item.ToListAsync();

        public async Task<bool> Save(Item item)
        {
            try
            {
                item.ItemId= Guid.NewGuid();
                _context.Item.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }catch
            {
                return false;
            }
        }

        public Task<bool> Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
