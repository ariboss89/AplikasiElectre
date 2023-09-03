using System;
using Android.App;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using System.Collections.Generic;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class PenilaianListView : BaseAdapter<penilaian>
    {
        TextView txtId, txtAlternatif, txtKriteria, txtPilihan, txtNilai;

        public PenilaianListView(Activity activity, int listViewRow)
        {

        }

        public List<penilaian> listPenilaian;
        private Context mContext;
        private Activity activity;



        public PenilaianListView(Context context, List<penilaian> items)
        {
            listPenilaian = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listPenilaian.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override penilaian this[int position]
        {
            get
            {
                return listPenilaian[position];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.PenilaianListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtAlternatif = row.FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtKriteria = row.FindViewById<TextView>(Resource.Id.txtKriteria);
            txtPilihan = row.FindViewById<TextView>(Resource.Id.txtPilihan);
            txtNilai = row.FindViewById<TextView>(Resource.Id.txtNilai);

            txtId.Text = listPenilaian[position].Id.ToString();
            txtAlternatif.Text = listPenilaian[position].alternatif;
            txtKriteria.Text = listPenilaian[position].kriteria.ToString();
            txtPilihan.Text = listPenilaian[position].pilihan.ToString();
            txtNilai.Text = listPenilaian[position].nilai.ToString();

            return row;
        }
    }
}

