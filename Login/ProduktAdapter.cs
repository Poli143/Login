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
using Android.Graphics;

namespace Login
{
    class ProduktAdapter : BaseAdapter<Produkt>
    {
        private Context mContext;
        private int mRowLayout;
        private List<Produkt> mProdukt;
        private int[] mAlternatingColors;

        public ProduktAdapter(Context context, int rowLayout, List<Produkt> Produkt)
        {
            mContext = context;
            mRowLayout = rowLayout;
            mProdukt = Produkt;
            mAlternatingColors = new int[] { 0xF2F2F2, 0x009900 };
        }

        public override int Count
        {
            get { return mProdukt.Count; }
        }

        public override Produkt this[int position]
        {
            get { return mProdukt[position]; }
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
                row = LayoutInflater.From(mContext).Inflate(mRowLayout, parent, false);
            }

            row.SetBackgroundColor(GetColorFromInteger(mAlternatingColors[position % mAlternatingColors.Length]));


            TextView NazwaProduktu = row.FindViewById<TextView>(Resource.Id.txtNazwaProduktu);
            NazwaProduktu.Text = mProdukt[position].NProduktu;

            TextView OcenaProduktu = row.FindViewById<TextView>(Resource.Id.txtOcenaProduktu);
            OcenaProduktu.Text = mProdukt[position].OProduktu;

            

            if ((position % 2) == 1)
            {
                //Green background, set text white
                NazwaProduktu.SetTextColor(Color.White);
                OcenaProduktu.SetTextColor(Color.White);
                
            }

            else
            {
                //White background, set text black
                NazwaProduktu.SetTextColor(Color.Black);
                OcenaProduktu.SetTextColor(Color.Black);
                
            }

            return row;
        }

        private Color GetColorFromInteger(int color)
        {
            return Color.Rgb(Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color));
        }

    }
}