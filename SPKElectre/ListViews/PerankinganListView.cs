using System;
using Android.App;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using System.Collections.Generic;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class PerankinganListView : BaseAdapter<perankingan>
    {
        TextView txtAlternatif, txtHasilAkhir, txtKet;

        public PerankinganListView(Activity activity, int listViewRow)
        {

        }

        public List<perankingan> listPerankingan;
        private Context mContext;
        private Activity activity;

        public PerankinganListView(Context context, List<perankingan> items)
        {
            listPerankingan = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listPerankingan.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override perankingan this[int position]
        {
            get
            {
                return listPerankingan[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.PerankinganListView_Layout, null, false);
            }

            txtAlternatif = row.FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtHasilAkhir = row.FindViewById<TextView>(Resource.Id.txtHasilAkhir);
            txtKet = row.FindViewById<TextView>(Resource.Id.txtKet);

            txtAlternatif.Text = listPerankingan[position].alternatif;
            txtHasilAkhir.Text = listPerankingan[position].Hasil.ToString();
            txtKet.Text = listPerankingan [position].Keterangan.ToString();
            
            return row;
        }
    }
}

