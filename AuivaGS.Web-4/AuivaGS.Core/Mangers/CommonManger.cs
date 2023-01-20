using AutoMapper;
using AuviaGS.DbModel.ModelView;
using AuviaGS.Common;
using AuviaGS.Core.Mangers.MagersInterfaces;
using AuivaGS.DbModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AuviaGS.Core.Mangers
{
    public class CommonManger : ICommonManger
    {
        private AuivaGSDbContext _auivaGSDbContext;
        private IMapper _mapper;

        public CommonManger(AuivaGSDbContext auivaGSDbContext, IMapper mapper)
        {
            _auivaGSDbContext = auivaGSDbContext;
            _mapper = mapper;
        }

        public UserModel GetUserRole(UserModel user)
        {
            var dbUser = _auivaGSDbContext.Users.AsNoTracking()
                                         .FirstOrDefault(a => a.Id == user.Id)
                                      ?? throw new ServiceValidationException("Invalid user id received");

            var mappedUser = _mapper.Map<UserModel>(dbUser);

            return mappedUser;
        }
    }
}
