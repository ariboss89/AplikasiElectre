
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
	[Activity (Label = "AlternatifUpdate_Activity")]			
	public class AlternatifUpdate_Activity : Activity
	{
		EditText edtId,edtNama, edtAlamat;
		Button btnDelete;
		ImageView imgUpdate, imgBack;

		alternatif alt = new alternatif();
		AlternatifService asr = new AlternatifService();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.AlternatifUpdate_Layout);

			edtId = FindViewById<EditText>(Resource.Id.edtId);
			edtNama = FindViewById<EditText>(Resource.Id.edtNama);
			edtAlamat = FindViewById<EditText>(Resource.Id.edtAlamat);
			btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
			imgBack = FindViewById<ImageView>(Resource.Id.imgArrow);
			imgUpdate = FindViewById<ImageView>(Resource.Id.imgUpdate);

            imgBack.Click += ImgBack_Click;
            imgUpdate.Click += ImgUpdate_Click;
            btnDelete.Click += BtnDelete_Click;

            edtId.Text = StaticDetails_Alternatif.Id.ToString();
            edtNama.Text = StaticDetails_Alternatif.nama;
            edtAlamat.Text = StaticDetails_Alternatif.alamat;
		}

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                asr.DeleteAlternatif(Convert.ToInt16(edtId.Text));

                Toast.MakeText(this, "Data Alternatif Berhasil di Hapus !!", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(AlternatifActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                StartActivity(intent);

            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Data Alternatif Gagal di Hapus !!" + x.ToString(), ToastLength.Long).Show();

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
                else if (edtAlamat.Text.Equals(""))
                {
                    Toast.MakeText(this, "Alamat Tidak Boleh Kosong !", ToastLength.Short).Show();
				}
				else
				{
					alt = new alternatif()
					{
						Id = Convert.ToInt16(edtId.Text),
						nama = edtNama.Text,
						alamat = edtAlamat.Text
					};

                    asr.UpdateAlternatif(alt);

                    Toast.MakeText(this, "Update Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(AlternatifActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                }


            }
            catch(Exception x)
			{
				Toast.MakeText(this, "Update gagal karena " +x.ToString(), ToastLength.Short).Show();
			}
        }

        private void ImgBack_Click(object sender, EventArgs e)
        {
			Intent intent = new Intent(this, typeof(AlternatifActivity));
            StartActivity(intent);
        }


    }
}

