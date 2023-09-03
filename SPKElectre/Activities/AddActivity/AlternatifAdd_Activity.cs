
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
	[Activity (Label = "AlternatifAdd_Activity")]			
	public class AlternatifAdd_Activity : Activity
	{
        EditText edtNama, edtAlamat;
        ListView lvAlternatif;
        ImageView imgSave, imgBack;

        alternatif alt = new alternatif();
        AlternatifService asr = new AlternatifService();

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.AlternatifAdd_Layout);

            edtNama = FindViewById<EditText>(Resource.Id.edtNama);
            edtAlamat = FindViewById<EditText>(Resource.Id.edtAlamat);
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
                else if (edtAlamat.Text.Equals(""))
                {
                    Toast.MakeText(this, "Alamat Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else
                {
                    alt = new alternatif()
                    {
                        nama = edtNama.Text,
                        alamat = edtAlamat.Text
                    };

                    asr.SaveAlternatif(alt);

                    Toast.MakeText(this, "Tambah Data Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(AlternatifActivity));
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
            Intent intent = new Intent(this, typeof(AlternatifActivity));
            StartActivity(intent);
        }
    }
}

