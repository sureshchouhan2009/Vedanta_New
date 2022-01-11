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
        private string _PageTitle;
        public string PageTitle
        {
            get { return _PageTitle; }
            set { SetProperty(ref _PageTitle, value); }
        }



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
                    if (!string.IsNullOrWhiteSpace(ObservationSummaryText) && UploadedImagesList.Count >=3)
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
                        observationModel.Measure = "AO Awareness";
                        observationModel.AoGembaCheckListMasterId = Session.Instance.ChecklistParametersList.FirstOrDefault(ck => ck.Measure == "AO Awareness").Id;
                        observationModel.AoSBUMasterId = GembaScheduleModelParamMeasure.AoSBUMasterId;
                        observationModel.Category = GembaScheduleModelParamMeasure.Category;
                        observationModel.Date = GembaScheduleModelParamMeasure.Date.ToString("dd/MM/yyyy");// check what send here
                        observationModel.UserName = GembaScheduleModelParamMeasure.UserName;
                        observationModel.Status = GembaScheduleModelParamMeasure.Status;
                        observationModel.SBU = GembaScheduleModelParamMeasure.SBU;
                        observationModel.Score = GembaScheduleModelParamMeasure.Score;
                        observationModel.PerformedOn = DateTime.Now.ToString();
                        observationModel.PerformedBy = "Umesh.ecgit"; // to get current logged in user
                        observationModel.Percentage = GembaScheduleModelParamMeasure.Percentage;
                        observationModel.Leader = GembaScheduleModelParamMeasure.Employee;
                        observationModel.GembaWalkScheduleId = GembaScheduleModelParamMeasure.Id;
                        observationModel.Department = GembaScheduleModelParamMeasure.Department;
                        observationModel.EmployeeMappingId = GembaScheduleModelParamMeasure.EmployeeMappingId;
                        observationModel.Employee = GembaScheduleModelParamMeasure.Employee;
                        var request=  JsonConvert.SerializeObject(observationModel);
                        var result = await ApiService.Instance.AddObservationApiCall(observationModel);

                        if (result)
                        {
                            getAllLeaderObservationList();
                            ObservationSummaryText = "";//clearing the edit text field
                            await Application.Current.MainPage.DisplayAlert("Success", "New Onservation added successfuly", "Ok");
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

        // need to make dynamic via id , id we need to get from measure page.
        private async void getAllLeaderObservationList()
        {
            var allObservationList = await ApiService.Instance.GetAllObservationAgainstSchedule(GembaScheduleModelParamMeasure.Id);
            if (allObservationList.Count > 0)
            {
                PreviousObservations = new ObservableCollection<GetObservationModel>(allObservationList.OrderByDescending(e=>e.Id));
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
                PageTitle= parameters.GetValue<string>("Title");
            }
            if (parameters.ContainsKey("ScheduleDataForAwareness"))
            {
                GembaScheduleModelParamMeasure = parameters.GetValue<GembaScheduleModel>("ScheduleDataForAwareness");
                getAllLeaderObservationList();
            }
            IsBusy = false;
        }
    }
}
