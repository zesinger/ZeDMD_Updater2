using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZeDMD_Updater2
{
    internal static class InternetFirmwares
    {
        public const int MIN_MAJOR_VERSION= 5;
        public const int MIN_MINOR_VERSION = 1;
        public const int MIN_PATCH_VERSION = 0;
        const int MAX_VERSIONS_TO_LIST = 64;
        public static byte[] avMVersion = new byte[MAX_VERSIONS_TO_LIST];
        public static byte[] avmVersion = new byte[MAX_VERSIONS_TO_LIST];
        public static byte[] avpVersion = new byte[MAX_VERSIONS_TO_LIST];
        public static byte avmajVersion = 0;
        public static byte avminVersion = 0;
        public static byte avpatVersion = 0;
        private readonly static string[] FirmwareFiles = { "ZeDMD-128x32.zip", "ZeDMD-128x64.zip", "ZeDMD-256x64.zip",
            " ZeDMD-LilygoS3Amoled_128x32.zip ", "ZeDMD-LilygoS3Amoled_128x32_wifi.zip",
            "ZeDMD-S3-N16R8_128x32.zip","ZeDMD-S3-N16R8_128x64.zip","ZeDMD-S3-N16R8_256x64.zip"};
        private static bool[] FirmwareFilesAvailable = new bool[FirmwareFiles.Length];
        public static byte navVersions = 0;

        public static bool IsUrlAvailable(string url, int timeoutMilliseconds)
        {
            bool isAvailable = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            // Create a timer to handle the timeout
            using (var timer = new System.Threading.Timer(state => request.Abort(), null, timeoutMilliseconds, Timeout.Infinite))
            {
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        isAvailable = response.StatusCode == HttpStatusCode.OK;
                    }
                }
                catch
                {
                    // Handle exceptions, e.g., request timed out or failed
                    // You can log the exception or take appropriate action
                    isAvailable = false;
                }
            }
            return isAvailable;
        }
        public static bool IsVersionAvailable(byte majv, byte minv, byte patv)
        {
            string URL = "https://github.com/PPUC/ZeDMD/releases/download/v" + majv.ToString() + "." + minv.ToString() + "." + patv.ToString() + "/ZeDMD-128x32.zip";
            return IsUrlAvailable(URL, 1000);
        }
        public static int ValVersion(byte M, byte m, byte p)
        {
            return (int)(M << 16) + (int)(m << 8) + (int)p;
        }
        public static void CheckPages(string url)
        {
            for (int i = 0; i < FirmwareFiles.Length; i++)
            {
                if (IsUrlAvailable(url + FirmwareFiles[i], 1000)) FirmwareFilesAvailable[i] = true; else FirmwareFilesAvailable[i] = false;
            }
        }
        public static void PopulateVersions(MainForm form)
        {
            form.versionList.Items.Clear();
            int idxmax = 0;
            //versionList.Items.Add("v"+avmajVersion.ToString()+"."+avminVersion.ToString()+"."+avpatVersion.ToString());
            for (int ti = 0; ti < navVersions; ti++)
            {
                form.versionList.Items.Add("v" + avMVersion[navVersions - ti - 1].ToString() + "." + avmVersion[navVersions - ti - 1].ToString() + "." + avpVersion[navVersions - ti - 1].ToString());
                if (avMVersion[navVersions - ti - 1] == avmajVersion && avmVersion[navVersions - ti - 1] == avminVersion &&
                    avpVersion[navVersions - ti - 1] == avpatVersion) idxmax = ti;
            }
            form.versionList.SelectedIndex = idxmax;

        }
        public static string GetAvailableVersions()
        {











            //return "0.0.0";













            string zipFileUrl = "https://github.com/PPUC/ZeDMD/releases/latest/download/ZeDMD-128x32.zip";
            string versionFileName = "version.txt";

            using (WebClient client = new WebClient())
            {
                byte[] zipData = client.DownloadData(zipFileUrl);

                using (MemoryStream zipStream = new MemoryStream(zipData))
                using (ZipArchive archive = new ZipArchive(zipStream))
                {
                    ZipArchiveEntry versionEntry = archive.GetEntry(versionFileName);
                    if (versionEntry != null)
                    {
                        using (Stream versionStream = versionEntry.Open())
                        {
                            // Read firmware file into memory
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                versionStream.CopyTo(memoryStream);
                                byte[] bytes = memoryStream.ToArray();
                                string versionData = Encoding.ASCII.GetString(bytes);
                                string[] parts = versionData.Split('.');
                                avmajVersion = byte.Parse(parts[0]);
                                avminVersion = byte.Parse(parts[1]);
                                avpatVersion = byte.Parse(parts[2]);
                            }
                            versionStream.Close();
                        }
                    }
                    else return "";
                }
            }
            byte majv = MIN_MAJOR_VERSION, minv = MIN_MINOR_VERSION, patv = MIN_PATCH_VERSION;
            navVersions = 0;
            bool over = false;
            while ((ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion)) && !over)
            {
                if (IsVersionAvailable(majv, minv, patv))
                {
                    avMVersion[navVersions] = majv;
                    avmVersion[navVersions] = minv;
                    avpVersion[navVersions] = patv;
                    navVersions++;
                    if (navVersions == MAX_VERSIONS_TO_LIST - 2) break;
                    patv++;
                }
                else
                {
                    patv = 0;
                    minv++;
                    if (ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion))
                    {
                        if (IsVersionAvailable(majv, minv, patv))
                        {
                            avMVersion[navVersions] = majv;
                            avmVersion[navVersions] = minv;
                            avpVersion[navVersions] = patv;
                            navVersions++;
                            if (navVersions == MAX_VERSIONS_TO_LIST - 2) break;
                            patv++;
                        }
                        else
                        {
                            minv = 0;
                            majv++;
                            if (ValVersion(majv, minv, patv) < ValVersion(avmajVersion, avminVersion, avpatVersion))
                            {
                                if (IsVersionAvailable(majv, minv, patv))
                                {
                                    avMVersion[navVersions] = majv;
                                    avmVersion[navVersions] = minv;
                                    avpVersion[navVersions] = patv;
                                    navVersions++;
                                    if (navVersions == MAX_VERSIONS_TO_LIST - 2) break;
                                    patv++;
                                }
                                else over = true;
                            }
                        }
                    }
                }
            }
            avMVersion[navVersions] = avmajVersion;
            avmVersion[navVersions] = avminVersion;
            avpVersion[navVersions] = avpatVersion;
            navVersions++;
            return avmajVersion.ToString() + "." + avminVersion.ToString() + "." + avpatVersion.ToString();
        }
    }
}
