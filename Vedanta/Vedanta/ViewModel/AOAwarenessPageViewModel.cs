using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vedanta.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class AOAwarenessPageViewModel : ViewModelBase
    {
        private ObservableCollection<UploadImageModel> _uploadedImagesList = new ObservableCollection<UploadImageModel>();
        public ObservableCollection<UploadImageModel> UploadedImagesList
        {
            get { return _uploadedImagesList; }
            set { SetProperty(ref _uploadedImagesList, value); }
        }



        private ICommand _pickOrCaptureImageCommmand;

        public ICommand PickOrCaptureImageCommmand
        {
            get
            {
                if (_pickOrCaptureImageCommmand == null)
                {
                    _pickOrCaptureImageCommmand = new Command<object>(PickOrCaptureImageCommmandExecute);
                }
                return _pickOrCaptureImageCommmand;
            }
        }
        private ICommand _deleteCurrentImageCommand;

        public ICommand DeleteCurrentImageCommand
        {
            get
            {
                if (_deleteCurrentImageCommand == null)
                {
                    _deleteCurrentImageCommand = new Command<object>(DeleteCurrentImageCommandExecute);
                }
                return _deleteCurrentImageCommand;
            }
        }

        private void DeleteCurrentImageCommandExecute(object obj)
        {

        }

        private async void PickOrCaptureImageCommmandExecute(object obj)
        {
            try
            {
                int index = UploadedImagesList.IndexOf(obj as UploadImageModel);
                // UploadedImagesList.Add(new UploadImageModel { ImageName = "homeLogo", imageSource = ImageSource.FromResource("siteImage") });
                //UploadedImagesList[index]= new UploadImageModel { ImageName = "homeLogo", imageSource = ImageSource.FromResource("siteImage") };
                //UploadedImagesList.Add(obj as UploadImageModel);

                var pickedfile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Capture Image"
                });
                var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                UploadedImagesList.Add( new UploadImageModel { imageSource = imgSource });

            }
            catch (Exception ex)
            {

                
            }

        }
        
        public AOAwarenessPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            var ImagesList = new ObservableCollection<UploadImageModel>(new List<UploadImageModel>
            {

                new UploadImageModel{ ImageName="siteImage" , imageSource= ImageSource.FromResource("siteImage")}

            });

            UploadedImagesList = ImagesList;
        }
    }
}
