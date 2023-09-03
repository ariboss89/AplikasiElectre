using System;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPKElectre.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;

namespace SPKElectre.Services
{
	public class KriteriaService
	{
        ApiService api = new ApiService();
        List<kriteria> listKriteria = new List<kriteria>();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        kriteria tba = new kriteria();

        public List<kriteria> ShowDataKriteria()
        {
            listKriteria = new List<kriteria>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllKriteria()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listKriteria = JsonConvert.DeserializeObject<List<kriteria>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listKriteria;
        }

        public async void SaveKriteria(kriteria atr)
        {
            try
            {
                tba = new kriteria();
                httpClient = new HttpClient();
                string url = api.SaveKriteria();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Kriteria Failed !", ToastLength.Short).Show();
            }
        }

        public async void UpdateKriteria(kriteria atr)
        {
            try
            {
                tba = new kriteria();
                httpClient = new HttpClient();
                string url = api.UpdateKriteria();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PutAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Update Kriteria Failed !", ToastLength.Short).Show();
            }
        }

        public async void DeleteKriteria(int Id)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.DeleteKriteria(Id);
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await myClient.DeleteAsync(uri);

                Toast.MakeText(Application.Context, "Data Kriteria Deleted", ToastLength.Long).Show();

            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Delete Kriteria Failed !", ToastLength.Short).Show();
            }
        }
    }
}

