namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;


    [Table("Test")]
    public partial class Test:INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Test()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int ID_Test { get; set; }

        [Required]
        [StringLength(50)]
        private string name ;

        public string Name_Test
        {
            get => name;

            set
            {
                name = value;
                OnPropertyChanged("Name_Test");
            }
        }

        private int questionsNumber;
        public int Questions_Number
        {
            get => questionsNumber;

            set
            {
                questionsNumber = value;
                OnPropertyChanged("Questions_Number");
            }

        }

        private double max_score;

        public double Max_score
        {
            get => max_score;

            set
            {
                max_score = value;
                OnPropertyChanged("Max_score");
            }
        }


        private double min_score;

        public double Min_score
        {
            get => min_score;

            set
            {
                min_score = value;
                OnPropertyChanged("Max_score");
            }
        }

        public int ID_Property { get; set; }

       

        public virtual Property Property { get; set; }

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
