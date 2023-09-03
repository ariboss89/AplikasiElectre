
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
	[Activity (Label = "AlternatifActivity", MainLauncher =false)]			
	public class AlternatifActivity : Activity
	{
        ListView lvAlternatif;
        EditText edtSearch;
        AlternatifService asr = new AlternatifService();
        List<alternatif> listAlternatif = new List<alternatif>();
        AlternatifListView altAdapter;
        ImageView imgAdd, imgArrow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AlternatifLayout);

            // Create your application here
            lvAlternatif = FindViewById<ListView>(Resource.Id.lvAlternatif);
            imgAdd = FindViewById<ImageView>(Resource.Id.imgAdd);


            //edtSearch.TextChanged += EdtSearch_TextChanged;

            lvAlternatif.ItemClick += LvAlternatif_ItemClick;

            imgAdd.Click += ImgAdd_Click;

            listAlternatif = new List<alternatif>();
            listAlternatif = asr.ShowDataAlternatif();

            altAdapter = new AlternatifListView(this, listAlternatif);
            lvAlternatif.Adapter = altAdapter;

            imgArrow = FindViewById<ImageView>(Resource.Id.imgBack);
            imgArrow.Click += ImgArrow_Click;
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void ImgAdd_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AlternatifAdd_Activity));
            StartActivity(intent);
        }

        private void LvAlternatif_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var data = altAdapter[e.Position];
            StaticDetails_Alternatif.Id = data.Id;
            StaticDetails_Alternatif.nama = data.nama;
            StaticDetails_Alternatif.alamat = data.alamat;

            Intent intent = new Intent(this, typeof(AlternatifUpdate_Activity));
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

