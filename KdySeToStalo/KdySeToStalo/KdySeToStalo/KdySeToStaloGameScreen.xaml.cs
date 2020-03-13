using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using System.Reflection;

namespace KdySeToStalo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KdySeToStaloGameScreen : ContentPage
    {
        public KdySeToStaloGameScreen()
        {
            InitializeComponent();
            TestGame();
          
        }
        public void načti_ze_souborů()
        {
            #region načteni ze souboru názvů událostí
            var assemblyText = IntrospectionExtensions.GetTypeInfo(typeof(NačtiNazvyUdalosti)).Assembly;
            Stream streamText = assemblyText.GetManifestResourceStream("KdySeToStalo.udalosti_nazev.txt");
            string text = "";
            List<string> udalosti_nazev_list = new List<string>();
            using (var readerT = new System.IO.StreamReader(streamText))
            {
                text = readerT.ReadToEnd();
                var vyslednyText = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < vyslednyText.Length; i++)
                {
                    udalosti_nazev_list.Add(vyslednyText[i]);
                }

            }
            
            #endregion
            #region načtení ze souboru letopočtů
            var assemblyRoky = IntrospectionExtensions.GetTypeInfo(typeof(NačtiLetopočty)).Assembly;
            Stream streamRoky = assemblyRoky.GetManifestResourceStream("KdySeToStalo.letopocty.txt");
            string letopočty = "";
            List<int> udalosti_letopočty_list = new List<int>();
            using (var readerR = new System.IO.StreamReader(streamRoky))
            {
                letopočty = readerR.ReadToEnd();
                var vysledneLetopočty = letopočty.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < vysledneLetopočty.Length; i++)
                {
                    udalosti_letopočty_list.Add(int.Parse(vysledneLetopočty[i]));
                }

            }

            
            #endregion
            #region načteni ze souboru odkazy na wikipedii
            var assemblyWiki = IntrospectionExtensions.GetTypeInfo(typeof(Načtiwiki)).Assembly;
            Stream streamWiki = assemblyWiki.GetManifestResourceStream("KdySeToStalo.wiki.txt");
            string textWiki = "";
            List<string> udalosti_wiki_list = new List<string>();
            using (var readerW = new System.IO.StreamReader(streamWiki))
            {
                textWiki = readerW.ReadToEnd();
                var vyslednyWiki = textWiki.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < vyslednyWiki.Length; i++)
                {
                    udalosti_wiki_list.Add(vyslednyWiki[i]);
                }

            }
            
            var udalosti_letopočty_arr = udalosti_letopočty_list.ToArray();
            for(int i = 0; i < udalosti_letopočty_arr.Length; i++)
            {
                udalosti_list.Add(new Události(udalosti_nazev_list[i], udalosti_letopočty_list[i], i, udalosti_wiki_list[i])); // přídá do listu událostí objekty s parametry
            }
            #endregion
        }
        

        //public void pridej_udalosti()
        //{
        //    udalosti_list.Add(new Události("Bitva u Hastings", 1066, 1, "https://cs.wikipedia.org/wiki/Bitva_u_Hastingsu"));
        //    udalosti_list.Add(new Události("Upalení Jana Husa", 1415, 2, "https://cs.wikipedia.org/wiki/Jan_Hus"));
        //    udalosti_list.Add(new Události("Začátek WWII", 1939, 3, "https://cs.wikipedia.org/wiki/Druh%C3%A1_sv%C4%9Btov%C3%A1_v%C3%A1lka"));
        //    udalosti_list.Add(new Události("Vznik Francké říše", 486, 4, "https://cs.wikipedia.org/wiki/Fransk%C3%A1_%C5%99%C3%AD%C5%A1e"));
        //    udalosti_list.Add(new Události("Rozdělení Římské říše", 330, 5, "https://cs.wikipedia.org/wiki/%C5%98%C3%ADmsk%C3%A1_%C5%99%C3%AD%C5%A1e#Rozd%C4%9Blen%C3%AD_%C5%99%C3%AD%C5%A1e_a_nastolen%C3%AD_k%C5%99es%C5%A5anstv%C3%AD"));
        //    udalosti_list.Add(new Události("Vyplenění Říma Vandaly", 455, 6, "https://cs.wikipedia.org/wiki/Vyplen%C4%9Bn%C3%AD_%C5%98%C3%ADma_(455)"));
        //    udalosti_list.Add(new Události("Zánik Západořímské říše", 476, 7, "https://cs.wikipedia.org/wiki/P%C3%A1d_Z%C3%A1pado%C5%99%C3%ADmsk%C3%A9_%C5%99%C3%AD%C5%A1e"));
        //    udalosti_list.Add(new Události("Milánský edikt", 313, 8, "https://cs.wikipedia.org/wiki/Edikt_mil%C3%A1nsk%C3%BD"));
        //    udalosti_list.Add(new Události("Velké schizma", 1054, 9, "https://cs.wikipedia.org/wiki/Velk%C3%A9_schizma"));
        //    udalosti_list.Add(new Události("První křížová výprava", 1095, 10, "https://cs.wikipedia.org/wiki/Prvn%C3%AD_k%C5%99%C3%AD%C5%BEov%C3%A1_v%C3%BDprava"));
        //    udalosti_list.Add(new Události("Zánik Kartága", -146, 11, "https://cs.wikipedia.org/wiki/Kart%C3%A1go"));

            
        //}
        public Random randy = new Random();
        public List<Události> udalosti_list = new List<Události>();
        int udalost_rok;
        int random_index;
        public void Reset()
        {
            OdpovedBtn.BackgroundColor = Color.FromHex("#b4654a");
            EntryRok.Text = "";
        }
        public void TestGame() // hlavní metoda
        {
            načti_ze_souborů();
            random_index = randy.Next(udalosti_list.ToArray().Length); // vybere náhodný index události z listu objektů
            UdálostBtn.Text = udalosti_list[random_index].Název;
            udalost_rok = udalosti_list[random_index].Datum;
            SpravnaOdpovedLabel.Opacity = 0;
            PokracovatBtn.Opacity = 0;

        }
        private void OdpovedBtnClick(object sender, EventArgs e)
        {
            try
            {

                int odpoved_rok = Convert.ToInt32(EntryRok.Text);
                if (odpoved_rok == udalosti_list[random_index].Datum)
                {
                    OdpovedBtn.BackgroundColor = Color.Green;
                    PokracovatBtn.Opacity = 1;
                }
                else
                {
                    OdpovedBtn.BackgroundColor = Color.Red;
                    
                    SpravnaOdpovedLabel.Opacity = 1;
                    PokracovatBtn.Opacity = 1;
                    if(udalosti_list[random_index].Datum < 0)
                    {
                        udalosti_list[random_index].Datum = -udalosti_list[random_index].Datum;
                        SpravnaOdpovedLabel.Text = "Správná Odpověď: " + Convert.ToString(udalosti_list[random_index].Datum) + " př. n. l.";
                    }
                    else
                    {
                        SpravnaOdpovedLabel.Text = "Správná Odpověď: " + Convert.ToString(udalosti_list[random_index].Datum);
                    }
                }
            }
            catch
            {
                if (EntryRok.Text == null)
                {
                    GameHeader.Text = "Prosím zadej rok";
                }
            }

        }
        protected override void OnAppearing() // zajistí, aby se po spuštění aktivity automaticky spustila klávesnice a dalo se psát do pole
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(250);
                EntryRok.Focus();
            });
        }
        private void PokracovatBtnClick(object sender, EventArgs e)
        {
            Reset();
            TestGame();
            EntryRok.Focus();
        }

    }
}