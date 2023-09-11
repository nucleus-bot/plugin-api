using Nucleus.Api;
using Nucleus.Api.Enums;

namespace Nucleus.Helpers {
    public static class EnumsHelper {
        /// <summary>
        /// Gets the Power Level of the Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static int GetPowerLevel(this UserRoles role) => role switch {
            UserRoles.ADMIN => 8,
            UserRoles.OWNER => 7,
            UserRoles.EDITOR => 6,
            UserRoles.SUPERMOD => 5,
            UserRoles.MODERATOR => 4,
            UserRoles.REGULAR => 3,
            UserRoles.SUBSCRIBER => 2,
            UserRoles.STAFF => 1,
            UserRoles.PUBLIC or UserRoles.FOLLOWER => 0,
            _ => 0
        };
        
        public static bool IsStaff(this UserRoles role)
            => role is UserRoles.ADMIN or UserRoles.STAFF;
        
        public static bool IsGrantable(this UserRoles role)
            => !role.Restricted();
        
        public static bool IsGrantableTo(this UserRoles role, PayloadUser user)
            => role is UserRoles.ADMIN ? user.Roles is UserRoles[] roles && roles.Contains(UserRoles.STAFF) : role.IsGrantable();
        
        public static bool Restricted(this UserRoles role)
            => role.IsStaff() || role is UserRoles.FOLLOWER or UserRoles.SUBSCRIBER;
    }
}
