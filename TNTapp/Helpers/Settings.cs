using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TNTapp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// Checking
    /// </summary> 
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string AccessToken = "AccessToken";
        private static readonly string AccessTokenDefault = string.Empty;
        public static string GeneralAccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessToken, AccessTokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccessToken, value);
            }
        }

        private const string GetContactListJason = "GetContactListJason";
        private static readonly string GetContactListJasonDefault = string.Empty;
        public static string GeneralGetContactListJason
        {
            get
            {
                return AppSettings.GetValueOrDefault(GetContactListJason, GetContactListJasonDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GetContactListJason, value);
            }
        }

        private const string GetAllSyncContactListJason = "GetAllSyncContactListJason";
        private static readonly string GetAllSyncContactListJasonDefault = string.Empty;
        public static string GeneralGetAllSyncContactListJason
        {
            get
            {
                return AppSettings.GetValueOrDefault(GetAllSyncContactListJason, GetAllSyncContactListJasonDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(GetAllSyncContactListJason, value);
            }
        }

        private const string UserID = "UserID";
        private static readonly string UserIDDefault = string.Empty;

        private const string UserName = "UserName";
        private static readonly string UserNameDefault = string.Empty;

        private const string UserFullName = "UserFullName";
        private static readonly string UserFullNameDefault = string.Empty;

        private const string UserEmail = "UserEmail";
        private static readonly string UserEmailDefault = string.Empty;

        private const string UserProfilePic = "UserProfilePic";
        private static readonly string UserProfilePicDefault = string.Empty;

        private const string LoginPassword = "password";
        private static readonly string LoginPasswordDefault = string.Empty;

        private const string StaffId = "StaffId";
        private static readonly string StaffIdDefault = string.Empty;

        private const string CustomerId = "CustomerId";
        private static readonly string CustomerIdDefault = string.Empty;

        public static string GeneralUserID
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserID, UserIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserID, value);
            }
        }

        public static string GeneralUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserName, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserName, value);
            }
        }

        public static string GeneralUserFullName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserFullName, UserFullNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserFullName, value);
            }
        }

        public static string GeneralLoginPassword
        {
            get
            {
                return AppSettings.GetValueOrDefault(LoginPassword, LoginPasswordDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LoginPassword, value);
            }
        }

        public static string GeneralUserEmail
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserEmail, UserEmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserEmail, value);
            }
        }

        public static string GeneralUserProfilePic
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserProfilePic, UserProfilePicDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserProfilePic, value);
            }
        }


        public static string GeneralStaffId
        {
            get
            {
                return AppSettings.GetValueOrDefault(StaffId, StaffIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(StaffId, value);
            }
        }

        public static string GeneralCustomerId
        {
            get
            {
                return AppSettings.GetValueOrDefault(CustomerId, CustomerIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(CustomerId, value);
            }
        }
        #endregion
    }
}
