﻿using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcAutomation.Infrastructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace MvcAutomation.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        IUserService userService = (IUserService)(new NinjectDependencyResolver().GetService(typeof(IUserService)));

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string username, string firstname, string lastname, string password, string email, int? courseId, int? groupId, int? specialityId, int? facultyId, int? roleId)
        {
            UserEntity user = new UserEntity() { Nickname = username, FirstName = firstname, LastName = lastname, Email = email, CourseId = courseId, GroupId = groupId, SpecialityId = specialityId, FacultyId = facultyId };
            MembershipUser memberUser = GetUser(username, false);

            if (memberUser == null)
            {
                user.Password = Crypto.HashPassword(password);
                user.RoleId = roleId;
                userService.CreateUser(user);
                memberUser = GetUser(username, false);
                return memberUser;
            }
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == username);
            if (user != null)
            {
                MembershipUser memberUser = new MembershipUser("MyMembershipProvider", user.Nickname, null, user.Email, null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                return memberUser;
            }
            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string nickname, int? facultyId, int? specialityId, int? courseId, int? groupId)
        {
            UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == nickname);
            user.FacultyId = facultyId;
            user.SpecialityId = specialityId;
            user.CourseId = courseId;
            user.GroupId = groupId;
            userService.UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            if (username != null && password != null)
            {

                UserEntity user = userService.GetAllUserEntities().FirstOrDefault(ent => ent.Nickname == username);

                if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                    isValid = true;
                else
                    isValid = false;
            }
            return isValid;
        }
    }
}