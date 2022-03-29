using System;
using System.Drawing;
using System.Windows.Forms;
using WMI;

namespace UI_v1
{
    public partial class Form1 : Form
    {
        private WmiHandler handler;
        private SaveFileDialog fileDialog;

        public Form1()
        {
            InitializeComponent();
            handler = new();
            fileDialog = new();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = handler.HardwareKeys[13];
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Процессор":
                    key = handler.HardwareKeys[0];
                    break;
                case "Видеокарта":
                    key = handler.HardwareKeys[1];
                    break;
                case "Чипсет":
                    key = handler.HardwareKeys[2];
                    break;
                case "Батарея":
                    key = handler.HardwareKeys[3];
                    break;
                case "Биос":
                    key = handler.HardwareKeys[4];
                    break;
                case "Оперативная память":
                    key = handler.HardwareKeys[5];
                    break;
                case "Кэш":
                    key = handler.HardwareKeys[6];
                    break;
                case "USB":
                    key = handler.HardwareKeys[7];
                    break;
                case "Диск":
                    key = handler.HardwareKeys[8];
                    break;
                case "Логические диски":
                    key = handler.HardwareKeys[9];
                    break;
                case "Клавиатура":
                    key = handler.HardwareKeys[10];
                    break;
                case "Сеть":
                    key = handler.HardwareKeys[11];
                    break;
                case "Пользователи":
                    key = handler.HardwareKeys[12];
                    break;
                case "Системная плата":
                    key = handler.HardwareKeys[13];
                    break;
                case "Запущенные процессы":
                    key = handler.HardwareKeys[14];
                    break;
                case "Монитор":
                    key = handler.HardwareKeys[15];
                    break;
                case "Мышь":
                    key = handler.HardwareKeys[16];
                    break;
                default:
                    key = handler.HardwareKeys[17];
                    break;
            }
            ShowInfo(key, listView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;

        }

        private void ShowInfo(string key, ListView list)
        {
            list.Items.Clear();
            try
            {
                var report = handler.GetWin32InfoReportByKey(key);
                foreach (var hardware in report)
                {
                    if (hardware is null || hardware.InfoProps.Count == 0)
                    {
                        MessageBox.Show("Не удалось получить информацию", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ListViewGroup listViewGroup;
                    listViewGroup = list.Groups.Add(hardware.Group, hardware.Group);
                    foreach (var prop in hardware.InfoProps)
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
            fileDialog.ShowDialog();
            handler.GetWin32InfoReportJson(fileDialog.FileName);
        }
    }
}
