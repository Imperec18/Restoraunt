using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DB.Core
{
    public class Model
    {
        public string NameTable { get; protected set; }


        public DataTable query(string select)
        {
            return null;
        }
    }
}
