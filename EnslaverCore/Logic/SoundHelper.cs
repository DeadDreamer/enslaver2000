using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverCore.Logic.Sound
{
    public class SoundHelper
    {
        public void PlaySoundFile(string pathToFile)
        {
            if (File.Exists(pathToFile)) 
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = pathToFile;
                player.Load();
                player.Play();
            }           
        }
    }
}
