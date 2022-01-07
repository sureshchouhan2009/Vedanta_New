using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Vedanta.Helpers
{

    public class Loading
    {
        #region Fields

        private static Loading _instance;

        #endregion

        #region Properties

        public virtual bool IsShowing { get; set; }

        public static Loading Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Loading();
                return _instance;
            }
          
        }

        #endregion

        #region Methods

        public virtual void ShowLoadingDialog(string loadingText = "Loading")
        {
            //If there is a currently showing dialog, then hide it first before showing the next dialog to avoid blocking.
            if (IsShowing)
            {
                HideLoadingDialog();
            }

            IsShowing = true;

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Device.iOS == Device.RuntimePlatform)
                {
                    UserDialogs.Instance.ShowLoading(loadingText, MaskType.Gradient);
                }
                else if (Device.Android == Device.RuntimePlatform)
                {
                    UserDialogs.Instance.ShowLoading(loadingText, MaskType.Black);
                }
            });
        }

        public virtual void HideLoadingDialog()
        {
            if (IsShowing)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.HideLoading();
                });

                IsShowing = false;
            }
        }



        #endregion
    }

}
