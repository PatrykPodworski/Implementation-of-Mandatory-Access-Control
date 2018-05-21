using bsk2v2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace bsk2v2.ViewModels
{
    public class UserCreateViewModel
    {
        public ICollection<SelectListItem> CleranceLevels { get; set; }
        public int CleranceLevel { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserCreateViewModel()
        {
        }

        public UserCreateViewModel(ICollection<ControlLevel> controlLevels)
        {
            CleranceLevels = controlLevels
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Level.ToString()
                })
                .ToList();
        }
    }

    public class UserListViewModel
    {
        public ICollection<UserListPartViewModel> Users { get; set; }

        public UserListViewModel(ICollection<ApplicationUser> users)
        {
            Users = users
                .Select(x => new UserListPartViewModel(x))
                .ToList();
        }
    }

    public class UserListPartViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string CleranceLevel { get; set; }

        public UserListPartViewModel()
        {
        }

        public UserListPartViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Username = user.UserInfo.Name;
            CleranceLevel = user.UserInfo.CleranceLevel.Name;
        }
    }

    public class UserDeleteViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CleranceLevelName { get; set; }

        [Required]
        public int CleranceLevel { get; set; }

        public UserDeleteViewModel()
        {
        }

        public UserDeleteViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.UserInfo.Name;
            CleranceLevelName = user.UserInfo.CleranceLevel.Name;
            CleranceLevel = user.UserInfo.CleranceLevel.Level;
        }
    }
}