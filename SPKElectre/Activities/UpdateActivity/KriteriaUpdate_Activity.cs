
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
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities.UpdateActivity
{
	[Activity (Label = "KriteriaUpdate_Activity")]			
	public class KriteriaUpdate_Activity : Activity
	{
        EditText edtId, edtNama, edtBobot;
        Button btnDelete;
        ImageView imgUpdate, imgBack;

        kriteria krt = new kriteria();
        KriteriaService ksr = new KriteriaService();

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.KriteriaUpdate_Layout);

            edtId = FindViewById<EditText>(Resource.Id.edtId);
            edtNama = FindViewById<EditText>(Resource.Id.edtNama);
            edtBobot = FindViewById<EditText>(Resource.Id.edtBobot);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            imgBack = FindViewById<ImageView>(Resource.Id.imgArrow);
            imgUpdate = FindViewById<ImageView>(Resource.Id.imgUpdate);

            imgBack.Click += ImgBack_Click;
            imgUpdate.Click += ImgUpdate_Click;
            btnDelete.Click += BtnDelete_Click;

            edtId.Text = StaticDetails_Kriteria.Id.ToString();
            edtNama.Text = StaticDetails_Kriteria.nama;
            edtBobot.Text = StaticDetails_Kriteria.bobot.ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ksr.DeleteKriteria(Convert.ToInt16(edtId.Text));

                Toast.MakeText(this, "Data Kriteria Berhasil di Hapus !!", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(AlternatifActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                StartActivity(intent);

            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Data Kriteria Gagal di Hapus !!" + x.ToString(), ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(AlternatifActivity));
                StartActivity(intent);
            }
        }

        private void ImgUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (edtNama.Text.Equals(""))
                {
                    Toast.MakeText(this, "Nama Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else if (edtBobot.Text.Equals("")|| edtBobot.Text.Equals("0"))
                {
                    Toast.MakeText(this, "Bobot Tidak Boleh Kosong !", ToastLength.Short).Show();
                }
                else
                {
                    krt = new kriteria()
                    {
                        Id = Convert.ToInt16(edtId.Text),
                        nama = edtNama.Text,
                        bobot = Convert.ToInt16(edtBobot.Text)
                    };

                    ksr.UpdateKriteria(krt);

                    Toast.MakeText(this, "Update Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(KriteriaActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                }


            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Update gagal karena " + x.ToString(), ToastLength.Short).Show();
            }
        }

        private void ImgBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AlternatifActivity));
            StartActivity(intent);
        }
    }
}

