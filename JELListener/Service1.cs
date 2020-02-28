using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace JELListener
{
    public partial class JELListenerService : ServiceBase
    {
        private readonly String _basedir = AppDomain.CurrentDomain.BaseDirectory;
        private readonly String _filepath;
        private List<Timer> _timers;

        public JELListenerService()
        {
            _filepath = Path.Combine(_basedir, "log.txt"); 

            if (!File.Exists(_filepath))
            {
                Logger.Stream = File.CreateText(_filepath);
            }
            else
            {
                Logger.Stream = new StreamWriter(_filepath);
            }

            _timers = new List<Timer>(); 

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Info("Listener started");
            AddEventHandler(RunTransfers, TimeSpan.FromSeconds(10));
        }

        private void RunTransfers(object source, ElapsedEventArgs e)
        {
            try
            {
                TransferExecutor.ExecuteTransfers();
            }
            catch (Exception exception)
            {
                String message = exception.Message + "\n" + exception.StackTrace;
                Logger.Error(message);
            }
        }

        protected override void OnStop()
        {
            Logger.Info("Listener stopped");
        }

        private void AddEventHandler(ElapsedEventHandler handler, TimeSpan timeSpan)
        {
            Timer newTimer = new Timer();
            newTimer.Elapsed += handler;
            newTimer.Interval = timeSpan.TotalMilliseconds;
            newTimer.Start();
        }
    }
}
