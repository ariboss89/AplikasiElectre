
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using SPKElectre.Helper;

namespace SPKElectre.Activities.ShowActivity
{
	[Activity (Label = "Spk Electre", MainLauncher =true)]			
	public class MainActivity : Activity
	{
		FloatingActionButton fabAlternatif, fabKriteria, fabSubkriteria, fabPenilaian, fabKeputusan, fabRiwayat, fabLogout;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView(Resource.Layout.Main_Layout);

			fabAlternatif = FindViewById<FloatingActionButton>(Resource.Id.fabAlternatif);
			fabKriteria = FindViewById<FloatingActionButton>(Resource.Id.fabKriteria);
            fabSubkriteria = FindViewById<FloatingActionButton>(Resource.Id.fabSubkriteria);
			fabPenilaian = FindViewById<FloatingActionButton>(Resource.Id.fabScoring);
			fabKeputusan = FindViewById<FloatingActionButton>(Resource.Id.fabDecision);
			fabRiwayat = FindViewById<FloatingActionButton>(Resource.Id.fabHistory);
			fabLogout = FindViewById<FloatingActionButton>(Resource.Id.fabLogout);

            fabAlternatif.Click += FabAlternatif_Click;
            fabKriteria.Click += FabKriteria_Click;
            fabSubkriteria.Click += FabSubkriteria_Click;
            fabPenilaian.Click += FabPenilaian_Click;
            fabKeputusan.Click += FabKeputusan_Click;
            fabRiwayat.Click += FabRiwayat_Click;
            fabLogout.Click += FabLogout_Click;

            Check();
        }

        void Check()
        {
            AppPreferences app = new AppPreferences(Application.Context);

            string roles = app.getAccessKey("role");

            if(roles== "")
            {
                Toast.MakeText(this, "Session anda berakhir, silahkan login kembali ...", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
            else if (roles.Equals("Admin"))
            {
                Toast.MakeText(this, "Anda Tidak Memiliki Akses", ToastLength.Long).Show();
            }
        }

        public override void OnBackPressed()
        {
          
        }

        private void FabLogout_Click(object sender, EventArgs e)
        {
            AppPreferences app = new AppPreferences(Application.Context);
            app.deleteAccessKey();
            Intent intent = new Intent(this, typeof(LoginActivity));
            intent.PutExtra("Logout", "Logout");
            StartActivity(intent);
        }

        private void FabRiwayat_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RiwayatActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void FabSubkriteria_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SubkriteriaActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void FabKeputusan_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KeputusanActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void FabPenilaian_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PenilaianActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void FabKriteria_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KriteriaActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void FabAlternatif_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AlternatifActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }
    }
}

