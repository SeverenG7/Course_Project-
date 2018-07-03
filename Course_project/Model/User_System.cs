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

    public partial class User_System : INotifyPropertyChanged
    {
        [Key]
        public int ID_User { get; set; }

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
        [StringLength(30)]
        private string password;
        public string Password_User
        {
            get => password;

            set
            {
                password = value;
                OnPropertyChanged("Password_User");
            }
        }


        private bool rights;
        public bool AdminRights
        {
            get => rights;

            set
            {
                rights = value;
                OnPropertyChanged("AdminRights");
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
