
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
using SPKElectre.SD;
using SPKElectre.Services;

namespace SPKElectre.Activities.ShowActivity
{
	[Activity (Label = "PerankinganActivity")]			
	public class PerankinganActivity : Activity
	{
		TextView txtAlternatif, txtHasilAkhir, txtKet;
		ListView lvPerankingan;
		List<perankingan> listRank = new List<perankingan>();
		ElectreService esr = new ElectreService();
		PerankinganListView lvRank;
		Button btnSimpan;

        ConfigService csr = new ConfigService();
        ApiService api = new ApiService();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.Perankingan_Layout);

			txtAlternatif = FindViewById<TextView>(Resource.Id.txtAlternatif);
			txtHasilAkhir = FindViewById<TextView>(Resource.Id.txtHasilAkhir);
			txtKet = FindViewById<TextView>(Resource.Id.txtKet);
			lvPerankingan = FindViewById<ListView>(Resource.Id.lvPerankingan);
			btnSimpan = FindViewById<Button>(Resource.Id.btnSimpan);

            listRank = esr.Perankingan();

            lvRank = new PerankinganListView(this, listRank);
            lvPerankingan.Adapter = lvRank;

            btnSimpan.Click += BtnSimpan_Click;

            csr.IdKeputusan();
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            riwayat rwt = new riwayat();
            dt_riwayat dtr = new dt_riwayat();

            try
            {
                int count = listRank.Count();

                string idKeputusan = StaticDetails.IdKeputusan;

                var subIdPenjualan = idKeputusan.Substring(12);

                int id = Convert.ToInt32(subIdPenjualan);

                
                if (count != 0)
                {
                    for(int a=0; a < count; a++)
                    {
                        dtr = new dt_riwayat()
                        {
                            alternatif = listRank[a].alternatif,
                            id_riwayat = idKeputusan,
                            hasil = Convert.ToInt16(listRank[a].Hasil),
                            keterangan = listRank[a].Keterangan,
                            
                        };

                        csr.SaveDetailRiwayat(dtr);
                    }

                    rwt = new riwayat()
                    {
                        Id = idKeputusan,
                        alternatif_tertinggi = listRank[0].alternatif,
                        jumlah_data = count
                    };

                    csr.SaveRiwayat(rwt);

                    config con = new config()
                    {
                        config_key = "keputusan",
                        config_value = id
                    };

                    csr.UpdateConfigValue(con);

                    Toast.MakeText(this, "Tambah Data Berhasil !!", ToastLength.Long).Show();

                    Intent intent = new Intent(this, typeof(MainActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);

                }
                else
                {
                    Toast.MakeText(this, "Data kosong, gagal melanjutkan !!", ToastLength.Long).Show();
                }
            }
            catch (Exception x)
            {
                Toast.MakeText(this, "Tambah Data gagal karena " + x.ToString(), ToastLength.Short).Show();
            }
        }
    }
}

