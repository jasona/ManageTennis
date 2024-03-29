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
    public partial class UserBilling
    {
        #region Primitive Properties
    
        public virtual int UserBillingId
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
    
        public virtual string CCNumber
        {
            get;
            set;
        }
    
        public virtual string CCType
        {
            get;
            set;
        }
    
        public virtual string CCExpMonth
        {
            get;
            set;
        }
    
        public virtual string CCExpYear
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Transaction> Transactions
        {
            get
            {
                if (_transactions == null)
                {
                    var newCollection = new FixupCollection<Transaction>();
                    newCollection.CollectionChanged += FixupTransactions;
                    _transactions = newCollection;
                }
                return _transactions;
            }
            set
            {
                if (!ReferenceEquals(_transactions, value))
                {
                    var previousValue = _transactions as FixupCollection<Transaction>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTransactions;
                    }
                    _transactions = value;
                    var newValue = value as FixupCollection<Transaction>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTransactions;
                    }
                }
            }
        }
        private ICollection<Transaction> _transactions;
    
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
            if (previousValue != null && previousValue.UserBillings.Contains(this))
            {
                previousValue.UserBillings.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.UserBillings.Contains(this))
                {
                    User.UserBillings.Add(this);
                }
                if (UserId != User.UserId)
                {
                    UserId = User.UserId;
                }
            }
        }
    
        private void FixupTransactions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Transaction item in e.NewItems)
                {
                    item.UserBilling = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Transaction item in e.OldItems)
                {
                    if (ReferenceEquals(item.UserBilling, this))
                    {
                        item.UserBilling = null;
                    }
                }
            }
        }

        #endregion
    }
}
