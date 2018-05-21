using bsk2v2.Models;
using System.Collections.Generic;
using System.Linq;

namespace bsk2v2.Services
{
    public class ControlLevelService
    {
        private ApplicationDbContext _context;

        public ControlLevelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<ControlLevel> GetWriteableFor(ControlLevel controlLevel)
        {
            return _context.ControlLevels
                .Where(x => x.Level >= controlLevel.Level)
                .ToList();
        }

        public int GetIdByLevel(int level)
        {
            return _context.ControlLevels
                .FirstOrDefault(x => x.Level == level)
                .Id;
        }

        internal ICollection<ControlLevel> GetReadableFor(ControlLevel cleranceLevel)
        {
            return _context.ControlLevels
                .Where(x => x.Level <= cleranceLevel.Level)
                .ToList();
        }
    }
}