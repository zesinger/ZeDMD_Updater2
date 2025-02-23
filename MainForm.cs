using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ZeDMD_Updater2
{
    public partial class MainForm : Form
    {
        public readonly int Major_Version = 2;
        public readonly int Minor_Version = 1;
        public readonly int Patch_Version = 0;

        public MainForm()
        {
            InitializeComponent();
            AttachMouseEnterEvents(Controls);
            string libzedmdvers = Marshal.PtrToStringAnsi(Esp32Device.ZeDMD_GetVersion());
            Text = "ZeDMD Updater v" + Major_Version + "." + Minor_Version + "." + Patch_Version + " using libzedmd v" + libzedmdvers;
            MouseEnter += (s, e) => textDescription.Text = "";
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
            waitForm.mainText.Text = "Please wait while listing the available firmwares and devices...\r\n Any ZeDMD connected must NOT be in menu mode!";
            BackgroundWorker worker = new BackgroundWorker();
            string retlog = "";
            worker.DoWork += (s, ev) =>
            {
                latestVersion = InternetFirmwares.GetAvailableVersions();
                retlog = Esp32Devices.GetAvailableDevices(this);
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                textLog.Text = retlog;
                LatestVersion.Text = "Latest version available: " + latestVersion;
                InternetFirmwares.PopulateVersions(this);
                Esp32Devices.PopulateESP(this);
                Enabled = true;
                waitForm.Close();
            };
            Enabled = false;
            worker.RunWorkerAsync();
            waitForm.ShowDialog();
        }
        private void AttachMouseEnterEvents(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.MouseEnter += Control_MouseEnter;
                // Special handling for NumericUpDown made of subcontrols (2 buttons and a textbox)
                if (ctrl is NumericUpDown numericUpDown)
                {
                    foreach (Control c in numericUpDown.Controls)
                    {
                        c.MouseEnter += Control_MouseEnter;
                    }
                }
                // If the control contains child controls (like a GroupBox or Panel), recurse
                if (ctrl.HasChildren)
                {
                    AttachMouseEnterEvents(ctrl.Controls);
                }
            }
        }
        private void Control_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                string description;
                if (control != null && control.Parent is NumericUpDown numericUpDown)
                    description = Resources.Description.ResourceManager.GetString(control.Parent.Name);
                else description = Resources.Description.ResourceManager.GetString(control.Name);
                textDescription.Text = description ?? "";
            }
        }
        private void UpdateZeDMDList()
        {
            int comId = -1;
            if (deviceView.SelectedItems.Count > 0) comId = Esp32Devices.WhichDevice(this).ComId;
            Enabled = false;
            WaitForm waitForm = new WaitForm();
            waitForm.mainText.Text = "Please wait while updating the available devices...";
            BackgroundWorker worker = new BackgroundWorker();
            string retLog = "";
            worker.DoWork += (s, ev) =>
            {
                retLog = Esp32Devices.GetAvailableDevices(this);
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                textLog.Text = retLog;
                Esp32Devices.PopulateESP(this);
                Enabled = true;
                waitForm.Close();
                for (int i = 0; i < Esp32Devices.esp32Devices.Count; i++)
                {
                    Esp32Device device = Esp32Devices.esp32Devices[i];
                    if (device.ComId == comId)
                    {
                        deviceView.SelectedIndices.Add(i);
                        break;
                    }
                }
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
            System.Windows.Forms.ListView.ColumnHeaderCollection lvchc = deviceView.Columns;
            if (e.ColumnIndex == lvchc.IndexOf(columnVersion) && e.Item.SubItems[lvchc.IndexOf(columnVersion)].Text.StartsWith("*"))
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
                Esp32Device ed = Esp32Devices.WhichDevice(this);
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
                // no selected items
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
                Esp32Device zeddev = Esp32Devices.WhichDevice(this);
                flashed = FlashAndConfig.FlashEsp32(zeddev, filePath);
            }
            Enabled = true;
            if (flashed)
            {
                UpdateZeDMDList();
            }
        }

        private void buttonLTest_Click(object sender, EventArgs e)
        {
            Esp32Device.LedTest(this);
        }

        private void buttonFlash_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Esp32Device zeddev = Esp32Devices.WhichDevice(this);
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
                                flashed = FlashAndConfig.FlashEsp32(zeddev, filePath);
                                System.IO.File.Delete(filePath);
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
                    deviceView.SelectedIndices.Add(rowIndex);
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
                    deviceView.SelectedIndices.Add(rowIndex);
                }
            }
        }
        private void buttonRescan_Click(object sender, EventArgs e)
        {
            UpdateZeDMDList();
        }

        private void CalcAndSetParameters()
        {
            if (deviceView.SelectedItems.Count == 0) return;
            int transport;
            if (radioWUdp.Checked) transport = 1;
            else if (radioWTcp.Checked) transport = 2;
            else transport = 0;
            WaitForm waitForm = new WaitForm();
            waitForm.mainText.Text = "Please wait while updating the parameters of your device...";

            BackgroundWorker worker = new BackgroundWorker();
            Esp32Device zd = Esp32Devices.WhichDevice(this);
            string logRes = "";
            worker.DoWork += (s, ev) =>
            {
                FlashAndConfig.SetZeDmdParameters(zd,
                    (int)numericBrightness.Value, (int)numericROrder.Value, (int)numericPCPhase.Value,
                    (int)numericPDriver.Value, (int)numericPISpeed.Value, (int)numericPLBlanking.Value, (int)numericPMRRate.Value,
                    transport, (int)numericUDelay.Value, (int)numericUPSize.Value, textSsid.Text, textPassword.Text, (int)numericOY.Value, ref logRes, false);
            };
            worker.RunWorkerCompleted += (s, ev) =>
            {
                textLog.Text = logRes;
                Esp32Devices.FillEsp32Values(deviceView.SelectedItems[0].Index, this);
                Enabled = true;
                waitForm.Close();
            };
            Enabled = false;
            worker.RunWorkerAsync();
            waitForm.ShowDialog();
        }
        private void buttonSNParameters_Click(object sender, EventArgs e)
        {
            CalcAndSetParameters();
        }

        private void radio12864_CheckedChanged(object sender, EventArgs e)
        {
            numericOY.Enabled = radio12864.Checked;
        }

        private void radio12832_CheckedChanged(object sender, EventArgs e)
        {
            if (radio12832.Checked) numericOY.Enabled = false;
        }

        private void radio25664_CheckedChanged(object sender, EventArgs e)
        {
            if (radio25664.Checked) numericOY.Enabled = false;
        }
    }
}

