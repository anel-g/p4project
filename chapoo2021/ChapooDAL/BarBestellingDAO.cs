﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using ChapooModel;

namespace ChapooDAL
{
    public class BarBestellingDAO : Base
    {
        public List<BarBestellingModel> Db_Get_All_Bar_Bestellingen()
        {
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery("GetAllBarBestellingen", sqlParameters));
        }
        private List<BarBestellingModel> ReadTables(DataTable dataTable)
        {
            List<BarBestellingModel> barOrderList = new List<BarBestellingModel>();
            foreach (DataRow dr in dataTable.Rows)
            {
                    BarBestellingModel bbestelItem = new BarBestellingModel()
                    {
                        OrderItemID = (int)dr["orderItemId"],
                        MenuItemId = (int)dr["menuItemId"],
                        OrderItemStatus = (Boolean)dr["orderItemStatus"],
                        orderTableId = (int)dr["orderTableId"],
                        Price = (Decimal)dr["price"],
                        Name = (string)(dr["itemName"].ToString())
                    };
                    barOrderList.Add(bbestelItem);

            }
            return barOrderList;
        }
    }
}
