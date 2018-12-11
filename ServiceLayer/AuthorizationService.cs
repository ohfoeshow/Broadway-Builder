﻿using DataAccessLayer;
using DataAccessLayer.Enums;
//using ServiceLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer
{
    public class AuthorizationService : IAuthorizationService
    {
        //DbContext would be declared
        //List<string> permissions;//This stays
        private readonly IPermissionRepository permissionRepository;

        // Would pass in DbContext instead of List if we were to use DB
        public AuthorizationService(IPermissionRepository permissionRepository)
        {
            //Initialize the DbContext
            this.permissionRepository = permissionRepository;
            //permissions = authorizedPermissions;//call the DbContext to get the permissions and store them into the List
        }
        

        /// <summary>
        /// Checks if a user is authorized to perform a action by checking if they have the permission needed
        /// </summary>
        /// <param name="user">User that must be checked if they are authorized</param>
        /// <param name="checkIfAuthorized">check if user has this permission</param>
        /// <returns>true if user has permission, false otherwise</returns>
        public bool HasPermission(User user, Permission checkIfAuthorized)
        {
            if (permissionRepository.UserHasPermission(user, checkIfAuthorized))
            {
                return true;
            }
            return false;
        }


    }
}