using System;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace TCom.Models
{
    class DataCollectionNode
    {
        public string Username;
        public string Password;
        public void Login(string username, string password)
        {
            Username = username;
            Password = password;
            
            string request_string = JsonConvert.SerializeObject(this);
            long log_id = Session.DB.createSendDataLog(request_string, "DataCollectionNodeAuth");

            WebSocketManager.Request("DataCollectionNodeAuth", new AckImpl((data) =>
            {
                
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                bool isSuccess = Convert.ToBoolean(Convert.ToString(data));
                Session.MainForm.DisplayLoginMessage(isSuccess);
                if (isSuccess)
                {
                    Session.MainForm.Resync();
                }
            }), this);
        }

        public static void UpdateStatus(string client, string line, bool p, DateTime eventTime)
        {
            var status = new { Client = client, Line = line, Status = p, EventTime = eventTime };
            WebSocketManager.Request("DataCollectionNodeLineStatus", status);
        }

        public static void GetDevices(string client)
        {
            //var request = Dashboard.Request("DataCollectionNode/devicesetups", JsonConvert.SerializeObject(new object[] { client }));
            //var response = request.CallSync();

            //var jsonData = JsonConvert.DeserializeObject<NodeLine[]>(response);
            
            var obj = new { Client = client };
            string request_string = JsonConvert.SerializeObject(obj);
            long log_id = Session.DB.createSendDataLog(request_string, "DataCollectionNodeDeviceSetups");

            WebSocketManager.Request("DataCollectionNodeDeviceSetups", new AckImpl(data =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                var jArrData = JArray.FromObject(data);
                NodeLine[] devices = jArrData.ToObject<NodeLine[]>();
                Session.MainForm.ResyncNodes(devices);
            }), obj);
        }

        public static void Ping(string client, string line, bool status)
        {
            var ping = new { Client = client, Line = line, Status = status };
            WebSocketManager.Request("DataCollectionNodePing", ping);
        }
    }
}
