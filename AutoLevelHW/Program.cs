using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoLevelHW
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataSet = new DataSet("ShopDb");
            var dataHolder = new SqlDataAdapter("Select * from Employees", "Data Source=DESKTOP-H5GCVAL;Initial Catalog=ShopDb;Integrated Security=True");
            dataHolder.Fill(dataSet);
            
            var clientsTable = dataSet.Tables.Add("clients");
            var clientsId = new DataColumn("id", typeof(int));
            clientsId.ReadOnly = true;
            clientsId.AutoIncrementSeed = 0;
            clientsId.AutoIncrementStep = 1;

            var clientName = new DataColumn("Name", typeof(string));

            clientsTable.Columns.AddRange(new DataColumn[] { clientsId, clientName });
            clientsTable.PrimaryKey = new DataColumn[] { clientsId };

            var workerTable = dataSet.Tables.Add("Employees");
            var workerId = new DataColumn("id", typeof(int));
            workerId.ReadOnly = true;
            workerId.AutoIncrementSeed = 0;
            workerId.AutoIncrementStep = 1;

            var workerName = new DataColumn("Name", typeof(string));

            workerTable.Columns.AddRange(new DataColumn[] { workerId, workerName });

            var newRow = workerTable.NewRow();

            newRow.ItemArray = new object[] { 3, "Аспандияр" };
            workerTable.Rows.Add(newRow);

            workerTable.PrimaryKey = new DataColumn[] { workerId };

            var ordersTable = dataSet.Tables.Add("Orders");
            var ordersId = new DataColumn("id", typeof(int));
            ordersId.ReadOnly = true;
            ordersId.AutoIncrementSeed = 0;
            ordersId.AutoIncrementStep = 1;

            var productsTable = dataSet.Tables.Add("Products");
            var productsId = new DataColumn("id", typeof(int));
            productsId.ReadOnly = true;
            productsId.AutoIncrementSeed = 0;
            productsId.AutoIncrementStep = 1;
            var productName = new DataColumn("Name", typeof(string));
            var productPrice = new DataColumn("Price", typeof(int));
            productsTable.Columns.AddRange(new DataColumn[] { productsId, productName, productPrice });

            productsTable.PrimaryKey = new DataColumn[] { productsId };

            var productId = new DataColumn("productId", typeof(int));
            var clientId = new DataColumn("clientId", typeof(int));

            ordersTable.Columns.AddRange(new DataColumn[] { ordersId, productId, clientId });
            ordersTable.PrimaryKey = new DataColumn[] { ordersId };

            var orderDetailTable = dataSet.Tables.Add("OrderDetails");
            var orderDetailId = new DataColumn("id", typeof(int));
            orderDetailId.ReadOnly = true;
            orderDetailId.AutoIncrementSeed = 0;
            orderDetailId.AutoIncrementStep = 1;

            var orderDepiction = new DataColumn("orderDepiction", typeof(string));

            orderDetailTable.Columns.AddRange(new DataColumn[] { orderDetailId, orderDepiction });

            orderDetailTable.PrimaryKey = new DataColumn[] { orderDetailId };

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataHolder);
            dataHolder.Update(dataSet);
            dataSet.AcceptChanges();
        }
    }
}
