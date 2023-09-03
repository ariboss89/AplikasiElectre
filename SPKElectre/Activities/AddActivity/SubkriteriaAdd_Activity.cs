
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SPKElectre.Activities.ShowActivity;
using SPKElectre.Models;
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities.AddActivity
{
	[Activity (Label = "SubkriteriaAdd_Activity", MainLauncher = false)]			
	public class SubkriteriaAdd_Activity : Activity
	{
        EditText edtSubkriteria, edtNilai;
        Spinner spinKategori;
        SubkriteriaService sks = new SubkriteriaService();
        subkriteria sbk = new subkriteria();
        List<kriteria> listKriteria = new List<kriteria>();
        KriteriaService ksr = new KriteriaService();
        SubkriteriaService skr = new SubkriteriaService();
        List<string> listDataKriteria = new List<string>();
        ImageView imgSave, imgArrow;
        ElectreService esr = new ElectreService();

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.SubkriteriaAdd_Layout);

			edtSubkriteria = FindViewById<EditText>(Resource.Id.edtSubkriteria);
			edtNilai = FindViewById<EditText>(Resource.Id.edtNilai);
			spinKategori = FindViewById<Spinner>(Resource.Id.spinKriteria);
            imgSave = FindViewById<ImageView>(Resource.Id.imgSave);
            imgArrow = FindViewById<ImageView>(Resource.Id.imgArrow);

            listKriteria = ksr.ShowDataKriteria();

            foreach(var  a in listKriteria)
            {
                listDataKriteria.Add(a.nama);
            }

            ArrayAdapter<string> adapterKriteria = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listDataKriteria);
            adapterKriteria.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinKategori.Adapter = adapterKriteria;

            spinKategori.ItemSelected += SpinKategori_ItemSelected;
            imgSave.Click += ImgSave_Click;
            imgArrow.Click += ImgArrow_Click;
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SubkriteriaActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void ImgSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (edtSubkriteria.Text.Equals(""))
                {
                    Toast.MakeText(this, "Nama Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else if (edtNilai.Text.Equals("") || edtNilai.Text.Equals("0"))
                {
                    Toast.MakeText(this, "Nilai Subkriteria Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else
                {
                    sbk = new subkriteria()
                    {
                        id_kriteria = StaticDetails_Subkriteria.id_kriteria,
                        nama_kriteria = spinKategori.SelectedItem.ToString(),
                        pilihan = edtSubkriteria.Text,
                        nilai = Convert.ToInt32(edtNilai.Text)
                    };

                    skr.SaveSubKriteria(sbk);

                    Toast.MakeText(this, "Tambah Data Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(SubkriteriaActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                }
            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Tambah Data gagal karena " + x.ToString(), ToastLength.Short).Show();
            }
        }

        private void SpinKategori_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var dataId = listKriteria.Where(x => x.nama == spinKategori.SelectedItem.ToString()).ToList();

             StaticDetails_Subkriteria.id_kriteria = dataId[0].Id;
        }
    }
}

