using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static ZeDMD_Updater2.Esp32Device;

namespace ZeDMD_Updater2
{
    internal static class FlashAndConfig
    {
        public static void HardReboot(Esp32Device zd)
        {

            string devtype = "esp32";
            if (zd.isS3 || zd.isLilygo) devtype = "esp32s3";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("esptool.exe", "--no-stub --chip " + devtype + " --port COM" + zd.ComId.ToString() + "  flash_id");
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit(); // Wait for the process to complete
            }
        }
        public static bool FlashEsp32(Esp32Device zd, string filePath)
        {
            string devtype = "esp32";
            if (zd.isS3 || zd.isLilygo) devtype = "esp32s3";
            string commands = "--chip " + devtype + " --port COM" + zd.ComId.ToString() + "  write_flash 0x0 \"" + filePath + "\"";
            ProcessStartInfo processStartInfo = new ProcessStartInfo("esptool.exe", commands);
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit(); // Wait for the process to complete

                int exitCode = process.ExitCode;

                if (exitCode != 0)
                {
                    MessageBox.Show("The flashing failed. ESP32 are known to have an issue while flashing at connection time, retry pushing the ESP32 'BOOT' button during the connection", "Failed");
                    return false;
                }
                Thread.Sleep(1500);
                HardReboot(zd);
                return true;
            }
        }
        private static string logBox;
        private static void LogHandler(string format, IntPtr args, IntPtr pUserData)
        {
            logBox += Marshal.PtrToStringAnsi(Esp32Device.ZeDMD_FormatLogMessage(format, args, pUserData)) + "\r\n";
        }
        public static bool SetZeDmdParameters(Esp32Device device, int brightness, int rgborder, int panelclockphase, int paneldriver,
            int paneli2sspeed, int panellatchblanking, int panelminrefresh, int transport, int udpdelay, int usbpackagesize,
            string wifissid, string wifipassword, int yoffset, ref string logres)
        {
            logBox = "=== Setting ZeDMD Parameters ===\r\n";
            // create an instance
            GCHandle handle;
            IntPtr _pZeDMD = IntPtr.Zero;
            _pZeDMD = ZeDMD_GetInstance();
            ZeDMD_LogCallback callbackDelegate = new ZeDMD_LogCallback(LogHandler);
            // Keep a reference to the delegate to prevent GC from collecting it
            handle = GCHandle.Alloc(callbackDelegate);
            ZeDMD_SetLogCallback(_pZeDMD, callbackDelegate, IntPtr.Zero);
            bool openOK = false;
            if (device.isWifi) openOK = ZeDMD_OpenDefaultWiFi(_pZeDMD);
            else
            {
                string comport = @"COM" + device.ComId.ToString();
                ZeDMD_SetDevice(_pZeDMD, comport);
                openOK = ZeDMD_Open(_pZeDMD);
            }
            if (openOK)
            {
                ZeDMD_SetBrightness(_pZeDMD, (byte)brightness);
                ZeDMD_SetRGBOrder(_pZeDMD, (byte)rgborder);
                ZeDMD_SetPanelClockPhase(_pZeDMD, (byte)panelclockphase);
                ZeDMD_SetPanelDriver(_pZeDMD, (byte)paneldriver);
                ZeDMD_SetPanelI2sSpeed(_pZeDMD, (byte)paneli2sspeed);
                ZeDMD_SetPanelLatchBlanking(_pZeDMD, (byte)panellatchblanking);
                ZeDMD_SetPanelMinRefreshRate(_pZeDMD, (byte)panelminrefresh);
                ZeDMD_SetTransport(_pZeDMD, (byte)transport);
                ZeDMD_SetUdpDelay(_pZeDMD, (byte)udpdelay);
                ZeDMD_SetUsbPackageSize(_pZeDMD, (ushort)usbpackagesize);
                ZeDMD_SetYOffset(_pZeDMD, (byte)yoffset);
                if (wifissid != "")
                {
                    ZeDMD_SetWiFiSSID(_pZeDMD, wifissid);
                    ZeDMD_SetWiFiPassword(_pZeDMD, wifipassword);
                    //Esp32Device.ZeDMD_SetWiFiPort(_pZeDMD, 3333);
                }
                ZeDMD_SaveSettings(_pZeDMD);
                ZeDMD_Reset(_pZeDMD);
                ZeDMD_Close(_pZeDMD);
                device.Brightness = brightness;
                device.RgbOrder = rgborder;
                device.PanelClockPhase = panelclockphase;
                device.PanelDriver = paneldriver;
                device.PanelI2SSpeed = paneli2sspeed;
                device.PanelLatchBlanking = panellatchblanking;
                device.PanelMinRefreshRate = panelminrefresh;
                if (transport == 1 || transport == 2) device.isWifi = true;
                else device.isWifi = false;
                device.UdpDelay = udpdelay;
                device.UsbPacketSize = usbpackagesize;
                device.YOffset = yoffset;
                device.SSID = wifissid;
            }
            else
            {
                MessageBox.Show("Unable to Open the device on COM" + device.ComId.ToString() + " to set the parameters");
                logBox += "Unable to Open the device on COM" + device.ComId.ToString() + " to set the parameters\r\n";
                logres = logBox;
                return false;
            }
            logres = logBox;
            return true;
        }
    }
}
