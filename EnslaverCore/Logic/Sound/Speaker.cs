using System.Collections.Generic;
using System.Media;
using System.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnslaverCore.Logic.Sound
{
    [Serializable]
    public class Phrase
    {
        public Phrase()
        {
            this.Emotion = Emotion.Evil;
        }

        public Phrase(string text)
            : this()
        {
            this.Text = text;
        }

        public Phrase(string text, Emotion emotion)
            : this(text)
        {
            this.Emotion = emotion;
        }

        public string Text { get; set; }
        public Emotion Emotion { get; set; }
    }

    public static class Speaker
    {
        private static YandexSpeechKitCloudUrl Url = new YandexSpeechKitCloudUrl();

        private static SoundStorage Storage = new SoundStorage();

        private static string GetUrl(Phrase phrase)
        {            
            return new YandexSpeechKitCloudUrl
            {
                Emotion = phrase.Emotion,
                Text = phrase.Text
            }.ToString();
        }

        private static void Pronounce(Phrase phrase, bool sync)
        {
            var url = GetUrl(phrase);
            var sound = Storage.GetSoundStream(url);
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
            Pronounce(new Phrase(text), false);
        }

        public static void SaySync(string text)
        {
            Pronounce(new Phrase(text), true);
        }

        public static void Say(Phrase phrase)
        {
            Pronounce(phrase, false);
        }

        public static void SaySync(Phrase phrase)
        {
            Pronounce(phrase, true);
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

        public static void CachePhrases(IEnumerable<Phrase> phrases)
        {
            new Task(() =>
            {
                foreach (var phrase in phrases)
                {
                    Storage.GetChachedFileName(GetUrl(phrase));
                }
            }).Start();
        }
    }
}
