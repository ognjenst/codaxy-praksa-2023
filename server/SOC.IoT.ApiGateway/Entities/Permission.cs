using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SOC.IoT.ApiGateway.Entities
{
    public class Permission
    {
        public int ScopeId { get; set; }
        public int ResourceId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
