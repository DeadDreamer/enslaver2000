using EnslaverFrontEnd.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Forms;
using Emgu.CV.CvEnum;

namespace EnslaverFrontEnd.Presenters
{
    public class AdminFormPresenter : BasePresenter
    {
        public AdminFormPresenter(IBaseView view)
            : base(view)
        {
            (this.View as IAdminView).OnStartClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStartClick);
            (this.View as IAdminView).OnStopClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStopClick);
            (this.View as IAdminView).OnTeachClick +=new EventHandler<EventArgs>(AdminFormPresenter_OnTeachClick); 
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            //var capture = (this.View as IAdminView).GetCapture();
            //currentFrame = capture.QueryFrame().ToImage<Bgr, byte>().Resize(320, 240, Emgu.CV.CvEnum.Inter.Cubic);
            //gray_frame = capture.QueryFrame().ToImage<Gray, byte>().Resize(320, 240, Emgu.CV.CvEnum.Inter.Cubic);
            //gray_frame._EqualizeHist();

            //Rectangle[] facesDetected = face.DetectMultiScale(gray_frame, 1.2, 2, new Size(5, 5), new Size(320, 240));

            //for (int i = 0; i < facesDetected.Length; i++)
            //{
            //    gray_frame.ROI = facesDetected[i];

            //    Rectangle[] eyesDetected = eyes.DetectMultiScale(gray_frame, 1.2, 5, new Size(5, 5), new Size(30, 30));
            //    Rectangle[] smileDetected = smile.DetectMultiScale(gray_frame, 1.1, 10, new Size(5, 5), new Size(60, 60));

            //    foreach (Rectangle f in eyesDetected)
            //    {
            //        f.Offset(facesDetected[i].X, facesDetected[i].Y);
            //        currentFrame.Draw(f, new Bgr(Color.Blue), 2);
            //    }

            //    if (smileDetected.Length > 0)
            //    {
            //        smileDetected[0].Offset(facesDetected[i].X, facesDetected[i].Y);
            //        DrawSmile(smileDetected[0], facesDetected[i]);
            //    }

            //    DrawHead(facesDetected[i], i);
            //}

            //(this.View as IAdminView).SetImage(currentFrame.Resize(800, 600, Emgu.CV.CvEnum.Inter.Cubic));            
        }

        private void DrawSmile(Rectangle smile, Rectangle head)
        {
            //currentFrame.Draw(smile, new Bgr(Color.Black), 2);
            Point loc = new Point(head.X, head.Y - 10);
            //currentFrame.Draw("smile detected", loc, FontFace.HersheyPlain, 0.7, new Bgr(Color.Yellow));
        }

        private void DrawHead(Rectangle face, int i)
        {
            //currentFrame.Draw(face, new Bgr(Color.Red), 2);
        }

        void AdminFormPresenter_OnStopClick(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(ProcessFrame);            
        }

        void AdminFormPresenter_OnStartClick(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(ProcessFrame);            
        }

        void AdminFormPresenter_OnTeachClick(object sender, EventArgs e)
        {

        }
    }
}
