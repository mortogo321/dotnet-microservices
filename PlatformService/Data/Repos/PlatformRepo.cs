using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Repos
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePlatformAsync(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            await _context.Platforms!.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _context.Platforms!.ToListAsync();
        }

        public async Task<Platform?> GetPlatformByIdAsync(int id)
        {
            return await _context.Platforms!.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}