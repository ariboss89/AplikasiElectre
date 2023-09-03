
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SPKElectre.Activities.ShowActivity;
using SPKElectre.Models;
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities.AddActivity
{
	[Activity (Label = "PenilaianAdd_Activity")]			
	public class PenilaianAdd_Activity : Activity
	{
        PenilaianService psr = new PenilaianService();
        ApiService asr = new ApiService();
        penilaian pnl = new penilaian();

        List<alternatif> listAlternatif = new List<alternatif>();
        List<kriteria> listKriteria = new List<kriteria>();
        List<subkriteria> listSubkriteria = new List<subkriteria>();

        AlternatifService alr = new AlternatifService();
        SubkriteriaService skr = new SubkriteriaService();
        KriteriaService ksr = new KriteriaService();

        TextView txtNilai;
        Spinner spinKriteria, spinSubkriteria, spinAlternatif;

        List<string> listDataKriteria = new List<string>();
        List<string> listDataSubkriteria = new List<string>();
        List<string> listDataAlternatif = new List<string>();

        ImageView imgBack, imgSave;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.PenilaianAdd_Layout);

            txtNilai = FindViewById<TextView>(Resource.Id.txtNilai);
            spinKriteria = FindViewById<Spinner>(Resource.Id.spinKriteria);
            spinSubkriteria = FindViewById<Spinner>(Resource.Id.spinSubkriteria);
            spinAlternatif = FindViewById<Spinner>(Resource.Id.spinAlternatif);
            imgBack = FindViewById<ImageView>(Resource.Id.imgArrow);
            imgSave = FindViewById<ImageView>(Resource.Id.imgSave);

            listAlternatif = alr.ShowDataAlternatif();
            listKriteria = ksr.ShowDataKriteria();
            listSubkriteria = skr.ShowDataSubkriteria();

            foreach (var a in listKriteria)
            {
                listDataKriteria.Add(a.nama);
            }

            ArrayAdapter<string> adapterKriteria = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listDataKriteria);
            adapterKriteria.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinKriteria.Adapter = adapterKriteria;

            foreach (var a in listAlternatif)
            {
                listDataAlternatif.Add(a.nama);
            }

            ArrayAdapter<string> adapterAlt = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listDataAlternatif);
            adapterAlt.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinAlternatif.Adapter = adapterAlt;


            spinKriteria.ItemSelected += SpinKriteria_ItemSelected;

            spinSubkriteria.ItemSelected += SpinSubkriteria_ItemSelected;

            imgBack.Click += ImgBack_Click;

            imgSave.Click += ImgSave_Click;

        }

        private void ImgSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData(spinAlternatif.SelectedItem.ToString(), spinKriteria.SelectedItem.ToString());

            }catch(Exception x)
            {
                x.ToString();
            }
        }

        private void ImgBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PenilaianActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void SpinSubkriteria_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                var dataId = listSubkriteria.Where(x => x.nama_kriteria == spinKriteria.SelectedItem.ToString()).ToList();

                var data = dataId.Where(x => x.pilihan == spinSubkriteria.SelectedItem.ToString()).ToList();

                txtNilai.Text = data[0].nilai.ToString();

                //    StaticDetails_Penilaian.pilihan = spinSubkriteria.SelectedItem.ToString();

            }catch(Exception x)
            {
                x.ToString();
            }
        }

        private void SpinKriteria_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                var dataId = listSubkriteria.Where(x => x.nama_kriteria == spinKriteria.SelectedItem.ToString()).ToList();

                listDataSubkriteria = new List<string>();

                foreach (var a in dataId)
                {
                    listDataSubkriteria.Add(a.pilihan);
                }

                ArrayAdapter<string> adapterSubKriteria = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listDataSubkriteria);
                adapterSubKriteria.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinSubkriteria.Adapter = adapterSubKriteria;

            }catch(Exception x)
            {
                x.ToString();
            }
        }

        public async void CheckData(string alternatif, string kriteria)
        {
            string message = "";
            try
            {
                HttpClient myClient = new HttpClient();
                string url = asr.CheckData(alternatif, kriteria);
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;

                var json = JsonConvert.SerializeObject(pnl);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await myClient.PostAsync(uri, content);

                var mesg = response.Content.ReadAsStringAsync();

                var msge = mesg.Result.ToString();

                if (msge.Contains("Data Exist"))
                {
                    message = "Data Exist";

                    Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
                }

                if (message != ("Data Exist"))
                {
                    message = "";

                    var test = listKriteria.Where(x => x.nama == spinKriteria.SelectedItem.ToString()).ToList();
                    int idKriteria = test[0].Id;


                    try
                    {
                        pnl = new penilaian()
                        {
                            alternatif = spinAlternatif.SelectedItem.ToString(),
                            id_kriteria = idKriteria,
                            kriteria = spinKriteria.SelectedItem.ToString(),
                            pilihan = spinSubkriteria.SelectedItem.ToString(),
                            nilai = Convert.ToInt16(txtNilai.Text)
                            
                        };

                        psr.SavePenilaian(pnl);

                        Toast.MakeText(this, "Data Penilaian Berhasil diTambahkan !!", ToastLength.Long).Show();

                        Intent intent = new Intent(this, typeof(PenilaianActivity));
                        intent.SetFlags(ActivityFlags.NewTask);
                        StartActivity(intent);

                    }
                    catch (Exception x)
                    {
                        Toast.MakeText(Application.Context, x.ToString(), ToastLength.Short).Show();
                    }

                }
                else
                {
                    Toast.MakeText(Application.Context, "Data Exist, Please Change", ToastLength.Short).Show();
                }


            }
            catch (Exception x)
            {
                Toast.MakeText(Application.Context, x.ToString(), ToastLength.Short).Show();
            }
        }
    }
}

