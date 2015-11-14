using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using EnslaverCore.Logic;
using EnslaverCore.Logic.Sound;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Views;

namespace EnslaverFrontEnd.Logic
{
    public class AppGlobalContext : FormFactory
    {
        //Здесь будет хранится  тот, чей компьютер
        public string Owner = string.Empty;
        public string OwnerId = string.Empty;
        public bool IsDebug = false;
        public int TimerPeriodInMilSec = 4000;
        public int TimerBlinkPeriodInMilSec = 4000;

        public static IntPtr FileMapHandler = IntPtr.Zero, HandlerOfMapView = IntPtr.Zero;

        public CamHelper CamHelper = new CamHelper();

        // public SoundHelper SoundHelper = new SoundHelper();

        public static AppGlobalContext GetInstance()
        {
            if (_formFactory == null)
            {
                lock (typeof(AppGlobalContext))
                {
                    if (_formFactory == null)
                    {
                        _formFactory = new AppGlobalContext();

                    }
                }
            }
            return (AppGlobalContext)_formFactory;
        }

        private static FormFactory _formFactory = null;

        public bool AllowExit = false;

        public AppGlobalContext()
        {
            Owner = ConfigurationManager.AppSettings["owner"];
            OwnerId = ConfigurationManager.AppSettings["ownerId"];

            PhrasesConfig.Load(Path.Combine(Directory.GetCurrentDirectory(), ConfigurationManager.AppSettings["phrasesConfig"]));

            var phrases = new List<Phrase>(PhrasesConfig.AllPhrases);
                //.Select(phrase => new Phrase { Emotion = phrase.Emotion, Text = this.FormatWithOwner(phrase.Text) })
                //.ToList();

            Speaker.CachePhrases(phrases);
        }

        public Type GetTypeByID(long formTypeID)
        {
            switch (formTypeID)
            {
                case (long)FormTypes.MainForm:
                    return typeof(MainForm);
                case (long)FormTypes.AlertForm:
                    return typeof(AlertForm);
                case (long)FormTypes.AdminForm:
                    return typeof(AdminForm);
                case (long)FormTypes.LoginForm:
                    return typeof(LoginForm);
                case (long)FormTypes.WarningForm:
                    return typeof(WarningForm);
            }
            throw new Exception("Form not found");
        }


        /// <summary>
        /// Убрать значение из справочника 
        /// </summary>
        /// <param name="deletedForm"></param>
        /// <returns></returns>
        public virtual int DeleteForm(Guid guidOfDeletedForm)
        {
            if (IsContainForm(guidOfDeletedForm))
            {
                DicOfForms[guidOfDeletedForm].ForceClose();
                DicOfForms.Remove(guidOfDeletedForm);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public override BaseForm CreateForm(long formTypeID, FormMessage formMessage)
        {
            var type = GetTypeByID(formTypeID);
            var result = (BaseForm)Activator.CreateInstance(type, this, formMessage);
            result.TypeID = formTypeID;
            return result;
        }


        private BaseForm _startForm = null;
        public override BaseForm StartForm
        {
            get
            {
                if (_startForm == null)
                {
                    _startForm = new MainForm(this) { Visible = IsDebug };

                }
                return _startForm;
            }
        }

        public void Exit()
        {
            CamHelper.Stop();
            AllowExit = true;
            System.Windows.Forms.Application.Exit();
        }

        public string FormatWithOwner(string format)
        {
            return string.Format(format, this.Owner);
        }
    }

    public static class StringExtensions
    {
        public static string FormatWithOwner(this string format)
        {
            return AppGlobalContext.GetInstance().FormatWithOwner(format);
        }
    }
}
