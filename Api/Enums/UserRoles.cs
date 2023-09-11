using System.Text.Json.Serialization;
using Nucleus.Internal.Attributes;
using Nucleus.Internal.Enums;

namespace Nucleus.Api.Enums {
    [JsonEnumName(Policy = EnumNamingPolicy.LOWERCASE)]
    public enum UserRoles {
        /// <summary>
        /// Admins are <see cref="STAFF"/> that have been granted permission by an <see cref="OWNER"/>,
        /// this allows for providing support only when granted. Otherwise NucleusBot <see cref="STAFF"/>
        /// are not given any additional access.
        /// </summary>
        [JsonIgnore] ADMIN = 9,
        
        /// <summary>
        /// 
        /// </summary>
        OWNER = 8,
        
        /// <summary>
        /// 
        /// </summary>
        EDITOR = 7,
        
        /// <summary>
        /// 
        /// </summary>
        SUPERMOD = 6,
        
        /// <summary>
        /// 
        /// </summary>
        MODERATOR = 5,
        
        /// <summary>
        /// 
        /// </summary>
        REGULAR = 4,
        
        /// <summary>
        /// Indicates that a User is a paid Channel Subscriber
        /// </summary>
        SUBSCRIBER = 3,
        
        /// <summary>
        /// A role given to NucleusBot Employees that allows them to submit a request for the <see cref="ADMIN"/> role from
        /// an <see cref="OWNER"/> to provide support for their Channel
        /// </summary>
        STAFF = 2,
        
        /// <summary>
        /// Indicates that a User is a Channel Follower
        /// </summary>
        FOLLOWER = 1,
        
        /// <summary>
        /// Any user of a Platform implicitly has this Role
        /// </summary>
        [JsonIgnore] PUBLIC = 0
    }
}
