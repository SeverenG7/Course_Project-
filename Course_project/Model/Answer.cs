namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Table("Answer")]
    public partial class Answer : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int ID_Answer { get; set; }

        [Column(TypeName = "text")]
        [Required]
        private string text;
        public string Text_Answer
        {
            get => text;

            set
            {
                text = value;
                OnPropertyChanged("Text_Answer");
            }
        }

        private bool correct;

        public bool Correct
        {
            get => correct;

            set
            {
                correct = value;
                OnPropertyChanged("Correct");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
