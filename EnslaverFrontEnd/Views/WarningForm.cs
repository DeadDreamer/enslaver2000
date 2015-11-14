using System;
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

namespace EnslaverFrontEnd.Views
{
    public partial class WarningForm : BaseForm, IWarningView
    {
        const int HeightMargins = 10;

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

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;


        public void ShowVideo(string uri)
        {
            //Тут проигрывание видео
            axWindowsMediaPlayer1.Visible = true;
            axWindowsMediaPlayer1.URL = uri;
        }

        private void WarningForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
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
    }
}
