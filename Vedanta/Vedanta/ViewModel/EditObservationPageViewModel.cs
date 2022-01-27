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
        private GembaScheduleModel _CurrentGembaScheduleModel;
        private MeasuresAndScoreModel _currentMeasureAndScoreData;
        #endregion

        #region Properties
        public GembaScheduleModel CurrentGembaScheduleModel
        {
            get { return _CurrentGembaScheduleModel; }
            set { SetProperty(ref _CurrentGembaScheduleModel, value); }
        }
        public MeasuresAndScoreModel CurrentMeasureAndScoreData
        {
            get { return _currentMeasureAndScoreData; }
            set { SetProperty(ref _currentMeasureAndScoreData, value); }
        }
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
        private void handleDefaultImageVisibility()
        {
            if (UploadedImagesList.Count >= 1 && UploadedImagesList.Any(e => e.ImageName == "siteImage"))
            {
                UploadedImagesList.Remove(UploadedImagesList.FirstOrDefault(t => t.ImageName == "siteImage"));
            }
            if (UploadedImagesList.Count == 0)
            {
                UploadedImagesList.Add(new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") });
            }
        }
        private void DeleteCurrentImageCommandExecute(object obj)
        {
            try
            {
                var param = obj as UploadImageModel;
                UploadedImagesList.Remove(param);
                handleDefaultImageVisibility();
            }
            catch (Exception ex)
            {
            }
        }
        private async void PickOrCaptureImageCommmandExecute(object obj)
        {
            try
            {
                if (UploadedImagesList.Count <= 3)
                {
                    int index = UploadedImagesList.IndexOf(obj as UploadImageModel);
                    var pickedfile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Image"
                    });

                    var byteArray = await GeneralUtility.getByteArrayFromFile(pickedfile);
                    var compressedBarray = await ImageResizer.ResizeImage(byteArray, 500, 500);
                    var Base64String = Convert.ToBase64String(compressedBarray);
                    var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageBase64String = Base64String, ImageName = "" });
                    handleDefaultImageVisibility();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Maximum three images allowed", "Ok");
                }

            }
            catch (Exception ex)
            {
            }

        }
        private async void UpdateObservationCommandExecute(object obj)
        {

            PostObservationModel EditobservationModel = new PostObservationModel();

            try
            {
                if (Session.Instance.CheckInternetConnection())
                {
                    if (!string.IsNullOrWhiteSpace(ObservationSummaryText) && UploadedImagesList.Count >= 1)
                    {
                        IsBusy = true;
                        //attach input data collected from user/observer
                        for (int i = 0; i < UploadedImagesList.Count; i++)
                        {
                            if (i == 0)
                            {
                                EditobservationModel.ObservationImage = UploadedImagesList[i].ImageBase64String;
                            }
                            if (i == 1)
                            {
                                EditobservationModel.ObservationImage1 = UploadedImagesList[i].ImageBase64String;
                            }
                            if (i == 2)
                            {
                                EditobservationModel.ObservationImage2 = UploadedImagesList[i].ImageBase64String;
                            }
                        }
                        EditobservationModel.Observations = ObservationSummaryText;
                        // below details assigning from schedule model
                        EditobservationModel.Id = CurrentObservation.Id;
                        EditobservationModel.AoCategoryMasterId = CurrentGembaScheduleModel.AoCategoryMasterId;//need to check for parameter
                        EditobservationModel.AoDepartmentMasterId = CurrentGembaScheduleModel.AoDepartmentMasterId;
                        EditobservationModel.Measure = CurrentObservation.Measure;
                        EditobservationModel.AoGembaCheckListMasterId = Session.Instance.ChecklistParametersList.FirstOrDefault(ck => ck.Measure == "AO Awareness").Id;
                        EditobservationModel.AoSBUMasterId = CurrentGembaScheduleModel.AoSBUMasterId;
                        EditobservationModel.Category = CurrentGembaScheduleModel.Category;
                        EditobservationModel.Date = CurrentGembaScheduleModel.Date.ToString("yyyy-MM-dd");// check what send here
                        EditobservationModel.UserName = Preferences.Get("UserName", "");
                        EditobservationModel.Status = CurrentGembaScheduleModel.Status;
                        EditobservationModel.SBU = CurrentGembaScheduleModel.SBU;
                        EditobservationModel.Score = CurrentObservation.Score;
                        EditobservationModel.PerformedOn = DateTime.Now.ToString("yyyy-MM-dd");
                        EditobservationModel.PerformedBy = Preferences.Get("UserName", "");// to get current logged in user
                        EditobservationModel.Leader = CurrentObservation.Employee;
                        EditobservationModel.GembaWalkScheduleId = CurrentObservation.GembaWalkScheduleId;
                        EditobservationModel.Department = CurrentGembaScheduleModel.Department;
                        EditobservationModel.EmployeeMappingId = CurrentGembaScheduleModel.EmployeeMappingId;
                        EditobservationModel.Employee = CurrentObservation.Employee;
                        var request = JsonConvert.SerializeObject(EditobservationModel);
                        var result = await ApiService.Instance.UpdateObservationApiCall(EditobservationModel);

                        if (result)
                        {

                            ObservationSummaryText = "";//clearing the edit text field
                            UploadedImagesList.Clear();
                            await Application.Current.MainPage.DisplayAlert("Success", "Onservation updated successfully", "Ok");
                            var navigationParameters = new NavigationParameters();
                            navigationParameters.Add("ScheduleDataForAwareness", CurrentGembaScheduleModel);
                            navigationParameters.Add("CurrentMeasureModel", CurrentMeasureAndScoreData);
                            await NavigationService.GoBackAsync(navigationParameters);
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
            IsBusy = true;
            base.OnNavigatedTo(parameters);


            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("CurrentGembaScheduleModel"))
            {
                CurrentGembaScheduleModel = parameters.GetValue<GembaScheduleModel>("CurrentGembaScheduleModel");
            } 
            if (parameters.ContainsKey("MeasuresAndScoreModelData"))
            {
                CurrentMeasureAndScoreData = parameters.GetValue<MeasuresAndScoreModel>("MeasuresAndScoreModelData");
            }
            if (parameters.ContainsKey("ParametersForEditObservationPage"))
            {
                CurrentObservation = parameters.GetValue<GetObservationModel>("ParametersForEditObservationPage");
                //perform here api call to get image data
                var completed = await FillTheImagesToList(CurrentObservation);
            }
            IsBusy = false;
        }

        private async Task<bool> FillTheImagesToList(GetObservationModel currentObservation)
        {
            List<GetObservationImagesModel> imagesList = await ApiService.Instance.GetObservationImage(currentObservation.Id);
            if (imagesList.Count > 0)
            {
                for (int index = 0; index <= imagesList.Count; index++)
                {
                    try
                    {
                        if (index == 0)
                        {
                            Byte[] bytes = Convert.FromBase64String(imagesList[index].FileName);
                            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image.png");
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                            File.WriteAllBytes(fileName, bytes);
                            var imgSource = ImageSource.FromFile(fileName);
                            UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = imagesList[index].FileName });
                        }
                        else if (index == 1) 
                        {
                            Byte[] bytes = Convert.FromBase64String(imagesList[index].FileName1);
                            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image1.png");
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                            File.WriteAllBytes(fileName, bytes);
                            var imgSource = ImageSource.FromFile(fileName);
                            UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = imagesList[index].FileName1 });
                        }
                        else if (index == 2)
                        {
                            Byte[] bytes = Convert.FromBase64String(imagesList[index].FileName2);
                            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image2.png");
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                            File.WriteAllBytes(fileName, bytes);
                            var imgSource = ImageSource.FromFile(fileName);
                            UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = imagesList[index].FileName2 });
                        }
                          
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
                if (UploadedImagesList != null || UploadedImagesList.Count == 0)
                {
                    UploadedImagesList.Add(new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") });
                }
            }
            if (!string.IsNullOrWhiteSpace(currentObservation.Observations))
            {
                ObservationSummaryText = currentObservation.Observations;
            }

            return true;
        }
        #endregion


        #region Constructer
        public EditObservationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }


        #endregion

        #region testCode
        //public async void TestMethod()
        //{

        //    try
        //    {
        //        Byte[] bytes = Convert.FromBase64String(str);
        //        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.jpg");
        //        if (File.Exists(fileName))
        //        {
        //            File.Delete(fileName);
        //        }
        //        File.WriteAllBytes(fileName, bytes);
        //        var imgSource = ImageSource.FromFile(fileName);
        //        UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "test.jpg" });
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //} 
        #endregion
    }
}
