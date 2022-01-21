using System;
using System.Collections.Generic;
using System.Linq;
using AutomatedSolutions.Win.Comm.MB.Master.Net;
using TCom.Models;
using MB = AutomatedSolutions.Win.Comm.MB.Master;

namespace TCom
{
    internal class TS
    {
        public int insertDowntime(DowntimeData dt)
        {
            try
            {
                dt.Save();
                return dt.ID;
            }
            catch (Exception ex)
            {
                Session.logException(ex);
            }
            return -1;
        }

        public void refreshNodeLines()
        {
            Session.turnOn();

            try
            {
                if (Session.Nodes != null && Session.Nodes.Any())
                {
                    foreach (MBNode node in Session.Nodes)
                    {
                        foreach (MBHeartbeat nodeLine in node.NodeLines.Where(o => o.Value).Select(o => o.Key))
                            //If Value = true, then it is a Heartbeat
                            try
                            {
                                AsyncCallback asyncCallback = nodeLine.OnAsyncReadComplete;
                                nodeLine.Item.BeginRead(asyncCallback, nodeLine.Item);
                            }
                            catch (Exception e)
                            {
                                Session.logException(e);
                            }

                        foreach (MBCounter counter in node.NodeLines.Where(o => o.Value == false).Select(o => o.Key))
                            //Grab all Non-Heartbeats
                            try
                            {
                                AsyncCallback asyncCallback = counter.OnAsyncReadComplete;
                                counter.Item.BeginRead(asyncCallback, counter.Item);
                            }
                            catch (Exception e)
                            {
                                Session.logException(e);
                            }
                    }
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }
        }

        public List<Node> syncNodes(NodeLine[] sessionNodes)
        {
            try
            {
                if (sessionNodes != null)
                {
                    var devices = sessionNodes.ToList();
                    var lineNames = devices.Select(d => d.Line).Distinct();
                    Session.MainForm.SetLineFilterList(lineNames.ToArray());
                    var nodes = new List<Node>();
                    var objNodes = new List<Node>();
                    var filteredLines = Session.MainForm.GetLineFilterList();

                    Session.MBChannel = new Channel();
                    Session.MBChannel.Devices.Clear();

                    foreach (var device in devices)
                    {
                        var node = (from o in nodes
                                    where o.LineName == device.Line
                                    select o).FirstOrDefault();

                        bool flag = true;

                        if (node == null)
                        {
                            node = new Node
                            {
                                DeviceSetupId = device.DeviceSetupId,
                                NodeLines = new Dictionary<NodeLine, bool>(),
                                LineName = device.Line,
                                ClientName = device.Client,
                                NodeIP = device.IpAddress,
                                DowntimeThreshold = device.DowntimeThreshold,
                                UptimeThreshold = device.UptimeThreshold,
                                AddressType = device.AddressType
                            };

                            if (filteredLines.Contains(node.LineName))
                                nodes.Add(node);
                            else
                                flag = false;
                        }

                        if(flag)
                        {
                            var nodeLine = node.newNodeLine();
                            nodeLine.EventStart = null;
                            nodeLine.DeviceSetupId = device.DeviceSetupId;
                            nodeLine.DataType = device.DataType;
                            nodeLine.TagType = device.TagType;

                            nodeLine.TagName = device.TagName;
                            nodeLine.TrackType = device.TrackType;

                            switch (nodeLine.TrackType)
                            {
                                case TrackType.Heartbeat:
                                    node.NodeLines.Add(node.newMBHeartbeat(nodeLine), true);
                                    break;

                                case TrackType.CaseCounter:
                                case TrackType.PieceCounter:
                                    node.NodeLines.Add(node.newMBCounter(nodeLine), false);
                                    break;

                                case TrackType.CaseCounterHB:
                                    node.NodeLines.Add(node.newMBHybrid(nodeLine), false);
                                    break;

                                case TrackType.AlarmCode:
                                    node.NodeLines.Add(node.newMBHybrid(nodeLine), false);
                                    break;

                                case TrackType.SKU:
                                    node.NodeLines.Add(node.newMBHybrid(nodeLine), false);
                                    break;

                                default:
                                    node.NodeLines.Add(node.newMBNodeLine(nodeLine), false);
                                    break;
                            }
                        }
                    }

                    foreach (var node in nodes)
                    {
                        Session.gridStatusGroup = null;

                        var mbNode = new MBNode(node);
                        Session.gridStatusGroup = new MB.Group(true, Session.getReadRate());

                        mbNode.Device = new MB.Device(node.NodeIP)
                        {
                            AddressType = node.AddressType,
                            WordSwapFloat = false,
                            WordSwapLong = false,
                            Label = node.LineName.Trim(),
                            TimeoutTransaction = Session.getDeviceTimeoutTransaction(),
                            TimeoutConnect = Session.getDeviceTimeoutConnect()
                        };

                        mbNode.Device.Groups.Add(Session.gridStatusGroup);

                        foreach (MBHeartbeat nL in mbNode.NodeLines.Where(o => o.Value).Select(o => o.Key))
                        {
                            nL.Item = new MB.Item { HWTagType = nL.TagType, HWTagName = nL.TagName, Elements = 1 };

                            if (nL.DataType.HasValue) nL.Item.HWDataType = nL.DataType.Value; // (MB.DataType)5;

                            nL.Item.Error += nL.item_ErrorEvent;
                            nL.Item.DataChanged += nL.item_DataChanged;

                            if (Session.gridStatusGroup != null) Session.gridStatusGroup.Items.Add(nL.Item);
                        }

                        foreach (MBCounter nL in mbNode.NodeLines.Where(o => o.Value == false).Select(o => o.Key))
                        {
                            nL.Item = new MB.Item { HWTagType = nL.TagType, HWTagName = nL.TagName, Elements = 1 };

                            if (nL.DataType.HasValue) nL.Item.HWDataType = nL.DataType.Value; // (MB.DataType)5;

                            nL.Item.Error += nL.item_ErrorEvent;
                            nL.Item.DataChanged += nL.item_DataChanged;

                            if (Session.gridStatusGroup != null) Session.gridStatusGroup.Items.Add(nL.Item);
                        }

                        mbNode.Device.Groups.Add(Session.gridStatusGroup);
                        Session.MBChannel.Devices.Add(mbNode.Device);

                        objNodes.Add(mbNode);
                    }

                    Session.Nodes = objNodes;
                }
                else
                {
                    Session.log("Error Session Nodes not found.");
                }
            }
            catch (Exception e)
            {
                Session.logException(e);
            }

            return Session.Nodes;
        }


        public void updateLineStatus(string lineName, bool p, DateTime eventTime, string timer)
        {
            try
            {
                DataCollectionNode.UpdateStatus(Session.CurrentAccount.Username, lineName, p, eventTime);
                Session.DB.insertStatusLogEvent(Session.CurrentAccount.Username, lineName, p, eventTime);
                
                foreach (var node in Session.Nodes)
                    if (node.LineName == lineName)
                    {
                        foreach (var nodeLineKP in node.NodeLines.Where(nl => nl.Key is MBCounter))
                        {
                            var nodeLine = (MBCounter) nodeLineKP.Key;
                            nodeLine.UpdateValues();
                            nodeLine.saveCount(eventTime, nodeLine.getValue(), timer);
                        }
                    }
            }
            catch (Exception ex)
            {
                Session.logException(ex);
            }
        }
    }
}