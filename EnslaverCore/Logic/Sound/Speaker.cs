using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace EnslaverCore.Logic.Sound
{
    public class Phrase
    {
        public Phrase(string text)
        {
            this.Emotion = Emotion.Evil;
            this.Text = text;
        }

        public Phrase(string text, Emotion emotion)
        {
            this.Emotion = emotion;
            this.Text = text;
        }

        public string Text { get; set; }
        public Emotion Emotion { get; set; }
    }

    public static class Speaker
    {
        private static YandexSpeechKitCloudUrl Url = new YandexSpeechKitCloudUrl();

        private static SoundStorage Storage = new SoundStorage();

        private static void Pronounce(string text, bool sync)
        {
            Url.Text = text;
            var sound = Storage.GetSoundStream(Url.ToString());

            var player = new SoundPlayer(sound);

            if (sync)
            {
                player.PlaySync();
            }
            else
            {
                player.Play();
            }
        }

        public static void Say(string text)
        {
            Pronounce(text, false);
        }

        public static void SaySync(string text)
        {
            Pronounce(text, true);
        }

        private static Timer timer;

        public static void BeginSay(string text, int interval)
        {
            EndSay();

            timer = new Timer((_) =>
            {
                SaySync(text);
            }, null, 0, interval);
        }

        public static void BeginSay(IList<Phrase> phrases, int interval)
        {
            EndSay();

            int currentPhraseIndex = 0;

            timer = new Timer((_) =>
            {
                if (currentPhraseIndex >= phrases.Count)
                {
                    currentPhraseIndex = 0;
                }

                var phrase = phrases[currentPhraseIndex++];

                Url.Emotion = phrase.Emotion;
                SaySync(phrase.Text);
            }, null, 0, interval);
        }

        public static void EndSay()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
    }
}
