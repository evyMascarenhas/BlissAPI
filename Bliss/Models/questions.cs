//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bliss.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public questions()
        {
            this.questions_choices = new HashSet<questions_choices>();
        }
    
        public int id { get; set; }
        public string question { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public System.DateTime published_at { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<questions_choices> questions_choices { get; set; }
    }
}
