using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using Android.Widget;
using Newtonsoft.Json;
using SPKElectre.Models;

namespace SPKElectre.Services
{
	public class PenilaianService
	{
        ApiService api = new ApiService();
        List<penilaian> listPenilaian = new List<penilaian>();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        penilaian tba = new penilaian();

        public List<penilaian> ShowDataPenilaian()
        {
            listPenilaian = new List<penilaian>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllPenilaian()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listPenilaian = JsonConvert.DeserializeObject<List<penilaian>>(result);

            }
            catch (Exception ex)
            {
                Android.Widget.Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listPenilaian;
        }

        public async void SavePenilaian(penilaian atr)
        {
            try
            {
                tba = new penilaian();
                httpClient = new HttpClient();
                string url = api.SavePenilaian();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Penilaian Failed !", ToastLength.Short).Show();
            }
        }

        //public async void UpdateAlternatif(alternatif atr)
        //{
        //    try
        //    {
        //        tba = new alternatif();
        //        httpClient = new HttpClient();
        //        string url = api.UpdateAlternatif();
        //        var uri = new Uri(url);
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var json = JsonConvert.SerializeObject(atr);
        //        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //        response = await httpClient.PutAsync(uri, content);
        //    }
        //    catch (Exception)
        //    {
        //        Toast.MakeText(Application.Context, "Update Alternatif Failed !", ToastLength.Short).Show();
        //    }
        //}

        public async void DeletePenilaian(int Id)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.DeletePenilaian(Id);
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await myClient.DeleteAsync(uri);

                Toast.MakeText(Application.Context, "Data Alternatif Deleted", ToastLength.Long).Show();

            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Delete Alternatif Failed !", ToastLength.Short).Show();
            }
        }

    }
}

