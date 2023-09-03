using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Android.App;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SPKElectre.Models;

namespace SPKElectre.Services
{
    public class AlternatifService
    {
        ApiService api = new ApiService();
        List<alternatif> listAlternatif = new List<alternatif>();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        alternatif tba = new alternatif();

        public List<alternatif> ShowDataAlternatif()
        {
            listAlternatif = new List<alternatif>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllAlternatif()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                //var jObj = (JObject)JsonConvert.DeserializeObject(result, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None });

                //var _dataResponse = JToken.Parse(JsonConvert.SerializeObject(result));

                //try
                //{
                //    var _dataResponseProductID = _dataResponse["nama"];
                //    var _dataResponsePrice = _dataResponse["alamat"];
                //}catch(Exception e)
                //{
                //    _dataResponse.ToString();
                //}

                listAlternatif = JsonConvert.DeserializeObject<List<alternatif>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listAlternatif;
        }

        public async void SaveAlternatif(alternatif atr)
        {
            try
            {
                tba = new alternatif();
                httpClient = new HttpClient();
                string url = api.SaveAlternatif();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Alternatif Failed !", ToastLength.Short).Show();
            }
        }

        public async void UpdateAlternatif(alternatif atr)
        {
            try
            {
                tba = new alternatif();
                httpClient = new HttpClient();
                string url = api.UpdateAlternatif();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PutAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Update Alternatif Failed !", ToastLength.Short).Show();
            }
        }

        public async void DeleteAlternatif(int Id)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.DeleteAlternatif(Id);
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

