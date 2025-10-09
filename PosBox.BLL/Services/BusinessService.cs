using AutoMapper;
using PosBox.BLL.DTOs;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.Services
{
    public class BusinessService
    {
        private readonly DataAccess DA;

        public BusinessService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Business, BusinessDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool RegisterBusiness(BusinessDTO obj, out string errorMsg)
        {
            errorMsg = string.Empty;
            obj.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            var data = GetMapper().Map<Business>(obj);
            return DA.BusinessData().Create(data, out errorMsg);
        }

        public BusinessDTO? GetBusinessByUserName(string uName)
        {
            var business = DA.BusinessData().GetByBusinessByUserName(uName);
            if (business == null)
            {
                return null;
            }
            return GetMapper().Map<BusinessDTO>(business);
        }

        public bool BlockBusinesses(List<int> Ids)
        {
            foreach (var Id in Ids)
            {
                if (!UpdateBusinessStatus(Id, UserStatus.Blocked))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UnblockBusinesses(List<int> Ids)
        {
            foreach (var Id in Ids)
            {
                if (!UpdateBusinessStatus(Id, UserStatus.Active))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ActivateBusinesses(List<int> Ids)
        {
            foreach (var Id in Ids)
            {
                if (!UpdateBusinessStatus(Id, UserStatus.Active))
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeActivateBusinesses(List<int> Ids)
        {
            foreach (var Id in Ids)
            {
                if (!UpdateBusinessStatus(Id, UserStatus.Inactive))
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteBusinesseses(List<int> Ids)
        {
            foreach (var userId in Ids)
            {
                var business = DA.BusinessData().Get(userId);
                if (business == null)
                {
                    return false;
                }
                if (!DA.UserData().Delete(business.Id))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateBusinessStatus(int Id, UserStatus status)
        {
            return DA.BusinessData().UpdateBusinessStatus(Id, status);
        }


        public bool UpdatePreferences(int Id, UserPreferencesDTO preferences)
        {
            return DA.BusinessData().UpdatePreferences(Id, preferences.PreferredLanguage, preferences.PreferredTheme);
        }
        public List<BusinessDTO> SearchBusiness(string searchTerm)
        {
            var businesses = DA.BusinessData().SearchBusiness(searchTerm);
            return GetMapper().Map<List<BusinessDTO>>(businesses);
        }

        public IEnumerable<BusinessDTO> GetAll(int userId)
        {
            var data = DA.BusinessData().GetAll();
            var businessList = new List<BusinessDTO>();
            foreach (var item in data)
            {
                if(DA.UserData().UserBusinessAccess(userId, item.Id))
                {
                    businessList.Add(GetMapper().Map<BusinessDTO>(item));
                }
            }
            return businessList;
        }

        public BusinessDTO? GetById(int Id)
        {
            var data = DA.BusinessData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<BusinessDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.BusinessData().Get(Id);
            if (ext == null) return false;
            DA.BusinessData().Delete(ext.Id);
            return true;
        }

        public bool Update(BusinessDTO obj)
        {
            var data = GetMapper().Map<Business>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.BusinessData().Update(data);
        }
    }


}
