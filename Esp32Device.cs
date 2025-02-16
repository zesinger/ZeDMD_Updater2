using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZeDMD_Updater2
{
    internal class Esp32Device
    {
        public int ComId { get; set; } = -1;
        public bool isUnknown { get; set; } = true;
        public bool isS3 { get; set; } = false;
        public bool isLilygo { get; set; } = false;
        public bool isZeDMD { get; set; } = false;
        public bool isWifi { get; set; } = false;
        public string SSID { get; set; } = "";
        public int SSIDPort { get; set; } = -1;
        //public bool isCdc { get; set; } = false;
        public int RgbOrder { get; set; } = 0;
        public int Brightness { get; set; } = 6;
        public int Width { get; set; } = 128;
        public int Height { get; set; } = 32;
        public int MajVersion { get; set; } = 0;
        public int MinVersion { get; set; } = 0;
        public int PatVersion { get; set; } = 0;
        public int PanelDriver { get; set; } = 0;
        public int PanelClockPhase { get; set; } = 0;
        public int PanelI2SSpeed { get; set; } = 8;
        public int PanelLatchBlanking { get; set; } = 0;
        public int PanelMinRefreshRate { get; set; } = 0;
        public int UsbPacketSize { get; set; } = 64;
        public int UdpDelay { get; set; } = 0;
        public int YOffset { get; set; } = 0;

        public Esp32Device(int comid, bool iss3, bool islilygo, bool isunknown)
        {
            ComId = comid;
            isS3 = iss3;
            isLilygo = islilygo;
            isUnknown = isunknown;
        }
        public static void CheckZeDMDs(ref List<Esp32Device> esp32Devices)
        {
            IntPtr _pZeDMD = IntPtr.Zero;
            _pZeDMD = ZeDMD_GetInstance();
            foreach (var device in esp32Devices)
            {
                string comport = @"COM" + device.ComId.ToString();
                ZeDMD_SetDevice(_pZeDMD, comport);
                if (ZeDMD_Open(_pZeDMD))
                {
                    device.isZeDMD = true;
                    device.isS3 = ZeDMD_IsS3(_pZeDMD);
                    // get the firmware version
                    string version = Marshal.PtrToStringAnsi(ZeDMD_GetFirmwareVersion(_pZeDMD));
                    string[] parts = version.Split('.');
                    int.TryParse(parts[0], out int major);
                    int.TryParse(parts[1], out int minor);
                    int.TryParse(parts[2], out int patch);
                    device.MajVersion = major;
                    device.MinVersion = minor;
                    device.PatVersion = patch;
                    // get the RGB order
                    device.RgbOrder = ZeDMD_GetRGBOrder(_pZeDMD);
                    // get the brightness
                    device.Brightness = ZeDMD_GetBrightness(_pZeDMD);
                    // get the width
                    device.Width = ZeDMD_GetPanelWidth(_pZeDMD);
                    if (device.Width == 0) device.Width = 256;
                    // get the height
                    device.Height = ZeDMD_GetPanelHeight(_pZeDMD);
                    // get the Wifi SSID (if available, "" if not) and port
                    device.SSID = Marshal.PtrToStringAnsi(ZeDMD_GetWiFiSSID(_pZeDMD));
                    if (device.SSID != "") device.SSIDPort = ZeDMD_GetWiFiPort(_pZeDMD);
                    else device.SSIDPort = -1;
                    device.PanelDriver = ZeDMD_GetPanelDriver(_pZeDMD);
                    device.PanelClockPhase = ZeDMD_GetPanelClockPhase(_pZeDMD);
                    device.PanelI2SSpeed = ZeDMD_GetPanelI2sSpeed(_pZeDMD);
                    device.PanelLatchBlanking = ZeDMD_GetPanelLatchBlanking(_pZeDMD);
                    device.PanelMinRefreshRate = ZeDMD_GetPanelMinRefreshRate(_pZeDMD);
                    device.UsbPacketSize = ZeDMD_GetUsbPackageSize(_pZeDMD);
                    device.UdpDelay = ZeDMD_GetUdpDelay(_pZeDMD);
                    device.YOffset = ZeDMD_GetYOffset(_pZeDMD);
                    ZeDMD_Close(_pZeDMD);
                }
            }
        }

        public static void LedTest(int devCOM)
        {
            IntPtr _pZeDMD = IntPtr.Zero;
            _pZeDMD = ZeDMD_GetInstance();
            string comport = @"\\.\COM" + devCOM.ToString();
            ZeDMD_SetDevice(_pZeDMD, comport);
            if (ZeDMD_Open(_pZeDMD))
            {
                ZeDMD_LedTest(_pZeDMD);
                ZeDMD_Close(_pZeDMD);
            }
        }

        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI ZeDMD* ZeDMD_GetInstance();
        public static extern IntPtr ZeDMD_GetInstance();
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetDevice(ZeDMD* pZeDMD, const char* const device);
        public static extern bool ZeDMD_SetDevice(IntPtr pZeDMD, string device);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI bool ZeDMD_Open(ZeDMD* pZeDMD);
        public static extern bool ZeDMD_Open(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI bool ZeDMD_IsS3(ZeDMD* pZeDMD);
        protected static extern bool ZeDMD_IsS3(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_Close(ZeDMD* pZeDMD);
        public static extern void ZeDMD_Close(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI const char* ZeDMD_GetFirmwareVersion(ZeDMD* pZeDMD);
        protected static extern IntPtr ZeDMD_GetFirmwareVersion(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetRGBOrder(ZeDMD* pZeDMD);
        private static extern byte ZeDMD_GetRGBOrder(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetBrightness(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetBrightness(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetBrightness(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetYOffset(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint16_t ZeDMD_GetWidth(ZeDMD* pZeDMD);
        protected static extern UInt16 ZeDMD_GetPanelWidth(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint16_t ZeDMD_GetHeight(ZeDMD* pZeDMD);
        protected static extern UInt16 ZeDMD_GetPanelHeight(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI const char* ZeDMD_GetWiFiSSID(ZeDMD* pZeDMD);
        private static extern IntPtr ZeDMD_GetWiFiSSID(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI int ZeDMD_GetWiFiPort(ZeDMD* pZeDMD);
        private static extern int ZeDMD_GetWiFiPort(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint16_t ZeDMD_GetUsbPackageSize(ZeDMD* pZeDMD);
        protected static extern UInt16 ZeDMD_GetUsbPackageSize(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetUdpDelay(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetUdpDelay(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetPanelDriver(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetPanelDriver(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetPanelClockPhase(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetPanelClockPhase(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetPanelI2sSpeed(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetPanelI2sSpeed(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetPanelLatchBlanking(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetPanelLatchBlanking(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI uint8_t ZeDMD_GetPanelMinRefreshRate(ZeDMD* pZeDMD);
        protected static extern byte ZeDMD_GetPanelMinRefreshRate(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetRGBOrder(ZeDMD* pZeDMD, uint8_t rgbOrder);
        public static extern void ZeDMD_SetRGBOrder(IntPtr pZeDMD, byte rgbOrder);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetBrightness(ZeDMD* pZeDMD, uint8_t brightness);
        public static extern void ZeDMD_SetBrightness(IntPtr pZeDMD, byte brightness);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetWiFiSSID(ZeDMD* pZeDMD, const char* const ssid);
        public static extern void ZeDMD_SetWiFiSSID(IntPtr pZeDMD, string ssid);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetWiFiPassword(ZeDMD* pZeDMD, const char* const password);
        public static extern void ZeDMD_SetWiFiPassword(IntPtr pZeDMD, string password);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetPanelDriver(ZeDMD* pZeDMD, uint8_t driver);
        public static extern void ZeDMD_SetPanelDriver(IntPtr pZeDMD, byte uint8_t);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetPanelClockPhase(ZeDMD* pZeDMD, uint8_t clockPhase);
        public static extern void ZeDMD_SetPanelClockPhase(IntPtr pZeDMD, byte clockPhase);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetPanelI2sSpeed(ZeDMD* pZeDMD, uint8_t i2sSpeed);
        public static extern void ZeDMD_SetPanelI2sSpeed(IntPtr pZeDMD, byte i2sSpeed);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetPanelLatchBlanking(ZeDMD* pZeDMD, uint8_t latchBlanking);
        public static extern void ZeDMD_SetPanelLatchBlanking(IntPtr pZeDMD, byte latchBlanking);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetPanelMinRefreshRate(ZeDMD* pZeDMD, uint8_t minRefreshRate);
        public static extern void ZeDMD_SetPanelMinRefreshRate(IntPtr pZeDMD, byte minRefreshRate);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetUdpDelay(ZeDMD* pZeDMD, uint8_t udpDelay);
        public static extern void ZeDMD_SetUdpDelay(IntPtr pZeDMD, byte udpDelay);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetUsbPackageSize(ZeDMD* pZeDMD, uint16_t usbPackageSize);
        public static extern void ZeDMD_SetUsbPackageSize(IntPtr pZeDMD, UInt16 usbPackageSize);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetYOffset(ZeDMD* pZeDMD, uint8_t yOffset);
        public static extern void ZeDMD_SetYOffset(IntPtr pZeDMD, byte yOffset);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SetTransport(ZeDMD* pZeDMD, uint8_t transport);
        public static extern void ZeDMD_SetTransport(IntPtr pZeDMD, byte transport);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_SaveSettings(ZeDMD* pZeDMD);
        private static extern void ZeDMD_SaveSettings(IntPtr pZeDMD);
        [DllImport("zedmd64.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        // C format: extern ZEDMDAPI void ZeDMD_LedTest(ZeDMD* pZeDMD);
        private static extern void ZeDMD_LedTest(IntPtr pZeDMD);
    }
}
