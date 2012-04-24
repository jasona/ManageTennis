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
    public partial class EventRegistration
    {
        #region Primitive Properties
    
        public virtual int EventRegistrationId
        {
            get;
            set;
        }
    
        public virtual int EventId
        {
            get { return _eventId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_eventId != value)
                    {
                        if (Event != null && Event.EventId != value)
                        {
                            Event = null;
                        }
                        _eventId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _eventId;
    
        public virtual int UserId
        {
            get { return _userId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_userId != value)
                    {
                        if (User != null && User.UserId != value)
                        {
                            User = null;
                        }
                        _userId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _userId;
    
        public virtual System.DateTime RegistrationDate
        {
            get;
            set;
        }
    
        public virtual Nullable<int> TransactionId
        {
            get { return _transactionId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_transactionId != value)
                    {
                        if (Transaction != null && Transaction.TransactionId != value)
                        {
                            Transaction = null;
                        }
                        _transactionId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _transactionId;

        #endregion
        #region Navigation Properties
    
        public virtual Event Event
        {
            get { return _event; }
            set
            {
                if (!ReferenceEquals(_event, value))
                {
                    var previousValue = _event;
                    _event = value;
                    FixupEvent(previousValue);
                }
            }
        }
        private Event _event;
    
        public virtual Transaction Transaction
        {
            get { return _transaction; }
            set
            {
                if (!ReferenceEquals(_transaction, value))
                {
                    var previousValue = _transaction;
                    _transaction = value;
                    FixupTransaction(previousValue);
                }
            }
        }
        private Transaction _transaction;
    
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
    
        private bool _settingFK = false;
    
        private void FixupEvent(Event previousValue)
        {
            if (previousValue != null && previousValue.EventRegistrations.Contains(this))
            {
                previousValue.EventRegistrations.Remove(this);
            }
    
            if (Event != null)
            {
                if (!Event.EventRegistrations.Contains(this))
                {
                    Event.EventRegistrations.Add(this);
                }
                if (EventId != Event.EventId)
                {
                    EventId = Event.EventId;
                }
            }
        }
    
        private void FixupTransaction(Transaction previousValue)
        {
            if (previousValue != null && previousValue.EventRegistrations.Contains(this))
            {
                previousValue.EventRegistrations.Remove(this);
            }
    
            if (Transaction != null)
            {
                if (!Transaction.EventRegistrations.Contains(this))
                {
                    Transaction.EventRegistrations.Add(this);
                }
                if (TransactionId != Transaction.TransactionId)
                {
                    TransactionId = Transaction.TransactionId;
                }
            }
            else if (!_settingFK)
            {
                TransactionId = null;
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.EventRegistrations.Contains(this))
            {
                previousValue.EventRegistrations.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.EventRegistrations.Contains(this))
                {
                    User.EventRegistrations.Add(this);
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