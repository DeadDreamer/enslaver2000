using System;
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

            var message = formMessage.Body as MessageBodyOfWarningForm;

            if (message != null && message.MessageText.Length > 0)
            {                
                this.WarningView.ShowWarningMessage(message.MessageText[0].Text);

                if (!string.IsNullOrEmpty(message.PathToVideoFile)) //проверка наличия видео
                {
                    this.WarningView.ShowVideo(message.PathToVideoFile);
                }
                else
                {
                    this.WarningView.HidePlayer();
                }

                Speaker.BeginSay(message.MessageText, 5000);
            }
            else
            {
                string defaultMessage = "{0}, вернитесь на рабочее место!".FormatWithOwner();
                this.WarningView.ShowWarningMessage(defaultMessage);
                Speaker.BeginSay(new List<Phrase>
                    {
                        new Phrase(defaultMessage),
                        new Phrase("{0}, все должны работать!".FormatWithOwner(), Emotion.Neutral),
                        new Phrase("{0}, я тебя очень жду!".FormatWithOwner(), Emotion.Good)
                    }, 5000);
            }
        }

        public IWarningView WarningView { get; set; }
    }
}
