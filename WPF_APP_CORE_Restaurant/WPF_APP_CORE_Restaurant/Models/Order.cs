using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Mvvm;

namespace WPF_APP_CORE_Restaurant.Models
{
    public class Order : BindableBase
    {
        public int ID { get; set; }

        private Account id_user;
        public Account ID_Account
        {
            get { return id_user; }
            set { SetProperty(ref id_user,value); }
        }

        private Table table;
        public Table ID_Table
        {
            get { return table; }
            set { SetProperty(ref table,value); }
        }

        private ObservableCollection<Dish> dish;
        public ObservableCollection<Dish> Dish
        {
            get
            {
                return dish;
            }
            set
            {
                SetProperty(ref dish,value);
            }
        }

        private string dateTime;
        public string Date
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime,value); }
        }

        public int EndPrice
        {
            get
            {
                if(Dish != null)
                {
                    int price = 0;
                    foreach (Dish item in Dish)
                    {
                        price += item.Price * item.DishesCount;
                    }
                    return price;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
