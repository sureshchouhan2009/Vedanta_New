using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class EditObservationPageViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<UploadImageModel> _uploadedImagesList = new ObservableCollection<UploadImageModel>();
        private string _observationSummaryText;
        private GetObservationModel _currentObservation;
        private ICommand _deleteCurrentImageCommand;
        private ICommand _pickOrCaptureImageCommmand;
        private ICommand _updateObservationCommand;
        #endregion

        #region Properties
        public ObservableCollection<UploadImageModel> UploadedImagesList
        {
            get { return _uploadedImagesList; }
            set { SetProperty(ref _uploadedImagesList, value); }
        }
        public string ObservationSummaryText
        {
            get { return _observationSummaryText; }
            set { SetProperty(ref _observationSummaryText, value); }
        }
        public GetObservationModel CurrentObservation
        {
            get { return _currentObservation; }
            set { SetProperty(ref _currentObservation, value); }
        }

        #endregion

        #region Command Properties
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

        public ICommand UpdateObservationCommand
        {
            get
            {
                if (_updateObservationCommand == null)
                {
                    _updateObservationCommand = new Command<object>(UpdateObservationCommandExecute);
                }
                return _updateObservationCommand;
            }
        }

        #endregion

        #region Methods

        private void DeleteCurrentImageCommandExecute(object obj)
        {
            try
            {
                var param = obj as UploadImageModel;
                UploadedImagesList.Remove(param);
                if (UploadedImagesList != null || UploadedImagesList.Count == 0)
                {
                    new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") };
                }
            }
            catch (Exception ex)
            {



            }
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

                var byteArray = await GeneralUtility.getByteArrayFromFile(pickedfile);
                var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageByteArray = byteArray, ImageName = pickedfile.FileName });

            }
            catch (Exception ex)
            {
            }

        }
        private async void UpdateObservationCommandExecute(object obj)
        {

            PostObservationModel observationModel = new PostObservationModel();

            try
            {
                if (Session.Instance.CheckInternetConnection())
                {
                    if (!string.IsNullOrWhiteSpace(ObservationSummaryText) && UploadedImagesList.Count >= 3)
                    {
                        IsBusy = true;
                        UploadedImagesList.RemoveAt(0);//removing the default image.
                                                       //attach input data collected from user/observer
                        for (int i = 0; i < UploadedImagesList.Count; i++)
                        {
                            if (i == 0)
                            {
                                observationModel.ObservationImage = UploadedImagesList[i].ImageName;
                            }
                            if (i == 1)
                            {
                                observationModel.ObservationImage1 = UploadedImagesList[i].ImageName;
                            }
                            if (i == 2)
                            {
                                observationModel.ObservationImage2 = UploadedImagesList[i].ImageName;
                            }
                        }
                        observationModel.Observations = ObservationSummaryText;


                        // below details assigning from schedule model
                        //ask for commented properties

                        observationModel.Id = CurrentObservation.Id;
                        // observationModel.AoCategoryMasterId = CurrentObservation..AoCategoryMasterId;//need to check for parameter
                        // observationModel.AoDepartmentMasterId = CurrentObservation..AoDepartmentMasterId;
                        observationModel.Measure = CurrentObservation.Measure;
                        observationModel.AoGembaCheckListMasterId = Session.Instance.ChecklistParametersList.FirstOrDefault(ck => ck.Measure == "AO Awareness").Id;
                        //   observationModel.AoSBUMasterId = CurrentObservation.AoSBUMasterId;
                        // observationModel.Category = CurrentObservation.Category;
                        //  observationModel.Date = CurrentObservation..Date.ToString("dd/MM/yyyy");// check what send here
                        observationModel.UserName = Preferences.Get("UserName", "");
                        //  observationModel.Status = CurrentObservation.Status;
                        //  observationModel.SBU = CurrentObservation.SBU;
                        observationModel.Score = CurrentObservation.Score;
                        observationModel.PerformedOn = DateTime.Now.ToString();
                        observationModel.PerformedBy = Preferences.Get("UserName", "");// to get current logged in user
                                                                                       // observationModel.Percentage = CurrentObservation.Percentage;
                        observationModel.Leader = CurrentObservation.Employee;
                        observationModel.GembaWalkScheduleId = CurrentObservation.GembaWalkScheduleId;
                        //  observationModel.Department = CurrentObservation.Department;
                        //  observationModel.EmployeeMappingId = CurrentObservation.EmployeeMappingId;
                        observationModel.Employee = CurrentObservation.Employee;
                        var request = JsonConvert.SerializeObject(observationModel);
                        var result = await ApiService.Instance.UpdateObservationApiCall(observationModel);

                        if (result)
                        {

                            ObservationSummaryText = "";//clearing the edit text field
                            UploadedImagesList.Clear();
                            await Application.Current.MainPage.DisplayAlert("Success", "Onservation updated successfuly", "Ok");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                        }

                        IsBusy = false;
                    }
                    else
                    {
                        if (UploadedImagesList.Count == 1)
                        {
                            await Application.Current.MainPage.DisplayAlert("Require fields", "Please upload Atleast one image", "Ok");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Require fields", "Please write some observation summary", "Ok");
                        }

                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please check your internet connectivity", "Ok");

                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsBusy = true;
            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("ParametersForEditObservationPage"))
            {
                CurrentObservation = parameters.GetValue<GetObservationModel>("ParametersForEditObservationPage");
                //perform here api call to get image data

                FillTheImagesToList(CurrentObservation);
            }
            IsBusy = false;
        }

        private async void FillTheImagesToList(GetObservationModel currentObservation)
        {

            List<GetObservationImagesModel> imagesList = await ApiService.Instance.GetObservationImage(currentObservation.Id);
            if (imagesList.Count > 0)
            {
                foreach(var imageModel in imagesList)
                {
                    try
                    {
                        Byte[] bytes = Convert.FromBase64String(imageModel.FileSource);
                        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), imageModel.FileName);
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                        File.WriteAllBytes(fileName, bytes);
                        var imgSource = ImageSource.FromFile(fileName);
                        UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = imageModel.FileName });
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            #region TestCode

            //if (!string.IsNullOrWhiteSpace(currentObservation.ObservationImage1))
            //{
            //    Byte[] bytes = Convert.FromBase64String(currentObservation.ObservationImage);
            //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "image1.jpg");
            //    if (File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
            //    }
            //    File.WriteAllBytes(fileName, bytes);
            //    var imgSource = ImageSource.FromFile(fileName);
            //    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "image1.jpg" });

            //} 
            #endregion
            if (UploadedImagesList != null || UploadedImagesList.Count == 0)
            {
                UploadedImagesList.Add(new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") });
            }
            if (!string.IsNullOrWhiteSpace(currentObservation.Observations))
            {
                ObservationSummaryText = currentObservation.Observations;
            }
        }
        #endregion


        #region Constructer
        public EditObservationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }


        #endregion

      
    }
}
