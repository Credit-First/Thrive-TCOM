using System;
using System.Data;
using TCom.Models;

namespace TCom
{
    class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isHeartBeat { get; set; }
        public bool PullLatestSKU { get; set; }
        public int Other_DTThreshold_Override { get; set; }
        public string lineFilters;
        public int caseCounterInterval { get; set; }

        public Account importDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0) return this;

            ID = int.Parse(dt.Rows[0]["id"].ToString());
            Username = Convert.ToString(dt.Rows[0]["Username"]);
            Password = Convert.ToString(dt.Rows[0]["Password"]);
            isHeartBeat = Convert.ToBoolean(dt.Rows[0]["isHeartBeat"]);
            PullLatestSKU = Convert.ToBoolean(dt.Rows[0]["PullLatestSKU"]);
            Other_DTThreshold_Override = dt.Rows[0]["Other_DTThreshold_Override"] is DBNull ? 0 : Convert.ToInt32(dt.Rows[0]["Other_DTThreshold_Override"]);
            lineFilters = Convert.ToString(dt.Rows[0]["lineFilters"]);
            caseCounterInterval = Convert.ToInt32(dt.Rows[0]["caseCounterInterval"]);

            return this;
        }

        public void login()
        {
            try
            {
                var a = new DataCollectionNode();
                a.Login(Username, Password);
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }
    }

    public enum TrackType
    {
        Heartbeat = 0,
        CaseCounter = 1,
        CaseCounterHB = 2,
        PieceCounter = 3,
        AlarmCode = 4,
        SKU = 5,
        Unknown
    }

    class Event
    {
        public static DataTable filterDataViewByTime(DataTable dataTable, int timeIndex)
        {
            DateTime lastWeek = DateTime.Now.AddDays(-7);

            foreach (DataRow row in dataTable.Rows)
            {
                var dataTime = new DateTime();
                try { dataTime = Convert.ToDateTime(row.ItemArray[timeIndex]); }
                catch (Exception e) { Session.logException(e); }

                if (lastWeek > dataTime)
                {
                    row.Delete();
                }
            }

            return dataTable;
        }

    }

    public static class Global
    {
        public const string Version = "2.0";
    }
}
