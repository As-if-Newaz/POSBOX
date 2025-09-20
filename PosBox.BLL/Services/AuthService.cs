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

        public UserDTO? Authenticate(UserLoginDTO loginData, out string errorMsg)
        {
            errorMsg = string.Empty;
            var user = DA.UserData().Authenticate(loginData.Email, loginData.Password, out errorMsg);
            if (user == null)
            {
                errorMsg = "Login Failed!, User not found";
                return null;
            }
            if (user.UserStatus.Equals(UserStatus.Blocked))
            {
                errorMsg = "User is blocked!";
                DA.AuditData().RecordLog(user.Id, AuditActions.LoginAttempt, "Block user attempted login");
                return null;
            }
            DA.AuditData().RecordLog(user.Id, AuditActions.LoggedIn, "User logged in");
            return GetMapper().Map<UserDTO>(user);

        }
    }
}
