﻿using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace SyncService
{
    public partial class SyncService : ServiceBase
    {
        public SyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (args.Length!=0)
            {             
                config.AppSettings.Settings["User"].Value = args[0];
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }

            var user = config.AppSettings.Settings["User"].Value;

            Configurations.GetConfigurations(user).Wait();

            var configuration = Configurations.GetInstance();

            var timer = new Timer();
            timer.Elapsed += (sender, e) => OnTimer(timer, user);
            timer.Interval = configuration.Timer;
            timer.Enabled = true;

        }

        protected override void OnStop()
        {
        }

        public void OnTimer(Timer timer, string user)
        {
            timer.Stop();
            var controller = new SyncController();
            controller.Sync(user).Wait();
            timer.Start();
        }
    }
}
