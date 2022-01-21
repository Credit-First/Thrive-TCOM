using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using AutomatedSolutions.Win.Comm;
using TCom.Models;
using Group = AutomatedSolutions.Win.Comm.MB.Master.Group;

namespace TCom
{
    internal class Session
    {
        public static MainForm MainForm;
        public static TS Service = new TS();
        private static readonly Dictionary<string, bool> updatingHeartbeats = new Dictionary<string, bool>();
        private static readonly Dictionary<string, bool> updatingCounters = new Dictionary<string, bool>();

        protected static int readRate = 500;
        public static Group gridStatusGroup = null;

        protected static int deviceTimeoutTransaction = 500;
        protected static int deviceTimeoutConnect = 500;
        protected static bool toolIsOne { get; set; }
        public static Account CurrentAccount { get; set; }
        public static DB DB { get; set; }
        public static List<Node> Nodes { get; set; }
        public static Channel MBChannel { get; set; }
        public static DataGridView lineGridView { get; set; }
        public static DataGridView logGridView { get; set; }

        public static bool isOn()
        {
            return toolIsOne;
        }

        public static void setReadRate(int rate)
        {
            readRate = rate;

            if (gridStatusGroup != null) gridStatusGroup.UpdateRate = readRate;
        }

        public static int getReadRate()
        {
            return readRate;
        }

        public static void syncLocalDowntimes()
        {
            if (Nodes == null || !Nodes.Any()) return;

            var dtEvents = DB.getPendingLocalDowntimeEventsListForSync();

            if (dtEvents == null || !dtEvents.Any()) return;

            foreach (DowntimeData dt in dtEvents)
            {
                dt.EventStart = dt.EventStart.ToUniversalTime();
                if (dt.EventStop.HasValue)
                {
                    dt.EventStop = dt.EventStop.Value.ToUniversalTime();
                }
                var node = Nodes.FirstOrDefault(n => n.LineName == dt.Line);
                if (node != null)
                {
                    dt.DeviceSetupId = node.DeviceSetupId;
                }
                //dt.SaveRemotely();
            }
        }

        public static void syncLocalCounters()
        {
            if (Nodes == null || !Nodes.Any()) return;

            var counters = DB.getCaseCounterDataTableForSync();

            if (counters == null || counters.Rows.Count <= 0) return;

            foreach (DataRow row in counters.Rows)
            {
                if (!DB.objectIsNullOrEmpty(row["casecountId"]) || DB.objectIsNullOrEmpty(row["EventStart"]) ||
                    DB.objectIsNullOrEmpty(row["EventStop"])) continue;

                var c = new CaseCount
                {
                    Client = Convert.ToString(row["Client"]),
                    Line = Convert.ToString(row["Line"]),
                    EventStart = Convert.ToDateTime(row["EventStart"]).ToUniversalTime(),
                    EventStop = Convert.ToDateTime(row["EventStop"]).ToUniversalTime(),
                    Count = Convert.ToInt32(row["Count"]),
                    DeviceSetupId = Convert.ToInt32(row["DeviceSetupId"]),
                    Saved = false,
                    LiteId = Convert.ToInt32(row["Id"]),
                    Timer = "SyncLocalCounter"
                };

                if (c.EventStop == null) continue;

                c.Save();
            }
        }

        public static void syncPieceCounters()
        {
            if (Nodes == null || !Nodes.Any()) return;

            var counters = DB.getPieceCounterDataTableForSync();

            if (counters == null || counters.Rows.Count <= 0) return;

            foreach (DataRow row in counters.Rows)
            {
                var c = new PieceCount
                {
                    Client = Convert.ToString(row["Client"]),
                    Line = Convert.ToString(row["Line"]),
                    Event = Convert.ToDateTime(row["Event"]),
                    Count = Convert.ToInt32(row["Count"]),
                    DeviceSetupId = Convert.ToInt32(row["DeviceSetupId"]),
                    Saved = false,
                    LiteId = Convert.ToInt32(row["Id"])
                };

                c.Save();
            }
        }

        public static int getDeviceTimeoutTransaction()
        {
            return deviceTimeoutTransaction;
        }

        public static int getDeviceTimeoutConnect()
        {
            return deviceTimeoutConnect;
        }

        public static void setDeviceTimeoutTransaction(int timeOut)
        {
            deviceTimeoutTransaction = timeOut;

            try
            {
                if (MBChannel == null) return;

                for (var i = 0; i < MBChannel.Devices.Count; i++)
                    MBChannel.Devices[i].TimeoutTransaction = deviceTimeoutTransaction;
            }
            catch (Exception e)
            {
                logException(e);
            }
        }

        public static void setDeviceTimeoutConnect(int timeOut)
        {
            deviceTimeoutConnect = timeOut;

            try
            {
                if (MBChannel == null) return;

                for (var i = 0; i < MBChannel.Devices.Count; i++)
                    MBChannel.Devices[i].TimeoutConnect = deviceTimeoutConnect;
            }
            catch (Exception e)
            {
                logException(e);
            }
        }

        public static void turn(bool on)
        {
            toolIsOne = on;

            try
            {
                if (MBChannel == null) return;

                for (var i = 0; i < MBChannel.Devices.Count; i++)
                    for (var x = 0; x < MBChannel.Devices[i].Groups.Count; x++)
                        MBChannel.Devices[i].Groups[x].Active = toolIsOne;
            }
            catch (Exception e)
            {
                logException(e);
            }
        }

        public static bool setLineCounterUpdateStatus(string line, bool u)
        {
            if (!updatingCounters.Keys.Contains(line))
                updatingCounters[line] = u;

            updatingCounters[line] = u;

            return u;
        }

        public static bool beingCounterUpdated(string line)
        {
            if (!updatingCounters.Keys.Contains(line))
                updatingCounters[line] = false;

            return updatingCounters[line];
        }

        public static bool setLineHBUpdateStatus(string line, bool u)
        {
            if (!updatingHeartbeats.Keys.Contains(line))
                updatingHeartbeats[line] = u;

            updatingHeartbeats[line] = u;

            return u;
        }

        public static bool beingHBUpdated(string line)
        {
            if (!updatingHeartbeats.Keys.Contains(line))
                updatingHeartbeats[line] = false;

            return updatingHeartbeats[line];
        }

        public static void turnOff()
        {
            turn(false);
        }

        public static void turnOn()
        {
            turn(true);
        }

        public static void logException(Exception exception, bool saveToLocal = true)
        {
            if (!(exception is SocketException))
                log(exception.Message + "***** Stacktrace: " + exception.StackTrace, saveToLocal);
        }

        public static void log(string mes, bool saveToLocal = true)
        {
            if (saveToLocal) DB.log(mes);

            if (logGridView == null) return;

            if (logGridView.InvokeRequired)
            {
                refreshLogCallback fn = log;

                logGridView.Invoke(fn, mes, false);
                return;
            }

            logGridView.DataSource = DB.getLogDatatable();
        }

        public static void refreshLineGridView(bool clear = false)
        {
            if (clear)
            {
                if (lineGridView.InvokeRequired)
                {
                    clearLineGridCallback fn = refreshLineGridView;

                    lineGridView.Invoke(fn, true);
                    return;
                }

                var dt = (DataTable)lineGridView.DataSource;
                dt.Clear();
                lineGridView.DataSource = dt;
            }

            if (Nodes == null) return;

            foreach (Node node in Nodes)
                foreach (NodeLine nodeLine in node.NodeLines.Keys) //.Where(o => o.Value == true).Select(o => o.Key))
                    syncLineGrid(nodeLine);
        }

        public static void syncLineGrid(NodeLine nodeLine, bool highlight = false)
        {
            if (nodeLine == null || lineGridView.IsDisposed) return;

            if (lineGridView.InvokeRequired)
            {
                refreshLineGridCallback fn = syncLineGrid;

                lineGridView.Invoke(fn, nodeLine, highlight);
                return;
            }

            var dt = (DataTable)lineGridView.DataSource;

            var row = (from o in dt.AsEnumerable()
                       where o.Field<string>("Line") == nodeLine.Line
                             && o.Field<string>("Tag Name") == nodeLine.TagName
                             && o.Field<string>("Type") == nodeLine.TrackType.ToString()
                       select o).FirstOrDefault();

            if (row != null)
            {
                row["Type"] = nodeLine.TrackType.ToString();
                row["Line"] = nodeLine.Line;
                row["Node IP"] = nodeLine.IpAddress;
                row["Quality"] = nodeLine.Quality;
                row["Value"] = nodeLine.getValue();
                row["Tag Name"] = nodeLine.TagName;
                row["Downtime Threshold"] = nodeLine.DowntimeThreshold;
                row["Uptime Threshold"] = nodeLine.UptimeThreshold;
            }
            else
            {
                row = dt.NewRow();
                row["Type"] = nodeLine.TrackType.ToString();
                row["Line"] = nodeLine.Line;
                row["Node IP"] = nodeLine.IpAddress;
                row["Quality"] = nodeLine.Quality;
                row["Value"] = nodeLine.getValue();
                row["Tag Name"] = nodeLine.TagName;
                row["Downtime Threshold"] = nodeLine.DowntimeThreshold;
                row["Uptime Threshold"] = nodeLine.UptimeThreshold;
                dt.Rows.Add(row);
            }

            lineGridView.DataSource = dt;
        }

        public static bool DoOrphanDTEventsExist(string line)
        {
            return DB.DoOrphanDTEventsExist(line);
        }

        public static bool DoOverlappingDTEventsExist(string line)
        {
            return DB.DoOverlappingDTEventsExist(line);
        }

        public static void disposeDevices()
        {
            if (MBChannel == null || Nodes == null) return;

            foreach (MBNode node in Nodes)
                foreach (MBNodeLine hb in node.NodeLines.Select(o => o.Key))
                {
                    if (hb.Item == null) continue;
                    hb.Dispose();
                }

            Nodes = null;

            MBChannel.Dispose();
            MBChannel = null;
        }

        private delegate void refreshLogCallback(string mes, bool stoL);

        private delegate void clearLineGridCallback(bool clear);

        private delegate void refreshLineGridCallback(NodeLine nl, bool hl);
    }
}