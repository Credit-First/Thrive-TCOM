using Newtonsoft.Json.Linq;
using Quobject.EngineIoClientDotNet.Client;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Linq;
using Socket = Quobject.SocketIoClientDotNet.Client.Socket;
using Newtonsoft.Json;

namespace TCom
{
    public class WebSocketManager
    {
        public static string WebSocketURL = ConfigurationManager.AppSettings["WebSocketURL"];
        public static Socket webSocket;
        public static bool IsConnected = false;
        public static void Initialize()
        {
            webSocket = IO.Socket(WebSocketURL, new IO.Options { AutoConnect = false, Reconnection = true, Timeout = -1 });
            webSocket.On(Socket.EVENT_CONNECT, () =>
            {
                IsConnected = true;
                Session.MainForm.SetConnectionStatus(true);
                if (Session.Nodes == null || !Session.Nodes.Any())
                {
                    Session.MainForm.Relog();
                }
                else
                {
                    Session.syncLocalDowntimes();
                    Session.syncLocalCounters();
                    Session.syncPieceCounters();
                }
            });

            webSocket.On(Socket.EVENT_ERROR, (err) =>
            {
                var exception = (EngineIOException)err;
                Session.log("Error in WS: " + exception.code + " (" + exception.Message + ")");
            });

            webSocket.On(Socket.EVENT_DISCONNECT, () =>
            {
                IsConnected = false;
                Session.MainForm.SetConnectionStatus(false);
            });

            webSocket.On("GetVersion", (cb) =>
            {
                var iack = (IAck)cb;
                iack.Call(Global.Version);
            });

            webSocket.On("GetConnectedLinesData", (cb) =>
            {
                var iack = (IAck)cb;
                var dt = (DataTable)Session.lineGridView.DataSource;
                iack.Call(JArray.FromObject(dt));
            });

            webSocket.Connect();
        }

        public static void Request(string eventName, object data = null)
        {
            if (IsConnected)
            {
                object jObj = GetJsonObject(data);
                webSocket.Emit(eventName, jObj);
            }
        }

        public static void Request(string eventName, IAck ack, object data = null)
        {
            if (IsConnected)
            {
                object jObj = GetJsonObject(data);
                webSocket.Emit(eventName, ack, jObj);
            }
        }

        private static object GetJsonObject(object data)
        {
            object jObj = null;
            if (data != null)
            {
                Type valueType = data.GetType();
                if (valueType.IsArray)
                {
                    jObj = JArray.FromObject(data);
                }
                else
                {
                    jObj = JObject.FromObject(data);
                }
            }
            return jObj;
        }
    }
}