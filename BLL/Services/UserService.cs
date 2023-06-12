﻿using SocialNetwork.BLL.Exeeptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaceses;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        IUserRepository userRepository;
        public UserService() 
        {
            userRepository = new UserRepository();
        }

        public void Register(UserRegistrationData userRegistrationData)
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName))
                throw new ArgumentNullException("Имя не может быть пустым!");
            if (String.IsNullOrEmpty(userRegistrationData.LastName))
                throw new ArgumentNullException("Фамилия не может быть пустой!");
            if (String.IsNullOrEmpty(userRegistrationData.Password)) 
                throw new ArgumentNullException("Пароль не может быть пустым!");
            if (String.IsNullOrEmpty(userRegistrationData.Email))
                throw new ArgumentNullException("Емаил не может быть пустым");

            if (userRegistrationData.Password.Length < 8)
                throw new ArgumentNullException("Длинна пароля должна быть больше 8 символов");

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentNullException("Некорректный формат почты!");

            if (userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new ArgumentNullException("Пользователь уже зарегистрирован");

            var userEntity = new UserEntity()
            {
                firstName = userRegistrationData.FirstName,
                lastName = userRegistrationData.LastName,
                password = userRegistrationData.Password,
                email = userRegistrationData.Email,
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new Exception();
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) throw new UserNotFoundException();

            if (findUserEntity.password != userAuthenticationData.Password) throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatableUserEntity = new UserEntity()
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                password = user.Password,
                email = user.Email,
                photo = user.Photo,
                favorite_book = user.FavoriteBook,
                favorite_movie = user.FavoriteMovie
            };

            if (this.userRepository.Update(updatableUserEntity) == 0) throw new Exception();
        }
        private User ConstructUserModel(UserEntity userEntity)
        {
            return new User(userEntity.id,
                          userEntity.firstName,
                          userEntity.lastName,
                          userEntity.password,
                          userEntity.email,
                          userEntity.photo,
                          userEntity.favorite_movie,
                          userEntity.favorite_book);
        }
    }
}
