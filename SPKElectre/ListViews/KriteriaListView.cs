using System;
using Android.App;
using Android.Views;
using System.Collections.Generic;
using Android.Widget;
using SPKElectre.Models;
using Android.Content;

namespace SPKElectre.ListViews
{
	public class KriteriaListView : BaseAdapter<kriteria>
	{
        TextView txtId, txtNama, txtBobot;

        public KriteriaListView(Activity activity, int listViewRow)
        {

        }

        public List<kriteria> listKriteria;
        private Context mContext;
        private Activity activity;



        public KriteriaListView(Context context, List<kriteria> items)
        {
            listKriteria = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listKriteria.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override kriteria this[int position]
        {
            get
            {
                return listKriteria[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.KriteriaListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtNama = row.FindViewById<TextView>(Resource.Id.txtNama);
            txtBobot = row.FindViewById<TextView>(Resource.Id.txtBobot);

            txtId.Text = listKriteria[position].Id.ToString();
            txtNama.Text = listKriteria[position].nama;
            txtBobot.Text = listKriteria[position].bobot.ToString();

            return row;
        }
    }
}

