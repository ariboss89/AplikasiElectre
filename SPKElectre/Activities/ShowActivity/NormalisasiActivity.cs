
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
using SPKElectre.ListViews;
using SPKElectre.Models;
using SPKElectre.Services;

namespace SPKElectre.Activities.ShowActivity
{
	[Activity (Label = "NormalisasiActivity")]			
	public class NormalisasiActivity : Activity
	{
		TextView txtId, txtAlternatif, txtPilihan, txtNilai;
		Button btnBobot;
		ElectreService esr = new ElectreService();
		List<normalisasi> listNormalisasi = new List<normalisasi>();
		NormalisasiListView normAdapter;
		ListView lvNormalisasi;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.Normalisasi_Layout);

			txtId = FindViewById<TextView>(Resource.Id.txtId);
			txtAlternatif = FindViewById<TextView>(Resource.Id.txtAlternatif);
			txtPilihan = FindViewById<TextView>(Resource.Id.txtPilihan);
			txtNilai = FindViewById<TextView>(Resource.Id.txtNilai);
			btnBobot = FindViewById<Button>(Resource.Id.btnBobot);
			lvNormalisasi = FindViewById<ListView>(Resource.Id.lvNormalisasi);

			listNormalisasi = esr.ProsesNormalisasi();

            normAdapter = new NormalisasiListView(this, listNormalisasi);
            lvNormalisasi.Adapter = normAdapter;

            btnBobot.Click += BtnBobot_Click;

        }

        private void BtnBobot_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PerankinganActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }
    }
}

