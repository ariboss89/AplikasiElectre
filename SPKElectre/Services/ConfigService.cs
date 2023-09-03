using System;
using Android.Widget;
using Newtonsoft.Json;
using SPKElectre.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using SPKElectre.SD;

namespace SPKElectre.Services
{
	public class ConfigService
	{
        ApiService api = new ApiService();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        config tbc = new config();
        riwayat tbr = new riwayat();
        dt_riwayat dtb = new dt_riwayat();
        List<dt_riwayat> listDetail = new List<dt_riwayat>();
        List<riwayat> listRiwayat = new List<riwayat>();

        public async void IdKeputusan()
        {
            string id = "";
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.IdKeputusan();
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                var json = JsonConvert.SerializeObject(tbc);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await myClient.PostAsync(uri, content);

                var mesg = response.Content.ReadAsStringAsync();

                var msge = mesg.Result.ToString();

                var idPenjualan = JsonConvert.DeserializeObject(msge);

                StaticDetails.IdKeputusan = idPenjualan.ToString();

            }
            catch (Exception x)
            {
                Toast.MakeText(Application.Context, x.ToString(), ToastLength.Short).Show();
            }
        }

        public async void UpdateConfigValue(config con)
        {
            try
            {
                httpClient = new HttpClient();
                string url = api.UpdateConfig();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(con);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Update Id Keputusan Failed !", ToastLength.Short).Show();
            }
        }

        public List<riwayat> ShowRiwayat()
        {
            listRiwayat = new List<riwayat>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllRiwayat()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listRiwayat = JsonConvert.DeserializeObject<List<riwayat>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listRiwayat;
        }

        public List<dt_riwayat> ShowDetailRiwayat(string idRiwayat)
        {
            listDetail = new List<dt_riwayat>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllDetailRiwayat(idRiwayat)).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listDetail = JsonConvert.DeserializeObject<List<dt_riwayat>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listDetail;
        }

        public async void SaveRiwayat(riwayat atr)
        {
            try
            {
                tbr = new riwayat();
                httpClient = new HttpClient();
                string url = api.SaveRiwayat();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(atr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Riwayat Failed !", ToastLength.Short).Show();
            }
        }

        public async void SaveDetailRiwayat(dt_riwayat dtr)
        {
            try
            {
                httpClient = new HttpClient();
                string url = api.SaveDetailRiwayat();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(dtr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Save Detail Riwayat Failed !", ToastLength.Short).Show();
            }
        }
    }
}

