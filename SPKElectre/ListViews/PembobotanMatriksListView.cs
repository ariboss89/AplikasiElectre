using System;
using Android.App;
using Android.Views;
using Android.Widget;
using SPKElectre.Models;
using System.Collections.Generic;
using Android.Content;

namespace SPKElectre.ListViews
{
    public class PembobotanMatriksListView : BaseAdapter<pembobotan>
    {
        TextView txtId, txtAlternatif, txtPilihan, txtNilai;

        public PembobotanMatriksListView(Activity activity, int listViewRow)
        {

        }

        public List<pembobotan> listPembobotan;
        private Context mContext;
        private Activity activity;



        public PembobotanMatriksListView(Context context, List<pembobotan> items)
        {
            listPembobotan = items;
            mContext = context;
        }

        public override int Count
        {
            get
            {
                try
                {
                    return listPembobotan.Count;
                }
                catch (Exception)
                {
                    Toast.MakeText(Application.Context, "Can't connect to server !", ToastLength.Long).Show();

                    return 0;
                }
            }
        }

        public override pembobotan this[int position]
        {
            get
            {
                return listPembobotan[position];
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.PembobotanListView_Layout, null, false);
            }

            txtId = row.FindViewById<TextView>(Resource.Id.txtId);
            txtAlternatif = row.FindViewById<TextView>(Resource.Id.txtAlternatif);
            txtPilihan = row.FindViewById<TextView>(Resource.Id.txtPilihan);
            txtNilai = row.FindViewById<TextView>(Resource.Id.txtNilai);

            int id = position + 1;

            txtId.Text = id.ToString();
            txtAlternatif.Text = listPembobotan[position].alternatif;
            txtPilihan.Text = listPembobotan[position].kriteria.ToString();
            txtNilai.Text = listPembobotan[position].nilai_pembobotan.ToString();

            return row;
        }
    }
}

