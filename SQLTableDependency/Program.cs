﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient.Base.EventArgs;

namespace SQLTableDependency
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class Program
    {
        //private static string _con = "data source=.; initial catalog=SQLTableDependency ; integrated security=True";
        //private static string _con = "data source=.; initial catalog=SQLTableDependency ; integrated security=True";
        private static string _con = "data source=.; Initial Catalog=SQLTableDependency;Integrated Security=false;User ID=sa;Password=$$123456;";
        public static void Main()
        {
            // The mapper object is used to map model properties 
            // that do not have a corresponding table column name.
            // In case all properties of your model have same name 
            // of table columns, you can avoid to use the mapper.
            var mapper = new ModelToTableMapper<Customer>();
            mapper.AddMapping(c => c.Surname, "Surname");
            mapper.AddMapping(c => c.Name, "Name");

            // Here - as second parameter - we pass table name: 
            // this is necessary only if the model name is different from table name 
            // (in our case we have Customer vs Customers). 
            // If needed, you can also specifiy schema name.
            using (var dep = new SqlTableDependency<Customer>(
                connectionString: _con,

                tableName: "Customers", mapper: mapper, executeUserPermissionCheck:false))
            {
                dep.OnChanged += Changed;
                dep.Start();
                string x = Console.ReadLine();

                Console.WriteLine("Press a key to exit");
                Console.ReadKey();
                if (x == "c")
                {
                    dep.Stop();
                }
            }
        }

        public static void Changed(object sender, RecordChangedEventArgs<Customer> e)
        {
            var changedEntity = e.Entity;

            Console.WriteLine("DML operation: " + e.ChangeType);
            Console.WriteLine("ID: " + changedEntity.Id);
            Console.WriteLine("Name: " + changedEntity.Name);
            Console.WriteLine("Surname: " + changedEntity.Surname);
            Console.WriteLine("DateTime: " + DateTime.Now);
        }
    }
}
