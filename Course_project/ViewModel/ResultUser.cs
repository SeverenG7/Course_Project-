using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace Course_project
{
    public class ResultUser : INotifyPropertyChanged
    {
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

        private double? score_result;
        public double? Score_Result
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



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
