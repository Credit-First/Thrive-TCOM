using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace TCom.Models
{
    public class DowntimeData
    {
        public int DeviceSetupId { get; set; }
        public int ID { get; set; }
        public int LiteId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime? EventStop { get; set; }
        public string Line { get; set; }
        public string Timer { get; set; }
        public string Client { get; set; }
        public decimal? Seconds { get; set; }
        public bool Terminated => EventStop.HasValue && EventStop.Value > EventStart;

        public decimal? Minutes
        {
            get
            {
                return (Seconds.HasValue ? Convert.ToDecimal(Seconds / 60) : default(decimal?));
            }
            set
            {
                Seconds = Convert.ToInt32(value * 60);
            }
        }

        public int AlarmCode { get; set; }
        public int SKU { get; set; }
        public bool SavedLocally { get; set; }
        public bool SavedRemotely { get; set; }

        public DowntimeData()
        {
            SavedLocally = false;
            SavedRemotely = false;
            ID = -1;
            AlarmCode = 0;
            SKU = 0;
            EventStop = null;
        }

        public void Save()
        {
            if (EventStart >= EventStop)
            {
                ID = -4;
                return;
            }

            if(this.DeviceSetupId != 0)
            {
                //AlarmCode = RetrieveCurrentAlarmCode();
                //SKU = RetrieveCurrentSKU();
                SaveLocally();
                SaveRemotely();

            }
        }

        // Get AlarmCode value
        private int RetrieveCurrentAlarmCode()
        {
            var node = Session.Nodes.OfType<MBNode>().FirstOrDefault(n => n.LineName == Line);
            if (node == null) return 0; // This shouldn't happen!
            var alarmKey = node.NodeLines.FirstOrDefault(nl => nl.Key is MBHybrid && ((MBHybrid)nl.Key).TrackType == TrackType.AlarmCode);

            // Empty Alarm Code
            if (alarmKey.Equals(new KeyValuePair<object, bool>())) return 0;

            MBHybrid alarm = (MBHybrid)alarmKey.Key;

            if (alarm == null) return -1;

            return alarm.getValue();
        }

        // Get SKU value
        private int RetrieveCurrentSKU()
        {
            var node = Session.Nodes.OfType<MBNode>().FirstOrDefault(n => n.LineName == Line);
            if (node == null) return 0; // This shouldn't happen!
            var skuKey = node.NodeLines.FirstOrDefault(nl => nl.Key is MBHybrid && ((MBHybrid)nl.Key).TrackType == TrackType.SKU);

            // Empty SKU Code
            if (skuKey.Equals(new KeyValuePair<object, bool>())) return 0;

            MBHybrid sku = (MBHybrid)skuKey.Key;

            if (sku == null) return -1;

            return sku.getValue();
        }

        public void SaveRemotely()
        {
            DowntimeData[] pcs = { this };
            
            string request_string = JsonConvert.SerializeObject(this);
            long log_id = Session.DB.createSendDataLog(request_string, "CreateDowntimeData");

            WebSocketManager.Request("CreateDowntimeData", new AckImpl((data) =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                ID = Convert.ToInt32(data);

                if (ID <= 0) return;

                switch (ID)
                {
                    case -2:
                        Session.log("Overlapping Downtime Event");
                        break;
                    case -3:
                        Session.log("Conflict for Local ID: " + LiteId);
                        break;
                    default:
                        SavedRemotely = true;
                        SaveLocally();
                        break;
                }
            }), this);
        }

        // Save locally to sqlite
        public bool SaveLocally()
        {
            Session.DB.saveDowntimeEvent(this);
            SavedLocally = true;

            return SavedLocally;
        }

        public static void SaveMany(DowntimeData[] objects)
        {
            string request_string = JsonConvert.SerializeObject(objects);
            long log_id = Session.DB.createSendDataLog(request_string, "CreateDowntimeDataBulk");
            WebSocketManager.Request("CreateDowntimeDataBulk", new AckImpl((data) =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                var jArrData = JArray.FromObject(data);
                List<DowntimeLiteIdPair> savedData = jArrData.ToObject<List<DowntimeLiteIdPair>>();
                foreach (DowntimeLiteIdPair pair in savedData)
                {
                    Session.DB.UpdateDowntimeIdLocally(pair);
                }
                Session.syncLocalDowntimes();
            }), objects);
        }

        private static void SaveManyCompleted(object sender, EventArgs e)
        {
            var request = (ApiRequest)sender;
            List<DowntimeLiteIdPair> savedData = JsonConvert.DeserializeObject<List<DowntimeLiteIdPair>>(request.ResponseText);
            foreach (DowntimeLiteIdPair pair in savedData)
            {
                Session.DB.UpdateDowntimeIdLocally(pair);
            }
            Session.syncLocalDowntimes();
        }

        void request_Completed(object sender, EventArgs e)
        {
            var request = (ApiRequest)sender;

            ID = Convert.ToInt32(request.ResponseText);

            if (ID <= 0) return;

            switch (ID)
            {
                case -2:
                    Session.log("Overlapping Downtime Event");
                    break;
                case -3:
                    Session.log("Conflict for Local ID: " + LiteId);
                    break;
                default:
                    ID = ID;
                    SavedRemotely = true;
                    SaveLocally();
                    break;
            }
        }
    }

    public class DowntimeLiteIdPair
    {
        public int downtime_id { get; set; }
        public int lite_id { get; set; }
    }
}
