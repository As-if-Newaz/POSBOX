using AutoMapper;
using PosBox.BLL.DTOs;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.Services
{
    public class AuthService
    {
        private readonly DataAccess DA;

        public AuthService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            return new Mapper(config);
        }

        public UserDTO? AuthenticateUser(LoginDTO loginData, out string errorMsg)
        {
            errorMsg = string.Empty;
            var user = DA.UserData().Authenticate(loginData.Identification, loginData.Password, out errorMsg);
            if (user == null)
            {
                errorMsg = "Login Failed!, User not found";
                return null;
            }
            if (user.UserStatus.Equals(UserStatus.Blocked))
            {
                errorMsg = "User is blocked!";
                DA.AuditData().RecordLog(user.Id, user.UserRole, AuditActions.LoginAttempt, "Block user attempted login");
                return null;
            }
            return GetMapper().Map<UserDTO>(user);

        }

        public BusinessDTO? AuthenticateBusiness(LoginDTO loginData, out string errorMsg)
        {
            errorMsg = string.Empty;
            var business = DA.BusinessData().Authenticate(loginData.Identification, loginData.Password, out errorMsg);
            if (business == null)
            {
                errorMsg = "Login Failed!, Business not found";
                return null;
            }
            if(business.BusinessStatus.Equals(UserStatus.Blocked))
            {
                errorMsg = "Business is blocked!";
                DA.AuditData().RecordLog(business.Id, UserRole.Business, AuditActions.LoginAttempt, "Blocked business attempted login");
                return null;
            }
            return GetMapper().Map<BusinessDTO>(business);
        }
    }
}
