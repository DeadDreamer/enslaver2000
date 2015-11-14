using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using AForge.Video;
//using AForge.Video.DirectShow;
using System.Drawing.Imaging;

namespace EnslaverCore.Logic
{
    /* public class CamHelper
     {
         public List<FilterInfo> GetListOfDevices()
         {
             List<FilterInfo> result = new List<FilterInfo>();
             var filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
             foreach (FilterInfo item in filterInfoCollection)
             {
                 result.Add(item);
             }
             return result;
         }

         public bool IsActive
         {
             get { return isActive; }
         }

         private bool isActive = false;
         public bool Start()
         {
             if (videoCaptureDevice != null && !isActive)
             {
                 videoCaptureDevice.Start();
                 isActive = true;
                 return true;
             }
             return false;
         }

         public bool Stop()
         {
             if (videoCaptureDevice != null && isActive)
             {
                 videoCaptureDevice.Stop();
                 isActive = false;
                 return true;
             }
             return false;
         }

         public void SelectCamDeviceByMonikerString(string monikerString)
         {
             if (videoCaptureDevice != null)
             {
                 videoCaptureDevice.Stop();
                 isActive = false;
             }
             videoCaptureDevice = new VideoCaptureDevice(monikerString);
             videoCaptureDevice.NewFrame += videoCaptureDevice_NewFrame;
         }

         /// <summary>
         /// Подписаться на это событие для получения bitmap'ов. Например : Сохраним картинки  bitmap.Save("C:\\tmp\\delme\\" + (DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds.ToString()+ ".bmp",System.Drawing.Imaging.ImageFormat.Bmp);
         /// </summary>
         public event EventHandler<CamEvent> OnNewFrame;
        

         private void videoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
         {
             if (OnNewFrame != null)
             {
                 OnNewFrame(sender, new CamEvent(eventArgs.Frame));
             }
         }

         private VideoCaptureDevice videoCaptureDevice { get; set; }
     }

     public class CamEvent : EventArgs
     {
         public CamEvent(Bitmap bitmap) 
         {
             BitmapFromCam = new Bitmap(bitmap);
         }

         public Bitmap BitmapFromCam;
     }
     * */
}
