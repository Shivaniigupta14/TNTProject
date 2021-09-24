using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace TNTapp.Common
{
    public class AppPermission
    {
        public async Task<Tuple<string, bool>> GetContactAccessPermission()
        {
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Contacts);
            string retResponse = "";
            bool res = false;

            var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Contacts);
            if (response.ContainsKey(Permission.Contacts))
                permissionStatus = response[Permission.Contacts];

            if (permissionStatus == PermissionStatus.Granted)
            {
                retResponse = "Permission Granted";
                res = true;
            }
            else if (permissionStatus == PermissionStatus.Denied)
            {
                retResponse = "Permission Denied";
            }
            else if (permissionStatus == PermissionStatus.Disabled)
            {
                retResponse = "Permission Disabled";
            }
            else if (permissionStatus == PermissionStatus.Restricted)
            {
                retResponse = "Permission Restricted";
            }
            else if (permissionStatus == PermissionStatus.Unknown)
            {
                retResponse = "Permission Unknown";
            }
            return Tuple.Create(retResponse, res);
        }
    }
}
