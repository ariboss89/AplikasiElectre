using System;
using Android.App;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using System.Collections.Generic;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class NormalisasiListView : BaseAdapter<normalisasi>
    {
        TextView txtId, txtAlternatif, txtPilihan, txtNilai;

        public NormalisasiListView(Activity activity, int listViewRow)
        {

        }

        public List<normalisasi> listNormalisasi;
        private Context mContext;
        private Activity activity;



        public NormalisasiListView(Context context, List<normalisasi> items)
        {
            listNormalisasi = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listNormalisasi.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override normalisasi this[int position]
        {
            get
            {
                return listNormalisasi[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.NormalisasiListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtAlternatif = row.FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtPilihan = row.FindViewById<TextView>(Resource.Id.txtPilihan);
            txtNilai = row.FindViewById<TextView>(Resource.Id.txtNilai);

            int id = position + 1;

            txtId.Text = id.ToString();
            txtAlternatif.Text = listNormalisasi[position].alternatif;
            txtPilihan.Text = listNormalisasi[position].kriteria.ToString();
            txtNilai.Text = listNormalisasi[position].nilai_normalisasi.ToString();

            return row;
        }
    }
}

