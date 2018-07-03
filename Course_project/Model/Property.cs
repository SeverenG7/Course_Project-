namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Table("Property")]
    public partial class Property : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            Questions = new HashSet<Question>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int ID_Property { get; set; }

        [Required]
        [StringLength(20)]
        public string Difficult { get; set; }

        public int? ID_Theme { get; set; }

        [Column(TypeName = "text")]
        [Required]
        private string description;
        public string Description_Property
        {
            get => description;

            set
            {
                description = value;
                OnPropertyChanged("Description_Property");
            }

        }

        public virtual Theme Theme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Tests { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

     
    }
}
