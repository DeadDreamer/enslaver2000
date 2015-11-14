using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EnslaverCore.Logic.Sound
{
    public class SoundStorage
    {
        public SoundStorage() : this(Path.Combine(Directory.GetCurrentDirectory(), "audio")) { }

        public SoundStorage(string folder)
        {
            this.Folder = folder;

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        private string Folder { get; set; }

        private Dictionary<string, string> cache = new Dictionary<string, string>();

        private string GetHash(string text)
        {
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private string CreateFileName(string url)
        {
            return Path.Combine(this.Folder, this.GetHash(url) + ".wav");
        }

        private void DownloadFile(string url, string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }
        }

        public Stream GetSoundStream(string url)
        {
            string fileName;

            url = url.ToLower();

            if (!this.cache.TryGetValue(url, out fileName))
            {
                fileName = this.CreateFileName(url);

                if (!File.Exists(fileName))
                {
                    this.DownloadFile(url, fileName);
                }

                this.cache.Add(url, fileName);
            }

            return File.OpenRead(this.cache[url]);
        }
    }
}
