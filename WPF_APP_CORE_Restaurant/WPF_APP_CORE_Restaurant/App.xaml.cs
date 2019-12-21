using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using WPF_APP_CORE_Restaurant.Interfaces;
using WPF_APP_CORE_Restaurant.Models;
using WPF_APP_CORE_Restaurant.View;
using WPF_APP_CORE_Restaurant.ViewModels;

namespace WPF_APP_CORE_Restaurant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static new Window MainWindow { get; set; }
        public static Window AuthWindow { get; set; }
        public static DataBaseSaource Data { get; set; }
        public static Account InitAccount { get; set; }

        public static string IMAGES_DIR
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "/Images";
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Images"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Images");

            Data = new DataBaseSaource();
            AuthWindow = new AuthorizationView();
            AuthWindow.Show();
        }

        public static Account LoginAccount(string login, string password)
        {
            Account account = new Account();
            try
            {
                DataTable table_Accounts = App.Data.Select($"SELECT * FROM [Accounts] WHERE Password = '{password}' AND Login = '{login}';");
                account.ID = (int)table_Accounts.Rows[0]["ID"];
                account.Password = table_Accounts.Rows[0]["Password"].ToString();
                account.Login = table_Accounts.Rows[0]["Login"].ToString();
                account.Email = table_Accounts.Rows[0]["Email"].ToString();
                account.FullName = table_Accounts.Rows[0]["FullName"].ToString();
                account.isAdmin = (bool)table_Accounts.Rows[0]["isAdmin"];
                account.Phone = table_Accounts.Rows[0]["Phone"].ToString();
                return account;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static Account GetAccount(int id)
        {
            Account account = new Account();
            try
            {
                DataTable table_Accounts = App.Data.Select($"SELECT * FROM [Accounts] WHERE id = {id};");
                account.ID = (int)table_Accounts.Rows[0]["ID"];
                account.Password = table_Accounts.Rows[0]["Password"].ToString();
                account.Login = table_Accounts.Rows[0]["Login"].ToString();
                account.Email = table_Accounts.Rows[0]["Email"].ToString();
                account.FullName = table_Accounts.Rows[0]["FullName"].ToString();
                account.isAdmin = (bool)table_Accounts.Rows[0]["isAdmin"];
                account.Phone = table_Accounts.Rows[0]["Phone"].ToString();
                return account;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool RegisterAccount(Account account)
        {
            //insert into [Accounts]([Password],[Login])values('','');
            int rows = Data.ScalarQuery($"insert into [Accounts]" +
                $"([Password],[Login],[Email],[FullName],[Phone])" +
                $"values" +
                $"('{account.Password}','{account.Login}','{account.Email}','{account.FullName}','{account.Phone}');");
            if(rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static ObservableCollection<Account> SelectAccounts()
        {
            ObservableCollection<Account> accounts = new ObservableCollection<Account>();
            DataTable data = Data.Select("select * from Accounts");

            foreach (DataRow row in data.Rows)
            {
                accounts.Add(new Account()
                {
                    ID = (int)row["ID"],
                    FullName = row["FullName"].ToString(),
                    Login = row["Login"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    isAdmin = (bool)row["isAdmin"],
                    Phone = row["isAdmin"].ToString(),
                });
            }

            return accounts;
        }
        public static bool UpdateAccount(Account account)
        {
            if(account != null)
            {
                int rows = Data.ScalarQuery("update Accounts " +
                    $"set Fullname = '{account.FullName}'," +
                    $"Email = '{account.Email}', " +
                    $"Password = '{account.Password}', " +
                    $"Login = '{account.Login}', " +
                    $"isAdmin = '{account.isAdmin}', " +
                    $"Phone = '{account.Phone}' " +
                    $"where Id = {account.ID};");
                if(rows > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }else
            {
                return false;
            }
        }
        public static bool DeleteAccount(int ID)
        {
            int rows = Data.ScalarQuery($"delete Accounts where Id = {ID};");
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Dish
        public static ObservableCollection<Dish> SelectDishes()
        {
            ObservableCollection<Dish> dishes = new ObservableCollection<Dish>();
            DataTable table = Data.Select("select * from Dish;");
            foreach (DataRow row in table.Rows)
            {
                dishes.Add(new Dish() { 
                    ID = (int)row["id"],
                    Name = row["_name"].ToString(),
                    Description = row["_description"].ToString(),
                    Image = new FileInfo(IMAGES_DIR+"/"+row["_image_name"].ToString()),
                    Price = (int)row["_price"]
                });
            }
            return dishes;
        }
        public static Dish GetDish(int id)
        {
            DataTable table = Data.Select($"select * from Dish where id = {id};");
            if(table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                return new Dish()
                {
                    ID = (int)row["id"],
                    Name = row["_name"].ToString(),
                    Description = row["_description"].ToString(),
                    Image = new FileInfo(IMAGES_DIR + "/" + row["_image_name"].ToString()),
                    Price = (int)row["_price"]
                };
            }
            else
            {
                return null;
            }
        }
        public static bool AddDish(Dish dish)
        {
            int data = Data.ScalarQuery($"insert into Dish (_name,_description,_image_name,_price)values('{dish.Name}', '{dish.Description}', '{dish.Image.Name}','{dish.Price}'); ");
            dish.Image.CopyTo(IMAGES_DIR + "/" + dish.Image.Name);
            return data > 0 ? true : false;
        }
        public static bool DeleteDish(Dish dish)
        {
            int data = Data.ScalarQuery($"delete Dish where id = {dish.ID};");
            return data > 0 ? true : false;
        }
        public static void SelectDish(Dish dish)
        {
            SelectDish selectDish = new SelectDish();

            SelectedDishViewModel selectedDishViewModel = selectDish.DataContext as SelectedDishViewModel;
            if (selectedDishViewModel != null)
            {
                selectedDishViewModel._dish = dish;
            }
            selectDish.ShowDialog();
        }
        #endregion

        #region Table
        public static ObservableCollection<Table> Selecttable()
        {
            ObservableCollection<Table> Tables = new ObservableCollection<Table>();
            DataTable table = Data.Select("select * from [Table];");
            foreach (DataRow row in table.Rows)
            {
                Tables.Add(new Table()
                {
                    ID = (int)row["id"],
                    CountHuman = (int) row["CountHuman"],
                });
            }
            return Tables;
        }
        public static Table GetTable(int id)
        {
            Table table = new Table();
            DataTable stable = Data.Select($"select * from [Table] where id = {id};");
            foreach (DataRow row in stable.Rows)
            {
                table = new Table()
                {
                    ID = (int)row["id"],
                    CountHuman = (int)row["CountHuman"],
                };
            }
            return table;
        }
        #endregion

        #region orders
        public static ObservableCollection<Order> SelectOrder()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();

            DataTable dataTable = Data.Select("select * from [Order];");
            foreach (DataRow row in dataTable.Rows)
            {
                Order order = new Order()
                {

                    ID = (int)row["id"],
                    ID_Account = GetAccount((int)row["id_Account"]),
                    ID_Table = GetTable((int)row["id_Table"]),
                    Date = row["Date"].ToString()

                };
                if(row["Dishes"].ToString() != "" && row["Dishes"] != null)
                {
                    string[] dishes_id = row["Dishes"].ToString().Split(',');
                    order.Dish = new ObservableCollection<Dish>();
                    foreach (string item in dishes_id)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] item_id = item.Split(':');
                            int id = int.Parse(item_id[0]);
                            int count = int.Parse(item_id[1]);
                            Dish dish = GetDish(id);
                            dish.DishesCount = count;
                            if(dish != null)
                            {
                                order.Dish.Add(dish);
                            }
                        }
                    }
                }
                orders.Add(order);
            }

            return orders;
        }
        #endregion
    }
}
