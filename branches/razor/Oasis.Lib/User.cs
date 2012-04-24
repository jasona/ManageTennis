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
    public partial class User
    {
        #region Primitive Properties
    
        public virtual int UserId
        {
            get;
            set;
        }
    
        public virtual string EmailAddress
        {
            get;
            set;
        }
    
        public virtual string Password
        {
            get;
            set;
        }
    
        public virtual bool AccountValidated
        {
            get;
            set;
        }
    
        public virtual System.DateTime RegisterDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> LastLoginDate
        {
            get;
            set;
        }
    
        public virtual int UserStatusID
        {
            get { return _userStatusID; }
            set
            {
                if (_userStatusID != value)
                {
                    if (UserStatu != null && UserStatu.UserStatusId != value)
                    {
                        UserStatu = null;
                    }
                    _userStatusID = value;
                }
            }
        }
        private int _userStatusID;
    
        public virtual int UserAccessBitMask
        {
            get;
            set;
        }
    
        public virtual int UserTypeId
        {
            get { return _userTypeId; }
            set
            {
                if (_userTypeId != value)
                {
                    if (UserType != null && UserType.UserTypeId != value)
                    {
                        UserType = null;
                    }
                    _userTypeId = value;
                }
            }
        }
        private int _userTypeId;
    
        public virtual int RankId
        {
            get { return _rankId; }
            set
            {
                if (_rankId != value)
                {
                    if (Rank != null && Rank.RankId != value)
                    {
                        Rank = null;
                    }
                    _rankId = value;
                }
            }
        }
        private int _rankId;
    
        public virtual string Address1
        {
            get;
            set;
        }
    
        public virtual string Address2
        {
            get;
            set;
        }
    
        public virtual string City
        {
            get;
            set;
        }
    
        public virtual string State
        {
            get;
            set;
        }
    
        public virtual string ZipCode
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> ActivationKey
        {
            get;
            set;
        }
    
        public virtual string FirstName
        {
            get;
            set;
        }
    
        public virtual string LastName
        {
            get;
            set;
        }
    
        public virtual string PhoneNumber
        {
            get;
            set;
        }
    
        public virtual string MobilePhoneNumber
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Event> Events
        {
            get
            {
                if (_events == null)
                {
                    var newCollection = new FixupCollection<Event>();
                    newCollection.CollectionChanged += FixupEvents;
                    _events = newCollection;
                }
                return _events;
            }
            set
            {
                if (!ReferenceEquals(_events, value))
                {
                    var previousValue = _events as FixupCollection<Event>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupEvents;
                    }
                    _events = value;
                    var newValue = value as FixupCollection<Event>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupEvents;
                    }
                }
            }
        }
        private ICollection<Event> _events;
    
        public virtual ICollection<EventRegistration> EventRegistrations
        {
            get
            {
                if (_eventRegistrations == null)
                {
                    var newCollection = new FixupCollection<EventRegistration>();
                    newCollection.CollectionChanged += FixupEventRegistrations;
                    _eventRegistrations = newCollection;
                }
                return _eventRegistrations;
            }
            set
            {
                if (!ReferenceEquals(_eventRegistrations, value))
                {
                    var previousValue = _eventRegistrations as FixupCollection<EventRegistration>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupEventRegistrations;
                    }
                    _eventRegistrations = value;
                    var newValue = value as FixupCollection<EventRegistration>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupEventRegistrations;
                    }
                }
            }
        }
        private ICollection<EventRegistration> _eventRegistrations;
    
        public virtual ICollection<ProBillRate> ProBillRates
        {
            get
            {
                if (_proBillRates == null)
                {
                    var newCollection = new FixupCollection<ProBillRate>();
                    newCollection.CollectionChanged += FixupProBillRates;
                    _proBillRates = newCollection;
                }
                return _proBillRates;
            }
            set
            {
                if (!ReferenceEquals(_proBillRates, value))
                {
                    var previousValue = _proBillRates as FixupCollection<ProBillRate>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupProBillRates;
                    }
                    _proBillRates = value;
                    var newValue = value as FixupCollection<ProBillRate>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupProBillRates;
                    }
                }
            }
        }
        private ICollection<ProBillRate> _proBillRates;
    
        public virtual Rank Rank
        {
            get { return _rank; }
            set
            {
                if (!ReferenceEquals(_rank, value))
                {
                    var previousValue = _rank;
                    _rank = value;
                    FixupRank(previousValue);
                }
            }
        }
        private Rank _rank;
    
        public virtual UserStatu UserStatu
        {
            get { return _userStatu; }
            set
            {
                if (!ReferenceEquals(_userStatu, value))
                {
                    var previousValue = _userStatu;
                    _userStatu = value;
                    FixupUserStatu(previousValue);
                }
            }
        }
        private UserStatu _userStatu;
    
        public virtual UserType UserType
        {
            get { return _userType; }
            set
            {
                if (!ReferenceEquals(_userType, value))
                {
                    var previousValue = _userType;
                    _userType = value;
                    FixupUserType(previousValue);
                }
            }
        }
        private UserType _userType;
    
        public virtual ICollection<UserBilling> UserBillings
        {
            get
            {
                if (_userBillings == null)
                {
                    var newCollection = new FixupCollection<UserBilling>();
                    newCollection.CollectionChanged += FixupUserBillings;
                    _userBillings = newCollection;
                }
                return _userBillings;
            }
            set
            {
                if (!ReferenceEquals(_userBillings, value))
                {
                    var previousValue = _userBillings as FixupCollection<UserBilling>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUserBillings;
                    }
                    _userBillings = value;
                    var newValue = value as FixupCollection<UserBilling>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUserBillings;
                    }
                }
            }
        }
        private ICollection<UserBilling> _userBillings;
    
        public virtual ICollection<UserGroup> UserGroups
        {
            get
            {
                if (_userGroups == null)
                {
                    var newCollection = new FixupCollection<UserGroup>();
                    newCollection.CollectionChanged += FixupUserGroups;
                    _userGroups = newCollection;
                }
                return _userGroups;
            }
            set
            {
                if (!ReferenceEquals(_userGroups, value))
                {
                    var previousValue = _userGroups as FixupCollection<UserGroup>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUserGroups;
                    }
                    _userGroups = value;
                    var newValue = value as FixupCollection<UserGroup>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUserGroups;
                    }
                }
            }
        }
        private ICollection<UserGroup> _userGroups;
    
        public virtual ICollection<CommunityActivity> CommunityActivities
        {
            get
            {
                if (_communityActivities == null)
                {
                    var newCollection = new FixupCollection<CommunityActivity>();
                    newCollection.CollectionChanged += FixupCommunityActivities;
                    _communityActivities = newCollection;
                }
                return _communityActivities;
            }
            set
            {
                if (!ReferenceEquals(_communityActivities, value))
                {
                    var previousValue = _communityActivities as FixupCollection<CommunityActivity>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCommunityActivities;
                    }
                    _communityActivities = value;
                    var newValue = value as FixupCollection<CommunityActivity>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCommunityActivities;
                    }
                }
            }
        }
        private ICollection<CommunityActivity> _communityActivities;

        #endregion
        #region Association Fixup
    
        private void FixupRank(Rank previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (Rank != null)
            {
                if (!Rank.Users.Contains(this))
                {
                    Rank.Users.Add(this);
                }
                if (RankId != Rank.RankId)
                {
                    RankId = Rank.RankId;
                }
            }
        }
    
        private void FixupUserStatu(UserStatu previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (UserStatu != null)
            {
                if (!UserStatu.Users.Contains(this))
                {
                    UserStatu.Users.Add(this);
                }
                if (UserStatusID != UserStatu.UserStatusId)
                {
                    UserStatusID = UserStatu.UserStatusId;
                }
            }
        }
    
        private void FixupUserType(UserType previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (UserType != null)
            {
                if (!UserType.Users.Contains(this))
                {
                    UserType.Users.Add(this);
                }
                if (UserTypeId != UserType.UserTypeId)
                {
                    UserTypeId = UserType.UserTypeId;
                }
            }
        }
    
        private void FixupEvents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Event item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Event item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupEventRegistrations(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (EventRegistration item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (EventRegistration item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupProBillRates(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ProBillRate item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ProBillRate item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupUserBillings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UserBilling item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UserBilling item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupUserGroups(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UserGroup item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UserGroup item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupCommunityActivities(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CommunityActivity item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (CommunityActivity item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }

        #endregion
    }
}