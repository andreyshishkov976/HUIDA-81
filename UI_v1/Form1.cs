using System;
using System.Drawing;
using System.Windows.Forms;
using static HardwareManager.HardwareSearcher;

namespace UI_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HardwareKeys key = HardwareKeys.Win32_BaseBoard;
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Процессор":
                    key = HardwareKeys.Win32_Processor;
                    break;
                case "Видеокарта":
                    key = HardwareKeys.Win32_VideoController;
                    break;
                case "Чипсет":
                    key = HardwareKeys.Win32_IDEController;
                    break;
                case "Батарея":
                    key = HardwareKeys.Win32_Battery;
                    break;
                case "Биос":
                    key = HardwareKeys.Win32_BIOS;
                    break;
                case "Оперативная память":
                    key = HardwareKeys.Win32_PhysicalMemory;
                    break;
                case "Кэш":
                    key = HardwareKeys.Win32_CacheMemory;
                    break;
                case "USB":
                    key = HardwareKeys.Win32_USBController;
                    break;
                case "Диск":
                    key = HardwareKeys.Win32_DiskDrive;
                    break;
                case "Логические диски":
                    key = HardwareKeys.Win32_LogicalDisk;
                    break;
                case "Клавиатура":
                    key = HardwareKeys.Win32_Keyboard;
                    break;
                case "Сеть":
                    key = HardwareKeys.Win32_NetworkAdapter;
                    break;
                case "Пользователи":
                    key = HardwareKeys.Win32_Account;
                    break;
                case "Системная плата":
                    key = HardwareKeys.Win32_BaseBoard;
                    break;
                case "Запущенные процессы":
                    key = HardwareKeys.Win32_Process;
                    break;
                case "Монитор":
                    key = HardwareKeys.Win32_DesktopMonitor;
                    break;
                case "Мышь":
                    key = HardwareKeys.Win32_PointingDevice;
                    break;
                default:
                    key = HardwareKeys.Win32_BaseBoard;
                    break;
            }
            ShowInfo(key, listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void ShowInfo(HardwareKeys key, ListView list)
        {
            list.Items.Clear();
            try
            {
                var report = GetHardwareInfo(key);
                foreach (var hardware in report)
                {
                    if (hardware is null || hardware.HardwareProps.Count == 0)
                    {
                        MessageBox.Show("Не удалось получить информацию", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ListViewGroup listViewGroup;
                    listViewGroup = list.Groups.Add(hardware.Group, hardware.Group);
                    foreach (var prop in hardware.HardwareProps)
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
    }
}
