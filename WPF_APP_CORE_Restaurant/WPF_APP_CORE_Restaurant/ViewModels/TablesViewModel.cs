using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Mvvm;
using Prism.Commands;
using WPF_APP_CORE_Restaurant.Models;
using System.Windows;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class TablesViewModel : BindableBase
    {
        private ObservableCollection<Table> tables;
        public ObservableCollection<Table> Tables
        {
            get { return tables ?? App.Selecttable(); }
            set { SetProperty(ref tables,value); }
        }

        public ObservableCollection<Account> Accounts
        {
            get
            {
                return App.SelectAccounts();
            }
        }

        private Account selectedAccount;

        public Account SelectedAccount
        {
            get { return selectedAccount; }
            set { SetProperty(ref selectedAccount,value); }
        }

        public DelegateCommand<Table> UpdateTableCommand
        {
            get
            {
                return new DelegateCommand<Table>((table) => {
                    int data = App.Data.ScalarQuery($"update TableOrder set CountHuman = {table.CountHuman} where id = {table.ID};");
                    if(data > 0)
                    {
                        MessageBox.Show("Данные обновлены");
                    }
                });
            }
        }

    }
}
