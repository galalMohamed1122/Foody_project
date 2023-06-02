using AutoMapper;
using FoodyDAL.APIModels;
using FoodyDAL.Models;
using FoodyDl.FoodyService.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FoodyDl.FoodyService
{
    public interface IUser
    {
        public List<User> GetUsers();
        public User GetUserById(Guid id);
        public bool InsertUser(NewUserModel userModel);
        public bool UpdatetUser(UbdateUserModel user);
        public bool DeletetUser(Guid id);
        public User GetUserByUserName_Password(string Username, string Password);
        public LoginResponse LoginService(LoginRequest loginRequest);
    }

    public class UserService:IUser
    {
       
        readonly FoodyContext _context;
        readonly IMapper _mapper;
       
        public UserService(FoodyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Users
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(Guid id)
        {
            if (id != Guid.Empty)
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(m => m.UserId == id);
                    if (user != null)
                    {
                        return user;
                    }
                    throw new Exception("User not found!!");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }
        public bool InsertUser(NewUserModel userModel)
        {
            if (userModel != null)
            {
                try
                {
                    var branchId = Guid.NewGuid();
                    var organizationId = Guid.NewGuid();

                    var organization = new Orginization
                    {
                        OrganizationId = organizationId,
                        Org_name = userModel.OrgName
                    };

                    var branch = new Branch
                    {
                        BranchId = branchId,
                        OrganizationId = organizationId,
                        Location = userModel.Location
                    };

                    User _User = new()
                    {
                        EMail = userModel.EMail,
                        FullName = userModel.FullName,
                        Password = userModel.Password,
                        ConfirmPassword = userModel.ConfirmPassword,
                        Phone = userModel.Phone,
                        UserType = userModel.UserType,
                        BranchId = branchId
                    };
                    _context.Orginization.Add(organization);
                    _context.Branch.Add(branch);
                    _context.Users.Add(_User);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                   
                }
            }
            return false;
        }
        public bool UpdatetUser(UbdateUserModel user)
        {
            if (user != null && user.IdUser != Guid.Empty)
            {
                try
                {
                    User currentUser = _context.Users.FirstOrDefault(s => s.UserId == user.IdUser);
                    Branch currentBranch = _context.Branch.FirstOrDefault(b => b.BranchId == currentUser.BranchId);

                    if (currentUser != null)
                    {
                        currentUser.EMail = user.EMail;
                        currentUser.FullName = user.FullName;
                        currentUser.Phone = user.Phone;
                        currentUser.Password = user.Password;
                        currentUser.ConfirmPassword = user.ConfirmPassword;
                        currentBranch.Location = user.Location;
                       
                        _context.Update(currentUser);
                        _context.Update(currentBranch);
                       int rowEffect = _context.SaveChanges();
                        if (rowEffect != 0)
                            return true;
                        else 
                            return false;
                        
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
        public bool DeletetUser(Guid id)
        {
            if (id != Guid.Empty)
            {
                try
                {
                    var currentUser = _context.Users.FirstOrDefault(s => s.UserId == id);
                    var currentBranch = _context.Branch.FirstOrDefault(s => s.BranchId == currentUser.BranchId);
                    var currentOrgnaization = _context.Orginization.FirstOrDefault(s => s.OrganizationId == currentBranch.OrganizationId);
                    
                    if (currentUser != null)
                    {
                        _context.Users.Remove(currentUser);
                        _context.Branch.Remove(currentBranch);
                        _context.Orginization.Remove(currentOrgnaization);
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
        public User GetUserByUserName_Password(string Username, string Password)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(m => m.EMail == Username && m.Password == Password);

                    return user;

                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new Exception("User object can't be null");
        }

    //Login
    public LoginResponse LoginService(LoginRequest loginRequest)
        {
            try
            {
                LoginResponse response = new ()
                {
                    ValidUser = false,
                    UserInfo = null
                };

                User _User = GetUserByUserName_Password(loginRequest.UserEmail, loginRequest.UserPassword);
                if (_User != null)
                {
                    response.ValidUser = true;
                    response.UserInfo = _mapper.Map<UserDTO>(_User);
                }
                return response;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
