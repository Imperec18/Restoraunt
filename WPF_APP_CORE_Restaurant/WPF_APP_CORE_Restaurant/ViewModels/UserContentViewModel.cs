using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using System.Windows;
using WPF_APP_CORE_Restaurant.View;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class UserContentViewModel : BindableBase
    {
        private List<Order> orders;
        public List<Order> Orders
        {
            get
            {
                return orders ?? App.SelectOrder().Where(s => s.ID_Account.ID == App.InitAccount.ID).ToList();
            }
            set
            {
                SetProperty(ref orders,value);
            }
        }

        public DelegateCommand AddOrderCommand
        {
            get
            {
                return new DelegateCommand(() => {
                    AddOrder addOrder = new AddOrder();
                    addOrder.Show();
                    AddOrderViewModel addOrderViewModel = addOrder.DataContext as AddOrderViewModel;
                    if (addOrderViewModel != null)
                    {
                        //insert into [Order] (id_Account,id_Table,[Date],Dishes)values(7,1,'','');
                        addOrderViewModel.AddedOrder += (order) => {
                            Orders = App.SelectOrder().Where(s => s.ID_Account.ID == App.InitAccount.ID).ToList();
                        };
                    }
                });
            }
        }

        public DelegateCommand<Order> DeleteOrderCommand
        {
            get
            {
                return new DelegateCommand<Order>((order) => {
                    if (App.Data.ScalarQuery($"delete [Order] where Id = {order.ID};") > 0)
                    {
                        MessageBox.Show("Заказ удалён");
                        Orders = App.SelectOrder().Where(s => s.ID_Account.ID == App.InitAccount.ID).ToList();
                    }

                });
            }
        }
    }
}
