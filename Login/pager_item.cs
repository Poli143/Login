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
using Android.Views.InputMethods;

namespace Login
{
    [Activity(Label = "pager_item", MainLauncher = true)]
    public class pager_item : Activity
    {
        private List<Produkt> mProdukt;
        private ListView mListView;
        private EditText mSearch;
        private LinearLayout mConteiner;
        private bool mAnimatedDown;
        private bool mIsAnimated;
        private ProduktAdapter mAdapter;

        private TextView mTxtNProduktu;
        private TextView mTxtOProduktu;


        private bool mNProduktuAscending;
        private bool mOProduktuAscending;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.pager_item);

            mListView = FindViewById<ListView>(Resource.Id.listView);
            mListView = FindViewById<ListView>(Resource.Id.listView);
            mSearch = FindViewById<EditText>(Resource.Id.etSearch);
            mConteiner = FindViewById<LinearLayout>(Resource.Id.llContainer);

            mTxtNProduktu = FindViewById<TextView>(Resource.Id.txtNProduktu);
            mTxtOProduktu = FindViewById<TextView>(Resource.Id.txtOProduktu);


            mTxtNProduktu.Click += mTxtNProduktu_Click;
            mTxtOProduktu.Click += mTxtOProduktu_Click;




            mSearch.Alpha = 0;
            mConteiner.BringToFront();


            mSearch.TextChanged += mSearch_TextChanged; ;



            mProdukt = new List<Produkt>();
            mProdukt.Add(new Produkt { NProduktu = "Produkt", OProduktu = "5", Opis = "zajebisty produkt", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt1", OProduktu = "6", Opis = "jest moc", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt2", OProduktu = "5", Opis = "super", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt3", OProduktu = "4", Opis = "niezle", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt4", OProduktu = "3", Opis = "kapa", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt5", OProduktu = "2", Opis = "moze byc", Obraz = "blank" });
            mProdukt.Add(new Produkt { NProduktu = "Produkt6", OProduktu = "1", Opis = "okej", Obraz = "blank" });

            mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, mProdukt);
            mListView.Adapter = mAdapter;

        }

        private void mSearch_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {//pozwala na wyszukiwanie przez wpisywanie w wyszukiwarce obojetnie czy z malej czy duzej litery
            List<Produkt> searchFriends = (from Produkt in mProdukt
                                           where Produkt.NProduktu.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)
                                           || Produkt.OProduktu.Contains(mSearch.Text, StringComparison.OrdinalIgnoreCase)

                                           select Produkt).ToList<Produkt>();
            //odswieza liste
            mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, searchFriends);
            mListView.Adapter = mAdapter;

        }

        private void mTxtOProduktu_Click(object sender, EventArgs e)
        {
            List<Produkt> filteredProdukt;

            if (!mOProduktuAscending)
            {
                filteredProdukt = (from Produkt in mProdukt
                                   orderby Produkt.OProduktu
                                   select Produkt).ToList<Produkt>();
                //Odswiezenie listy
                mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, filteredProdukt);
                mListView.Adapter = mAdapter;

            }
            else
            {
                filteredProdukt = (from Produkt
                                    in mProdukt
                                   orderby Produkt.OProduktu descending
                                   select Produkt).ToList<Produkt>();
                //odswiezenie
                mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, filteredProdukt);
                mListView.Adapter = mAdapter;
            }

            mOProduktuAscending = !mOProduktuAscending;
        }

        private void mTxtNProduktu_Click(object sender, EventArgs e)
        {
            List<Produkt> filteredProdukt;

            if (!mNProduktuAscending)
            {
                filteredProdukt = (from Produkt in mProdukt
                                   orderby Produkt.NProduktu
                                   select Produkt).ToList<Produkt>();
                //Odswiezenie listy
                mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, filteredProdukt);
                mListView.Adapter = mAdapter;

            }
            else
            {
                filteredProdukt = (from Produkt
                                    in mProdukt
                                   orderby Produkt.NProduktu descending
                                   select Produkt).ToList<Produkt>();
                //odswiezenie
                mAdapter = new ProduktAdapter(this, Resource.Layout.row_produkt, filteredProdukt);
                mListView.Adapter = mAdapter;
            }

            mNProduktuAscending = !mNProduktuAscending;
        }
        public override bool OnCreateOptionsMenu(IMenu menu) // action bar jest dzieki temu widoczny
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.search:
                    //jesli ikonka zostanie wcisnieta

                    mSearch.Visibility = ViewStates.Visible;

                    if (mIsAnimated)
                    {
                        return true;

                    }
                    if (!mAnimatedDown)
                    {//jesli nie wyswietla paska wyszukiwania
                        MojaAnimacja anim = new MojaAnimacja(mListView, mListView.Height - mSearch.Height); //obniza widok 
                        anim.Duration = 500;
                        mListView.StartAnimation(anim);
                        anim.AnimationStart += anim_AnimationStartDown;
                        anim.AnimationEnd += anim_AnimationEndDown;
                        mConteiner.Animate().TranslationYBy(mSearch.Height).SetDuration(500).Start();
                    }
                    else
                    {//jesli wyswietla pasek wyszukiwania
                        MojaAnimacja anim = new MojaAnimacja(mListView, mListView.Height + mSearch.Height); //podnisi widok
                        anim.Duration = 500;
                        mListView.StartAnimation(anim);
                        anim.AnimationStart += anim_AnimationStartUp;
                        anim.AnimationEnd += anim_AnimationEndUp;
                        mConteiner.Animate().TranslationYBy(-mSearch.Height).SetDuration(500).Start();
                    }

                    mAnimatedDown = !mAnimatedDown;
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void anim_AnimationEndUp(object sender, Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            mIsAnimated = false;
            mSearch.ClearFocus(); //klawiatrura jest zwijana kiedy chowa sie okno wyszukiwarki lub nie jest ono zaznaczone
            InputMethodManager inputMenager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputMenager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        private void anim_AnimationEndDown(object sender, Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            mIsAnimated = false;
        }

        private void anim_AnimationStartDown(object sender, Android.Views.Animations.Animation.AnimationStartEventArgs e)
        {//pokazywanie editboxa w pasku wyszyukiwania
            mIsAnimated = true;
            mSearch.Animate().AlphaBy(1.0f).SetDuration(500).Start();
        }
        private void anim_AnimationStartUp(object sender, Android.Views.Animations.Animation.AnimationStartEventArgs e)
        {//chowanie editboxa z paskiem wyszukiwarki
            mIsAnimated = true;
            mSearch.Animate().AlphaBy(-1.0f).SetDuration(300).Start();
        }

    } }