using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ZeDMD_Updater2
{
    internal static class Esp32Devices
    {
        public static List<Esp32Device> esp32Devices = new List<Esp32Device>();
        public static Esp32Device wifiDevice = new Esp32Device(-1, false, false, false);
        public static int[] I2SallowedSpeed = { 8, 16, 20 };
        public static void FillEsp32Values(int deviceId, MainForm form)
        {
            Esp32Device ed = WhichDevice(form);
            if (ed!=null && ed.isZeDMD)
            {
                form.numericPDriver.Value = Clamp(ed.PanelDriver, Convert.ToInt32(form.numericPDriver.Minimum), Convert.ToInt32(form.numericPDriver.Maximum));
                form.numericPCPhase.Value = Clamp(ed.PanelClockPhase, Convert.ToInt32(form.numericPCPhase.Minimum), Convert.ToInt32(form.numericPCPhase.Maximum));
                // we take the closest available I2SSpeed
                form.numericPISpeed.Value = I2SallowedSpeed.OrderBy(v => Math.Abs(v - ed.PanelI2SSpeed)).First(); ;
                form.numericPLBlanking.Value = Clamp(ed.PanelLatchBlanking, Convert.ToInt32(form.numericPLBlanking.Minimum), Convert.ToInt32(form.numericPLBlanking.Maximum));
                form.numericPMRRate.Value = Clamp(ed.PanelMinRefreshRate, Convert.ToInt32(form.numericPMRRate.Minimum), Convert.ToInt32(form.numericPMRRate.Maximum));
                form.numericROrder.Value = Clamp(ed.RgbOrder, Convert.ToInt32(form.numericROrder.Minimum), Convert.ToInt32(form.numericROrder.Maximum));
                form.numericBrightness.Value = Clamp(ed.Brightness, Convert.ToInt32(form.numericBrightness.Minimum), Convert.ToInt32(form.numericBrightness.Maximum));
                form.numericUPSize.Value = Clamp(ed.UsbPacketSize, Convert.ToInt32(form.numericUPSize.Minimum), Convert.ToInt32(form.numericUPSize.Maximum));
                form.numericUDelay.Value = Clamp(ed.UdpDelay, Convert.ToInt32(form.numericUDelay.Minimum), Convert.ToInt32(form.numericUDelay.Maximum));
                if (ed.Width == 128 && ed.Height == 64)
                {
                    form.radio12832.Checked = false;
                    form.radio12864.Checked = true;
                    form.radio25664.Checked = false;
                }
                else if (ed.Width == 256 && ed.Height == 64)
                {
                    form.radio12832.Checked = false;
                    form.radio12864.Checked = false;
                    form.radio25664.Checked = true;
                }
                else
                {
                    form.radio12832.Checked = true;
                    form.radio12864.Checked = false;
                    form.radio25664.Checked = false;
                }
                form.textSsid.Text = ed.SSID;
                form.textPassword.Text = "";
            }
            else
            {
                form.numericPDriver.Value = 0;
                form.numericPCPhase.Value = 0;
                form.numericPISpeed.Value = 8;
                form.numericPLBlanking.Value = 2;
                form.numericPMRRate.Value = 30;
                form.numericROrder.Value = 0;
                form.numericBrightness.Value = 6;
                form.radio12832.Checked = true;
                form.radio12864.Checked = false;
                form.radio25664.Checked = false;
                form.textSsid.Text = "";
                form.textPassword.Text = "";
                form.numericUPSize.Value = 64;
                form.numericUDelay.Value = 5;
            }
        }
        private readonly static (string chip, bool s3, bool lilygo)[] USBtoSerialDevices = { ("CP210x", false, false), ("CH340", false, false), ("CH9102", true, true), ("CH343", true, false) };
        public static void GetPortNames()
        {
            using (var searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

                for (int tj = 0; tj < portList.Count; tj++)
                {
                    bool devfound = false;
                    foreach (var device in USBtoSerialDevices)
                    {
                        if (portList[tj].Contains(device.chip))
                        {
                            Match match = Regex.Match(portList[tj], @"COM(\d+)");
                            if (match.Success)
                            {
                                devfound = true;
                                string comNumberString = match.Groups[0].Value;
                                esp32Devices.Add(new Esp32Device(int.Parse(comNumberString.Substring(3)), device.s3, device.lilygo, false));
                                break;
                            }
                        }
                    }
                    if (!devfound)
                    {
                        Match match = Regex.Match(portList[tj], @"COM(\d+)");
                        if (match.Success)
                        {
                            string comNumberString = match.Groups[0].Value;
                            esp32Devices.Add(new Esp32Device(int.Parse(comNumberString.Substring(3)), false, false, true));
                        }
                    }
                }
            }
        }
        public static Esp32Device WhichDevice(MainForm form)
        {
            if (form.deviceView.SelectedItems.Count == 0) return null;
            int selit = form.deviceView.SelectedItems[0].Index;
            if (selit > 0 && !wifiDevice.isUnknown) return esp32Devices[selit - 1];
            else if (wifiDevice.isUnknown) return esp32Devices[selit];
            return wifiDevice;
        }
        public static string DisplayedVersion(Esp32Device device)
        {
            return device.MajVersion.ToString() + "." + device.MinVersion.ToString() + "." + device.PatVersion.ToString();
        }
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        public static void GetAvailableDevices()
        {
            Thread.Sleep(500);
            esp32Devices.Clear();
            GetPortNames();
            Esp32Device.CheckZeDMDs(ref esp32Devices,ref wifiDevice);
            //zdc.Open(ref ZeDMDW, ref ZeDMDH, out nZeDMDCOMs, ref ZeDMDCOMs, ref majVersion, ref minVersion, ref patVersion, ref brightness, ref RGBorder);
        }
        public static void PopulateESP(MainForm form)
        {
            form.deviceView.Items.Clear();
            form.deviceView.ShowGroups = true;
            ListView.ColumnHeaderCollection lvchc = form.deviceView.Columns;
            if (!wifiDevice.isUnknown)
            {
                ListViewItem item = new ListViewItem(new string[form.deviceView.Columns.Count]);
                item.SubItems[lvchc.IndexOf(form.columnCOM)].Text = wifiDevice.ComId.ToString();
                string tstr;
                if (wifiDevice.isUnknown) tstr = "Unknown";
                else if (wifiDevice.isZeDMD) tstr = "ZeDMD";
                else tstr = "Stock ESP32";
                item.SubItems[lvchc.IndexOf(form.columnType)].Text = tstr;
                if (wifiDevice.isS3) tstr = "Yes"; else tstr = "No";
                item.SubItems[lvchc.IndexOf(form.columnS3)].Text = tstr;
                if (wifiDevice.isLilygo) tstr = "Yes"; else tstr = "No";
                item.SubItems[lvchc.IndexOf(form.columnLilygo)].Text = tstr;
                item.SubItems[lvchc.IndexOf(form.columnDevId)].Text = ((UInt16)wifiDevice.ZeID).ToString("X4");
                item.SubItems[lvchc.IndexOf(form.columnWifiIP)].Text = wifiDevice.WifiIp;
                if (wifiDevice.MajVersion != InternetFirmwares.avmajVersion || wifiDevice.MinVersion != InternetFirmwares.avminVersion ||
                    wifiDevice.PatVersion != InternetFirmwares.avpatVersion)
                {
                    item.SubItems[lvchc.IndexOf(form.columnVersion)].Text = "*" + DisplayedVersion(wifiDevice) + "*";
                }
                else item.SubItems[lvchc.IndexOf(form.columnVersion)].Text = DisplayedVersion(wifiDevice);
                item.SubItems[lvchc.IndexOf(form.columnWidth)].Text = wifiDevice.Width.ToString();
                item.SubItems[lvchc.IndexOf(form.columnHeight)].Text = wifiDevice.Height.ToString();
                form.deviceView.Items.Add(item);
            }
            foreach (var device in esp32Devices)
            {
                ListViewItem item = new ListViewItem(new string[form.deviceView.Columns.Count]);
                item.SubItems[lvchc.IndexOf(form.columnCOM)].Text = device.ComId.ToString();
                string tstr;
                if (device.isUnknown) tstr = "Unknown";
                else if (device.isZeDMD) tstr = "ZeDMD";
                else tstr = "Stock ESP32";
                item.SubItems[lvchc.IndexOf(form.columnType)].Text = tstr;
                if (device.isS3) tstr = "Yes"; else tstr = "No";
                item.SubItems[lvchc.IndexOf(form.columnS3)].Text = tstr;
                if (device.isLilygo) tstr = "Yes"; else tstr = "No";
                item.SubItems[lvchc.IndexOf(form.columnLilygo)].Text = tstr;
                item.SubItems[lvchc.IndexOf(form.columnDevId)].Text = ((UInt16)device.ZeID).ToString("X4");
                item.SubItems[lvchc.IndexOf(form.columnWifiIP)].Text = "-----------";
                if (device.isZeDMD)
                {
                    if (device.MajVersion != InternetFirmwares.avmajVersion || device.MinVersion != InternetFirmwares.avminVersion ||
                        device.PatVersion != InternetFirmwares.avpatVersion)
                    {
                        item.SubItems[lvchc.IndexOf(form.columnVersion)].Text = "*" + DisplayedVersion(device) + "*";
                    }
                    else item.SubItems[lvchc.IndexOf(form.columnVersion)].Text = DisplayedVersion(device);
                    item.SubItems[lvchc.IndexOf(form.columnWidth)].Text = device.Width.ToString();
                    item.SubItems[lvchc.IndexOf(form.columnHeight)].Text = device.Height.ToString();
                }
                else
                {
                    item.SubItems[lvchc.IndexOf(form.columnVersion)].Text = "-";
                    item.SubItems[lvchc.IndexOf(form.columnWidth)].Text = "-";
                    item.SubItems[lvchc.IndexOf(form.columnHeight)].Text = "-";
                }
                form.deviceView.Items.Add(item);
            }
            if (form.deviceView.Items.Count ==1) form.deviceView.SelectedIndices.Add(0);
            form.buttonLTest.Enabled = false;
        }
    }
}
