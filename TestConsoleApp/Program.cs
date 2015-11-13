using EnslaverCore.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
        	
        	string         		pathToSamples="C:\\Users\\am.kuznecov\\Desktop\\Enslaver2000\\FotoSamples\\";
        	            System.IO.DirectoryInfo directory = new DirectoryInfo(pathToSamples);
            FileInfo[] filesForTeach = directory.GetFiles();
            NeuralNetwork neuralNet = new NeuralNetwork("c:\\tmp\\NN\\", 4);


            List<string>
                man1Files = new List<string>(), //Барган Алексей
                man2Files = new List<string>(), //Тарас
                man3Files = new List<string>(), //Полина
                man4Files = new List<string>(), //Северьянов Алексей
                smileFiles = new List<string>() { "face1.bmp", "face4.bmp", "face14.bmp", "face16.bmp", "face17.bmp", "face20.bmp", "face26.bmp" };

            for (int i = 1; i <= 12; i++)
            {
                man1Files.Add("face" + i.ToString() + ".bmp");
            }
            for (int i = 13; i <= 18; i++)
            {
                man2Files.Add("face" + i.ToString() + ".bmp");
            }
            for (int i = 19; i <= 22; i++)
            {
                man3Files.Add("face" + i.ToString() + ".bmp");
            }
            for (int i = 23; i <= 29; i++)
            {
                man4Files.Add("face" + i.ToString() + ".bmp");
            }

            neuralNet.DeleteTeachedData();
            foreach (var item in filesForTeach)
            {
                Bitmap bitmap = new Bitmap(item.FullName);
                bool isSmile = (smileFiles.Contains(item.Name));
                bool isMan = !(man3Files.Contains(item.Name));
                if (man1Files.Contains(item.Name))
                {
                    neuralNet.RememberThis(bitmap, isMan, isSmile, 0);
                }
                if (man2Files.Contains(item.Name))
                {
                    neuralNet.RememberThis(bitmap, isMan, isSmile, 1);
                }
                if (man3Files.Contains(item.Name))
                {
                    neuralNet.RememberThis(bitmap, isMan, isSmile, 2);
                }
                if (man4Files.Contains(item.Name))
                {
                    neuralNet.RememberThis(bitmap, isMan, isSmile, 3);
                }
            }
            neuralNet.ApplyTeachedDataToNeuralNetwork();
            neuralNet.SaveNeuralNetwork();

            NeuralNetwork neuralNet1 = new NeuralNetwork("c:\\tmp\\NN\\", 4);
            neuralNet1.RestoreNeuralNetwork();
            bool man=false;
            bool smile=false;
            int indexOfMan=0;
            
            neuralNet1.RecognizeThis(new Bitmap(pathToSamples+"face23.bmp"),ref man, ref smile, ref indexOfMan);
            neuralNet1.RecognizeThis(new Bitmap(pathToSamples+"ControlImage\\SadBargan.bmp"),ref man, ref smile, ref indexOfMan);
            neuralNet1.RecognizeThis(new Bitmap(pathToSamples+"ControlImage\\SadSever.bmp"),ref man, ref smile, ref indexOfMan);
            neuralNet1.RecognizeThis(new Bitmap(pathToSamples+"ControlImage\\SmileSever.bmp"),ref man, ref smile, ref indexOfMan);
            neuralNet1.RecognizeThis(new Bitmap(pathToSamples+"ControlImage\\SmileTaras.bmp"),ref man, ref smile, ref indexOfMan);
            

            
        }
    }
}
