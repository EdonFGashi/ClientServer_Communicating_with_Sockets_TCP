using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public enum FilePermissions
    {
        None = 0,      // No permissions
        Read = 1,      // Read permission
        Write = 2,     // Write permission
        Execute = 4,   // Execute permission
        ReadWrite = Read | Write,  // Read and Write
        All = Read | Write | Execute  // All permissions
    }

    internal class ClientPermissions
    {
        public string ClientID { get; set; }
        public Dictionary<string, FilePermissions> PathPermissions { get; set; }

        public ClientPermissions(string clientID)
        {
            ClientID = clientID;
            PathPermissions = new Dictionary<string, FilePermissions>();
        }

        // Add permissions for a specific file/directory
        public void SetPermissions(string path, FilePermissions permissions)
        {
            PathPermissions[path] = permissions;
        }

        // Get permissions for a specific file/directory
        public FilePermissions GetPermissions(string path)
        {
            return PathPermissions.ContainsKey(path) ? PathPermissions[path] : FilePermissions.None;
        }

        // Check if client has specific permission on a path
        public bool HasPermission(string path, FilePermissions permission)
        {
            return (GetPermissions(path) & permission) == permission;

        }
    }
}
