using BPMPlus.Data;
using BPMPlus.Models;
using BPMPlus.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace BPMPlus.Service
{
    
    public class userDetailData(ApplicationDbContext context)
    {
        readonly ApplicationDbContext _context = context;
        public  List<bKUsersViewModel> userData()
        {
           var userslist =  _context.User.AsNoTracking()
        .Include(u => u.Department)
        .Include(u => u.Grade)
        .Select(c => new bKUsersViewModel
        {
            UserId = c.UserId,
            UserName = c.UserName,
            DepartmentName = c.Department.DepartmentName,
            GradeName = c.Grade.GradeName,
            Email = c.Email,
            IncertDataTime = c.CreatedTime.AddHours(8).ToString("yyyy-MM-dd"),
            UserIsActive = c.UserIsActive,
        }
        ).ToList();
            return userslist;
        }
        public User userDetail(string id)
        {
            var user =  _context.User
                .Include(u => u.Department)
                .Include(u => u.Grade).AsNoTracking()
                .FirstOrDefault(m => m.UserId == id);
            return user;
        }
    }
}
