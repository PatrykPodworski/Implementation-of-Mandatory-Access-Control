using bsk2v2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bsk2v2.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContextBase _httpContext;

        public UserService(ApplicationDbContext context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public ControlLevel GetCurrentUserCleranceLevel()
        {
            var userName = _httpContext.User.Identity.Name;
            return _context.Users
                .Include(x => x.UserInfo.CleranceLevel)
                .FirstOrDefault(x => x.UserName == userName)
                .UserInfo
                .CleranceLevel;
        }

        public ICollection<ControlLevel> GetAvailableClassificationLevels()
        {
            var clearanceLevel = GetCurrentUserCleranceLevel();
            var controlLevelService = new ControlLevelService(_context);
            return controlLevelService.GetWriteableFor(clearanceLevel);
        }

        public int GetCurrentUserId()
        {
            return GetCurrentUserInfo().Id;
        }

        private User GetCurrentUserInfo()
        {
            var userName = _httpContext.User.Identity.Name;
            var user = _context.Users
                .Include(x => x.UserInfo.CleranceLevel)
                .FirstOrDefault(x => x.UserName == userName);

            return user.UserInfo;
        }
    }
}