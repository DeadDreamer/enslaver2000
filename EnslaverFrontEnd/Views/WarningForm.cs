﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Presenters;

using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Diagnostics;
using System.Windows.Forms;
using EnslaverCore.Logic.Sound;



namespace EnslaverFrontEnd.Views
{
    public partial class WarningForm : BaseForm, IWarningView
    {
        private bool isCloseAllowed= false;
        const int HeightMargins = 10;
        // Structure contain information about low-level keyboard input event
        [StructLayout(LayoutKind.Sequential)]

        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }

        //System level functions to be used for hook and unhook keyboard input
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);


        //Declaring Global objects
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;

        public WarningForm()
        {
            InitializeComponent();
        }

        public WarningForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm(null);
        }

        public WarningForm(FormFactory currentFormFactory, FormMessage someMessage)
            : base(currentFormFactory)
        {
            InitThisForm(someMessage);
        }

        private FormMessage formMessage = null;

        private void InitThisForm(FormMessage someMessage)
        {
            formMessage = someMessage;
            Presenter = new EnslaverFrontEnd.Presenters.WarningFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
            InitPlayer();
        }

        private void InitPlayer()
        {
            axWindowsMediaPlayer1.Top = 0 + WarningLabel.Height + HeightMargins;
            axWindowsMediaPlayer1.Left = 0;
            axWindowsMediaPlayer1.Height = this.Height - HeightMargins - WarningLabel.Height;
            axWindowsMediaPlayer1.Width = this.Width;
            axWindowsMediaPlayer1.settings.setMode("Loop", true);
        }

        public void ShowWarningMessage(string text)
        {
            WarningLabel.Text = text;
        }

        public void HidePlayer()
        {
            axWindowsMediaPlayer1.Visible = false;
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;


        public void ShowVideo(string uri)
        {
            //Тут проигрывание видео
            axWindowsMediaPlayer1.Visible = true;
            axWindowsMediaPlayer1.URL = uri;
        }

        public override void ForceClose() 
        {
            isCloseAllowed = true;
            Speaker.EndSay();
            base.ForceClose();
        }

        private void WarningForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isCloseAllowed;
            base.OnClosing(e);
        }

        public FormMessage GetFormMessage()
        {
            return this.formMessage;
        }

        private void WarningForm_SizeChanged(object sender, EventArgs e)
        {
            WarningLabel.Left = this.Width / 2 - WarningLabel.Width / 2;
            axWindowsMediaPlayer1.Height = this.Height - HeightMargins - WarningLabel.Height;
            axWindowsMediaPlayer1.Width = this.Width;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            AppGlobalContext.GetInstance().DeleteForm(this.Guid);
    }
    }
}
