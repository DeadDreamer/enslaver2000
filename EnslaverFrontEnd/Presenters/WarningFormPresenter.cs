﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Models;
using EnslaverCore.Logic.Sound;
using EnslaverFrontEnd.Logic;

namespace EnslaverFrontEnd.Presenters
{
    public class WarningFormPresenter : BasePresenter
    {
        public WarningFormPresenter(IBaseView view)
            : base(view)
        {
            if (view is IWarningView)
            {
                this.WarningView = (view as IWarningView);
                this.WarningView.Init += new EventHandler<EventArgs>(View_Init);
                this.WarningView.ExitClick += OnView_ExitClick;
            }
            else
            {
                new Exception("Это не наша форма!");
            }
        }

        void View_Init(object sender, EventArgs e)
        {
            FormMessage formMessage = this.WarningView.GetFormMessage();
            //Проверяем ,что это наше сообщение и не пустое...
            if (formMessage.Body is MessageBodyOfWarningForm && !string.IsNullOrEmpty((formMessage.Body as MessageBodyOfWarningForm).MessageText))
            {
                MessageBodyOfWarningForm messageBodyOfWarningForm = (formMessage.Body as MessageBodyOfWarningForm);
                this.WarningView.ShowWarningMessage(messageBodyOfWarningForm.MessageText);

                if (!string.IsNullOrEmpty(messageBodyOfWarningForm.PathToVideoFile)) //проверка наличия видео
                {
                    this.WarningView.ShowVideo(messageBodyOfWarningForm.PathToVideoFile);
                }
                else this.WarningView.HidePlayer();
                Speaker.BeginSay(messageBodyOfWarningForm.MessageText,5000);

            }
            else
            {
                string defaultMessage=string.Format("{0}, вернитесь на рабочее место!", AppGlobalContext.GetInstance().Owner);
                this.WarningView.ShowWarningMessage(defaultMessage);
				Speaker.BeginSay(defaultMessage,5000);
            }

        }

        public IWarningView WarningView { get; set; }
    }
}
