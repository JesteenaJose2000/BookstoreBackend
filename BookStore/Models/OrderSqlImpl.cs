﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace BookStore.Models
{
    public class OrderSqlImpl :IOrderRepository
    {
        SqlConnection conn;
        SqlCommand comm;

        public OrderSqlImpl()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString);
            comm = new SqlCommand();
        }

        public decimal AddOrder(Order order)
        {
            comm.CommandText = "insert into [Order](TotalPrice,OrderStatus,DeliveryDate,AddressId,UserId) Values("+order.TotalPrice+",'"+order.OrderStatus+"','"+order.DeliveryDate+"',"+order.AddressId+","+order.UserId+")";
            comm.Connection = conn;
            conn.Open();
            int row = comm.ExecuteNonQuery();
            comm.CommandText = "Select @@Identity as newId from [Order]";
            var OrderId = comm.ExecuteScalar();
            conn.Close();
            if (row > 0)
            {
                Console.WriteLine(OrderId);
                return (decimal)OrderId;
            }
            return 0;
        }

        public void DeleteOrder(int OrderId)
        {
            comm.CommandText = "Delete from [Order] where OrderId=" + OrderId;
            comm.Connection = conn;
            conn.Open();
            int row = comm.ExecuteNonQuery();
            conn.Close();
        }

        public List<Order> GetOrders(int GUserId)
        {
            List<Order> orders = new List<Order>();
            comm.CommandText = "select * from [Order] Where UserId=" + GUserId;
            conn.Open();
            comm.Connection = conn;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                int OrderId = Convert.ToInt32(reader["OrderId"]);
                DateTime OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                int TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                string Status = reader["OrderStatus"].ToString();
                DateTime DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);
                int AddressId = Convert.ToInt32(reader["AddressId"]);
                int UserId = Convert.ToInt32(reader["UserId"]);
                orders.Add(new Order(OrderId,OrderDate,TotalPrice,Status,DeliveryDate,AddressId,UserId));

            }
            conn.Close();
            return orders;
        }

        public void UpdateOrder(int OrderId, Order order)
        {
            comm.CommandText = "Update [Order] set TotalPrice="+order.TotalPrice+",OrderStatus='"+order.OrderStatus+"',DeliveryDate='"+order.DeliveryDate+"',AddressId="+order.AddressId+"  where OrderId=" + OrderId;
            comm.Connection = conn;
            conn.Open();
            int row = comm.ExecuteNonQuery();
            conn.Close();
        }
    }
}