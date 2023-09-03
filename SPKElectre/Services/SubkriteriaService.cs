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
	public class SubkriteriaService
	{
        ApiService api = new ApiService();
        List<subkriteria> listSubkriteria = new List<subkriteria>();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        subkriteria tba = new subkriteria();

        public List<subkriteria> ShowDataSubkriteria()
        {
            listSubkriteria = new List<subkriteria>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllSubkriteria()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listSubkriteria = JsonConvert.DeserializeObject<List<subkriteria>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listSubkriteria;
        }

        public async void SaveSubKriteria(subkriteria atr)
        {
            try
            {
                tba = new subkriteria();
                httpClient = new HttpClient();
                string url = api.SaveSubkriteria();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Subkriteria Failed !", ToastLength.Short).Show();
            }
        }

        public async void UpdateSubkriteria(subkriteria atr)
        {
            try
            {
                tba = new subkriteria();
                httpClient = new HttpClient();
                string url = api.UpdateSubkriteria();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PutAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Update Subkriteria Failed !", ToastLength.Short).Show();
            }
        }

        public async void DeleteSubkriteria(int Id)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.DeleteSubkriteria(Id);
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await myClient.DeleteAsync(uri);

                Toast.MakeText(Application.Context, "Data Subkriteria Deleted", ToastLength.Long).Show();

            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Delete Subkriteria Failed !", ToastLength.Short).Show();
            }
        }
    }
}

