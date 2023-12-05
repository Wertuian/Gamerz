﻿using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.Business.Types;
using Gamerz.Data.Entities;
using Gamerz.Data.Enums;
using Gamerz.Data.Repository;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtector _dataProtector;
        public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }
        public ServiceMessage AddUser(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == addUserDto.Email.ToLower()).ToList();

            if (hasMail.Any()) 
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu Eposta adresli bir kullanıcı zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                Name = addUserDto.Name,
                LastName = addUserDto.LastName,
                Email = addUserDto.Email,
                Password = _dataProtector.Protect(addUserDto.Password),
                UserType = UserTypeEnum.User
                
            };

            _userRepository.Add(userEntity);

            return new ServiceMessage()
            {
                IsSucceed = true
            };
            
        }

        public UserInfoDto LoginUser(LoginUserDto loginUserDto)
        {
            var userEntity = _userRepository.Get(x => x.Email == loginUserDto.Email);

            if (userEntity is null)
            {
                return null;
                
            }

            var rawPassword = _dataProtector.Unprotect(userEntity.Password);

            if (loginUserDto.Password == rawPassword)
            {
                return new UserInfoDto()
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name,
                    LastName = userEntity.LastName,
                    UserType = userEntity.UserType,
                    Email = userEntity.Email
                };
            }
            else
            {
                return null;
            }
        }
    }
}
