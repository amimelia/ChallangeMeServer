//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChallengeMeServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public notification()
        {
            this.notification_users = new HashSet<notification_users>();
        }
    
        public int NotificationID { get; set; }
        public string NotificationDescription { get; set; }
        public Nullable<System.DateTime> NotificationCreateDate { get; set; }
        public string NotificationStatus { get; set; }
        public Nullable<int> ChallengeID { get; set; }
        public Nullable<int> PostID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual challenge challenge { get; set; }
        public virtual post post { get; set; }
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification_users> notification_users { get; set; }
    }
}
