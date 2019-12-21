using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WPF_APP_CORE_Restaurant.Interfaces;
using System.Linq;
using Prism.Mvvm;
using System.ComponentModel;

namespace WPF_APP_CORE_Restaurant.Models
{
    public class Account : BindableBase
    {
        public int ID { get; set; }

        private string password;
        public string Password { 
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password,value);
            }
        }

        private string login;
        public string Login {
            get
            {
                return login;
            }
            set
            {
                SetProperty(ref login, value);
            }
        }

        private string email;
        public string Email {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email,value);
            }
        }
    
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }

        private bool isadmin;
        public bool isAdmin { 
            get
            {
                return isadmin;
            }
            set
            {
                SetProperty(ref isadmin,value);
            }
        }

        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                SetProperty(ref phone,value);
            }
        }
    }
}
