﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Tracker.UIModule
{
    internal class TrayModule : IDisposable
    {
        private NotifyIcon icon;
        const string iconPath = "./icon.ico";
        public TrayModule()
        {
            icon = new NotifyIcon();
            SetIcon();
            icon.Visible = true;
        }
        public void ShowMessage(string message)
        {
            icon.BalloonTipText = message;
            icon.ShowBalloonTip(200);
        }
        public void Dispose()
        {
            if (icon != null)
                icon.Dispose();
        }

        private void SetIcon()
        {
            try
            {
                icon.Icon = new System.Drawing.Icon(iconPath);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Fatal("Icon on path \"" + iconPath + "\" could not be uploaded \n Detail: " + e);
                throw;
            }
            NLog.LogManager.GetCurrentClassLogger().Info("Icon added successfully ");
            
        }
    }
}
