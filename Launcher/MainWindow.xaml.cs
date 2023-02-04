﻿using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Launcher
{
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        Installer installer;

        public MainWindow()
        {
            InitializeComponent();
            AllocConsole();

            Console.Title = "Luconia Launcher";

            installer = new Installer();

            Logger.LogInfo("Started Luconia Launcher");
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }

        private void Launch(object sender, RoutedEventArgs e)
        {
            string roamingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (Process.GetProcessesByName("Minecraft.Windows").Length == 0)
            {
                Process p = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = startInfo.FileName = @"shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";
                p.StartInfo = startInfo;
                p.Start();
            }

            if (Process.GetProcessesByName("Minecraft.Windows").Length != 0)
            {
                Logger.LogError("Can't find Minecraft process");
                return;
            }

            // TODO: downloader
            Injector.Inject($@"{roamingDirectory}\Luconia\luconia.dll");
        }

        private void CloseLauncher(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeLauncher(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        
    }
}
