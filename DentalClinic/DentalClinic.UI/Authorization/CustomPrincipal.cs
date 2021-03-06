﻿using System.Security.Principal;

namespace DentalClinic.UI.Authorization
{
    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity _identity;

        public CustomIdentity Identity
        {
            get { return _identity ?? new AnonymousIdentity(); }
            set { _identity = value; }
        }

        #region IPrincipal Members
        IIdentity IPrincipal.Identity => this.Identity;

        public bool IsInRole(string role)
        {
            return _identity.Role.Name.Contains(role);
        }
        #endregion
    }
}
