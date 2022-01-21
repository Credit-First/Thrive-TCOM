using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using System.Data;
using TCom.Models;

namespace TCom
{
    class DB
    {
        protected SQLiteConnection currentConnection = null;
        
        protected string tbSendData = "SendData";

        public DB()
        {
            try
            {
                if (!File.Exists("data.sqlite")) File.Create("data.sqlite");

                createConnection();
                createCaseCountTable();
                createProxyColumns();
                createStatusLogColumns();
                createLineFilterColumns();
                
                createSendDataTable();
                addColumnsForAlarmCodeSKU();
            }
            catch (Exception e)
            {
                Session.log(e.Message, false);
            }
        }

        protected void addColumnsForAlarmCodeSKU()
        {
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("AlarmCode", "INTEGER NOT NULL DEFAULT 0");
            columns.Add("SKU", "INTEGER NOT NULL DEFAULT 0");
            addColumns("downtimeevent", columns);
        }

        protected List<string> getColumnNames(string tableName)
        {
            using (SQLiteConnection con = createConnection())
            {
                try
                {
                    var cmd = con.CreateCommand();
                    cmd.CommandText = "PRAGMA table_info(" + tableName + ");";
                    var adp = new SQLiteDataAdapter(cmd);
                    var table = new DataTable();
                    adp.Fill(table);
                    var res = new List<string>();
                    for (int i = 0; i < table.Rows.Count; i++)
                        res.Add(table.Rows[i]["name"].ToString());
                    return res;
                }
                catch (Exception ex) { }

            }
            return new List<string>();
        }

        protected void addColumns(string tableName, Dictionary<string, string> columnFormats)
        {
            string q = "BEGIN TRANSACTION;";
            var names = getColumnNames(tableName);
            foreach (KeyValuePair<string, string> column in columnFormats)
            {
                var name = names.Where(na => na == column.Key).FirstOrDefault();
                if (name == null)
                {
                    q += $" ALTER TABLE '{tableName}' ADD '{column.Key}' {column.Value};";
                }
            }
            q += " COMMIT;";
            using (SQLiteConnection con = createConnection())
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
            }
        }

        protected void createCaseCountTable()
        {
            if (doesCaseCountTableExist()) return;
            using (SQLiteConnection con = createConnection())
            {
                const string q = "CREATE TABLE \"casecount\" (\"Id\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, \"casecountId\" INTEGER, \"EventStart\" DATETIME, \"EventStop\" DATETIME, \"Saved\" INTEGER, \"Line\" TEXT, \"Client\" TEXT, \"Count\" INTEGER);";
                var cmd = con.CreateCommand();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
            }
        }
        
        protected void createSendDataTable()
        {
            if (doesTableExist(tbSendData)) return;

            Dictionary<string, string> tableStructure = new Dictionary<string, string>();
            tableStructure.Add("Id", "INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE");
            tableStructure.Add("EventTime", "DATETIME NOT NULL");
            tableStructure.Add("RequestString", "TEXT NOT NULL");
            tableStructure.Add("ResponseString", "TEXT NULL");
            tableStructure.Add("CommandName", "TEXT NOT NULL");

            createTable(tbSendData, tableStructure);
        }

        public void clearOldSendData()
        {
            using (SQLiteConnection con = createConnection())
            {
                string sql = $"DELETE FROM {tbSendData} WHERE EventTime <= date('now','-14 day')";
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        public long createSendDataLog(string requestString, string commnadName)
        {
            using (SQLiteConnection con = createConnection())
            {
                var time = DateTime.Now;
                string sql = $"INSERT INTO {tbSendData}(RequestString, EventTime, CommandName) VALUES('{requestString}','{time}','{commnadName}'); SELECT last_insert_rowid();";
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                object obj = cmd.ExecuteScalar();
                long id = (long)obj;
                return id;
            }
        }
        public void updateSendDataLog(long id, string responseString)
        {
            using (SQLiteConnection con = createConnection())
            {
                string sql = $"UPDATE {tbSendData} SET ResponseString = '{responseString}' WHERE Id = {id}";
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
        }

        private void createTable(string tableName, Dictionary<string, string> tableStructure)
        {
            using (SQLiteConnection con = createConnection())
            {
                string query = $"CREATE TABLE \"{tableName}\" (";
                foreach (string key in tableStructure.Keys)
                {
                    string value = tableStructure[key];
                    query += $"\"{key}\" {value}, ";
                }
                query = query.Substring(0, query.Length - 2);
                query += ")";

                var cmd = con.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }

        private bool doesTableExist(string tableName)
        {
            try
            {
                using (var con = createConnection())
                {
                    string q = $"SELECT * FROM {tableName} LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void createProxyColumns()
        {
            if (doesProxyColumnsExist()) return;
            using (SQLiteConnection con = createConnection())
            {
                const string q = @"BEGIN TRANSACTION; ALTER TABLE 'account' ADD 'hasProxy' BOOLEAN NOT NULL DEFAULT false; ALTER TABLE 'account' ADD 'proxyUsername' TEXT; ALTER TABLE 'account' ADD 'proxyPassword' TEXT; ALTER TABLE 'account' ADD 'proxyUrl' TEXT; ALTER TABLE 'account' ADD 'proxyDomain' TEXT; COMMIT;";
                var cmd = con.CreateCommand();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
            }
        }

        protected void createLineFilterColumns()
        {
            if (doesLineFilterColumnsExist()) return;
            using (SQLiteConnection con = createConnection())
            {
                const string q = @"BEGIN TRANSACTION; ALTER TABLE 'account' ADD 'lineFilters' TEXT; COMMIT;";
                var cmd = con.CreateCommand();
                cmd.CommandText = q;
                cmd.ExecuteNonQuery();
            }
        }

        protected void createStatusLogColumns()
        {
            if (doesStatusLogTableExist()) return;
            using (SQLiteConnection con = createConnection())
            {
                const string acctQuery = @"CREATE TABLE statuslog (id INTEGER PRIMARY KEY, client TEXT, line TEXT, status TINYINT(1), loggedAt DATETIME DEFAULT CURRENT_TIMESTAMP);";
                var cmd = con.CreateCommand();
                cmd.CommandText = acctQuery;
                cmd.ExecuteNonQuery();
            }
        }

        protected SQLiteConnection createConnection()
        {
            var con = new SQLiteConnection("Data Source=data.sqlite;Version=3");
            con.Open();
            return con;
        }

        public CaseCount saveCaseCounter(CaseCount c)
        {
            if (c == null) return null;

            try
            {
                using (var con = createConnection())
                {
                    var q = "INSERT OR REPLACE INTO casecount (casecountId, EventStart, EventStop, Count, Saved, Line, Client, Timer, DeviceSetupId) VALUES (:casecountId, :EventStart, :EventStop, :Count, :Saved, :Line, :Client, :Timer, :DeviceSetupId); SELECT last_insert_rowid()";
                    var cmd = con.CreateCommand();

                    if (c.LiteId > 0)
                    {
                        q = "INSERT OR REPLACE INTO casecount (ID, casecountId, EventStart, EventStop, Count, Saved, Line, Client, Timer, DeviceSetupId) VALUES (:Id, :casecountId, :EventStart, :EventStop, :Count, :Saved, :Line, :Client, :Timer, :DeviceSetupId);";

                        cmd.Parameters.Add(":Id", DbType.Int32).Value = c.LiteId;
                    }

                    cmd.CommandText = q;

                    cmd.Parameters.Add(":casecountId", DbType.Int32).Value = c.Id;
                    cmd.Parameters.Add(":EventStart", DbType.DateTime).Value = c.EventStart;
                    cmd.Parameters.Add(":EventStop", DbType.DateTime).Value = c.EventStop;
                    cmd.Parameters.Add(":Count", DbType.Decimal).Value = c.Count;
                    cmd.Parameters.Add(":Saved", DbType.Boolean).Value = c.Saved;
                    cmd.Parameters.Add(":Line", DbType.String).Value = c.Line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = c.Client;
                    cmd.Parameters.Add(":Timer", DbType.String).Value = c.Timer;
                    cmd.Parameters.Add(":DeviceSetupId", DbType.Int32).Value = c.DeviceSetupId;

                    if (c.LiteId > 0)
                        cmd.ExecuteNonQuery();
                    else
                    {
                        var id = cmd.ExecuteScalar();
                        c.LiteId = Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
            return c;
        }

        public PieceCount savePieceCount(PieceCount c)
        {
            if (c == null) return null;

            try
            {
                using (var con = createConnection())
                {
                    var q = "INSERT OR REPLACE INTO PieceCount (PieceCountId, Event, Saved, Line, Client, Count, DeviceSetupId) VALUES (:PieceCountId, :Event, :Saved, :Line, :Client, :Count, :DeviceSetupId); SELECT last_insert_rowid()";
                    var cmd = con.CreateCommand();

                    if (c.LiteId > 0)
                    {
                        q = "INSERT OR REPLACE INTO PieceCount (Id, PieceCountId, Event, Saved, Line, Client, Count, DeviceSetupId) VALUES (:Id, :PieceCountId, :Event, :Saved, :Line, :Client, :Count, :DeviceSetupId);";

                        cmd.Parameters.Add(":Id", DbType.Int32).Value = c.LiteId;
                    }

                    cmd.CommandText = q;

                    cmd.Parameters.Add(":PieceCountId", DbType.Int32).Value = c.Id;
                    cmd.Parameters.Add(":Event", DbType.DateTime).Value = c.Event;
                    cmd.Parameters.Add(":Count", DbType.Decimal).Value = c.Count;
                    cmd.Parameters.Add(":Saved", DbType.Boolean).Value = c.Saved;
                    cmd.Parameters.Add(":Line", DbType.String).Value = c.Line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = c.Client;
                    cmd.Parameters.Add(":DeviceSetupId", DbType.Int32).Value = c.DeviceSetupId;

                    if (c.LiteId > 0)
                        cmd.ExecuteNonQuery();
                    else
                    {
                        var id = cmd.ExecuteScalar();
                        c.LiteId = Convert.ToInt32(id);
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
            return c;
        }

        public void insertStatusLogEvent(string client, string line, bool status, DateTime eventTime)
        {
            try
            {
                using (var con = createConnection())
                {
                    var cmd = con.CreateCommand();
                    const string q = "INSERT INTO statuslog (client, line, status, loggedAt) VALUES (:Client, :Line, :Status, :Time);";
                    cmd.CommandText = q;

                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = client;
                    cmd.Parameters.Add(":Status", DbType.Boolean).Value = status;
                    cmd.Parameters.Add(":Time", DbType.DateTime).Value = eventTime;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }


        public DowntimeData saveDowntimeEvent(DowntimeData dt)
        {
            if (dt == null) return null;

            try
            {
                using (var con = createConnection())
                {
                    var q = "INSERT OR REPLACE INTO downtimeevent (downtimeId, EventStart, EventStop, Minutes, Saved, Line, Client, SKU, AlarmCode) VALUES (:downtimeId, :EventStart, :EventStop, :Minutes, :Saved, :Line, :Client, :SKU, :AlarmCode); SELECT last_insert_rowid()";
                    var cmd = con.CreateCommand();

                    if (dt.LiteId > 0)
                    {
                        q = "INSERT OR REPLACE INTO downtimeevent (ID, downtimeId, EventStart, EventStop, Minutes, Saved, Line, Client, SKU, AlarmCode) VALUES (:Id, :downtimeId, :EventStart, :EventStop, :Minutes, :Saved, :Line, :Client, :SKU, :AlarmCode);";

                        cmd.Parameters.Add(":Id", DbType.Int32).Value = dt.LiteId;
                    }

                    cmd.CommandText = q;

                    cmd.Parameters.Add(":downtimeId", DbType.Int32).Value = dt.ID;
                    cmd.Parameters.Add(":EventStart", DbType.DateTime).Value = dt.EventStart;
                    cmd.Parameters.Add(":EventStop", DbType.DateTime).Value = dt.EventStop;
                    cmd.Parameters.Add(":Minutes", DbType.Decimal).Value = dt.Minutes;
                    cmd.Parameters.Add(":Saved", DbType.Boolean).Value = dt.SavedRemotely;
                    cmd.Parameters.Add(":Line", DbType.String).Value = dt.Line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = dt.Client;
                    cmd.Parameters.Add(":SKU", DbType.Int32).Value = dt.SKU;
                    cmd.Parameters.Add(":AlarmCode", DbType.Int32).Value = dt.AlarmCode;

                    if (dt.LiteId > 0)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        var id = cmd.ExecuteScalar();
                        dt.LiteId = Convert.ToInt32(id);
                        dt.SavedLocally = true;
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return dt;
        }

        public void UpdateDowntimeIdLocally(DowntimeLiteIdPair pair)
        {
            try
            {
                using (var con = createConnection())
                {
                    var cmd = con.CreateCommand();
                    const string q = "UPDATE downtimeevent SET downtimeId = :downtimeId WHERE Id = :Id;";
                    cmd.CommandText = q;

                    cmd.Parameters.Add(":downtimeId", DbType.String).Value = pair.downtime_id;
                    cmd.Parameters.Add(":Id", DbType.String).Value = pair.lite_id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public static bool objectIsNullOrEmpty(object obj)
        {
            return obj == null || obj == DBNull.Value;
        }

        public DowntimeData datarowToDowntimeEvent(DataRow row)
        {
            try
            {
                var dEvent = new DowntimeData();
                if (row == null) return dEvent;

                if (!objectIsNullOrEmpty(row["downtimeId"]))
                    dEvent.ID = Convert.ToInt32(row["downtimeId"]);

                dEvent.LiteId = Convert.ToInt32(row["Id"]);

                if (!objectIsNullOrEmpty(row["EventStart"]))
                    dEvent.EventStart = Convert.ToDateTime(row["EventStart"]);

                if (!objectIsNullOrEmpty(row["EventStop"]))
                    dEvent.EventStop = Convert.ToDateTime(row["EventStop"]);

                if (!objectIsNullOrEmpty(row["Minutes"]))
                    dEvent.Minutes = Convert.ToDecimal(row["Minutes"]);

                dEvent.SavedRemotely = Convert.ToBoolean(row["Saved"]);
                dEvent.Line = Convert.ToString(row["Line"]);
                dEvent.Client = Convert.ToString(row["Client"]);
                dEvent.AlarmCode = Convert.ToInt32(row["AlarmCode"]);
                dEvent.SKU = Convert.ToInt32(row["SKU"]);

                return dEvent;
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return null;
        }

        public DowntimeData datatableToDowntimeEvent(DataTable dt)
        {
            return dt != null ? datarowToDowntimeEvent(dt.Rows[0]) : new DowntimeData();
        }

        public List<DowntimeData> datatableToDowntimeEvents(DataTable dt)
        {
            return (from DataRow row in dt.Rows select datarowToDowntimeEvent(row)).ToList();
        }

        public DataTable getStatusLogDataTable()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM statuslog WHERE loggedat > date('now','-7 day') ORDER BY loggedAt desc LIMIT 0, 30";
                    var cmd = con.CreateCommand();

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getLocalDowntimeEventsDataTable(int id, bool isDTId = false)
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM downtimeevent WHERE Id = :id AND EventStart > date('now','-7 day')";

                    if (isDTId)
                        q = "SELECT * FROM downtimeevent WHERE downtimeId = :id AND EventStart > date('now','-7 day')";

                    var cmd = con.CreateCommand();

                    cmd.CommandText = q;
                    cmd.Parameters.Add(":id", DbType.Int32).Value = id;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getLocalCounterDataTable(string line = "")
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM casecount ORDER BY EventStart desc LIMIT 0, 30";

                    var cmd = con.CreateCommand();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower() != "all")
                        {
                            q = "SELECT * FROM casecount WHERE Line = :line ORDER BY EventStart desc LIMIT 0, 30";
                            cmd.Parameters.Add(":line", DbType.String).Value = line;
                        }
                    }

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getCaseCounterDataTableForSync(string line = "")
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM casecount WHERE casecountId IS NULL ORDER BY EventStart desc";

                    var cmd = con.CreateCommand();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower() != "all")
                        {
                            q = "SELECT * FROM casecount WHERE Line = :line AND casecountId IS NULL ORDER BY EventStart desc";
                            cmd.Parameters.Add(":line", DbType.String).Value = line;
                        }
                    }

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getLocalPieceCountDataTable(string line = "")
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM PieceCount ORDER BY Event desc LIMIT 0, 30";

                    var cmd = con.CreateCommand();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower() != "all")
                        {
                            q = "SELECT * FROM PieceCount WHERE Line = :line ORDER BY Event desc LIMIT 0, 30";
                            cmd.Parameters.Add(":line", DbType.String).Value = line;
                        }
                    }

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getPieceCounterDataTableForSync(string line = "")
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM PieceCount WHERE PieceCountId IS NULL AND Event IS NOT NULL ORDER BY Event desc";

                    var cmd = con.CreateCommand();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower() != "all")
                        {
                            q = "SELECT * FROM PieceCount WHERE PieceCountId IS NULL AND Event IS NOT NULL Line = :line ORDER BY Event";
                            cmd.Parameters.Add(":line", DbType.String).Value = line;
                        }
                    }

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getLocalDowntimeEventsDataTable(string line = "")
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT * FROM downtimeevent WHERE EventStart > date('now','-7 day') ORDER BY Id desc LIMIT 0, 100";
                    var cmd = con.CreateCommand();

                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.ToLower() != "all")
                        {
                            q = "SELECT * FROM downtimeevent WHERE Line = :line AND EventStart > date('now','-7 day') ORDER BY Id desc LIMIT 0, 100";
                            cmd.Parameters.Add(":line", DbType.String).Value = line;
                        }
                    }

                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public int getLastCaseCounterValue(string client, string line)
        {
            int count = 0;
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT MAX(Count) FROM CaseCount WHERE Line = :line AND Client = :client";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.Parameters.Add(":client", DbType.String).Value = client;
                    cmd.Parameters.Add(":line", DbType.String).Value = line;
                    object maxCount = cmd.ExecuteScalar();
                    if (maxCount != DBNull.Value)
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return count;
        }

        public int getLastPieceCountValue(string client, string line)
        {
            int count = 0;
            try
            {
                using (var con = createConnection())
                {
                    var q = "SELECT MAX(Count) FROM PieceCount WHERE Line = :line AND Client = :client";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.Parameters.Add(":client", DbType.String).Value = client;
                    cmd.Parameters.Add(":line", DbType.String).Value = line;
                    object maxCount = cmd.ExecuteScalar();
                    if (maxCount != DBNull.Value)
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return count;
        }

        public void removeDowntimeEvent(int liteId)
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "DELETE FROM downtimeevent WHERE Id = :id";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.Parameters.Add(":id", DbType.Int32).Value = liteId;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }


        }

        public void removeAllTemporaryDowntimeEvent(string line)
        {
            var events = getLocalTemporaryDowntimeEvents(line);

            try
            {
                using (var con = createConnection())
                {
                    var ids = (from o in events select o.LiteId).ToList();
                    const string q = "DELETE FROM downtimeevent WHERE Id IN (:ids)";
                    var cmd = con.CreateCommand();

                    cmd.CommandText = q;
                    cmd.Parameters.Add(":ids", DbType.String).Value = string.Join(",", ids.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }


        }
        public void TrimAllAfterOneWeek()
        {
            try
            {
                using (var con = createConnection())
                {
                    var date = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));

                    const string casecount = "DELETE FROM casecount WHERE EventStart <= :date";
                    const string downtimeevent = "DELETE FROM downtimeevent WHERE EventStart <= :date and Id not in (select Id from downtimeevent order by Id asc limit 10)";
                    const string log = "DELETE FROM log WHERE Added <= :date";
                    const string statuslog = "DELETE FROM statuslog WHERE loggedAt <= :date";

                    var cmd = con.CreateCommand();
                    cmd.CommandText = casecount;
                    cmd.Parameters.Add(":date", DbType.DateTime).Value = date;
                    cmd.ExecuteNonQuery();

                    cmd = con.CreateCommand();
                    cmd.CommandText = downtimeevent;
                    cmd.Parameters.Add(":date", DbType.DateTime).Value = date;
                    cmd.ExecuteNonQuery();

                    cmd = con.CreateCommand();
                    cmd.CommandText = log;
                    cmd.Parameters.Add(":date", DbType.DateTime).Value = date;
                    cmd.ExecuteNonQuery();

                    cmd = con.CreateCommand();
                    cmd.CommandText = statuslog;
                    cmd.Parameters.Add(":date", DbType.DateTime).Value = date;
                    cmd.ExecuteNonQuery();

                    cmd = con.CreateCommand();
                    cmd.CommandText = "vacuum;";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public List<DowntimeData> getLocalTemporaryDowntimeEvents(string line)
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM downtimeevent WHERE Line = :Line AND Client = :Client AND EventStop IS NULL ORDER BY Id LIMIT 0, 30";
                    var cmd = con.CreateCommand();

                    cmd.CommandText = q;
                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = Session.CurrentAccount.Username;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt.Rows.Count > 0 ? datatableToDowntimeEvents(dt) : new List<DowntimeData>();
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new List<DowntimeData>();

        }

        public DowntimeData getLocalTemporaryDowntEvent(string line)
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM downtimeevent WHERE Line = :Line AND Client = :Client AND EventStop IS NULL ORDER BY Id LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":Client", DbType.String).Value = Session.CurrentAccount.Username;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count <= 0) return null;

                    var de = datarowToDowntimeEvent(dt.Rows[0]);
                    de.EventStart = de.EventStart.ToUniversalTime();
                    de.EventStop = null;

                    return de;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return null;

        }

        public List<DowntimeData> getLocalDowntimeEventsList()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM downtimeevent";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return datatableToDowntimeEvents(dt);
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new List<DowntimeData>();

        }

        public List<DowntimeData> getPendingLocalDowntimeEventsListForSync()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM downtimeevent WHERE Saved = 0";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return datatableToDowntimeEvents(dt);
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new List<DowntimeData>();
        }

        public bool doesStatusLogTableExist()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM statuslog LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool doesCaseCountTableExist()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM casecount LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool doesProxyColumnsExist()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT hasProxy FROM account LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool doesLineFilterColumnsExist()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT lineFilters FROM account LIMIT 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Account getCurrentAccount()
        {
            try
            {
                var acct = new Account();

                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM account WHERE Id = 1";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return acct.importDataTable(dt);
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return null;

        }

        public Account saveAccount(Account acct)
        {
            if (acct == null || string.IsNullOrEmpty(acct.Username)) return null;

            try
            {
                using (var con = createConnection())
                {
                    const string q = "INSERT OR REPLACE INTO account (Id, Username, Password, isModBus, isHeartbeat, lineFilters, caseCounterInterval) VALUES (:id, :username, :password, :isModBus, :isHeartBeat, :lineFilters, :caseCounterInterval);";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    cmd.Parameters.Add(":id", DbType.Int32).Value = 1;
                    cmd.Parameters.Add(":username", DbType.String).Value = acct.Username;
                    cmd.Parameters.Add(":password", DbType.String).Value = acct.Password;
                    cmd.Parameters.Add(":isModBus", DbType.Boolean).Value = true;
                    cmd.Parameters.Add(":isHeartBeat", DbType.Boolean).Value = acct.isHeartBeat;
                    cmd.Parameters.Add(":lineFilters", DbType.String).Value = acct.lineFilters;
                    cmd.Parameters.Add(":caseCounterInterval", DbType.Int32).Value = acct.caseCounterInterval;

                    cmd.ExecuteNonQuery();

                    return acct;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return null;

        }

        public void log(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            try
            {
                using (var con = createConnection())
                {
                    const string q = "INSERT INTO log (Added, Message) VALUES (:added, :message);";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    cmd.Parameters.Add(":added", DbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add(":message", DbType.String).Value = message;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Session.log(e.Message, false);
            }

        }

        public DataTable getLogDatatable()
        {
            try
            {
                using (var con = createConnection())
                {
                    const string q = "SELECT * FROM log WHERE Added > date('now','-7 day') ORDER BY added desc";
                    var cmd = con.CreateCommand();
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();

        }

        public bool updateAccount(int accountId, bool pullLatestSKU, int other_DTThreshold_Override)
        {
            bool result = false;
            using (var con = createConnection())
            {

                var cmd = new SQLiteCommand("Update account set pullLatestSKU= @pullLatestSKU,other_DTThreshold_Override=@otherDTThresholdOverride where id=@id", con);
                cmd.Parameters.Add("@pullLatestSKU", DbType.Int32).Value = pullLatestSKU ? 1 : 0;
                cmd.Parameters.Add("@otherDTThresholdOverride", DbType.Int32).Value = other_DTThreshold_Override;
                cmd.Parameters.Add("@id", DbType.Int32).Value = accountId;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = true;
                }


            }
            return result;
        }

        public bool DoOrphanDTEventsExist(string line)
        {
            using (var con = createConnection())
            {
                const string q = "select 1 from DowntimeEvent where Line = :Line and EventStop is null and Id < (select Id from DowntimeEvent where Line = :Line and EventStop is not null order by Id desc limit 1)";
                var cmd = con.CreateCommand();
                cmd.Parameters.Add(":Line", DbType.String).Value = line;
                cmd.CommandText = q;
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public bool DoOverlappingDTEventsExist(string line)
        {
            using (var con = createConnection())
            {
                const string q = " SELECT 1 FROM (SELECT a.downtimeid, a.id, a.EventStart, a.EventStop, (SELECT EventStart FROM DowntimeEvent b WHERE b.Id > a.Id AND b.Line = :Line LIMIT 1) NextEventStart" +
                                 " FROM DowntimeEvent a WHERE a.Line = :Line ORDER BY a.EventStart) allrec WHERE allrec.NextEventStart > allrec.EventStart AND allrec.NextEventStart < allrec.EventStop ORDER BY id DESC;";
                var cmd = con.CreateCommand();
                cmd.Parameters.Add(":Line", DbType.String).Value = line;
                cmd.CommandText = q;
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public DataTable getDTEventsAccuracyDataTable(string line, DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = " SELECT strftime('%Y-%m-%d', Eventstart) AS ForDate, strftime('%H', Eventstart) AS OnHour, COUNT(*) AS TotalCounts" +
                            " FROM downtimeevent" +
                            " WHERE Line = :Line" +
                            " GROUP BY strftime('%Y-%m-%d', Eventstart), strftime('%H', Eventstart)" +
                            " HAVING date(EventStart) BETWEEN date(:FromDate) AND date(:ToDate)";

                    var cmd = con.CreateCommand();
                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":FromDate", DbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add(":ToDate", DbType.DateTime).Value = toDate;
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getCaseCountsAccuracyDataTable(string line, DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = " SELECT strftime('%Y-%m-%d', Eventstart) AS ForDate, strftime('%H', Eventstart) AS OnHour, COUNT(*) AS TotalCounts" +
                            " FROM casecount" +
                            " WHERE Line = :Line" +
                            " GROUP BY strftime('%Y-%m-%d', Eventstart), strftime('%H', Eventstart)" +
                            " HAVING date(EventStart) BETWEEN date(:FromDate) AND date(:ToDate)";

                    var cmd = con.CreateCommand();
                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":FromDate", DbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add(":ToDate", DbType.DateTime).Value = toDate;
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }

        public DataTable getPieceCountsAccuracyDataTable(string line, DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (var con = createConnection())
                {
                    var q = " SELECT strftime('%Y-%m-%d', Event) AS ForDate, strftime('%H', Event) AS OnHour, COUNT(*) AS TotalCounts" +
                            " FROM PieceCount" +
                            " WHERE Line = :Line" +
                            " GROUP BY strftime('%Y-%m-%d', Event), strftime('%H', Event)" +
                            " HAVING date(Event) BETWEEN date(:FromDate) AND date(:ToDate)";

                    var cmd = con.CreateCommand();
                    cmd.Parameters.Add(":Line", DbType.String).Value = line;
                    cmd.Parameters.Add(":FromDate", DbType.DateTime).Value = fromDate;
                    cmd.Parameters.Add(":ToDate", DbType.DateTime).Value = toDate;
                    cmd.CommandText = q;

                    var adp = new SQLiteDataAdapter(cmd);
                    var dt = new DataTable();
                    adp.Fill(dt);

                    return dt;
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return new DataTable();
        }
    }
}
