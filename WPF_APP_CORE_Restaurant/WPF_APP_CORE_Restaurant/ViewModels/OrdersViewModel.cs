using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using WPF_APP_CORE_Restaurant.View;
using System.Windows;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class OrdersViewModel : BindableBase
    {
        private ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return orders ?? App.SelectOrder();
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
                    if(addOrderViewModel != null)
                    {
                        //insert into [Order] (id_Account,id_Table,[Date],Dishes)values(7,1,'','');
                        addOrderViewModel.AddedOrder += (order) => {
                            Orders = App.SelectOrder();
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
                        Orders = App.SelectOrder();
                    }
                   
                });
            }
        }

    }
}
