using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Timers;
using System.Windows.Forms;
using TCom.Models;
using Timer = System.Timers.Timer;

namespace TCom
{
    public partial class MainForm : Form
    {
        private readonly DB _db = new DB();

        private readonly Timer _eventGridTimer = new Timer();
        private readonly Timer _pingTimer = new Timer();
        private readonly Timer _hourlyCountTimer = new Timer();
        private readonly Timer _counterUpdateTimer = new Timer();

        private readonly Timer _sendDataTimer = new Timer();


        private Account _acct;

        public MainForm()
        {
            this.Text = "TCom " + Global.Version;
            Session.MainForm = this;
            try
            {
                InitializeComponent();
                Session.DB = _db;

                WebSocketManager.Initialize();


                _acct = _db.getCurrentAccount();

                _eventGridTimer.BeginInit();
                _eventGridTimer.Elapsed += EventGridTimer_Elapsed;
                _eventGridTimer.Interval = 1000 * 10; // Every 10 seconds
                _eventGridTimer.Enabled = true;
                _eventGridTimer.EndInit();

                _pingTimer.BeginInit();
                _pingTimer.Elapsed += PingTimer_Elapsed;
                _pingTimer.Interval = 1000 * 60 * 5; // Every 5 minutes
                _pingTimer.Enabled = true;
                _pingTimer.EndInit();

                _hourlyCountTimer.BeginInit();
                _hourlyCountTimer.Elapsed += _hourlyCountTimer_Elapsed;

                DateTime now = DateTime.UtcNow;
                var nextFullHour = TimeSpan.FromHours(Math.Ceiling(now.TimeOfDay.TotalHours));
                var totalMillisecond = (nextFullHour - now.TimeOfDay).TotalSeconds * 1000;

                _hourlyCountTimer.Interval = totalMillisecond; //after next start of the hour.
                _hourlyCountTimer.Enabled = true;
                _hourlyCountTimer.EndInit();

                _counterUpdateTimer.BeginInit();
                _counterUpdateTimer.Elapsed += _counterUpdateTimer_Elapsed;
                _counterUpdateTimer.Interval = (_acct.caseCounterInterval == 0 ? 20 : _acct.caseCounterInterval) * 1000; // Default from local db
                _counterUpdateTimer.Enabled = true;
                _counterUpdateTimer.EndInit();



                _sendDataTimer.BeginInit();
                _sendDataTimer.Elapsed += SendDataTimer_Elapsed;
                _sendDataTimer.Interval = leftTimeToClearSendData();
                _sendDataTimer.AutoReset = false;
                _sendDataTimer.Enabled = true;
                _sendDataTimer.EndInit();


                PopulateFields();
                Session.CurrentAccount = _acct;
                InitExtraFormOptions();
                CreateLineGrid();
                CreateLogGrid();
                _acct = _db.getCurrentAccount();

                SyncEventGrid();
                Session.log("");

                SQLiteCleanup.Enabled = true;
                SQLiteCleanup.Start();
                pnlDiagnosisResult.Hide();
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        private void _hourlyCountTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("_hourlyCountTimer_Elapsed");
            var now = DateTime.UtcNow;
            var beforeNow = now.Subtract(new TimeSpan(0, 0, 1));

            // Only continue if we have valid nodes
            if (Session.Nodes == null)
            {
                Session.log("Hourly Count at " + now + " not saved at all because there were no valid nodes.");
                return;
            }

            foreach (var node in Session.Nodes)
            {
                var counters = node.NodeLines.Where(nl => nl.Key is MBCounter);

                foreach (var counter in counters)
                {
                    var counterKey = (MBCounter)counter.Key;
                    counterKey.UpdateValues();
                    var value = counterKey.getValue();

                    if (value < 0)
                    {
                        Session.log("Hourly count at " + now + " not saved because the current value (" + value + ") is less than zero (0).");
                        return;
                    }

                    if (counterKey.Quality.ToLower() != "good")
                    {
                        Session.log("Hourly count at " + now + " not saved because the Quality (" + counterKey.Quality.ToLower() + ") is not 'good'.");
                        return;
                    }

                    counterKey.saveCount(beforeNow, value, "hourlyCountTimer", true);
                    counterKey.saveCount(now, value, "hourlyCountTimer", true);
                }
            }

            Session.syncLocalDowntimes();
            Session.syncLocalCounters();
            Session.syncPieceCounters();

            now = DateTime.UtcNow;

            var nextFullHour = TimeSpan.FromHours(Math.Ceiling(now.TimeOfDay.TotalHours));
            var totalMillisecond = (nextFullHour - now.TimeOfDay).TotalSeconds * 1000;

            _hourlyCountTimer.Interval = totalMillisecond; //after next start of the hour.
        }

        private void _counterUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("_counterUpdateTimer_Elapsed");
            if (Session.Nodes == null) return;

            foreach (MBNode node in Session.Nodes)
            {
                var counters = node.NodeLines.Where(nl => nl.Key is MBCounter);

                foreach (var counter in counters)
                {
                    var nodeLine = (MBCounter)counter.Key;
                    nodeLine.UpdateValues();
                    nodeLine.saveCount(DateTime.UtcNow, nodeLine.getValue(), "counterUpdateTimer");
                    Session.syncLineGrid(nodeLine);
                }
            }
        }

        public void PopulateEventDropbox()
        {
            Console.WriteLine("PopulateEventDropbox");
            cboLines.Items.Clear();
            cboLines.Items.Add("All");

            if (Session.Nodes == null) return;

            foreach (MBNode node in Session.Nodes)
                cboLines.Items.Add(node.LineName);
        }

        public void PopulateDiagnosisLineDropbox()
        {
            Console.WriteLine("PopulateDiagnosisLineDropbox");
            cbxAccuracyElement.Enabled = false;
            dtAccuracyFromDate.Enabled = false;
            dtAccuracyToDate.Enabled = false;
            btnRunAccuracyChk.Enabled = false;

            cbxDiagnosisLine.Items.Clear();
            cbxDiagnosisLine.Items.Add("Please select");
            cbxDiagnosisLine.SelectedIndex = 0;

            if (Session.Nodes == null)
            {
                return;
            }

            foreach (MBNode node in Session.Nodes)
                cbxDiagnosisLine.Items.Add(node.LineName);
        }

        public void PopulateAccuracyElementDropbox()
        {
            Console.WriteLine("PopulateAccuracyElementDropbox");
            cbxAccuracyElement.Items.Clear();
            cbxAccuracyElement.Items.Add("Please select");
            cbxAccuracyElement.Items.Add("Downtime events");
            cbxAccuracyElement.Items.Add("Case counts");
            cbxAccuracyElement.Items.Add("Piece counts");
            cbxAccuracyElement.SelectedIndex = 0;
        }

        private void EventGridTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("EventGridTimer_Elapsed");
            SyncLineGrid();
        }

        private void PingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("PingTimer_Elapsed");
            if (Session.Nodes == null) return;

            foreach (MBNode node in Session.Nodes)
            {
                try
                {
                    DataCollectionNode.Ping(Session.CurrentAccount.Username, node.LineName, true);
                }
                catch (Exception ex)
                {
                    Session.logException(ex);
                }

                foreach (MBNodeLine nl in node.NodeLines.Keys)
                {
                    nl.DowntimeThreshold = node.DowntimeThreshold;
                    Session.syncLineGrid(nl);
                }
            }
        }


        private void SendDataTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("SendDataTimer_Elapsed");
            // clear old data at 00:00:00 every day.
            _sendDataTimer.Interval = 1000 * 24 * 3600;
            _sendDataTimer.AutoReset = true;
            _db.clearOldSendData();
        }
        private double leftTimeToClearSendData()
        {
            return 10 * 1000; // i set time to 10s for test.
        }

        public void InitExtraFormOptions()
        {
            Console.WriteLine("InitExtraFormOptions");
            Resize += mainForm_Resize;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;

            tabControl1.Selecting += tabControl1_Selecting;
            FormClosed += mainForm_FormClosed;

            Session.lineGridView = lineGridView;
            Session.logGridView = logGridView;
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("mainForm_Shown");
            Session.Service.refreshNodeLines();
        }

        //Do a safe dispose before complete exiting. Just to make sure
        private void mainForm_FormClosed(object sender, FormClosedEventArgs ev)
        {
            Console.WriteLine("mainForm_FormClosed");
            try
            {
                _eventGridTimer.Dispose();
                _pingTimer.Dispose();
                _hourlyCountTimer.Dispose();
                _counterUpdateTimer.Dispose();
                _sendDataTimer.Dispose();
                Session.disposeDevices();
                WebSocketManager.webSocket.Close();
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            Application.Exit();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        public void PopulateFields()
        {
            txtUsername.Text = _acct.Username;
            txtPassword.Text = _acct.Password;
            //CaseCount.GetTimerInterval(GetTimerIntervalCompleted);
            numCountUpdateRate.Value = _acct.caseCounterInterval;
        }

        private void GetTimerIntervalCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("GetTimerIntervalCompleted");
            var request = (ApiRequest)sender;
            int? interval = string.IsNullOrEmpty(request.ResponseText) ? (int?)null : Convert.ToInt32(request.ResponseText);

            numCountUpdateRate.Value = (interval ?? 1) * 1000; //default 1 second.
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("notifyIcon1_DoubleClick");
            Show();
        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("mainForm_Resize");
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(500);
                    Hide();
                    break;
                case FormWindowState.Normal:
                    notifyIcon1.Visible = false;
                    break;
            }
        }

        public void CreateLineGrid()
        {
            var dt = new DataTable();
            dt.Columns.Add("Type");
            dt.Columns.Add("Line");
            dt.Columns.Add("Node IP");
            dt.Columns.Add("Tag Name");
            dt.Columns.Add("Downtime Threshold");
            dt.Columns.Add("Uptime Threshold");
            dt.Columns.Add("Quality");
            dt.Columns.Add("Value");
            lineGridView.DataSource = dt;
        }

        public void CreateLogGrid()
        {
            var dt = new DataTable();
            dt.Columns.Add("Logged At");
            dt.Columns.Add("Message");
            logGridView.DataSource = dt;
        }

        public void SyncEventGrid()
        {
            Console.WriteLine("SyncEventGrid");
            if (IsDisposed) return;

            try
            {
                if (eventGrid.InvokeRequired)
                {
                    RefreshEventsCallback fn = SyncEventGrid;

                    eventGrid.Invoke(fn);
                    return;
                }

                if (numEventId.Value > 0)
                    eventGrid.DataSource = _db.getLocalDowntimeEventsDataTable(Convert.ToInt32(numEventId.Value));
                else if (numDTId.Value > 0)
                    eventGrid.DataSource = _db.getLocalDowntimeEventsDataTable(Convert.ToInt32(numDTId.Value), true);
                else
                    eventGrid.DataSource = _db.getLocalDowntimeEventsDataTable((string)cboLines.SelectedItem);

                var dataGridViewColumn = eventGrid.Columns["EventStart"];
                if (dataGridViewColumn != null)
                    dataGridViewColumn.DefaultCellStyle.Format = "MM/dd/yyyy h:mm:ss";

                var gridViewColumn = eventGrid.Columns["EventStop"];
                if (gridViewColumn != null)
                    gridViewColumn.DefaultCellStyle.Format = "MM/dd/yyyy h:mm:ss";
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public void SyncCounterGrid()
        {
            Console.WriteLine("SyncCounterGrid");
            if (IsDisposed) return;

            try
            {
                if (gridLocalCounters.InvokeRequired)
                {
                    RefreshCountersCallback fn = SyncCounterGrid;

                    gridLocalCounters.Invoke(fn);
                    return;
                }

                var logData = _db.getLocalCounterDataTable();
                logData = Event.filterDataViewByTime(logData, 2);
                gridLocalCounters.DataSource = logData;
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public void SyncPieceCountGrid()
        {
            Console.WriteLine("SyncPieceCountGrid");
            if (IsDisposed) return;
            try
            {
                if (gridPieceCount.InvokeRequired)
                {
                    RefreshPieceCountCallback fn = SyncPieceCountGrid;

                    gridPieceCount.Invoke(fn);
                    return;
                }

                var logData = _db.getLocalPieceCountDataTable();
                logData = Event.filterDataViewByTime(logData, 2);
                gridPieceCount.DataSource = logData;
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public void SyncLineGrid(bool clear = false)
        {
            Console.WriteLine("SyncLineGrid");
            Session.refreshLineGridView(clear);
        }

        private readonly object _ResyncLock = new object();

        public void Resync()
        {
            Console.WriteLine("Resync");
            lock (_ResyncLock)
            {
                Session.disposeDevices();
                DataCollectionNode.GetDevices(Session.CurrentAccount.Username);
            }
        }

        public void ResyncNodes(NodeLine[] devices)
        {
            Console.WriteLine("ResyncNodes");
            this.UIThread(() =>
            {
                Session.Service.syncNodes(devices);
                Session.Service.refreshNodeLines();
                SyncLineGrid(true);
                PopulateEventDropbox();
                PopulateDiagnosisLineDropbox();
                PopulateAccuracyElementDropbox();

                Session.syncLocalDowntimes();
                Session.syncLocalCounters();
                Session.syncPieceCounters();
            });
        }

        public void Relog()
        {
            Console.WriteLine("Relog");
            this.UIThread(() =>
            {
                Session.disposeDevices();
                _acct = _db.getCurrentAccount();
                _acct.Username = txtUsername.Text;
                _acct.Password = txtPassword.Text;
                _db.saveAccount(_acct);
                Session.CurrentAccount = _acct;
                _acct.login();
            });
        }

        public void DisplayLoginMessage(bool isSuccess)
        {
            Console.WriteLine("DisplayLoginMessage");
            if (isSuccess)
            {
                this.UIThread(() => lblLoginMsg.Text = "Success");
            }
            else
            {
                this.UIThread(() => lblLoginMsg.Text = "Failed");
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("startToolStripMenuItem_Click");
            if (Session.isOn())
            {
                startToolStripMenuItem.Text = "Stop";
                Session.turnOff();
            }
            else
            {
                startToolStripMenuItem.Text = "Start";
                Session.turnOn();
            }
        }

        private void resyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("resyncToolStripMenuItem_Click");
            Resync();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("closeToolStripMenuItem_Click");
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnLogin_Click");
            Relog();
        }

        private void asyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("asyncToolStripMenuItem_Click");
            MessageBox.Show("Syncing", "Syncing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Session.Service.refreshNodeLines();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("numericUpDown1_ValueChanged");
            SyncEventGrid();
        }

        private void numDTId_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("numDTId_ValueChanged");
            SyncEventGrid();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("updateToolStripMenuItem_Click");
            if (!File.Exists("TComUpdater.exe")) return;
            Close();
            Process.Start("TComUpdater.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button1_Click");
            Session.syncLocalDowntimes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button2_Click");
            Session.syncLocalCounters();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button3_Click");
            Session.setReadRate(Convert.ToInt32(numReadRate.Value));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button4_Click");
            Session.setDeviceTimeoutConnect(Convert.ToInt32(numTimeoutConnect.Value));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button5_Click");
            Session.setDeviceTimeoutTransaction(Convert.ToInt32(numTimeoutTransaction.Value));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button6_Click");
            _counterUpdateTimer.Interval = Convert.ToDouble(numCountUpdateRate.Value) * 1000;
            //CaseCount.UpdateTimerInterval(Convert.ToDouble(numCountUpdateRate.Value), UpdateTimerIntervalCompleted);
            _acct.caseCounterInterval = Convert.ToInt32(numCountUpdateRate.Value);
            _db.saveAccount(_acct);
        }

        private void UpdateTimerIntervalCompleted(object sender, EventArgs e)
        {
            // TODO: Notify user that the timer updated on dashboard.
        }

        private void numPullSkuRate_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("numPullSkuRate_ValueChanged");
            _pingTimer.Interval = Convert.ToDouble(numPullSkuRate.Value) * (1000 * 60);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("mainForm_Load");
            if (_acct != null) return;

            MessageBox.Show("Account has not been loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string[] GetLineFilterList()
        {
            Console.WriteLine("GetLineFilterList");
            var checkedItems = filterList.CheckedItems;
            var names = new List<string>(checkedItems.Count);
            names.AddRange(from object item in checkedItems select item.ToString());
            return names.ToArray();
        }

        public void SetLineFilterList(string[] names)
        {
            Console.WriteLine("SetLineFilterList");
            if (filterList.InvokeRequired) return;
            this.UIThread(() =>
            {
                string[] filteredLineList = _acct.lineFilters.Split(',');
                filterList.Items.Clear();
                foreach (var name in names)
                {
                    var check = filteredLineList.Count() == 0 || filteredLineList.Contains(name);
                    filterList.Items.Add(name, check);
                }
            });
        }

        private void btnSaveLineFilter_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnSaveLineFilter_Click");
            var names = GetLineFilterList();
            _acct.lineFilters = string.Join(",", names);
            _db.saveAccount(_acct);
            Resync();
        }

        private void SQLiteCleanup_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("SQLiteCleanup_Tick");
            Session.DB.TrimAllAfterOneWeek();
        }

        private void cleanDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.DB.TrimAllAfterOneWeek();
        }

        private void statusCheckTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("statusCheckTimer_Tick");
            if (Session.Nodes == null)
            {
                statusCheckTimer.Enabled = false;
                Session.log("Error, unable to find the session's nodes.");
                return;
            }

            var deviceCount = Session.Nodes.Sum(n => n.NodeLines.Count);

            if (lineGridView.RowCount != deviceCount + 1 || deviceCount == 0)
            {
                Console.WriteLine("statusCheckTimer_Tick ");
                Resync();

            }

            statusCheckTimer.Enabled = false;
        }

        private void testAlarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("testAlarmToolStripMenuItem_Click");
            if (Session.Nodes == null)
            {
                MessageBox.Show("No nodes/devices detected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var alarms = new List<MBHybrid>();

            foreach (MBNode node in Session.Nodes)
            {
                var alarmKey = node.NodeLines.FirstOrDefault(nl => nl.Key is MBAlertCode);

                if (alarmKey.Equals(new KeyValuePair<object, bool>())) continue;

                var alarm = alarmKey.Key;
                alarms.Add((MBHybrid)alarm);
            }

            //MessageBox.Show("Found: " + alarms.Count() + " alarms for this client!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //var result = MessageBox.Show("Do you want to view details about each ", "Alarm Codes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private delegate void RefreshEventsCallback();

        private delegate void RefreshCountersCallback();

        private delegate void RefreshStatusLogCallback();

        private delegate void RefreshPieceCountCallback();

        private void btnUpdateLocalCounters_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnUpdateLocalCounters_Click");
            SyncCounterGrid();
        }

        private void btnUpdateLocalEvents_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnUpdateLocalEvents_Click");
            SyncEventGrid();
        }

        private void btnUpdatePieceCounts_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnUpdatePieceCounts_Click");
            SyncPieceCountGrid();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button7_Click");
            Session.syncPieceCounters();
        }

        private void cbxDiagnosisLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("cbxDiagnosisLine_SelectedIndexChanged");
            lblOrphanDTEventsChkResult.Text = "Pending";
            lblOrphanDTEventsChkResult.ForeColor = System.Drawing.Color.Black;
            lblOverlappingDTEventsChkResult.Text = "Pending";
            lblOverlappingDTEventsChkResult.ForeColor = System.Drawing.Color.Black;
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                cbxAccuracyElement.Enabled = true;
                btnRunOrphanDTEvtChk.Enabled = true;
                btnRunOverlappingDTEvtChk.Enabled = true;
            }
            else
            {
                if (cbxAccuracyElement.SelectedIndex > 0)
                    cbxAccuracyElement.SelectedIndex = 0;
                cbxAccuracyElement.Enabled = false;
                dtAccuracyFromDate.Enabled = false;
                dtAccuracyToDate.Enabled = false;
                btnRunAccuracyChk.Enabled = false;
                btnRunOrphanDTEvtChk.Enabled = false;
                btnRunOverlappingDTEvtChk.Enabled = false;
            }
        }

        private void btnRunOrphanDTEvtChk_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnRunOrphanDTEvtChk_Click");
            if (Session.DoOrphanDTEventsExist((string)cbxDiagnosisLine.SelectedItem))
            {
                lblOrphanDTEventsChkResult.Text = "Failed";
                lblOrphanDTEventsChkResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblOrphanDTEventsChkResult.Text = "Passed";
                lblOrphanDTEventsChkResult.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void btnRunOverlappingDTEvtChk_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnRunOverlappingDTEvtChk_Click");
            if (Session.DoOverlappingDTEventsExist((string)cbxDiagnosisLine.SelectedItem))
            {
                lblOverlappingDTEventsChkResult.Text = "Failed";
                lblOverlappingDTEventsChkResult.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblOverlappingDTEventsChkResult.Text = "Passed";
                lblOverlappingDTEventsChkResult.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void btnRunAccuracyChk_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnRunAccuracyChk_Click");
            if (cbxAccuracyElement.SelectedIndex > 0)
            {
                if (Convert.ToString(cbxAccuracyElement.SelectedItem).ToLower() == "downtime events")
                {
                    grdCountAccuracyData.DataSource = this._db.getDTEventsAccuracyDataTable(Convert.ToString(cbxDiagnosisLine.SelectedItem), dtAccuracyFromDate.Value, dtAccuracyToDate.Value);
                }
                else if (Convert.ToString(cbxAccuracyElement.SelectedItem).ToLower() == "case counts")
                {
                    grdCountAccuracyData.DataSource = this._db.getCaseCountsAccuracyDataTable(Convert.ToString(cbxDiagnosisLine.SelectedItem), dtAccuracyFromDate.Value, dtAccuracyToDate.Value);
                }
                else if (Convert.ToString(cbxAccuracyElement.SelectedItem).ToLower() == "piece counts")
                {
                    grdCountAccuracyData.DataSource = this._db.getPieceCountsAccuracyDataTable(Convert.ToString(cbxDiagnosisLine.SelectedItem), dtAccuracyFromDate.Value, dtAccuracyToDate.Value);
                }
            }
            else
            {
                grdCountAccuracyData.DataSource = new DataTable();
            }
        }

        private void cbxAccuracyElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("cbxAccuracyElement_SelectedIndexChanged");
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                dtAccuracyFromDate.Enabled = true;
                dtAccuracyToDate.Enabled = true;
                btnRunAccuracyChk.Enabled = true;
            }
            else
            {
                dtAccuracyFromDate.Enabled = false;
                dtAccuracyToDate.Enabled = false;
                btnRunAccuracyChk.Enabled = false;
            }
        }

        private void btnViewDiagnosis_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnViewDiagnosis_Click");
            if (btnViewDiagnosis.Text == "Show")
            {
                if (txtDiagnosisPassword.Text == "ThriveDC$")
                {
                    pnlDiagnosisResult.Show();
                    btnViewDiagnosis.Text = "Hide";
                    lblDiagnosisLoginMsg.Text = "";
                }
                else
                {
                    lblDiagnosisLoginMsg.Text = "Invalid Password!";
                    pnlDiagnosisResult.Hide();
                }
            }
            else
            {
                pnlDiagnosisResult.Hide();
                btnViewDiagnosis.Text = "Show";
                lblDiagnosisLoginMsg.Text = "";
            }
            txtDiagnosisPassword.Text = "";
        }

        public void SetConnectionStatus(bool isConnected)
        {
            ledBulb1.On = isConnected;
        }
    }
}