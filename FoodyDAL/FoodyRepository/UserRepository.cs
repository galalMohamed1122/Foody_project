using FoodyDAL.APIModels;
using FoodyDAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FoodyDAL.FoodyRepository
{
    public class UserRepository
    {
        private readonly FoodyContext _context;
        public UserRepository(FoodyContext context)
        {
            _context = context;
        }
        // Users
        public List<ViewTotalUser> GetUsers()
        {
            return _context.ViewTotalUsers.ToList();
        }
        public ViewTotalUser DetailsUsers(int id)
        {
            if (id > 0)
            {
                try
                {
                    var user = _context.ViewTotalUsers.FirstOrDefault(m => m.IdUsers == id);
                    if (user != null)
                    {
                        return user;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }
        public UserOrgDetailedModel InsertUser(UserOrgDetailedModel userModel)
        {
            if (userModel != null)
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    try
                    {
                        User _User = new User()
                        {
                            ConfirmPassword = userModel.ConfirmPassword,
                            EMail = userModel.EMail,
                            FullName = userModel.FullName,
                            Password = userModel.Password,
                            Phone = userModel.Phone,
                            UserType = userModel.UserType
                        };
                        _context.Users.Add(_User);
                        int roweffected = _context.SaveChanges();
                        if (roweffected > 0)
                        {
                            List<Orginization> Orgs = new List<Orginization>();
                            foreach (var item in userModel.OrgLocation)
                            {
                                Orginization _Org = new Orginization()
                                {
                                    FkUserId = _User.IdUsers,
                                    OrgLocation = item,
                                    OrgName = userModel.OrgName,
                                    OrgType = userModel.OrgType,
                                };
                                Orgs.Add(_Org);
                            }

                            _context.Orginizations.AddRange(Orgs);
                            roweffected = _context.SaveChanges();
                            userModel.IdUsers = _User.IdUsers;
                        }

                        Scope.Complete();
                        Scope.Dispose();
                        return userModel;
                    }
                    catch (Exception)
                    {

                        Scope.Dispose();
                        return null;
                    }
                }
            }
            return null;
        }
        public bool UpdatetUser(UserOrgDetailedModel user)
        {
            if (user != null && user.FkUserId != 0)
            {
                try
                {
                    User currentUser = _context.Users.FirstOrDefault(s => s.IdUsers == user.FkUserId);
                    List<Orginization> currentORGs = _context.Orginizations.Where(s => s.FkUserId == user.FkUserId).ToList();
                    if (currentUser != null)
                    {
                        currentUser.EMail = user.EMail;
                        currentUser.FullName = user.FullName;
                        currentUser.Phone = user.Phone;
                        currentUser.UserType = user.UserType;
                        currentUser.Password = user.Password;
                        currentUser.ConfirmPassword = user.ConfirmPassword;
                        _context.Update(currentUser);
                        _context.SaveChanges();

                        List<Orginization> UpdatedOrgs = new List<Orginization>();
                        foreach (var item in currentORGs)
                        {
                            item.OrgName = user.OrgName;
                            item.OrgType = user.OrgType;
                            UpdatedOrgs.Add(item);
                        }
                        _context.UpdateRange(UpdatedOrgs);
                        _context.SaveChanges();
                        return true;
                    }
                    throw new Exception("User can't be found");
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }
        public bool DeletetUser(int id)
        {
            if (id > 0)
            {
                try
                {
                    var currentUser = _context.Users.FirstOrDefault(s => s.IdUsers == id);
                    if (currentUser != null)
                    {
                        _context.Users.Remove(currentUser);
                        int roweffected = _context.SaveChanges();

                        if (roweffected > 0)
                            return true;
                        else
                            return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }
        // Login
        public ViewUser GetUserByUserName_Password(string Username, string Password)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    var user = _context.ViewUsers.FirstOrDefault(m => m.EMail == Username && m.Password == Password);

                    return user;

                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }
    }
}
