﻿using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;
using System.Threading;
using EnslaverCore;

namespace EnslaverFrontEnd.Views
{
    public partial class AdminForm : BaseForm, IAdminView
    {
        //Declararation of all variables, vectors and haarcascades
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        HaarCascade mouth;
        HaarCascade smile;

        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.7d, 0.7d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        public AdminForm(FormFactory currentFormFactory, FormMessage someMessage)
            : base(currentFormFactory)
        {
            InitForm(someMessage);
        }

        public AdminForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitForm(null);
        }
        public AdminForm()
        {
            InitForm(null);
        }

        public void InitForm(FormMessage someMessage)
        {
            //Initialize the capture device
            grabber = new Capture();
            grabber.QueryFrame();

            formMessage = someMessage;
            InitializeComponent();
            Presenter = new EnslaverFrontEnd.Presenters.AdminFormPresenter(this);
            TryRaiseEvent(Init, EventArgs.Empty);
            //Load haarcascades for face detection
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            eye = new HaarCascade("haarcascade_eye.xml");
            mouth = new HaarCascade("mouth.xml");
            smile = new HaarCascade("haarcascade_smile.xml");

            try
            {
                //Load of previus trainned faces and labels for each image
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;

                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }


                
               

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("Nothing in binary database, please add at least a face(Simply train the prototype with the Add Face Button).", "Triained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //Initialize the FrameGraber event
            Application.Idle += new EventHandler(FrameGrabber);
        }


        private void TeachButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));

                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }

                //resize face detected image for force to compare the same size with the 
                //test image with cubic interpolation type method
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(UserNameTextBox.Text);

                //Show face added in gray scale
                TeachedImage.Image = TrainedFace;

                //Write the number of triained faces in a file text for further load
                File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                //Write the labels of triained faces in a file text for further load
                for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                {
                    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                }

                // MessageBox.Show("Лицо " + textBox1.Text + " обнаружено и запомнено", "Обучение прошло успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static List<HeadInformation> _lastInfo = null;

        public static List<HeadInformation> GetLastInfo()
        {
            return _lastInfo;
        }

        public void FrameGrabber(object sender, EventArgs e)
        {
            _lastInfo = new List<HeadInformation>();

            CountOfFacesLabel.Text = "0";
            //label4.Text = "";
            NamePersons.Add("");


            //Get the current frame form capture device
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
          face,
          1.2,
          10,
          Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
          new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    //Eigen face recognizer
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                       trainingImages.ToArray(),
                       labels.ToArray(),
                       3000,
                       ref termCrit);

                    name = recognizer.Recognize(result);

                    //Draw the label for each face detected and recognized
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));

                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");


                //Set the number of faces detected on the scene
                CountOfFacesLabel.Text = facesDetected[0].Length.ToString();


                //Set the region of interest on the faces

                gray.ROI = f.rect;
                MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                   eye,
                   1.9,
                   5,
                   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                   new Size(20, 20));
                gray.ROI = Rectangle.Empty;


                foreach (MCvAvgComp ey in eyesDetected[0])
                {
                    Rectangle eyeRect = ey.rect;
                    eyeRect.Inflate(-7, -7);
                    eyeRect.Offset(f.rect.X, f.rect.Y);
                    currentFrame.Draw(eyeRect, new Bgr(Color.Blue), 2);
                }


                //gray.ROI = f.rect;
                //MCvAvgComp[][] mouthDetected = gray.DetectHaarCascade(
                //   mouth,
                //   1.1,
                //   37,
                //   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                //   new Size(20, 20));
                //gray.ROI = Rectangle.Empty;

                //foreach (MCvAvgComp ey in mouthDetected[0])
                //{
                //    Rectangle mouthRect = ey.rect;
                //    mouthRect.Offset(f.rect.X, f.rect.Y);
                //    currentFrame.Draw(mouthRect, new Bgr(Color.Black), 2);
                //}


                gray.ROI = f.rect;
                MCvAvgComp[][] smileDetected = gray.DetectHaarCascade(
                   smile,
                   2,
                   20,
                   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                   new Size(20, 20));
                gray.ROI = Rectangle.Empty;

                HeadInformation hi = new HeadInformation();
                hi.IsSmile = false;

                foreach (MCvAvgComp ey in smileDetected[0])
                {
                    Rectangle smileRect = ey.rect;
                    smileRect.Offset(f.rect.X, f.rect.Y);
                    currentFrame.Draw(smileRect, new Bgr(Color.Black), 2);
                    currentFrame.Draw("smile", ref font, new Point(smileRect.X, smileRect.Y), new Bgr(Color.Red));
                    hi.IsSmile = true;
                }


                hi.Head = f.rect;
                if (eyesDetected[0] != null && eyesDetected[0].Length > 0)
                {
                    if (eyesDetected[0].Length == 1)
                    {
                        hi.Eye1 = eyesDetected[0][0].rect;
                    }
                    if (eyesDetected[0].Length == 2)
                    {
                        hi.Eye1 = eyesDetected[0][0].rect;
                        hi.Eye2 = eyesDetected[0][1].rect;
                    }
                }

                hi.Name = name;

                _lastInfo.Add(hi);
            }

            t = 0;

            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }
            //Show the faces procesed and recognized
            imageBoxFrameGrabber.Image = currentFrame.Resize(800, 600, INTER.CV_INTER_CUBIC);
            ListOfUserLabel.Text = names;
            names = "";
            //Clear the list(vector) of names
            NamePersons.Clear();

        }

        public event EventHandler<EventArgs> OnStartClick;

        public event EventHandler<EventArgs> OnStopClick;

        public event EventHandler<EventArgs> OnTeachClick;

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        private void AdminForm_VisibleChanged(object sender, EventArgs e)
        {         
        }

        public void SetListOfUsers(string listOfUsers)
        {
            throw new NotImplementedException();
        }

        public void SetCountOfUsers(string countOfUsers)
        {
            throw new NotImplementedException();
        }

        public void SetTrainedName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetTrainedImage(IImage someImage)
        {
            throw new NotImplementedException();
        }

        public Capture GetCapture()
        {
            throw new NotImplementedException();
        }

        public void SetImage(IImage someImage)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;


        public void CloseView()
        {
        }

        protected override void OnClosed(EventArgs e)
        {
        }

        public FormMessage formMessage { get; set; }

        public FormMessage GetFormMessage()
        {
            return formMessage;
        }

        public void HideView()
        {
            this.Hide();         
        }     
    }
}
