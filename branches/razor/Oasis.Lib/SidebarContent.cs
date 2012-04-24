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
    public partial class SidebarContent
    {
        #region Primitive Properties
    
        public virtual int SidebarContentId
        {
            get;
            set;
        }
    
        public virtual string ContentName
        {
            get;
            set;
        }
    
        public virtual string Title
        {
            get;
            set;
        }
    
        public virtual string Content
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<ContentSidebarContent> ContentSidebarContents
        {
            get
            {
                if (_contentSidebarContents == null)
                {
                    var newCollection = new FixupCollection<ContentSidebarContent>();
                    newCollection.CollectionChanged += FixupContentSidebarContents;
                    _contentSidebarContents = newCollection;
                }
                return _contentSidebarContents;
            }
            set
            {
                if (!ReferenceEquals(_contentSidebarContents, value))
                {
                    var previousValue = _contentSidebarContents as FixupCollection<ContentSidebarContent>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupContentSidebarContents;
                    }
                    _contentSidebarContents = value;
                    var newValue = value as FixupCollection<ContentSidebarContent>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupContentSidebarContents;
                    }
                }
            }
        }
        private ICollection<ContentSidebarContent> _contentSidebarContents;

        #endregion
        #region Association Fixup
    
        private void FixupContentSidebarContents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ContentSidebarContent item in e.NewItems)
                {
                    item.SidebarContent = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ContentSidebarContent item in e.OldItems)
                {
                    if (ReferenceEquals(item.SidebarContent, this))
                    {
                        item.SidebarContent = null;
                    }
                }
            }
        }

        #endregion
    }
}
