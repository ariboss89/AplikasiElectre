using System;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class AlternatifListView : BaseAdapter<alternatif>
    {
        TextView txtId, txtNama, txtAlamat;

        public AlternatifListView(Activity activity, int listViewRow)
        {

        }

        public List<alternatif> listAlternatif;
        private Context mContext;
        private Activity activity;



        public AlternatifListView(Context context, List<alternatif> items)
        {
            listAlternatif = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listAlternatif.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override alternatif this[int position]
        {
            get
            {
                return listAlternatif[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.AlternatifListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtNama = row.FindViewById<TextView>(Resource.Id.txtNama);
            txtAlamat = row.FindViewById<TextView>(Resource.Id.txtAlamat);
            
            txtId.Text = listAlternatif[position].Id.ToString();
            txtNama.Text = listAlternatif[position].nama;
            txtAlamat.Text = listAlternatif[position].alamat;
            
            return row;
        }
    }
}

