using System.Media;
using System.Threading;

namespace EnslaverCore.Logic.Sound
{
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

            if (timer == null)
            {
                timer = new Timer((obj) =>
                {
                    SaySync(text);
                }, null, 0, interval);
            }
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
