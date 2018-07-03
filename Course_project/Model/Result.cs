namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Runtime.InteropServices;

    [Table("Result")]
    public partial class Result : INotifyPropertyChanged
    {
        [Key]
        public int ID_Result { get; set; }

        [Required]
        [StringLength(30)]
        private string login;
        public string Login_User
        {

            get => login;

            set
            {
                login = value;
                OnPropertyChanged("Login_User");
            }
        }

        [Required]
        [StringLength(50)]
        private string name_test;
        public string Name_Test
        {
            get => name_test;

            set
            {
                name_test = value;
                OnPropertyChanged("Name_Test");
            }
        }

        private int? score_result;
        public int? Score_Result
        {
            get => score_result;

            set
            {
                score_result = value;
                OnPropertyChanged("Score_Result");
            }
        }

        private string description;

        public string Description_Result
        {
            get => description;

            set
            {
                description = value;
                OnPropertyChanged("Description_Result");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

       
    }
}
