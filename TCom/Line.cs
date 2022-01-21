using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using TCom.Models;
using MB = AutomatedSolutions.Win.Comm.MB.Master;

namespace TCom
{
    public class Node
    {
        public Node()
        {
        }

        public Node(Node node)
        {
            ClientName = node.ClientName;
            LineName = node.LineName;
            AddressType = node.AddressType;
            AddressTypeIndex = node.AddressTypeIndex;
            NodeLines = node.NodeLines;
            DowntimeThreshold = node.DowntimeThreshold;
            DeviceSetupId = node.DeviceSetupId;
        }

        public string ClientName { get; set; }
        public string LineName { get; set; }
        public string NodeIP { get; set; }
        public TrackType TrackType { get; set; }

        public MB.AddressType AddressType { get; set; }
        public int AddressTypeIndex { get; set; }

        public Dictionary<NodeLine, bool> NodeLines { get; set; }

        public int DowntimeThreshold { get; set; }
        public int UptimeThreshold { get; set; }
        public int DeviceSetupId { get; set; }

        public NodeLine newNodeLine()
        {
            return new NodeLine
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = UptimeThreshold,
                Line = LineName,
                Client = ClientName,
                DeviceSetupId = DeviceSetupId
            };
        }

        public MBNodeLine newMBNodeLine(NodeLine nl)
        {
            return new MBNodeLine(nl)
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = UptimeThreshold,
                Line = LineName,
                Client = ClientName,
                IpAddress = NodeIP,
                DeviceSetupId = DeviceSetupId
            };
        }

        public MBHeartbeat newMBHeartbeat(NodeLine nl)
        {
            return new MBHeartbeat(nl)
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = UptimeThreshold,
                Line = LineName,
                Client = ClientName,
                IpAddress = NodeIP,
                DeviceSetupId = DeviceSetupId
            };
        }

        public MBCounter newMBCounter(NodeLine nl)
        {
            return new MBCounter(nl)
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = 0,
                Line = LineName,
                Client = ClientName,
                IpAddress = NodeIP,
                DeviceSetupId = nl.DeviceSetupId
            };
        }

        public MBHybrid newMBHybrid(NodeLine nl)
        {
            return new MBHybrid(nl)
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = 0,
                Line = LineName,
                Client = ClientName,
                IpAddress = NodeIP,
                DeviceSetupId = nl.DeviceSetupId
            };
        }

        public MBAlertCode newMBAlertCode(NodeLine nl)
        {
            return new MBAlertCode(nl)
            {
                DowntimeThreshold = DowntimeThreshold,
                UptimeThreshold = 0,
                Line = LineName,
                Client = ClientName,
                IpAddress = NodeIP,
                DeviceSetupId = DeviceSetupId
            };
        }
    }

    public class MBNode : Node
    {
        public MBNode()
        {
        }

        public MBNode(Node node) : base(node)
        {
        }

        public MB.Device Device { get; set; }
    }

    public class NodeLine
    {
        public static Dictionary<int, MB.AddressType> AddressTypes = new Dictionary<int, MB.AddressType>
        {
            {1, MB.AddressType.MODICON_5_DIGIT},
            {2, MB.AddressType.MODICON_6_DIGIT},
            {3, MB.AddressType.ZERO_BASED},
            {4, MB.AddressType.ONE_BASED}
        };

        public static Dictionary<int, MB.DataType> DataTypes = new Dictionary<int, MB.DataType>
        {
            {1, MB.DataType.Int16},
            {2, MB.DataType.UInt16},
            {3, MB.DataType.BCD16},
            {4, MB.DataType.Int32},
            {5, MB.DataType.UInt32},
            {6, MB.DataType.BCD32},
            {7, MB.DataType.Single},
            {8, MB.DataType.String}
        };

        public static Dictionary<int, MB.TagType> TagTypes = new Dictionary<int, MB.TagType>
        {
            {1, MB.TagType.COIL},
            {2, MB.TagType.DISCRETE_INPUT},
            {3, MB.TagType.HOLDING_REGISTER},
            {4, MB.TagType.INPUT_REGISTER},
            {5, MB.TagType.EXCEPTION_STATUS},
            {6, MB.TagType.COMM_EVENT_COUNTER},
            {7, MB.TagType.COMM_EVENT_LOG},
            {8, MB.TagType.SLAVE_ID},
            {9, MB.TagType.FIFO_QUEUE},
            {10, MB.TagType.FILE_RECORD}
        };

        public NodeLine()
        {
            isLoaded = false;
            LastEventStatus = null;
            unitTrackType = false;
            WasUpLastTime = 0;
        }

        public NodeLine(NodeLine nl)
        {
            if (nl == null) return;

            isLoaded = nl.isLoaded;
            PLCStatus = nl.PLCStatus;
            Client = nl.Client;
            Line = nl.Line;
            DeviceSetupId = nl.DeviceSetupId;
            TagName = nl.TagName;
            TagTypeIndex = nl.TagTypeIndex;
            TagType = nl.TagType;
            IpAddress = nl.IpAddress;
            DowntimeThreshold = nl.DowntimeThreshold;

            DataType = nl.DataType;
            DataTypeIndex = nl.DataTypeIndex;
            TrackType = nl.TrackType;
            EventStart = nl.EventStart;
            EventStop = nl.EventStop;

            Minutes = nl.Minutes;

            LastTrueEvent = nl.LastTrueEvent;
            LastFalseEvent = nl.LastFalseEvent;
            LastEvent = nl.LastEvent;
            LastEventStatus = nl.LastEventStatus;
            unitTrackType = nl.unitTrackType;
            WasUpLastTime = nl.WasUpLastTime;
        }

        public bool isLoaded { get; set; }
        protected bool PLCStatus { get; set; }
        protected bool LineStatus { get; set; }
        private int Value { get; set; }
        public int LastValue { get; set; }
        public string Quality { get; set; }

        public string Client { get; set; }
        public string Line { get; set; }
        public string IpAddress { get; set; }
        public int DowntimeThreshold { get; set; }
        public int DeviceSetupId { get; set; }

        public string TagName { get; set; }
        public int TagTypeIndex { get; set; }
        public MB.TagType TagType { get; set; }

        public MB.DataType? DataType { get; set; }

        public MB.AddressType AddressType { get; set; }

        public int DataTypeIndex { get; set; }

        public TrackType TrackType { get; set; }
        public DateTime? EventStart { get; set; }
        public DateTime? EventStop { get; set; }
        public decimal Minutes { get; set; }

        public DateTime? LastTrueEvent { get; set; }
        public DateTime? LastFalseEvent { get; set; }
        public DateTime? LastEvent { get; set; }
        public bool? LastEventStatus { get; set; }
        public bool unitTrackType { get; set; }
        public int UptimeThreshold { get; set; }
        public int WasUpLastTime { get; set; }

        public int getValue()
        {
            return Value;
        }

        public int setValue(int val)
        {
            Value = val;

            return Value;
        }

        public int setValue(string val)
        {
            if (string.IsNullOrEmpty(val)) return Value;

            val = val.Trim().ToLower();

            switch (val)
            {
                case "true":
                    Value = 1;
                    break;
                case "false":
                    Value = 0;
                    break;
                default:
                    try
                    {
                        Value = int.Parse(val);
                    }
                    catch (Exception)
                    {
                        Value = -1;
                    }

                    break;
            }

            return Value;
        }

        public virtual bool checkStatus()
        {
            return false;
        }

        public string parseValue(object[] values)
        {
            var val = new StringBuilder(2048);
            // Build the value string

            if (values == null || values.Length <= 0) return "bad";

            for (var i = 0; i < values.Length; i++)
                val.Append(values[i] + (i == values.Length - 1 ? "" : ","));

            return val.ToString();
        }

        public bool IsPLCDown()
        {
            checkStatus();
            return !PLCStatus;
        }

        public bool isPLCUp()
        {
            checkStatus();
            return PLCStatus;
        }

        public bool isLineUp()
        {
            return LineStatus;
        }

        public bool isLineDown()
        {
            return !LineStatus;
        }

        public bool getLineStatus()
        {
            return LineStatus;
        }

        public virtual void Update(string timer)
        {
            if (!isLoaded)
                return;

            LastEvent = DateTime.UtcNow;

            if (isPLCUp())
                LastTrueEvent = LastEvent;
            else
                LastFalseEvent = LastEvent;
        }

        public void item_ErrorEvent(object sender, EventArgs evArgs)
        {
        }

        public virtual void setEventTimes()
        {
            Console.WriteLine("setEventTimes function");
            if (!unitTrackType) return;

            checkStatus();

            LastEvent = DateTime.UtcNow;

            if (PLCStatus)
                LastTrueEvent = LastEvent;
            else
                LastFalseEvent = LastEvent;
        }

        public void tick(string timer, bool setTime = true)
        {
            Console.WriteLine("tick function");
            try
            {
                if (setTime && unitTrackType)
                    setEventTimes();

                if (!isLoaded) //After first initial read, load it
                    isLoaded = true;
                else
                    Update(timer);

                Session.syncLineGrid(this);
            }
            catch (Exception ex)
            {
                Session.logException(ex);
            }
        }

        public void item_DataChanged(object sender, EventArgs evArgs)
        {
            Console.WriteLine("item_DataChanged" + ":: " + this.TrackType);
            tick("item_DataChanged");
        }

        public void createDowntimeEvent()
        {
            Console.WriteLine("createDowntimeEvent");
            if (!EventStart.HasValue || !EventStop.HasValue || TrackType == TrackType.PieceCounter) return;

            var dt = Session.DB.getLocalTemporaryDowntEvent(Line);

            if (dt == null) dt = new DowntimeData();
            else dt.LiteId = dt.LiteId;

            dt.EventStart = EventStart.Value;
            dt.EventStop = EventStop.Value;
            dt.Minutes = Minutes;
            dt.Client = Client;
            dt.Line = Line;
            dt.DeviceSetupId = DeviceSetupId;

            if (dt.EventStop <= dt.EventStart)
            {
                Session.log("Downtime attempted to be created, but EventStart >= EventStop");
                return;
            }

            dt.Save();
        }
    }

    public class MBNodeLine : NodeLine
    {
        private MB.Item item = new MB.Item();

        public MBNodeLine()
        {
            if (Item == null) Item = new MB.Item();
        }

        public MBNodeLine(NodeLine nl)
            : base(nl)
        {
            if (Item == null)
                Item = new MB.Item();
        }

        public MB.Item Item
        {
            get { return item ?? (item = new MB.Item()); }
            set { item = value; }
        }

        public override void setEventTimes()
        {
            if (!unitTrackType) return;
            checkStatus();

            LastEvent = Item.Timestamp.ToUniversalTime();

            if (PLCStatus)
                LastTrueEvent = LastEvent;
            else
                LastFalseEvent = LastEvent;
        }

        public void OnAsyncReadComplete(IAsyncResult ar)
        {
            Console.WriteLine("OnAsyncReadComplete");
            try
            {
                Item.EndRead(ar);
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            checkStatus();

            if (unitTrackType)
            {
                LastEvent = Item.Timestamp.ToUniversalTime();

                if (PLCStatus)
                    LastTrueEvent = LastEvent;
                else
                    LastFalseEvent = LastEvent;
            }

            tick("syncready", false);
        }

        public override bool checkStatus()
        {
            Console.WriteLine("CheckStatus ===========================");
            setValue(parseValue(Item.Values));
            Quality = Item.Quality.ToString();

            PLCStatus = getValue() == 1 && Quality.ToLower().Trim() == "good";

            return PLCStatus;
        }
        public virtual void Dispose()
        {
            try
            {
                Item.DataChanged -= item_DataChanged;
                Item.Error -= item_ErrorEvent;
                Item.Dispose();
                Item = null;
            }
            catch (Exception) { }
        }
    }
    
    public class MBHybrid : MBCounter
    {
        private Timer _dtTimer;
        private bool _ticked;
        private int _tickValue;

        public MBHybrid()
        {
            unitTrackType = true;

            setUpTimer();

            _tickValue = getValue();
        }

        public MBHybrid(NodeLine nodeLine)
            : base(nodeLine)
        {
            unitTrackType = true;

            setUpTimer();

            _tickValue = getValue();
        }

        protected void setUpTimer()
        {
            if (_dtTimer != null) return;
            _dtTimer = new Timer();

            ((ISupportInitialize)_dtTimer).BeginInit();
            _dtTimer.Elapsed += dtTimer_tick;
            _dtTimer.Interval = 1000; //Every second
            _dtTimer.Enabled = true;
            ((ISupportInitialize)_dtTimer).EndInit();
        }

        protected void dtCallback(Action action)
        {
            action?.Invoke();
        }

        protected void dtTimer_tick(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("dtTimer_tick");
            if (_ticked) return;

            _ticked = true;

            if (_tickValue > DowntimeThreshold && LineStatus && LastEvent != null)
            {
                Session.Service.updateLineStatus(Line, false, LastEvent.Value.ToUniversalTime(), "dtTimer");
                LineStatus = false;

                DowntimeData dt = Session.DB.getLocalTemporaryDowntEvent(Line) ?? new DowntimeData();

                dt.Client = Client;
                dt.Line = Line;
                dt.EventStart = LastEvent.Value.ToUniversalTime();
                dt.DeviceSetupId = DeviceSetupId;

                dt.Save();

                //recreate temp event
                //createTempDT();
                _dtTimer.Stop();
                _tickValue = 0;
            }

            _tickValue++;
            _ticked = false;
        }

        public override void Update(string timer)
        {
            Console.WriteLine("MBCounter update: trackType: " + TrackType);

            UpdateValues();
            checkCounter(timer);

            EventStart = LastEvent = Item.Timestamp.ToUniversalTime();

            var value = getValue();

            if (value > lastValue && TrackType != TrackType.PieceCounter)
            {
                var dtEvent = Session.DB.getLocalTemporaryDowntEvent(Line);
                if (dtEvent != null)
                {
                    if (TrackType == TrackType.SKU) dtEvent.SKU = value;
                    else if (TrackType == TrackType.AlarmCode) dtEvent.AlarmCode = value;
                    dtEvent.SaveLocally();
                }

                if (LineStatus == false && dtEvent!= null)
                {
                    dtEvent.EventStop = (DateTime)LastEvent;

                    var duration = (int)Math.Floor(LastEvent.Value.Subtract(dtEvent.EventStart).TotalSeconds);

                    if (duration >= DowntimeThreshold)
                    {
                        Minutes = (duration - DowntimeThreshold) / 60m;

                        dtEvent.Minutes = Minutes;
                        dtEvent.Client = Client;
                        dtEvent.Line = Line;

                        //Session.DB.saveDowntimeEvent(dtEvent);
                        dtEvent.Save();
                    }

                    Session.Service.updateLineStatus(Line, true, LastEvent.Value, timer);
                }

                LineStatus = true;

                _tickValue = 0;

                _dtTimer.Stop();
                _dtTimer.Start();

                //recreate temp event
                createTempDT();
            }

            lastValue = getValue();
            lastIncrement = LastEvent.Value;
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                this._dtTimer.Elapsed -= this.dtTimer_tick;
                this._dtTimer.Dispose();
                this._dtTimer = null;
            }
            catch (Exception) { }
        }
    }

    public class MBAlertCode : MBHybrid
    {
        public MBAlertCode()
        {
            unitTrackType = true;
        }

        public MBAlertCode(NodeLine nodeLine) : base(nodeLine)
        {
            unitTrackType = true;
        }
    }

    public class MBHeartbeat : MBNodeLine
    {
        private Timer statusTimer;
        private int ticker;

        public MBHeartbeat()
        {
        }

        public MBHeartbeat(NodeLine nodeLine)
            : base(nodeLine)
        {
            unitTrackType = true;

            statusTimer = new Timer();
            statusTimer.BeginInit();
            statusTimer.Elapsed += statusTimer_Elapsed;
            statusTimer.Interval = 1000; // Every second
            statusTimer.Enabled = true;
            statusTimer.EndInit();

            UptimeThreshold = DowntimeThreshold;
        }

        private void statusTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("statusTimer_Elapsed function");
            var threshold = DowntimeThreshold;

            if (PLCStatus) threshold = UptimeThreshold;

            Console.WriteLine("statusTimer_Elapsed: ticker=" + ticker + ", threshold=" + threshold + ", DowntimeThreshold=" + DowntimeThreshold + ", uptimethreshold=" + UptimeThreshold + ",LineStatus=" + LineStatus + ",PLCStatus=" + PLCStatus);

            if (ticker >= threshold && LineStatus != PLCStatus)
            {
                LineStatus = PLCStatus;

                TriggerLineStatus("statusTimer");
                statusTimer.Stop();
            }

            ticker++;
        }

        protected void TriggerLineStatus(string timer)
        {
            Console.WriteLine("TriggerLineStatus  WasUpLastTime=" + WasUpLastTime);
            // Reset Values on every Update. These will be set on a DT Event. 
            EventStart = null;
            EventStop = null;
            Minutes = 0;
            var LineUp = isLineUp();

            if ((WasUpLastTime == 0 || WasUpLastTime == 1) && !LineUp)
            {
                WasUpLastTime = -1;
                LastEventStatus = false;

                var dt = Session.DB.getLocalTemporaryDowntEvent(Line) ?? new DowntimeData();

                if (LastFalseEvent == null) return;

                dt.EventStart = LastFalseEvent ?? DateTime.UtcNow;
                dt.EventStop = null;
                dt.Minutes = null;
                dt.Line = Line;
                dt.Client = Client;
                dt.DeviceSetupId = DeviceSetupId;

                dt.Save();

                LastTrueEvent = null;
                Session.Service.updateLineStatus(Line, false, LastFalseEvent.Value, timer);
            }
            else if ((WasUpLastTime == 0 || WasUpLastTime == -1) && LineUp)
            {
                Session.Service.updateLineStatus(Line, true, LastTrueEvent.Value, timer);

                LastEventStatus = true;

                var dt = Session.DB.getLocalTemporaryDowntEvent(Line);

                if (dt != null)
                {
                    LastFalseEvent = dt.EventStart;
                    var fEvent = dt.EventStart;
                    var tEvent = LastTrueEvent ?? DateTime.UtcNow;
                    var totalSeconds = 0;

                    try
                    {
                        totalSeconds = Convert.ToInt32(Math.Floor(tEvent.Subtract(fEvent).TotalSeconds));
                    }
                    catch (Exception ex)
                    {
                        Session.logException(ex);
                    }

                    if (totalSeconds >= DowntimeThreshold && totalSeconds > 2) //2 seconds to prevent flickering
                    {
                        EventStart = fEvent;
                        EventStop = tEvent;
                        Minutes = (decimal)totalSeconds / 60;

                        createDowntimeEvent();

                        LastTrueEvent = null;
                        EventStart = null;
                        EventStop = null;
                        Minutes = 0;
                        WasUpLastTime = 1;
                    }
                }

                LastFalseEvent = null; //Reset, even if no DT Event
            }

        }

        public override void Update(string timer)
        {
            Console.WriteLine("LineStatus = " + LineStatus + ", PLCStatus = " + PLCStatus);
            if(LineStatus != PLCStatus)
            {
                statusTimer.Stop();
                ticker = 0;
                statusTimer.Start();
            }
        }
        public override void Dispose()
        {
            base.Dispose();
            try
            {
                statusTimer.Elapsed -= statusTimer_Elapsed;
                statusTimer.Dispose();
                statusTimer = null;
            }
            catch (Exception) { }
        }
    }

    public class MBCounter : MBNodeLine
    {
        protected Timer CountDeadTime;
        protected Timer HourlyTimer;

        protected MBCounter()
        {
            throw new NotImplementedException();
        }

        public MBCounter(NodeLine nl) : base(nl)
        {
            lastEvent = DateTime.UtcNow;
            lastIncrement = lastEvent;
            unitTick = false;
            isTicking = false;
            unitTrackType = false;
            isDown = false;

            if (nl.TrackType != TrackType.CaseCounter) return;

            CountDeadTime = new Timer();
            CountDeadTime.BeginInit();
            CountDeadTime.Elapsed += countDeadTime_tick;
            CountDeadTime.Interval = 5000;
            CountDeadTime.Enabled = true;
            CountDeadTime.EndInit();
        }

        protected int lastValue { get; set; }
        protected DateTime lastEvent { get; set; }
        protected DateTime lastIncrement { get; set; }
        protected bool unitTick { get; set; }
        protected bool isTicking { get; set; }
        protected bool isDown { get; set; }
        protected DateTime LastCheck { get; set; }

        private readonly object _caseCounterLock = new object();
        private readonly object _pieceCountLock = new object();

        /// <summary>
        ///     1. Update the counter's latest values from the registers
        ///     2. Make sure there's either a good or bad quality
        ///     3. If the quality is bad, then set the line isDown to true
        ///     4. If the line is down and the quality is "good", continue here
        ///     5. Create new Count then
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void countDeadTime_tick(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("countDeadTime_tick");
            // get the latest values
            UpdateValues();

            // can't do anything at this point
            if (Quality == null) return;

            // set the line down if the quality is bad
            if (Quality.ToLower() == "bad") isDown = true;

            // continue if the line is down and the quality is "good"
            if (!isDown || Quality.ToLower() != "good") return;

            var dt = stripDateTime(Item.Timestamp).ToUniversalTime();
            var value = getValue();
            saveCount(dt, value, "countDeadTime");
            lastEvent = dt;
            LastCheck = DateTime.Now;
            isDown = false;
        }

        private DateTime stripDateTime(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        protected void createTempDT()
        {
            var dtEvent = Session.DB.getLocalTemporaryDowntEvent(Line) ?? new DowntimeData();

            if (EventStart == null) return;

            dtEvent.EventStart = EventStart.Value.ToUniversalTime();
            dtEvent.Line = Line;
            dtEvent.Client = Client;
            dtEvent.DeviceSetupId = DeviceSetupId;

            Session.DB.saveDowntimeEvent(dtEvent);
            //dtEvent.Save();
        }

        public void UpdateValues()
        {
            Console.WriteLine("UpdateValues");
            setValue(parseValue(Item.Values));
            Quality = Item.Quality.ToString();
        }

        public void checkCounter(string timer)
        {
            Console.WriteLine("checkCounter");
            var dt = stripDateTime(Item.Timestamp).ToUniversalTime();
            if (LastCheck.AddMinutes(5) > DateTime.Now.ToUniversalTime()) return;

            LastCheck = DateTime.Now.ToUniversalTime();

            if (Quality != null && Quality.ToLower() != "good") return;
            UpdateValues();
            saveCount(dt, getValue(), timer);

            lastEvent = dt;
        }

        public void saveCount(DateTime dt, int value, string timer, bool isHourlyCounter = false)
        {
            Console.WriteLine("saveCount");
            if (value <= lastValue || value <= 0) return;

            lastEvent = dt;

            if (TrackType == TrackType.AlarmCode || TrackType == TrackType.SKU) return;

            if (TrackType == TrackType.PieceCounter)
            {
                lock (_pieceCountLock)
                {
                    int lastCount = Session.DB.getLastPieceCountValue(Client, Line);
                    if ((value > lastCount) || isHourlyCounter)
                    {
                        var pc = new PieceCount
                        {
                            Client = Client,
                            Line = Line,
                            Count = value,
                            Event = dt,
                            DeviceSetupId = DeviceSetupId,
                            Saved = false
                        };

                        pc.Save();
                    }
                }
            }

            if (TrackType == TrackType.CaseCounter)
            {
                lock (_caseCounterLock)
                {
                    int lastCount = Session.DB.getLastCaseCounterValue(Client, Line);
                    if ((value > lastCount) || isHourlyCounter)
                    {
                        var c = new CaseCount
                        {
                            Client = Client,
                            Line = Line,
                            EventStart = dt,
                            EventStop = dt,
                            DeviceSetupId = DeviceSetupId,
                            Count = value,
                            Saved = false,
                            Timer = timer
                        };

                        c.Save();
                    }
                }
            }
            // Session.Service.SoapClient.CreateTComCaseCount(lastEvent.ToString(CultureInfo.InvariantCulture), dt.ToString(CultureInfo.InvariantCulture), value, Line, Client);
        }

        public override void Update(string timer)
        {
            checkCounter(timer);

            lastValue = getValue();
            lastIncrement = Item.Timestamp.ToUniversalTime();
        }

        public override void Dispose()
        {
            base.Dispose();
            try
            {
                CountDeadTime.Elapsed -= countDeadTime_tick;
                CountDeadTime.Dispose();
                CountDeadTime = null;

                HourlyTimer.Dispose();
            }
            catch (Exception) { }
        }
    }
}