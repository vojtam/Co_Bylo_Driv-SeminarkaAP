using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;
using System.Reflection;
using Xamarin.Essentials;

namespace KdySeToStalo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KdySeToStaloGameScreen : ContentPage
    {
        public KdySeToStaloGameScreen()
        {
            InitializeComponent();
            KdySeToStaloGame();
          
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
            
            var počet_událostí = udalosti_letopočty_list.ToArray().Length;
            for(int i = 0; i < počet_událostí; i++)
            {
                udalosti_list.Add(new Události(udalosti_nazev_list[i], udalosti_letopočty_list[i], i, udalosti_wiki_list[i])); // přídá do listu událostí objekty s parametry
            }
            #endregion
        }
        

        
        public Random randy = new Random();
        public List<Události> udalosti_list = new List<Události>();
        int udalost_rok;
        int random_index;
        public void Reset()
        {
            OdpovedBtn.BackgroundColor = Color.FromHex("#010001");
            EntryRok.Text = "";
        }
        public void wiki()
        {
            /*
             vytvoří nový label, který si vezme adresu z příslušného objektu a vytvoří link
             */
            var adresa = udalosti_list[random_index].Wiki; // vezme adresu na wiki a uloží ji do proměnné
            label_wiki.FontSize = 16;


            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) => {                  // zajistí, že se na Label dá kliknout a zobrazit odkaz
                Launcher.OpenAsync(new Uri((adresa as String)));
            };
            label_wiki.GestureRecognizers.Add(tapGestureRecognizer);

            label_wiki.Text = "Odkaz na Wikipedii";
        }
        public void KdySeToStaloGame() // hlavní metoda
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
            label_wiki.IsVisible = true;
            wiki();
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
            label_wiki.IsVisible = false;
            KdySeToStaloGame    ();
            EntryRok.Focus();
        }

    }
}