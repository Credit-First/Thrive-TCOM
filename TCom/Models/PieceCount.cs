using System;
using System.Threading;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;

namespace TCom.Models
{
    public class PieceCount
    {
        public int? Id { get; set; }
        public int LiteId { get; set; }
        public string Client { get; set; }
        public string Line { get; set; }
        public int DeviceSetupId { get; set; }
        public int Count { get; set; }
        public DateTime Event { get; set; }
        public bool Saved;

        public void Save()
        {
            if (this.LiteId == 0)
            {
                Session.DB.savePieceCount(this);
            }
            string request_string = JsonConvert.SerializeObject(this);
            long log_id = Session.DB.createSendDataLog(request_string, "CreatePieceCount");

            WebSocketManager.Request("CreatePieceCount", new AckImpl((data) =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                Id = Convert.ToInt32(data);

                // Nothing more to do without a valid server database id
                if (Id != null)
                {
                    Saved = true;
                    Session.DB.savePieceCount(this);
                }
            }), this);
        }

        private void SaveCompleted(object sender, EventArgs e)
        {
            var request = (ApiRequest)sender;
            Id = Convert.ToInt32(request.ResponseText);

            // Nothing more to do without a valid server database id
            if (Id == null) return;

            Saved = true;
            Session.DB.savePieceCount(this);
        }

        public static void SaveMany(PieceCount[] objects)
        {
            string request_string = JsonConvert.SerializeObject(objects);
            long log_id = Session.DB.createSendDataLog(request_string, "CreatePieceCountBulk");

            WebSocketManager.Request("CreatePieceCountBulk", new AckImpl((data) =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                // TODO: Mark _saved = true for each object and set the Id so it can be referenced later
                Session.syncPieceCounters();
            }), objects);
        }

        private static void SaveManyCompleted(object sender, EventArgs e)
        {
            // TODO: Mark _saved = true for each object and set the Id so it can be referenced later
            Session.syncPieceCounters();
        }

        public bool IsSaved()
        {
            return Saved;
        }

        public void SetSaved(bool saved)
        {
            Saved = true;
        }
    }

    public class PieceCountIdPair
    {
        public int piececount_id { get; set; }
        public int lite_id { get; set; }
    }
}
