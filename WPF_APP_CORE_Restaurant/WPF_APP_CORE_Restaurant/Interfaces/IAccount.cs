using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_APP_CORE_Restaurant.Interfaces
{
    public interface IAccount
    {
        int ID { get; set; }
        string Password { get; set; }
        string Login { get; set; }
    }
}
