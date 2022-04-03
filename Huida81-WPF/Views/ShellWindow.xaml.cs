using System.Windows.Controls;

using Huida81_WPF.Contracts.Views;

using MahApps.Metro.Controls;

namespace Huida81_WPF.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();

        private void MetroWindow_StateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                hamburgerMenu.IsPaneOpen = false;
            else if (this.WindowState == System.Windows.WindowState.Maximized)
                hamburgerMenu.IsPaneOpen = true;

        }
    }
}
