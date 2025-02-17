using System;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static bool SetZeDmdParameters(Esp32Device device, int brightness, int rgborder, int panelclockphase, int paneldriver,
            int paneli2sspeed, int panellatchblanking, int panelminrefresh, int transport, int udpdelay, int usbpackagesize,
            string wifissid, string wifipassword, int yoffset)
        {
            // using libzedmd api
            IntPtr _pZeDMD = IntPtr.Zero;
            _pZeDMD = Esp32Device.ZeDMD_GetInstance();
            string comport = @"COM" + device.ComId.ToString();
            Esp32Device.ZeDMD_SetDevice(_pZeDMD, comport);
            if (Esp32Device.ZeDMD_Open(_pZeDMD))
            {
                Esp32Device.ZeDMD_SetBrightness(_pZeDMD, (byte)brightness);
                Esp32Device.ZeDMD_SetRGBOrder(_pZeDMD, (byte)rgborder);
                Esp32Device.ZeDMD_SetPanelClockPhase(_pZeDMD, (byte)panelclockphase);
                Esp32Device.ZeDMD_SetPanelDriver(_pZeDMD, (byte)paneldriver);
                Esp32Device.ZeDMD_SetPanelI2sSpeed(_pZeDMD, (byte)paneli2sspeed);
                Esp32Device.ZeDMD_SetPanelLatchBlanking(_pZeDMD, (byte)panellatchblanking);
                Esp32Device.ZeDMD_SetPanelMinRefreshRate(_pZeDMD, (byte)panelminrefresh);
                Esp32Device.ZeDMD_SetTransport(_pZeDMD, (byte)transport);
                Esp32Device.ZeDMD_SetUdpDelay(_pZeDMD, (byte)udpdelay);
                Esp32Device.ZeDMD_SetUsbPackageSize(_pZeDMD, (UInt16)usbpackagesize);
                Esp32Device.ZeDMD_SetYOffset(_pZeDMD, (byte)yoffset);
                if (wifissid!="")
                {
                    Esp32Device.ZeDMD_SetWiFiSSID(_pZeDMD, wifissid);
                    Esp32Device.ZeDMD_SetWiFiPassword(_pZeDMD, wifipassword);
                }
                Esp32Device.ZeDMD_SaveSettings(_pZeDMD);
                Esp32Device.ZeDMD_Close(_pZeDMD);
            }
            else
            {
                MessageBox.Show("Unable to Open the device on COM" + device.ComId.ToString() + " to set the parameters");
                return false;
            }
            if (Esp32Device.ZeDMD_Open(_pZeDMD))
            {
                Esp32Device.GetZeDMDValues(device, _pZeDMD);
                Esp32Device.ZeDMD_Close(_pZeDMD);
                return true;
            }
            else
            {
                MessageBox.Show("Unable to Open the device on COM" + device.ComId.ToString() + " to set the parameters");
                return false;
            }
        }
    }
}
