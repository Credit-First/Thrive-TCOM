using System;
using System.Threading;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;

namespace TCom.Models
{
    public class CaseCount
    {
        public int? Id { get; set; }
        public int LiteId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime? EventStop { get; set; }
        public string Line { get; set; }
        public string Client { get; set; }
        public string Timer { get; set; }
        public int Count { get; set; }
        public int? DeviceSetupId { get; set; }
        public bool Saved { get; set; }

        public CaseCount()
        {
            Saved = false;
            Id = null;
        }

        public void Save()
        {
            if (this.LiteId == 0)
            {
                Session.DB.saveCaseCounter(this);
            }
            
            string request_string = JsonConvert.SerializeObject(this);
            long log_id = Session.DB.createSendDataLog(request_string, "CreateCaseCount");

            WebSocketManager.Request("CreateCaseCount", new AckImpl((data) =>
            {
                string response_string = JsonConvert.SerializeObject(data);
                Session.DB.updateSendDataLog(log_id, response_string);

                Id = Convert.ToInt32(data);

                // Nothing more to do without a valid server database id
                if (Id != null)
                {
                    Saved = true;
                    Session.DB.saveCaseCounter(this);
                }
            }), this);
        }

        void request_Completed(object sender, EventArgs e)
        {
            var request = (ApiRequest)sender;
            Id = Convert.ToInt32(request.ResponseText);

            // Nothing more to do without a valid server database id
            if (Id == null) return;

            Saved = true;
            Session.DB.saveCaseCounter(this);
        }

        private static void SaveManyCompleted(object sender, EventArgs e)
        {
            // TODO: Mark _saved = true for each object and set the Id so it can be referenced later
            Session.syncLocalDowntimes();
        }

        public static void UpdateTimerInterval(double seconds, CompletedEventHandler completed)
        {
            WebSocketManager.Request("UpdateCaseCountInterval", new { seconds = seconds });
        }

        public static void GetTimerInterval(CompletedEventHandler completed)
        {
            WebSocketManager.Request("GetTimerInterval");
        }
    }

    public class CaseCountLiteIdPair
    {
        public int casecount_id { get; set; }
        public int lite_id { get; set; }
    }
}
