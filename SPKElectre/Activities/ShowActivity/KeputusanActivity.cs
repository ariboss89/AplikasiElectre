
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
    [Activity(Label = "KeputusanActivity")]
    public class KeputusanActivity : Activity
    {
        ListView lvPenilaian;
        PenilaianService psr = new PenilaianService();
        List<penilaian> listPenilaian = new List<penilaian>();
        PenilaianListView pnlAdapter;
        ImageView imgAdd, imgArrow;
        Button btnKeputusan;
        TextView txtMenu;
        KriteriaService ksr = new KriteriaService();
        AlternatifService asr = new AlternatifService();

        List<kriteria> listKriteria = new List<kriteria>();
        List<alternatif> listAlt = new List<alternatif>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Penilaian_Layout);

            lvPenilaian = FindViewById<ListView>(Resource.Id.lvPenilaian);
            imgAdd = FindViewById<ImageView>(Resource.Id.imgAdd);
            btnKeputusan = FindViewById<Button>(Resource.Id.btnKeputusan);
            //edtSearch.TextChanged += EdtSearch_TextChanged;

            txtMenu = FindViewById<TextView>(Resource.Id.txtMenu);
            lvPenilaian.ItemClick += LvPenilaian_ItemClick;

            txtMenu.Text = "MENU KEPUTUSAN";

            listPenilaian = new List<penilaian>();
            listPenilaian = psr.ShowDataPenilaian();

            pnlAdapter = new PenilaianListView(this, listPenilaian);
            lvPenilaian.Adapter = pnlAdapter;

            imgArrow = FindViewById<ImageView>(Resource.Id.imgBack);
            imgArrow.Click += ImgArrow_Click;

            imgAdd.Visibility = ViewStates.Gone;

            btnKeputusan.Click += BtnKeputusan_Click;

            listKriteria = ksr.ShowDataKriteria();
            listAlt = asr.ShowDataAlternatif();
        }

        private void BtnKeputusan_Click(object sender, EventArgs e)
        {
            int multiply = listKriteria.Count * listAlt.Count;

            if (listPenilaian.Count < multiply)
            {
                Toast.MakeText(this, "Data Penilaian Kurang !!", ToastLength.Short).Show();
            }
            else
            {
                Intent intent = new Intent(this, typeof(NormalisasiActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                StartActivity(intent);
            }
        }

        private void LvPenilaian_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        private void LvSubkriteria_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var data = pnlAdapter[e.Position];
            StaticDetails_Penilaian.Id = data.Id;
            StaticDetails_Penilaian.alternatif = data.alternatif;
            StaticDetails_Penilaian.id_kriteria = data.id_kriteria;
            StaticDetails_Penilaian.pilihan = data.pilihan;
            StaticDetails_Penilaian.nilai = data.nilai;

            Intent intent = new Intent(this, typeof(KriteriaUpdate_Activity));
            StartActivity(intent);
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {

        }
    }
}

