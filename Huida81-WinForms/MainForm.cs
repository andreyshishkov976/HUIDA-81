using Huida81.Core.Contracts.Services;
using Huida81.Core.Enums;
using Huida81.Core.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Huida81_WinForms
{
    public partial class MainForm : Form
    {
        private IWin32InfoService _infoService;
        private SaveFileDialog _fileDialog;

        public MainForm()
        {
            InitializeComponent();
            _infoService = new Win32InfoService();
            _fileDialog = new SaveFileDialog();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Win32InfoKey key = Win32InfoKey.Win32_BaseBoard;
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Процессор":
                    key = Win32InfoKey.Win32_Processor;
                    break;
                case "Видеокарта":
                    key = Win32InfoKey.Win32_VideoController;
                    break;
                case "Биос":
                    key = Win32InfoKey.Win32_BIOS;
                    break;
                case "Диск":
                    key = Win32InfoKey.Win32_DiskDrive;
                    break;
                case "Логические диски":
                    key = Win32InfoKey.Win32_LogicalDisk;
                    break;
                case "Клавиатура":
                    key = Win32InfoKey.Win32_Keyboard;
                    break;
                case "Сеть":
                    key = Win32InfoKey.Win32_NetworkAdapter;
                    break;
                case "Системная плата":
                    key = Win32InfoKey.Win32_BaseBoard;
                    break;
                case "Запущенные процессы":
                    key = Win32InfoKey.Win32_Process;
                    break;
                case "Монитор":
                    key = Win32InfoKey.Win32_DesktopMonitor;
                    break;
                case "Мышь":
                    key = Win32InfoKey.Win32_PointingDevice;
                    break;
                default:
                    key = Win32InfoKey.Win32_BaseBoard;
                    break;
            }
            ShowInfo(key, listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;

        }

        private async void ShowInfo(Win32InfoKey key, ListView list)
        {
            list.Items.Clear();
            try
            {
                var report = await _infoService.GetWin32InfoDataAsync(key);
                foreach (var win32Info in report)
                {
                    if (win32Info is null || win32Info.InfoDetails.Count == 0)
                    {
                        MessageBox.Show("Не удалось получить информацию", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ListViewGroup listViewGroup;
                    listViewGroup = list.Groups.Add(win32Info.Name, win32Info.Name);
                    foreach (var prop in win32Info.InfoDetails)
                    {
                        ListViewItem item = new ListViewItem(listViewGroup);
                        if (list.Items.Count % 2 == 0)
                        {
                            item.BackColor = Color.WhiteSmoke;
                        }
                        item.Text = prop.Name;
                        item.SubItems.Add(prop.Value);
                        list.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _fileDialog.ShowDialog();
            //do it later
        }
    }
}
