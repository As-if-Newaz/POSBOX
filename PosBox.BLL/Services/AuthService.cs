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
                cfg.CreateMap<Business, BusinessDTO>();
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
            else if (user.UserStatus.Equals(UserStatus.Inactive))
            {

                errorMsg = "User is inactive. Please verify your email first.";
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
            else if (business.BusinessStatus.Equals(UserStatus.Inactive))
            {

                errorMsg = "Business is inactive. Please ask admin to activate business";
                return null;
            }
            return GetMapper().Map<BusinessDTO>(business);
        }
    }
}
