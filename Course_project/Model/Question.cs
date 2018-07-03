namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows;
    using Microsoft.Win32;
    using System.Data.Entity;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.IO;
    using System.Drawing.Imaging;
    using System.Drawing;


    [Table("Question")]
    public partial class Question : INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int ID_Question { get; set; }

        [Column(TypeName = "text")]
        [Required]
        private string text;

        public string Text_Question
        {
            get => text;

            set
            {
                text = value;
                OnPropertyChanged("Text_Question");
            }
        }

        public int ID_Property { get; set; }

        private int variants;
        public int Number_Variant
        {
            get => variants;

            set
            {
                variants = value;
                OnPropertyChanged("Number_Variant");
            }
        }


        private double score;

        public double Score
        {

            get => score;

            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

        private byte[] picture;
        
        public byte[] Picture
        {
            get => picture;

            set
            {
                picture = value;
            }

        }

  
      
        public virtual Property Property { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

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
