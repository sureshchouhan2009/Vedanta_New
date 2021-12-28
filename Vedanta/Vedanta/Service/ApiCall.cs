using System.Collections.Generic;
using System.Threading.Tasks;


namespace Vedanta.Service
{
    public class ApiCall
    {
        private static ApiCall _instance;
        public static ApiCall Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiCall();
                return _instance;
            }
        }

        #region PostMethods

        public async Task<Response<object>> PostLoginApi(string address, object data)
        {
            Response<object> PostData = new Response<object>() { Data = data };
            return await WebMethods<Response<object>>.PostData(address, PostData);
        }

       

        //public async Task<Response<object>> PostFamilyDetails(string address, FamilyDetailsModel data)
        //{
        //    Response<FamilyDetailsModel> PostData = new Response<FamilyDetailsModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostNatureOfJobDetails(string address, NatureJobModel data)
        //{
        //    Response<NatureJobModel> PostData = new Response<NatureJobModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostFoodHabitDetails(string address, FoodHabitModel data)
        //{
        //    Response<FoodHabitModel> PostData = new Response<FoodHabitModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostHobbiesDetails(string address, List<HobbiesModel> data)
        //{
        //    Response<List<HobbiesModel>> PostData = new Response<List<HobbiesModel>>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostSportsDetails(string address, List<SportsModel> data)
        //{
        //    Response<List<SportsModel>> PostData = new Response<List<SportsModel>>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostHousingDetails(string address, HousingModel data)
        //{
        //    Response<HousingModel> PostData = new Response<HousingModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostOtherDetails(string address, OthersModel data)
        //{
        //    Response<OthersModel> PostData = new Response<OthersModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostForgotPassword(string address, ForgotPasswordModel data)
        //{
        //    Response<ForgotPasswordModel> PostData = new Response<ForgotPasswordModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostChangePassword(string address, ChangePasswordModel data)
        //{
        //    Response<ChangePasswordModel> PostData = new Response<ChangePasswordModel>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        //public async Task<Response<object>> PostSaveRecommend(string address, List<SaveRecommendModel> data)
        //{
        //    Response<List<SaveRecommendModel>> PostData = new Response<List<SaveRecommendModel>>() { Data = data };
        //    return await WebMethods<Response<object>>.PostData(address, PostData);
        //}

        #endregion

        #region GetMethods

        public async Task<Response<object>> GetData(string address)
        {
            return await WebMethods<Response<object>>.GetData(address);
        }

        #endregion
    }
}
