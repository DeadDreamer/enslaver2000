using System;
using System.Web;

namespace EnslaverCore.Logic.Sound
{
    public enum Emotion
    {
        Good,
        Neutral,
        Evil,
        Mixed
    }

    public enum Voice
    {
        Jane,
        Omazh,
        Zahar,
        Ermil
    }

    public class YandexSpeechKitCloudUrl
    {
        public YandexSpeechKitCloudUrl()
        {
            this.Speaker = Voice.Omazh;
            this.Emotion = Emotion.Evil;
            this.IsRobot = false;
            this.ApiKey = "c3b2d6ef-25aa-4b3c-b2e5-a642a1f0f618";
        }

        public Emotion Emotion { get; set; }

        public Voice Speaker { get; set; }

        public bool IsRobot { get; set; }

        public string Text { get; set; }

        public string ApiKey { get; set; }

        public override string ToString()
        {
            return String.Format("https://tts.voicetech.yandex.net/generate?text={0}&format=wav&speaker={1}&key={2}&emotion={3}&robot={4}",
                HttpUtility.UrlEncode(this.Text),
                this.Speaker.ToString().ToLower(),
                this.ApiKey,
                this.Emotion.ToString().ToLower(),
                this.IsRobot.ToString().ToLower());
        }
    }
}
