using bsk2v2.Models;
using bsk2v2.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IdentityResult> Add(UserCreateViewModel model)
        {
            var controlLevelService = new ControlLevelService(_context);
            var cleranceLevelId = controlLevelService.GetIdByLevel(model.CleranceLevel);
            var userInfo = new User { Name = model.Username, Email = model.Email, CleranceLevelId = cleranceLevelId };
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, UserInfo = userInfo };

            var userManager = _httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            return await userManager.CreateAsync(user, model.Password);
        }

        internal ICollection<ApplicationUser> GetAvailableUsers()
        {
            var cleranceLevel = GetCurrentUserCleranceLevel();

            var controlLevelService = new ControlLevelService(_context);
            var controlLevelsIds = controlLevelService.GetReadableFor(cleranceLevel).Select(x => x.Id);

            var users = _context.Users
                .Include(x => x.UserInfo.CleranceLevel)
                .Where(x => controlLevelsIds.Contains(x.UserInfo.CleranceLevel.Id))
                .ToList();

            return users;
        }
    }
}