
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
	[Activity (Label = "SubkriteriaActivity")]			
	public class SubkriteriaActivity : Activity
	{
        ListView lvSubkriteria;
        EditText edtSearch;
        SubkriteriaService skr = new SubkriteriaService();
        List<subkriteria> listSubkriteria = new List<subkriteria>();
        SubkriteriaListView sktAdapter;
        ImageView imgAdd, imgArrow;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.SubkriteriaLayout);

            // Create your application here
            lvSubkriteria = FindViewById<ListView>(Resource.Id.lvSubkriteria);
            imgAdd = FindViewById<ImageView>(Resource.Id.imgAdd);


            //edtSearch.TextChanged += EdtSearch_TextChanged;

            lvSubkriteria.ItemClick += LvSubkriteria_ItemClick; ;

            imgAdd.Click += ImgAdd_Click;

            listSubkriteria = new List<subkriteria>();
            listSubkriteria = skr.ShowDataSubkriteria();
            Tampil();

            imgArrow = FindViewById<ImageView>(Resource.Id.imgBack);
            imgArrow.Click += ImgArrow_Click;
        }

        private void LvSubkriteria_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var data = sktAdapter[e.Position];
            StaticDetails_Subkriteria.Id = data.Id;
            StaticDetails_Subkriteria.id_kriteria = data.id_kriteria;
            StaticDetails_Subkriteria.nama_kriteria = data.nama_kriteria;
            StaticDetails_Subkriteria.pilihan = data.pilihan;
            StaticDetails_Subkriteria.nilai = data.nilai;

            Intent intent = new Intent(this, typeof(AlternatifUpdate_Activity));
            StartActivity(intent);
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void ImgAdd_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SubkriteriaAdd_Activity));
            StartActivity(intent);
        }

        private void EdtSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //var data = listAlternatif.Where(x => x.nama.Contains(edtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            //altAdapter = new AlternatifAdapter(this, data);
            //lvAlternatif.Adapter = altAdapter;
        }

        void Tampil()
        {
            sktAdapter = new SubkriteriaListView(this, listSubkriteria);
            lvSubkriteria.Adapter = sktAdapter;
        }

        public override void OnBackPressed()
        {

        }
    }
}

