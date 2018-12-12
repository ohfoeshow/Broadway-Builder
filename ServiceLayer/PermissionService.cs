﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class PermissionService
    {
        private readonly BroadwayBuilderContext _dbContext;

        // Constructor
        public PermissionService(BroadwayBuilderContext Context)
        {
            this._dbContext = Context;
        }

        /// <summary>
        /// CreatePermission is a method in the RoleService class.
        /// This enables us to create a new permission in the DB if necessary
        /// </summary>
        /// <param name="role">The new role we want to create</param>
        public void CreatePermission(Permission permission)
        {

            _dbContext.Permissions.Add(permission);

        }

        /// <summary>
        /// GetPermission is a method in the RoleService class.
        /// This enables us to retrieve a permission from the DB if necessary.
        /// </summary>
        /// <param name="role">The role we want to retrieve</param>
        /// <returns></returns>
        public Permission GetUser(string permission)
        {
            return _dbContext.Permissions.Find(permission);
        }

        /// <summary>
        /// DeletePermission is a method in the the RoleService class.
        /// This enables us to delete a permission from the DB if necessary.
        /// </summary>
        /// <param name="role">The role that we want to delete</param>
        public void DeletePermission(Permission permission)
        {

            Permission permissionToDelete = _dbContext.Permissions.Find(permission.PermissionTitle);
            if (permissionToDelete != null)
            {
                _dbContext.Permissions.Remove(permissionToDelete);
            }

        }
    }
}
