using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using System.Windows.Forms;
using WPF_APP_CORE_Restaurant.View;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class AdminContentViewModel : BindableBase
    {
        public DataTable Table_Users
        {
            get
            {
                return App.Data.Select("select * from Accounts;");
            }
        }
        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get
            {
                return accounts ?? (App.SelectAccounts());
            }
            set
            {
                SetProperty(ref accounts,value);
            }
        }

        private ObservableCollection<Dish> dishes;
        public ObservableCollection<Dish> Dishes
        {
            get
            {
                return dishes ?? App.SelectDishes();
            }
            set
            {
                SetProperty(ref dishes, value);
            }
        }

        public DelegateCommand<Account> UpdateAccount
        {
            get
            {
                return new DelegateCommand<Account>((account) => {
                    if (App.UpdateAccount(account))
                    {
                        MessageBox.Show("Данные обновлены");
                    }
                });
            }
        }
        public DelegateCommand<Account> DeleteAccount
        {
            get
            {
                return new DelegateCommand<Account>((account) => {
                    if (App.DeleteAccount(account.ID))
                    {
                        Accounts = App.SelectAccounts();
                    }
                });
            }
        }

        public DelegateCommand AddDish
        {
            get
            {
                return new DelegateCommand(() => {
                    AddDishView addDishView = new AddDishView();
                    addDishView.Show();
                    AddDishViewModel dishViewModel = addDishView.DataContext as AddDishViewModel;
                    if(dishViewModel != null)
                    {
                        dishViewModel.ClickAdded += () => {
                            if (App.AddDish(dishViewModel.GetDish))
                            {
                                addDishView.Close();
                                addDishView = null;
                                MessageBox.Show("Блюдо добавлено");
                                Dishes = App.SelectDishes();
                            }
                        };
                    }
                });
            }
        }
        public DelegateCommand<Dish> SelectDishCommand
        {
            get
            {
                return new DelegateCommand<Dish>((dish) => {
                    App.SelectDish(dish);
                });
            }
        }
        public DelegateCommand<Dish> DeleteDish
        {
            get
            {
                return new DelegateCommand<Dish>((dish)=> {
                    if (App.DeleteDish(dish))
                    {
                        Dishes = App.SelectDishes();
                    }
                });
            }
        }
    }
}
