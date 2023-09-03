using System;
using Android.App;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using System.Collections.Generic;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class RiwayatListView : BaseAdapter<dt_riwayat>
    {
        TextView txtId, txtAlternatif, txtHasilAkhir, txtKet;

        public RiwayatListView(Activity activity, int listViewRow)
        {

        }

        public List<dt_riwayat> listDtRiwayat;
        private Context mContext;
        private Activity activity;

        public RiwayatListView(Context context, List<dt_riwayat> items)
        {
            listDtRiwayat = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listDtRiwayat.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override dt_riwayat this[int position]
        {
            get
            {
                return listDtRiwayat[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.RiwayatListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtAlternatif = row.FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtHasilAkhir = row.FindViewById<TextView>(Resource.Id.txtHasil);
            txtKet = row.FindViewById<TextView>(Resource.Id.txtKet);

            txtId.Text = listDtRiwayat[position].id.ToString();
            txtAlternatif.Text = listDtRiwayat[position].alternatif;
            txtHasilAkhir.Text = listDtRiwayat[position].hasil.ToString();
            txtKet.Text = listDtRiwayat[position].keterangan.ToString();

            return row;
        }
    }
}

