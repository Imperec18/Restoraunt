using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;
using WPF_APP_CORE_Restaurant.Models;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class SelectedDishViewModel: BindableBase
    {

        private Dish dish;
        public Dish _dish
        {
            get { return dish; }
            set { SetProperty(ref dish, value); }
        }

        public bool isAdmin
        {
            get
            {
                return App.InitAccount.isAdmin;
            }
        }

    }
}
