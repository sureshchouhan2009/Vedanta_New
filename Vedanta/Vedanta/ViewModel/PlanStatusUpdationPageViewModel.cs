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
    public class PlanStatusUpdationPageViewModel : ViewModelBase
    {

        private ObservableCollection<UploadImageModel> _uploadedImagesList = new ObservableCollection<UploadImageModel>();
        private List<string> _statuslist = new List<string>();
        private ObservableCollection<UploadImageModel> _newlyUploadedImages = new ObservableCollection<UploadImageModel>();
        private ActionPlanDetailsModel _planDetails;
        private string _selectedStatus;
        private string _ActionSummaryText;
        public ActionPlanDetailsModel PlanDetails
        {
            get { return _planDetails; }
            set { SetProperty(ref _planDetails, value); }
        }
        public ObservableCollection<UploadImageModel> UploadedImagesList
        {
            get { return _uploadedImagesList; }
            set { SetProperty(ref _uploadedImagesList, value); }
        }

        public List<string> StatusList
        {
            get { return _statuslist; }
            set { SetProperty(ref _statuslist, value); }
        }
        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set { SetProperty(ref _selectedStatus, value); }
        }

        public string ActionSummaryText
        {
            get { return _ActionSummaryText; }
            set { SetProperty(ref _ActionSummaryText, value); }
        }

        public ObservableCollection<UploadImageModel> NewlyUploadedImages
        {
            get { return _newlyUploadedImages; }
            set { SetProperty(ref _newlyUploadedImages, value); }
        }

        private ICommand _submitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new Command<object>(SubmitActionPlanStatusExecute);
                }
                return _submitCommand;
            }
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
            try
            {
                var param = obj as UploadImageModel;
                NewlyUploadedImages.Remove(param);
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
                if (NewlyUploadedImages.Count < 3)
                {
                    int index = NewlyUploadedImages.IndexOf(obj as UploadImageModel);
                    var pickedfile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = "Capture Image"
                    });

                    var byteArray = await GeneralUtility.getByteArrayFromFile(pickedfile);
                    var compressedBarray = await ImageResizer.ResizeImage(byteArray, 500, 500);
                    var Base64String = Convert.ToBase64String(compressedBarray);
                    var imgSource = ImageSource.FromFile(pickedfile.FullPath);
                    NewlyUploadedImages.Add(new UploadImageModel { imageSource = imgSource, ImageBase64String = Base64String, ImageByteArray = byteArray, ImageName = pickedfile.FileName });

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

        private void handleDefaultImageVisibility()
        {
            if (NewlyUploadedImages.Count >= 1 && NewlyUploadedImages.Any(e => e.ImageName == "siteImage"))
            {
                NewlyUploadedImages.Remove(NewlyUploadedImages.FirstOrDefault(t => t.ImageName == "siteImage"));
            }
            if (NewlyUploadedImages.Count == 0)
            {
                NewlyUploadedImages.Add(new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") });
            }
        }



        private async void SubmitActionPlanStatusExecute(object obj)
        {
            IsBusy = true;
            UpdateActionPlanModel update = new UpdateActionPlanModel();
           
            var message = PerformValidationsAndMessageGenerator();
            try
            {

                if (Session.Instance.CheckInternetConnection())
                {
                    if (message == "")
                    {
                        update.Remark = ActionSummaryText;
                        update.Id = PlanDetails.Id;
                        update.Status = SelectedStatus;
                        update.Date = PlanDetails.Date.ToString("yyyy-MM-dd");
                        update.TargetDate= PlanDetails.TargetDate.ToString("yyyy-MM-dd");
                        update.ActionPlan = PlanDetails.ActionPlan;
                        update.Department = PlanDetails.Department;
                        update.Employee = PlanDetails.Employee;
                        update.AoGembaObservationId = PlanDetails.AoGembaObservationId;
                        update.GembaWalkScheduleId = PlanDetails.GembaWalkScheduleId;
                        update.UserName = PlanDetails.UserName;
                        update.Observations = PlanDetails.Observations;
                        update.PerformedBy= Preferences.Get("UserName", "");
                        update.ResponsibilityUserId = Preferences.Get("UserName", "");
                        update.PerformedOn = DateTime.Now.ToString("yyyy-MM-dd");
                        update.IsDeleted = false;
                        for (int i = 0; i < UploadedImagesList.Count; i++)
                        {
                            if (i == 0)
                            {
                                update.ResponsibilityImage = NewlyUploadedImages[i].ImageBase64String;
                            }
                            if (i == 1)
                            {
                                update.ResponsibilityImage1 = NewlyUploadedImages[i].ImageBase64String;
                            }
                            if (i == 2)
                            {
                                update.ResponsibilityImage2 = NewlyUploadedImages[i].ImageBase64String;
                            }
                        }
                        var result = await ApiService.Instance.UpdateAcctionPlanApiCall(update);
                        if (result)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success", "Action Plan updated successfully", "Ok");
                            await NavigationService.NavigateAsync("/ActionPlansPage");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
                            IsBusy = false;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Require fields", message, "Ok");
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
            }
            IsBusy = false ;
        }

        private string PerformValidationsAndMessageGenerator()
        {
            string message = "";
            if (string.IsNullOrEmpty(SelectedStatus))
            {
                message = "Please select any Status";
            }
            else if (string.IsNullOrEmpty(ActionSummaryText))
            {
                message = "Summary is required";
            }
            else if (NewlyUploadedImages.Count > 0 && NewlyUploadedImages.Count < 1)
            {
                message = "Please upload Atleast one image";
            }
            else
            {
                message = "";
            }
            return message;
        }

        public PlanStatusUpdationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            StatusList = new List<string>
            {
                "Pending",
                "In Process",
                "Completed",
            };

            SelectedStatus = StatusList[0];

            handleDefaultImageVisibility();
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsBusy = true;

            try
            {

                if (parameters.ContainsKey("ActionPlanDetailsModel"))
                {
                    PlanDetails = parameters.GetValue<ActionPlanDetailsModel>("ActionPlanDetailsModel");
                    await FillTheImagesToList(PlanDetails);
                }

            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong", "Ok");
            }
            IsBusy = false;
        }
        private async Task<bool> FillTheImagesToList(ActionPlanDetailsModel ActionPlan)
        {


            List<GetObservationImagesModel> imagesList = await ApiService.Instance.GetObservationImage(ActionPlan.AoGembaObservationId);
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
            //else
            //{
            //    if (UploadedImagesList != null || UploadedImagesList.Count == 0)
            //    {
            //        UploadedImagesList.Add(new UploadImageModel { ImageName = "siteImage", imageSource = ImageSource.FromResource("siteImage") });
            //    }
            //}


            return true;

















            //if (!string.IsNullOrEmpty(ActionPlan.ResponsibilityImage))
            //{
            //    Byte[] bytes = Convert.FromBase64String(ActionPlan.ResponsibilityImage);
            //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image.png");
            //    if (File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
            //    }
            //    File.WriteAllBytes(fileName, bytes);
            //    var imgSource = ImageSource.FromFile(fileName);
            //    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = ActionPlan.ResponsibilityImage });
            //}
            //if (!string.IsNullOrEmpty(ActionPlan.ResponsibilityImage1))
            //{
            //    Byte[] bytes = Convert.FromBase64String(ActionPlan.ResponsibilityImage1);
            //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image2.png");
            //    if (File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
            //    }
            //    File.WriteAllBytes(fileName, bytes);
            //    var imgSource = ImageSource.FromFile(fileName);
            //    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = ActionPlan.ResponsibilityImage1 });

            //}
            //if (!string.IsNullOrEmpty(ActionPlan.ResponsibilityImage2))
            //{
            //    Byte[] bytes = Convert.FromBase64String(ActionPlan.ResponsibilityImage2);
            //    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "comp_image2.png");
            //    if (File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
            //    }
            //    File.WriteAllBytes(fileName, bytes);
            //    var imgSource = ImageSource.FromFile(fileName);
            //    UploadedImagesList.Add(new UploadImageModel { imageSource = imgSource, ImageName = "", ImageBase64String = ActionPlan.ResponsibilityImage2 });

            //}


            return true;
        }
    }
}
