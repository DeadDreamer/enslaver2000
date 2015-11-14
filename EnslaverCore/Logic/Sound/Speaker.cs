using System.Media;

namespace EnslaverCore.Logic.Sound
{
    public static class Speaker
    {
        private static YandexSpeechKitCloudUrl Url = new YandexSpeechKitCloudUrl();

        private static SoundStorage Storage = new SoundStorage();

        public static void Say(string text)
        {
            Url.Text = text;
            var sound = Storage.GetSoundStream(Url.ToString());
            new SoundPlayer(sound).Play();
        }
    }
}
