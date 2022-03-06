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
    public class ObservationsPageViewModel : ViewModelBase
    {


        #region Fields
        private ObservableCollection<UploadImageModel> _uploadedImagesList = new ObservableCollection<UploadImageModel>();
        private GembaScheduleModel _gembaScheduleModelParamFromMeasure;
        private MeasuresAndScoreModel _currentMeasureAndScoreModel;
        private string _observationSummaryText;


        #endregion

        public ObservableCollection<UploadImageModel> UploadedImagesList
        {
            get { return _uploadedImagesList; }
            set { SetProperty(ref _uploadedImagesList, value); }
        }

        public GembaScheduleModel GembaScheduleModelParamMeasure
        {
            get { return _gembaScheduleModelParamFromMeasure; }
            set { SetProperty(ref _gembaScheduleModelParamFromMeasure, value); }
        }

        public MeasuresAndScoreModel CurrentMeasureAndScoreModel
        {
            get { return _currentMeasureAndScoreModel; }
            set { SetProperty(ref _currentMeasureAndScoreModel, value); }
        }

        public string ObservationSummaryText
        {
            get { return _observationSummaryText; }
            set { SetProperty(ref _observationSummaryText, value); }
        }

        private ObservableCollection<GetObservationModel> _previousObservations;
        public ObservableCollection<GetObservationModel> PreviousObservations
        {
            get { return _previousObservations; }
            set { SetProperty(ref _previousObservations, value); }
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

        private ICommand _ContinueToScoreCommand;

        public ICommand ContinueToScoreCommand
        {
            get
            {
                if (_ContinueToScoreCommand == null)
                {
                    _ContinueToScoreCommand = new Command<object>(ContinueToScoreCommandExecute);
                }
                return _ContinueToScoreCommand;
            }
        }
        private ICommand _deleteObservationCommand;

        public ICommand DeleteObservationCommand
        {
            get
            {
                if (_deleteObservationCommand == null)
                {
                    _deleteObservationCommand = new Command<object>(DeleteObservationCommandExecute);
                }
                return _deleteObservationCommand;
            }
        }

        private async void DeleteObservationCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
                var currentObservation = obj as GetObservationModel;
                var response = await ApiService.Instance.DeleteObservationApiCall(currentObservation.Id);
                if (response)
                {
                    PreviousObservations.Remove(currentObservation);
                    getAllLeaderObservationList(GembaScheduleModelParamMeasure.Id, CurrentMeasureAndScoreModel.Id);
                    await Application.Current.MainPage.DisplayAlert("Success", "Observation deleted successfully", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                }
            }
            catch (Exception ex)
            {


            }
            IsBusy = false;

        }

        private ICommand _editObservationCommand;

        public ICommand EditObservationCommand
        {
            get
            {
                if (_editObservationCommand == null)
                {
                    _editObservationCommand = new Command<object>(EditObservationCommandExecute);
                }
                return _editObservationCommand;
            }
        }

        private async void EditObservationCommandExecute(object obj)
        {


            IsBusy = true;
            try
            {

                var currentObservation = obj as GetObservationModel;
               
                    var ParametersForEditObservationPage = new NavigationParameters();
                    ParametersForEditObservationPage.Add("ParametersForEditObservationPage", currentObservation);
                    ParametersForEditObservationPage.Add("Title", currentObservation.Measure);
                    ParametersForEditObservationPage.Add("CurrentGembaScheduleModel", GembaScheduleModelParamMeasure);
                    ParametersForEditObservationPage.Add("MeasuresAndScoreModelData", CurrentMeasureAndScoreModel);
                    await NavigationService.NavigateAsync("EditObservationPage", ParametersForEditObservationPage);
                
                
            }
            catch (Exception ex)
            {
            }
            IsBusy = false;


        }

        private async void ContinueToScoreCommandExecute(object obj)
        {
            IsBusy = true;
            try
            {
                var value = obj as GetObservationModel;
                if (value.IsAddScoreEnabled)
                {
                    var navigationParameters = new NavigationParameters();
                    navigationParameters.Add("GembaScheduleModelParamFromObservation", GembaScheduleModelParamMeasure);
                    navigationParameters.Add("Title", value.Measure);
                    navigationParameters.Add("GembaWalkScheduleId", value.GembaWalkScheduleId);
                    navigationParameters.Add("AoGembaCheckListMasterId", value.AoGembaCheckListMasterId);
                    await NavigationService.NavigateAsync("ScorePage", navigationParameters);
                }
            }
            catch (Exception ex)
            {
            }
            IsBusy = false;
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



        private void BackPageNavigation(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private ICommand _addObservationCommand;

        public ICommand AddObservationCommand
        {
            get
            {
                if (_addObservationCommand == null)
                {
                    _addObservationCommand = new Command<object>(AddObservationCommandExecute);
                }
                return _addObservationCommand;
            }
        }

        private async void AddObservationCommandExecute(object obj)
        {
            PostObservationModel observationModel = new PostObservationModel();

            try
            {
                if (Session.Instance.CheckInternetConnection())
                {
                    if (!string.IsNullOrWhiteSpace(ObservationSummaryText) && UploadedImagesList.Count >= 1)
                    {
                        IsBusy = true;
                        for (int i = 0; i < UploadedImagesList.Count; i++)
                        {
                            if (i == 0)
                            {
                                observationModel.ObservationImage = UploadedImagesList[i].ImageBase64String;
                            }
                            if (i == 1)
                            {
                                observationModel.ObservationImage1 = UploadedImagesList[i].ImageBase64String;
                            }
                            if (i == 2)
                            {
                                observationModel.ObservationImage2 = UploadedImagesList[i].ImageBase64String;
                            }
                        }
                        observationModel.Observations = ObservationSummaryText;


                        // below details assigning from schedule model
                        //ask for commented properties

                        //observationModel.Id = GembaScheduleModelParamMeasure.Id;
                        observationModel.AoCategoryMasterId = GembaScheduleModelParamMeasure.AoCategoryMasterId;
                        observationModel.AoDepartmentMasterId = GembaScheduleModelParamMeasure.AoDepartmentMasterId;
                        observationModel.Measure = CurrentMeasureAndScoreModel.Measure;
                        observationModel.AoGembaCheckListMasterId = Session.Instance.ChecklistParametersList.FirstOrDefault(ck => ck.Measure == CurrentMeasureAndScoreModel.Measure).Id;
                        observationModel.AoSBUMasterId = GembaScheduleModelParamMeasure.AoSBUMasterId;
                        observationModel.Category = GembaScheduleModelParamMeasure.Category;
                        observationModel.Date = GembaScheduleModelParamMeasure.Date.ToString("yyyy-MM-dd");// check what send here
                        observationModel.UserName = GembaScheduleModelParamMeasure.UserName;
                        observationModel.Status = GembaScheduleModelParamMeasure.Status;
                        observationModel.SBU = GembaScheduleModelParamMeasure.SBU;
                        observationModel.Score = GembaScheduleModelParamMeasure.Score;
                        observationModel.PerformedOn = DateTime.Now.ToString("yyyy-MM-dd");
                        observationModel.PerformedBy = Preferences.Get("UserName", "");
                        observationModel.Percentage = GembaScheduleModelParamMeasure.Percentage;
                        observationModel.Leader = GembaScheduleModelParamMeasure.Employee;
                        observationModel.GembaWalkScheduleId = GembaScheduleModelParamMeasure.Id;
                        observationModel.Department = GembaScheduleModelParamMeasure.Department;
                        observationModel.EmployeeMappingId = GembaScheduleModelParamMeasure.EmployeeMappingId;
                        observationModel.Employee = GembaScheduleModelParamMeasure.Employee;
                        var request = JsonConvert.SerializeObject(observationModel);
                        var result = await ApiService.Instance.AddObservationApiCall(observationModel);

                        if (result)
                        {
                            getAllLeaderObservationList(GembaScheduleModelParamMeasure.Id, CurrentMeasureAndScoreModel.Id);
                            ObservationSummaryText = "";//clearing the edit text field
                            UploadedImagesList.Clear();
                            handleDefaultImageVisibility();
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert("Success", "New Observation added successfully", "Ok");
                        }
                        else
                        {
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                           
                        }

                       
                    }
                    else
                    {
                        if (UploadedImagesList.Count <1)
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
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                
            }
        }

        // need to make dynamic via id , id we need to get from measure page.
        private async void getAllLeaderObservationList(int ScheduleID, int MeasureID)
        {
            var allObservationList = await ApiService.Instance.GetAllObservationAgainstMeasure(ScheduleID, MeasureID);
            if (allObservationList.Count > 0)
            {
                bool addScoreEnabled = CurrentMeasureAndScoreModel.Score == 1 ? true : false;
                allObservationList.ForEach(obs => obs.IsAddScoreEnabled = addScoreEnabled);
                PreviousObservations = new ObservableCollection<GetObservationModel>(allObservationList.OrderByDescending(e => e.Id));
                Session.Instance.CurrentMeasureObservations.Clear();
                Session.Instance.CurrentMeasureObservations = PreviousObservations.ToList();
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
                if (UploadedImagesList.Count < 3)
                {
                    int index = UploadedImagesList.IndexOf(obj as UploadImageModel);
                    var pickedfile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Image"
                    });
                   
                    var byteArray = await GeneralUtility.getByteArrayFromFile(pickedfile);
                var compressedBarray=await    ImageResizer.ResizeImage(byteArray, 500, 500);
                    var Base64String = Convert.ToBase64String(compressedBarray);
                    var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageBase64String = Base64String, ImageByteArray = byteArray, ImageName = pickedfile.FileName });

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Only three images can be upload", "Ok");
                }


                handleDefaultImageVisibility();
            }
            catch (Exception ex)
            {
            }

        }

        //public static byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        //{
        //    // Load the bitmap
        //    Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
        //    Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
        //        return ms.ToArray();
        //    }
        //}


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

        public ObservationsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            var ImagesList = new ObservableCollection<UploadImageModel>(new List<UploadImageModel>
            {
                new UploadImageModel{ ImageName="siteImage" , imageSource= ImageSource.FromResource("siteImage")}
            });
            UploadedImagesList = ImagesList;

           
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("ScheduleDataForAwareness"))
            {
                GembaScheduleModelParamMeasure = parameters.GetValue<GembaScheduleModel>("ScheduleDataForAwareness");

            }
            if (parameters.ContainsKey("CurrentMeasureModel"))
            {
                CurrentMeasureAndScoreModel = parameters.GetValue<MeasuresAndScoreModel>("CurrentMeasureModel");
                getAllLeaderObservationList(GembaScheduleModelParamMeasure.Id,CurrentMeasureAndScoreModel.Id);
            }
            IsBusy = false;
        }
    }
}
