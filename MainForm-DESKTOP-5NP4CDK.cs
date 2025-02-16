using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace ZeDMD_Updater2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            deviceView.ColumnWidthChanging += (sender, e) =>
            {
                e.NewWidth = deviceView.Columns[e.ColumnIndex].Width; // Keep the original width
                e.Cancel = true; // Prevent resizing
            };
            deviceView.OwnerDraw = true; // Enable custom drawing
            deviceView.DrawColumnHeader += deviceView_DrawColumnHeader;
            deviceView.DrawSubItem += deviceView_DrawSubItem;
            Enabled = false;
            deviceView.MouseDoubleClick += deviceView_MouseDoubleClick;
            string latestVersion = "";
            WaitForm waitForm = new WaitForm();
            waitForm.mainText.Text = "Please wait while listing the available firmwares and devices...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, ev) =>
            {
                latestVersion = InternetFirmwares.GetAvailableVersions();
                Esp32Devices.GetAvailableDevices();
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                LatestVersion.Text = "Latest version available: " + latestVersion;
                InternetFirmwares.PopulateVersions(this);
                Esp32Devices.PopulateESP(this);
                Enabled = true;
                waitForm.Hide();
            };
            Enabled = false;
            worker.RunWorkerAsync();
            waitForm.ShowDialog();
        }
        private void UpdateZeDMDList()
        {
            Enabled = false;
            WaitForm waitForm = new WaitForm();
            waitForm.mainText.Text = "Please wait while updating the available devices...";
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, ev) =>
            {
                Esp32Devices.GetAvailableDevices();
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                Esp32Devices.PopulateESP(this);
                Enabled = true;
                waitForm.Hide();
            };
            worker.RunWorkerAsync();
            waitForm.ShowDialog();
        }
        private void deviceView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        // Custom subitem coloring
        private void deviceView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Item.SubItems[4].Text.StartsWith("*"))
            {
                e.Graphics.DrawString(e.SubItem.Text, deviceView.Font, Brushes.Red, e.Bounds);
            }
            else
            {
                e.DrawDefault = true; // Use default drawing for other items
            }
        }
        private void radioWUdp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioWUdp.Checked)
            {
                radioUsb.Checked = false;
                numericUDelay.Enabled = true;
                textSsid.Enabled = true;
                textPassword.Enabled = true;
                numericUPSize.Enabled = false;
            }
        }
        private void radioWTcp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioWTcp.Checked)
            {
                radioUsb.Checked = false;
                numericUDelay.Enabled = false;
                numericUDelay.Enabled = false;
                textSsid.Enabled = true;
                textPassword.Enabled = true;
                numericUPSize.Enabled = false;
            }
        }
        private void radioUsb_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUsb.Checked)
            {
                radioWTcp.Checked = false;
                radioWUdp.Checked = false;
                numericUDelay.Enabled = false;
                textSsid.Enabled = false;
                textPassword.Enabled = false;
                numericUPSize.Enabled = true;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int current = (int)numericPISpeed.Value;

            // Find the closest allowed value
            int closest = Esp32Devices.I2SallowedSpeed.OrderBy(v => Math.Abs(v - current)).First();

            // Update if necessary
            if (closest != current)
            {
                numericPISpeed.Value = closest;
            }
        }
        private void deviceView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceView.SelectedItems.Count == 1)
            {
                buttonFFile.Enabled = true;
                Esp32Device ed = Esp32Devices.esp32Devices[deviceView.SelectedItems[0].Index];
                if (ed.isZeDMD)
                {
                    buttonLTest.Enabled = true;
                    buttonSNParameters.Enabled = true;
                }
                else
                {
                    buttonLTest.Enabled = false;
                    buttonSNParameters.Enabled = false;
                    radio12832.Checked = true;
                    radio12864.Checked = false;
                    radio25664.Checked = false;
                }
                if (versionList.SelectedIndex >= 0) buttonFlash.Enabled = true;
                else buttonFlash.Enabled = false;
                Esp32Devices.FillEsp32Values(deviceView.SelectedItems[0].Index, this);
            }
            else
            {
                buttonFFile.Enabled = false;
                buttonLTest.Enabled = false;
                buttonSNParameters.Enabled = false;
                Esp32Devices.FillEsp32Values(-1, this);
                buttonFlash.Enabled = false;
            }

        }

        private void versionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceView.SelectedItems.Count == 1)
            {
                if (versionList.SelectedIndex >= 0) buttonFlash.Enabled = true;
                else buttonFlash.Enabled = false;
            }
            else buttonFlash.Enabled = false;
        }

        private void buttonFFile_Click(object sender, EventArgs e)
        {
            Enabled = false;
            bool flashed = false;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BIN Files (*.bin)|*.bin",
                Title = "Select a BIN file"
            };

            // Show the dialog and get result
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;
                Esp32Device zeddev = Esp32Devices.esp32Devices[deviceView.SelectedItems[0].Index];
                string devtype = "esp32";
                if (zeddev.isS3 || zeddev.isLilygo) devtype = "esp32s3";
                string commandArgs = "--chip " + devtype + " --port COM" + zeddev.ComId.ToString() + "  write_flash 0x0 \"" + filePath + "\"";
                flashed = FlashAndConfig.FlashEsp32(commandArgs);
                if (flashed) CalcAndSetParameters();
            }
            Enabled = true;
            if (flashed)
            {
                //MessageBox.Show("Configure your ZeDMD with the menu, exit the menu, then click \"OK\"");
                UpdateZeDMDList();
            }
        }

        private void buttonLTest_Click(object sender, EventArgs e)
        {
            Esp32Device.LedTest(Esp32Devices.esp32Devices[deviceView.SelectedItems[0].Index].ComId);
        }

        private void buttonFlash_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Esp32Device zeddev = Esp32Devices.esp32Devices[deviceView.SelectedItems[0].Index];
            int vernum = versionList.SelectedIndex;
            if (vernum < 0) return;
            vernum = InternetFirmwares.navVersions - vernum - 1;
            string strVersion = "v" + InternetFirmwares.avMVersion[vernum].ToString() + "." + InternetFirmwares.avmVersion[vernum].ToString() + "." + InternetFirmwares.avpVersion[vernum].ToString();
            string zipFileUrl;
            bool flashed = false;
            zipFileUrl = "https://github.com/PPUC/ZeDMD/releases/download/" + strVersion + "/ZeDMD-";
            if (zeddev.isLilygo)
            {
                if (radio12864.Checked || radio25664.Checked)
                {
                    MessageBox.Show("Lilygo firmwares are only available for 128x32 format, this resolution is going to be flashed.");
                    radio12832.Checked = true;
                    radio12864.Checked = false;
                    radio25664.Checked = false;
                }
                zipFileUrl += "LilygoS3Amoled_128x32";
                if (radioWUdp.Checked || radioWTcp.Checked) zipFileUrl += "_wifi";
            }
            else
            {
                if (zeddev.isS3) zipFileUrl += "S3-N16R8_";
                if (radio12864.Checked) zipFileUrl += "128x64";
                else if (radio25664.Checked) zipFileUrl += "256x64";
                else zipFileUrl += "128x32";
            }
            zipFileUrl += ".zip";
            string firmwareFileName = "ZeDMD.bin";
            using (WebClient client = new WebClient())
            {
                byte[] zipData = client.DownloadData(zipFileUrl);

                using (MemoryStream zipStream = new MemoryStream(zipData))
                using (ZipArchive archive = new ZipArchive(zipStream))
                {
                    ZipArchiveEntry firmwareEntry = archive.GetEntry(firmwareFileName);

                    if (firmwareEntry != null)
                    {
                        using (Stream firmwareStream = firmwareEntry.Open())
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                firmwareStream.CopyTo(memoryStream);
                                byte[] firmwareData = memoryStream.ToArray();
                                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                string filePath = Path.Combine(documentsPath, firmwareFileName);
                                System.IO.File.WriteAllBytes(filePath, firmwareData);
                                string devtype = "esp32";
                                if (zeddev.isS3 || zeddev.isLilygo) devtype = "esp32s3";
                                string commandArgs = "--chip " + devtype + " --port COM" + zeddev.ComId.ToString() + "  write_flash 0x0 \"" + filePath + "\"";
                                flashed = FlashAndConfig.FlashEsp32(commandArgs);
                                System.IO.File.Delete(filePath);
                                if (flashed) CalcAndSetParameters();
                            }
                            firmwareStream.Close();
                        }
                    }
                }
            }
            Enabled = true;
            if (flashed)
            {
                //MessageBox.Show("Configure your ZeDMD with the menu, exit the menu, then click \"OK\"");
                UpdateZeDMDList();
            }
        }
        private void deviceView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = deviceView.HitTest(e.Location);

            if (hit.Item != null && hit.SubItem != null)
            {
                int rowIndex = hit.Item.Index; // Row number
                int columnIndex = hit.Item.SubItems.IndexOf(hit.SubItem); // Column number

                if (rowIndex >= 0 && rowIndex < Esp32Devices.esp32Devices.Count && columnIndex == 2)
                {
                    Esp32Device ed = Esp32Devices.esp32Devices[rowIndex];
                    if (ed.isS3)
                    {
                        ed.isS3 = false;
                        ed.isLilygo = false;
                    }
                    else ed.isS3 = true;
                    Esp32Devices.PopulateESP(this);
                }
                else if (rowIndex >= 0 && rowIndex < Esp32Devices.esp32Devices.Count && columnIndex == 3)
                {
                    Esp32Device ed = Esp32Devices.esp32Devices[rowIndex];
                    if (ed.isLilygo) ed.isLilygo = false;
                    else
                    {
                        ed.isLilygo = true;
                        ed.isS3 = true;
                    }
                    Esp32Devices.PopulateESP(this);
                }
            }
        }
        private void buttonRescan_Click(object sender, EventArgs e)
        {
            UpdateZeDMDList();
        }

        private void CalcAndSetParameters()
        {
            int comport = Esp32Devices.esp32Devices[deviceView.SelectedIndices[0]].ComId;
            int transport;
            if (radioWUdp.Checked) transport = 1;
            else if (radioWTcp.Checked) transport = 2;
            else transport = 0;
            int yoffs;
            if (radio12864.Checked) yoffs = 16;
            else yoffs = 0;
            FlashAndConfig.SetZeDmdParameters(comport, (int)numericBrightness.Value, (int)numericROrder.Value, (int)numericPCPhase.Value,
                (int)numericPDriver.Value, (int)numericPISpeed.Value, (int)numericPLBlanking.Value, (int)numericPMRRate.Value,
                transport, (int)numericUDelay.Value, (int)numericUPSize.Value, textSsid.Text, textPassword.Text, yoffs);
        }
        private void buttonSNParameters_Click(object sender, EventArgs e)
        {
            CalcAndSetParameters();
        }
    }
}

