
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
using SPKElectre.Activities.ShowActivity;
using SPKElectre.Activities.UpdateActivity;
using SPKElectre.ListViews;
using SPKElectre.Models;
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities
{
	[Activity (Label = "KriteriaActivity")]			
	public class KriteriaActivity : Activity
	{

        ListView lvKriteria;
        KriteriaService ksr = new KriteriaService();
        List<kriteria> listKriteria = new List<kriteria>();
        KriteriaListView krtAdapter;
        ImageView imgAdd, imgArrow;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.KriteriaLayout);
            lvKriteria = FindViewById<ListView>(Resource.Id.lvKriteria);
            imgAdd = FindViewById<ImageView>(Resource.Id.imgAdd);

            //edtSearch.TextChanged += EdtSearch_TextChanged;

            lvKriteria.ItemClick += LvSubkriteria_ItemClick; ;

            imgAdd.Click += ImgAdd_Click;

            listKriteria = new List<kriteria>();
            listKriteria = ksr.ShowDataKriteria();
            
            krtAdapter = new KriteriaListView(this, listKriteria);
            lvKriteria.Adapter = krtAdapter;

            imgArrow = FindViewById<ImageView>(Resource.Id.imgBack);
            imgArrow.Click += ImgArrow_Click;
        }

        private void LvSubkriteria_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var data = krtAdapter[e.Position];
            StaticDetails_Kriteria.Id = data.Id;
            StaticDetails_Kriteria.nama = data.nama;
            StaticDetails_Kriteria.bobot = data.bobot;

            Intent intent = new Intent(this, typeof(KriteriaUpdate_Activity));
            StartActivity(intent);
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void ImgAdd_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KriteriaAdd_Activity));
            StartActivity(intent);
        }

        private void EdtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //var data = listAlternatif.Where(x => x.nama.Contains(edtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            //altAdapter = new AlternatifAdapter(this, data);
            //lvAlternatif.Adapter = altAdapter;
        }

        public override void OnBackPressed()
        {

        }
    }
}

