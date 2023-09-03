
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
	[Activity (Label = "PembobotanActivity")]			
	public class PembobotanActivity : Activity
	{
        TextView txtId, txtAlternatif, txtPilihan, txtNilai;
        Button btnRank;
        ElectreService esr = new ElectreService();
        List<pembobotan> listPembobotan = new List<pembobotan>();
        PembobotanMatriksListView bobotAdapter;
		ListView lvPembobotan;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.Pembobotan_Layout);

            txtId = FindViewById<TextView>(Resource.Id.txtId);
            txtAlternatif = FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtPilihan = FindViewById<TextView>(Resource.Id.txtPilihan);
            txtNilai = FindViewById<TextView>(Resource.Id.txtPembobotan);
            btnRank = FindViewById<Button>(Resource.Id.btnRank);
            lvPembobotan = FindViewById<ListView>(Resource.Id.lvPembobotan);

            listPembobotan = esr.ProsesPembobotan();

            bobotAdapter = new PembobotanMatriksListView(this, listPembobotan);
            lvPembobotan.Adapter = bobotAdapter;

            btnRank.Click += BtnRank_Click;
        }

        private void BtnRank_Click(object sender, EventArgs e)
        {
            esr.Perankingan();
        }
    }
}

