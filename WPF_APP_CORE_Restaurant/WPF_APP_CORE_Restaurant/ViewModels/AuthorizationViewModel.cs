using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using WPF_APP_CORE_Restaurant.Models;
using WPF_APP_CORE_Restaurant.View;

namespace WPF_APP_CORE_Restaurant.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        #region Tab
        private bool registerWindow;
        public bool RegisterWindow
        {
            get { return registerWindow; }
            set {
                SetProperty(ref registerWindow, value);
                if (value)
                {
                    LoginWindow = false;
                }
            }
        }

        private bool loginWindow;
        public bool LoginWindow
        {
            get { return loginWindow; }
            set {
                SetProperty(ref loginWindow, value);
                if (value)
                {
                    RegisterWindow = false;
                }
            }
        }
        #endregion

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { 
                SetProperty(ref _login,value); 
            }
        }

        //Register Account
        private Account account;
        public Account Account_
        {
            get { return account ?? (account = new Account()); }
            set { SetProperty(ref account, value); }
        }


        public DelegateCommand Authoriation
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
                    {
                        App.InitAccount = App.LoginAccount(Login, Password);
                        if (App.InitAccount is null)
                        {
                            MessageBox.Show("Неудачно авторизовалися");
                        }
                        else
                        {
                            App.MainWindow = new MainView();
                            App.MainWindow.Show();
                            App.AuthWindow.Close();
                            App.AuthWindow = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль или логин");
                    }
                },()=> {
                    return true;
                });
            }
        }
        public DelegateCommand Register
        {
            get
            {
                return new DelegateCommand(() => {
                    bool isRegister = App.RegisterAccount(Account_);
                    if (isRegister)
                    {
                        MessageBox.Show("Вы зарегистрировались");
                        LoginWindow = true;
                        
                    }
                });
            }
        }
        public AuthorizationViewModel()
        {
            loginWindow = true;
        }
    }
}
