using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using SPKElectre.Models;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class SubkriteriaListView : BaseAdapter<subkriteria>
    {
        TextView txtIdSubkriteria, txtIdKriteria, txtNama, txtPilihan, txtBobot;

        public SubkriteriaListView(Activity activity, int listViewRow)
        {

        }

        public List<subkriteria> listSubkriteria;
        private Context mContext;
        private Activity activity;



        public SubkriteriaListView(Context context, List<subkriteria> items)
        {
            listSubkriteria = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listSubkriteria.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override subkriteria this[int position]
        {
            get
            {
                return listSubkriteria[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.SubkriteriaListView_Layout, null, false);
            }

            txtIdSubkriteria = row.FindViewById<TextView>(Resource.Id.txtId);
            txtIdKriteria = row.FindViewById<TextView>(Resource.Id.txtIdKriteria);
            txtNama = row.FindViewById<TextView>(Resource.Id.txtNama);
            txtPilihan = row.FindViewById<TextView>(Resource.Id.txtPilihan);
            txtBobot = row.FindViewById<TextView>(Resource.Id.txtNilai);

            txtIdSubkriteria.Text = listSubkriteria[position].Id.ToString();
            txtIdKriteria.Text = listSubkriteria [position].id_kriteria.ToString();
            txtNama.Text = listSubkriteria[position].nama_kriteria;
            txtPilihan.Text = listSubkriteria[position].pilihan;
            txtBobot.Text = listSubkriteria[position].nilai.ToString();

            return row;
        }
    }
}

