//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Oasis.Lib
{
    public partial class CommunityActivity
    {
        #region Primitive Properties
    
        public virtual int ActivityId
        {
            get;
            set;
        }
    
        public virtual int UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (User != null && User.UserId != value)
                    {
                        User = null;
                    }
                    _userId = value;
                }
            }
        }
        private int _userId;
    
        public virtual string Description
        {
            get;
            set;
        }
    
        public virtual System.DateTime ActivityDate
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;

        #endregion
        #region Association Fixup
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.CommunityActivities.Contains(this))
            {
                previousValue.CommunityActivities.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.CommunityActivities.Contains(this))
                {
                    User.CommunityActivities.Add(this);
                }
                if (UserId != User.UserId)
                {
                    UserId = User.UserId;
                }
            }
        }

        #endregion
    }
}
