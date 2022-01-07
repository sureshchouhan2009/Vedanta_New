using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public class AOAwarenessPageViewModel : ViewModelBase
    {
        private ObservableCollection<UploadImageModel> _uploadedImagesList = new ObservableCollection<UploadImageModel>();
        public ObservableCollection<UploadImageModel> UploadedImagesList
        {
            get { return _uploadedImagesList; }
            set { SetProperty(ref _uploadedImagesList, value); }
        }

        private GembaScheduleModel _gembaScheduleModelParamFromMeasure;
        public GembaScheduleModel GembaScheduleModelParamMeasure
        {
            get { return _gembaScheduleModelParamFromMeasure; }
            set { SetProperty(ref _gembaScheduleModelParamFromMeasure, value); }
        }

        private string _observationSummaryText;
        public string ObservationSummaryText
        {
            get { return _observationSummaryText; }
            set { SetProperty(ref _observationSummaryText, value); }
        }
        
        private List<string> _previousObservations;
        public List<string> PreviousObservations
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

        private ICommand _backPageNavigateCommand;

        public ICommand BackPageNavigateCommand
        {
            get
            {
                if (_backPageNavigateCommand == null)
                {
                    _backPageNavigateCommand = new Command<object>(BackPageNavigation);
                }
                return _backPageNavigateCommand;
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
            ObservationModel observationModel = new ObservationModel();

            try
            {
                if (Session.Instance.CheckInternetConnection())
                {
                    if (!string.IsNullOrWhiteSpace(ObservationSummaryText) && UploadedImagesList.Count > 1)
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

                        observationModel.Id = GembaScheduleModelParamMeasure.Id;
                        observationModel.AoCategoryMasterId = GembaScheduleModelParamMeasure.AoCategoryMasterId;
                        observationModel.AoDepartmentMasterId = GembaScheduleModelParamMeasure.AoDepartmentMasterId;
                        //observationModel.AoGembaCheckListMasterId = GembaScheduleModelParamMeasure;
                        //observationModel.AoGembaObservationId = GembaScheduleModelParamMeasure.ao;
                        observationModel.AoSBUMasterId = GembaScheduleModelParamMeasure.AoSBUMasterId;
                        observationModel.Category = GembaScheduleModelParamMeasure.Category;
                        observationModel.Date = GembaScheduleModelParamMeasure.Date.ToString("dd/MM/yyyy");// check what send here
                        observationModel.UserName = GembaScheduleModelParamMeasure.UserName;
                        observationModel.Status = GembaScheduleModelParamMeasure.Status;
                        observationModel.SBU = GembaScheduleModelParamMeasure.SBU;
                        observationModel.Score = GembaScheduleModelParamMeasure.Score;
                        observationModel.PerformedOn = DateTime.Now.ToString();
                        observationModel.PerformedBy = GembaScheduleModelParamMeasure.PerformedBy;
                        observationModel.Percentage = GembaScheduleModelParamMeasure.Percentage;
                        // observationModel.Leader=GembaScheduleModelParamMeasure.
                        // observationModel.GembaWalkScheduleId = GembaScheduleModelParamMeasure.gem;

                        observationModel.EmployeeMappingId = GembaScheduleModelParamMeasure.EmployeeMappingId;
                        observationModel.Employee = GembaScheduleModelParamMeasure.Employee;
                        var result = await ApiService.Instance.AddObservationApiCall(observationModel);
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

                var byteArray = await getByteArrayFromFile(pickedfile);
                var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageByteArray = byteArray, ImageName= pickedfile.FileName });

            }
            catch (Exception ex)
            {
            }

        }

        private async Task<byte[]> getByteArrayFromFile(FileResult pickedfile)
        {
            var bytes = default(byte[]);
            try
            {
                var st = await pickedfile.OpenReadAsync();
                using (var streamReader = new StreamReader(st))
                {
                    using (var memstream = new MemoryStream())
                    {
                        streamReader.BaseStream.CopyTo(memstream);
                        bytes = memstream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return bytes;
        }

        public AOAwarenessPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PreviousObservations = new List<string>
            {
                "","",""
            };
            var ImagesList = new ObservableCollection<UploadImageModel>(new List<UploadImageModel>
            {
                new UploadImageModel{ ImageName="siteImage" , imageSource= ImageSource.FromResource("siteImage")}
            });
            UploadedImagesList = ImagesList;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("ScheduleDataForAwareness"))
            {
                GembaScheduleModelParamMeasure = parameters.GetValue<GembaScheduleModel>("ScheduleDataForAwareness");
            }
        }
    }
}
