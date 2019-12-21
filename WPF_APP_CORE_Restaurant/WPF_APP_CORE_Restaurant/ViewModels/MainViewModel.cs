using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Prism.Commands;
using Prism.Mvvm;
using WPF_APP_CORE_Restaurant.Models;
using WPF_APP_CORE_Restaurant.View;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public Account InitAccount
        {
            get
            {
                return App.InitAccount;
            }
        }

        private UserControl content;
        public UserControl Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }


        public MainViewModel()
        {
            if (InitAccount.isAdmin)
            {
                Content = new AdminContentView();
            }else
            {
                Content = new UserContentView();
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


    }
}
