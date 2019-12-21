using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace WPF_APP_CORE_Restaurant.Models
{
    public class Table : BindableBase
    {
        public int ID { get; set; }

        private int counthuman;
        public int CountHuman
        {
            get { return counthuman; }
            set { SetProperty(ref counthuman,value); }
        }

        private object orderAccount;
        public object OrderAccount
        {
            get { return orderAccount; }
            set { SetProperty(ref orderAccount,value); }
        }



    }
}
