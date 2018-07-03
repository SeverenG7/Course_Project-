namespace Course_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Table("UserInfo")] 
    public partial class UserInfo : INotifyPropertyChanged
    {
        [Key]
        public int ID_UserInfo { get; set; }

        [Required]
        [StringLength(30)]
        private string login;
        public string Login_User
        {
            get => login;

            set
            {
                if (value.Length > 30 || value == null)
                {
                    throw new Exception("Проверье поле логина! Он может содержать до 30 символов");
                }
                login = value;
                OnPropertyChanged("Login_User");
            }
        }

        [Required]
        [StringLength(30)]
        private string firstname;
        public string FirstName_User
        {
            get => firstname;

            set
            {
                firstname = value;
                OnPropertyChanged("Firstname_User");
            }

        }

        [Required]
        [StringLength(30)]
        private string lastname;
        public string LastName_User
        {
            get => lastname;

            set
            {
                lastname = value;
                OnPropertyChanged("LastName_User");
            }
        }

        private int group;
        public int Group_User
        {
            get => group;

            set
            {
                group = value;
                OnPropertyChanged("Group_User");
            }
                
        }

        public int? Course_User { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

      
    }
}
