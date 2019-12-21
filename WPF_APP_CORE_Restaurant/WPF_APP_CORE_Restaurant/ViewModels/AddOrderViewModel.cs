using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using System.Windows;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class AddOrderViewModel : BindableBase
    {
        public event Action<Order> AddedOrder;

        private Order order;
        public Order ThisOrder
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        public ObservableCollection<Account> Accounts
        {
            get
            {
                return App.SelectAccounts();
            }
        }
        public ObservableCollection<Table> Tables
        {
            get
            {
                return App.Selecttable();
            }
        }

        private Account selectAccount;
        public Account SelectAccount
        {
            get { return selectAccount; }
            set { SetProperty(ref selectAccount,value); }
        }

        private Table selectTable;
        public Table Selecttable
        {
            get { return selectTable; }
            set { SetProperty(ref selectTable, value); }
        }

        private ObservableCollection<Dish> dishes;
        public ObservableCollection<Dish> Dishes
        {
            get
            {
                return dishes;
            }
            set
            {
                SetProperty(ref dishes,value);
            }
        }

        public DelegateCommand AddOrderCommnad
        {
            get
            {
                return new DelegateCommand(() => {
                    string a = "";
                    for (int i = 0; i < Dishes.Count; i++)
                    {
                        if (Dishes[i].IsSelected)
                        {
                            a += $"{dishes[i].ID}:{dishes[i].DishesCount},";
                        }
                    }
                    if (App.Data.ScalarQuery($"insert into [Order] " +
                        $"(id_Account,id_Table," +
                        $"[Date],[Dishes]" +
                        $")values" +
                        $"({ThisOrder.ID_Account.ID}," +
                        $"{ThisOrder.ID_Table.ID}," +
                        $"'{ThisOrder.Date}'," +
                        $"'{a}'" +
                        $");") > 0)
                    {
                        AddedOrder?.Invoke(ThisOrder);
                    }

                });
            }
        }
        

        public AddOrderViewModel()
        {
            ThisOrder = new Order();
            Dishes = App.SelectDishes();
        }
    }
}
