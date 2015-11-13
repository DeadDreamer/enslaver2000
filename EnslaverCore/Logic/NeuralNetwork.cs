using FANN.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverCore.Logic
{
    public class NeuralNetwork
    {
        public static int FaceWidth = 100;
        public static int FaceHeight = 100;
        public static string FileNameOfNeuralNetwork = "data.net";
        public static string TeachFilePattern = "{0}_{1}_{2}_{3}_.bmp";
        public static string TeachFileSuffix = "_.bmp";
        public string _pathToSaveOrRestore { get; set; }

        private NeuralNet neuralNet;
        private string path;
        private uint countOfPeoples;
        private uint countOfTeachFiles = 0;
        private uint[] layers = null;
        private uint numOfOutputs = 0;

        public NeuralNetwork(string pathToFolder, uint peoplesСount)
        {
            neuralNet = new NeuralNet();
            path = pathToFolder;
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }
            else
            {
                string[] files = Directory.GetFiles(pathToFolder);
                if (files != null && files.Length > 0)
                {
                    countOfTeachFiles = (uint)files.Count(c => c.EndsWith(TeachFileSuffix));
                }
            }

            _pathToSaveOrRestore = Path.Combine(path, FileNameOfNeuralNetwork);

            countOfPeoples = peoplesСount;
            var countOfNeurons = FaceWidth * FaceHeight;
            numOfOutputs = peoplesСount + 2; //countOfPeoples + smile+ sex
            // 6 слоев как кортекс человека...
            layers = new uint[]
                {           
                    (uint) countOfNeurons, (uint) (countOfNeurons/4),(uint) (countOfNeurons/16), (uint)numOfOutputs
                };
        }

        public void RestoreNeuralNetwork()
        {
            neuralNet.CreateFromFile(_pathToSaveOrRestore);
        }

        /// <summary>
        /// Запоминает данные для обучения  
        /// </summary>
        /// <param name="faceBitmap"></param>
        /// <param name="isMan"></param>
        /// <param name="isSmiling"></param>
        /// <param name="indexOfMan">Начиная с нуля</param>
        public void RememberThis(Bitmap faceBitmap, bool isMan, bool isSmiling, int indexOfMan)
        {
            countOfTeachFiles++;
            var fileName = string.Format(TeachFilePattern, countOfTeachFiles.ToString(), isMan ? "1" : "0", isSmiling ? "1" : "0", indexOfMan);
            faceBitmap.Save(Path.Combine(path, fileName));
        }
        public void RecognizeThis(Bitmap faceBitmap, ref  bool isMan, ref  bool isSmiling, ref int indexOfMan)
        {
            double[] output = neuralNet.Run(ExtractDataFromBitmap(faceBitmap));
            isMan = (output[0] > 0.5) ? true : false;
            isSmiling = (output[1] > 0.5) ? true : false;
            int maxIndex = 0;
            double maxValue = 0;
            for (int i = 0; i < countOfPeoples; i++)
            {
                if (maxValue < output[2 + i])
                {
                    maxIndex = i;
                    maxValue = output[2 + i];
                }
            }
            indexOfMan = maxIndex;
        }

        public void ApplyTeachedDataToNeuralNetwork()
        {
            double[][] inputData = new double[][] { }, outputData = new double[][] { };
            ExtractTrainDataFromFiles(ref inputData, ref   outputData);
            neuralNet.CreateStandardArray(layers);
            //Устанавливаем случайные веса
            neuralNet.RandomizeWeights(-0.1, 0.1);
            neuralNet.SetLearningRate(0.7f);
            var data = new TrainingData();
            data.SetTrainData(countOfTeachFiles, (uint)(FaceHeight * FaceWidth), inputData, numOfOutputs, outputData);
            //Обучаем сеть
            neuralNet.TrainOnData(data, 500, 0, 0.01f);
        }

        /// <summary>
        /// Извлекает данные для обучения из файлов
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private void ExtractTrainDataFromFiles(ref double[][] input, ref  double[][] output)
        {
            System.IO.DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles("*" + TeachFileSuffix);
            input = new double[files.Count()][];
            output = new double[files.Count()][];
            int counter = 0;
            foreach (FileInfo file in directory.GetFiles("*" + TeachFileSuffix))
            {
                Bitmap bitmap = new Bitmap(file.FullName);
                double[] data =
                input[counter] = ExtractDataFromBitmap(bitmap);
                output[counter] = ExtractDataFromFileName(file.Name);
                counter++;
            }
        }

        private double[] ExtractDataFromFileName(string fileName)
        {
            double[] result = new double[countOfPeoples + 2];
            if (fileName.Contains(TeachFileSuffix))
            {
                string[] fileParts = fileName.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                int indexOfMan = 0;
                if (fileParts.Count() >= 4 && int.TryParse(fileParts[3], out indexOfMan))
                {
                    result[0] = (fileParts[1] == "1") ? 1 : 0;//is man 
                    result[1] = (fileParts[2] == "1") ? 1 : 0;//is smiling
                    result[indexOfMan + 2] = 1;
                }
            }
            return result;
        }

        private double[] ExtractDataFromBitmap(Bitmap faceBitmap)
        {
            double[] data = new double[(int)FaceWidth * FaceHeight];
            var index = 0;
            for (int i = 0; i < FaceWidth; i++)
            {
                for (int j = 0; j < FaceHeight; j++)
                {
                    Color color = faceBitmap.GetPixel(i, j);
                    // получаем градацию серого
                    double grayScale = ((color.R * 0.2126) + (color.G * 0.7152) + (color.B * 0.0722)) / (255 * (0.2126 + 0.7152 + 0.0722));
                    data[index] = grayScale;
                    index++;
                }
            }
            return data;
        }

        public void DeleteTeachedData()
        {
            System.IO.DirectoryInfo directory = new DirectoryInfo(path);
            foreach (FileInfo file in directory.GetFiles("*" + TeachFileSuffix))
            {
                file.Delete();
            }
            countOfTeachFiles = 0;
        }

        public void SaveNeuralNetwork()
        {
            neuralNet.Save(_pathToSaveOrRestore);
        }


    }
}
