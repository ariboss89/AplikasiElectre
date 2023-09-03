
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
using SPKElectre.Models;
using SPKElectre.Services;

namespace SPKElectre.Activities.AddActivity
{
	[Activity (Label = "KriteriaAdd_Activity")]			
	public class KriteriaAdd_Activity : Activity
	{
        EditText edtNama, edtBobot;
        ImageView imgSave, imgBack;

        kriteria krt = new kriteria();
        KriteriaService ksr = new KriteriaService();

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.KriteriaAdd_Layout);

            edtNama = FindViewById<EditText>(Resource.Id.edtKriteria);
            edtBobot = FindViewById<EditText>(Resource.Id.edtBobot);
            imgBack = FindViewById<ImageView>(Resource.Id.imgArrow);
            imgSave = FindViewById<ImageView>(Resource.Id.imgSave);

            imgBack.Click += ImgBack_Click;
            imgSave.Click += ImgSave_Click;
        }

        private void ImgSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (edtNama.Text.Equals(""))
                {
                    Toast.MakeText(this, "Nama Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else if (edtBobot.Text.Equals("") || edtBobot.Text.Equals("0"))
                {
                    Toast.MakeText(this, "Bobot Preferensi Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else
                {
                    krt = new kriteria()
                    {
                        nama = edtNama.Text,
                        bobot = Convert.ToInt16(edtBobot.Text)
                    };

                    ksr.SaveKriteria(krt);

                    Toast.MakeText(this, "Tambah Data Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(KriteriaActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                }
            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Tambah Data gagal karena " + x.ToString(), ToastLength.Short).Show();
            }
        }

        private void ImgBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KriteriaActivity));
            StartActivity(intent);
        }
    }
}

