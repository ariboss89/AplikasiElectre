
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SPKElectre.Activities.AddActivity;
using SPKElectre.Activities.UpdateActivity;
using SPKElectre.ListViews;
using SPKElectre.Models;
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities.ShowActivity
{
    [Activity(Label = "RiwayatActivity")]
    public class RiwayatActivity : Activity
    {
        ListView lvRiwayat;
        ApiService api = new ApiService();
        ConfigService csr = new ConfigService();
        List<riwayat> listRiwayat = new List<riwayat>();
        List<dt_riwayat> listDetail = new List<dt_riwayat>();
        RiwayatListView riwayatAdapter;
        Button btnExit;
        Spinner spinRiwayat;

        List<string> listDataRiwayat = new List<string>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RiwayatLayout);

            lvRiwayat = FindViewById<ListView>(Resource.Id.lvRiwayat);
            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            spinRiwayat = FindViewById<Spinner>(Resource.Id.spinRiwayat);

            listRiwayat = csr.ShowRiwayat();

            if (listRiwayat.Count != 0)
            {

                foreach (var a in listRiwayat)
                {
                    listDataRiwayat.Add(a.Id);
                }

                ArrayAdapter<string> adapterKriteria = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listDataRiwayat);
                adapterKriteria.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinRiwayat.Adapter = adapterKriteria;


                spinRiwayat.ItemSelected += SpinRiwayat_ItemSelected;

            }
            else
            {
                Toast.MakeText(this, "Data Riwayat Tidak Tersedia .. !!", ToastLength.Long).Show();
            }

            btnExit.Click += BtnExit_Click;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void SpinRiwayat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            listDetail = csr.ShowDetailRiwayat(spinRiwayat.SelectedItem.ToString());

            riwayatAdapter = new RiwayatListView(this, listDetail);
            lvRiwayat.Adapter = riwayatAdapter;
        }

        public override void OnBackPressed()
        {

        }
    }
}
