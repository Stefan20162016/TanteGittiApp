using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.CommunityToolkit.UI.Views;
using System.Diagnostics;

namespace TanteGittiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //zoomLabel.Text = string.Format("Zoom: {0}", zoomSlider.Value);
			cameraView.Zoom = 4;

			var viewModel = new ViewModel();
			viewModel.TestEvent += Model_TestEvent;
			BindingContext = viewModel;

		}

		private void Model_TestEvent()
		{
			//camera.Shutter(); //set `x:Name` for the cameraView control
			Debug.WriteLine("clicked clicked clicked");
			cameraView.Shutter();
		}

		void ZoomSlider_ValueChanged(object sender, ValueChangedEventArgs e)
		{
			//cameraView.Zoom = (float)zoomSlider.Value;
			//zoomLabel.Text = string.Format("Zoom: {0}", Math.Round(zoomSlider.Value));
		}

		void VideoSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			var captureVideo = e.Value;

			if (captureVideo)
				cameraView.CaptureMode = CameraCaptureMode.Video;
			else
				cameraView.CaptureMode = CameraCaptureMode.Photo;

			previewPicture.IsVisible = !captureVideo;

			//doCameraThings.Text = e.Value ? "Start Recording": "Snap Picture";
		}

		// You can also set it to Default and External
		void FrontCameraSwitch_Toggled(object sender, ToggledEventArgs e)
			=> cameraView.CameraOptions = e.Value ? CameraOptions.Front : CameraOptions.Back;

		// You can also set it to Torch (always on) and Auto
		void FlashSwitch_Toggled(object sender, ToggledEventArgs e)
			=> cameraView.FlashMode = e.Value ? CameraFlashMode.On : CameraFlashMode.Off;

		void DoCameraThings_Clicked(object sender, EventArgs e)
		{
			cameraView.Shutter();
			
			//doCameraThings.Text = cameraView.CaptureMode == CameraCaptureMode.Video
				//? "Stop Recording"
				//: "Snap Picture";
		}

		void CameraView_OnAvailable(object sender, bool e)
		{
			if (e)
			{
				/*
				zoomSlider.Value = cameraView.Zoom;
				var max = cameraView.MaxZoom;
				if (max > zoomSlider.Minimum && max > zoomSlider.Value)
					zoomSlider.Maximum = max;
				else
					zoomSlider.Maximum = zoomSlider.Minimum + 1; // if max == min throws exception
				*/
			}

			//doCameraThings.IsEnabled = e;
			//zoomSlider.IsEnabled = e;
			var max = cameraView.MaxZoom;
			Debug.WriteLine("XXXX: maxzoom: " + max);
		}

		void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
		{
			switch (cameraView.CaptureMode)
			{
				default:
				case CameraCaptureMode.Default:
				case CameraCaptureMode.Photo:
					//previewVideo.IsVisible = false;
					previewPicture.IsVisible = true;
					previewPicture.Rotation = e.Rotation;
					

					
					previewPicture.Source = e.Image;

					byte[] imagebytes = e.ImageData;


					Debug.WriteLine("XXXX e.image: " + previewPicture.Source  );
					//doCameraThings.Text = "Snap Picture";

					cameraView.IsVisible = false;

					break;
				case CameraCaptureMode.Video:
					previewPicture.IsVisible = false;
					//previewVideo.IsVisible = true;
					//previewVideo.Source = e.Video;
					//doCameraThings.Text = "Start Recording";
					break;
			}
		}

        private void previewPicture_Clicked(object sender, EventArgs e)
        {
			previewPicture.IsVisible = false;
			cameraView.IsVisible = true;
        }
    }
}
