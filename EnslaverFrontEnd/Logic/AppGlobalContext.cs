using EnslaverCore.Logic;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EnslaverFrontEnd.Logic
{
    public class AppGlobalContext : FormFactory
    {
        //Здесь будет хранится  тот, чей компьютер
        public string Owner = "";
        public bool IsDebug = false;

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
                    _startForm = new MainForm(this) { Visible = IsDebug};

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
    }

    public static class StringExtensions
    {
        public static string FormatWithOwner(this string format)
        {
            return string.Format(format, AppGlobalContext.GetInstance().Owner);
        }
    }
}
