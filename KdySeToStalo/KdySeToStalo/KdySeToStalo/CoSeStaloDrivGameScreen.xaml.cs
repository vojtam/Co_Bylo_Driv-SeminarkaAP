using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace KdySeToStalo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoSeStaloDrivGameScreen : ContentPage
    {
        public CoSeStaloDrivGameScreen()
        {
            InitializeComponent();
            game();

        }
        
        public Random randy = new Random();
        public List<Události> udalosti_list = new List<Události>();
        int ev1_rok, ev2_rok;
        int pocet_dobre;
        int pocet_spatne;
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

            int počet_událostí = udalosti_letopočty_list.ToArray().Length;

            for (int i = 0; i < počet_událostí; i++)
            {
                udalosti_list.Add(new Události(udalosti_nazev_list[i], udalosti_letopočty_list[i], i, udalosti_wiki_list[i])); // přídá do listu událostí objekty s parametry
            }
            #endregion
        }
       
        public void wiki1()
        {
            /*
             vytvoří nový label, který si vezme adresu z příslušného objektu a vytvoří link
             */
            var adresa = udalosti_list[random_index1].Wiki; // vezme adresu na wiki a uloží ji do proměnné
            label_wiki1.FontSize = 16;


            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) => {                  // zajistí, že se na Label dá kliknout a zobrazit odkaz
                Launcher.OpenAsync(new Uri((adresa as String)));
            };
            label_wiki1.GestureRecognizers.Add(tapGestureRecognizer);

            label_wiki1.Text = "Odkaz na Wikipedii";
        }
        public void wiki2()
        {
            /*
             vytvoří nový label, který si vezme adresu z příslušného objektu a vytvoří link
             */
            var adresa = udalosti_list[random_index2].Wiki; // vezme adresu na wiki a uloží ji do proměnné
          
            label_wiki2.FontSize = 16;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {              // zajistí, že se na Label dá kliknout a zobrazit odkaz
                Launcher.OpenAsync(new Uri((adresa as String)));
            };
            label_wiki2.GestureRecognizers.Add(tapGestureRecognizer);
            label_wiki2.Text = "Odkaz na Wikipedii";

        }
        int random_index1;
        int random_index2;
        public void game()
        {
            načti_ze_souborů(); // spustí metodu, která načte data ze souborů a vytvoří příslušné objekty
            random_index1 = randy.Next(udalosti_list.ToArray().Length); // vybere první náhodnou událost 
            Event1Btn.Text = udalosti_list[random_index1].Název; // do buttonu vloží název první události
            ev1_rok = udalosti_list[random_index1].Datum; // vezme si datum první události a uloží do proměnné
            random_index2 = randy.Next(udalosti_list.ToArray().Length); // vybere druhou první náhodnou událost 
            Event2Btn.Text = udalosti_list[random_index2].Název; // do buttonu vloží název druhé události
            ev2_rok = udalosti_list[random_index2].Datum; // vezme si datum druhé události a uloží do proměnné
            if (Event1Btn.Text == Event2Btn.Text)
            {
                game(); // pokud se názvy obou událostí rovnají, tak spustí metodu znova
            }
            else if (ev1_rok == ev2_rok)
            {
                game(); // pokud se události staly ve stejný rok, tak spustí metodu znova
            }
        }
        public void reset_btn()
        {
            // vrátí barvy buttonů zpátky do normálu
            Event1Btn.BackgroundColor = Color.FromHex("#010001");
            Event2Btn.BackgroundColor = Color.FromHex("#010001");
        }
        public void vypiš_datum()
        {
            if (ev1_rok < 0) // pokud se událost stala před Kristem (parametr objektu je záporné číslo), tak je číslo převedeno na kladné a přidáno "př. n. l."
            {
                if (ev1_rok < 0 && ev2_rok < 0)
                {
                    ev1_rok = -ev1_rok;
                    Event1Btn.Text += '\n' + $"{ev1_rok.ToString()}" + " př. n. l.";
                    ev2_rok = -ev2_rok;
                    Event2Btn.Text += '\n' + $"{ev2_rok.ToString()}" + " př. n. l.";
                }
                else
                {
                    ev1_rok = -ev1_rok;
                    Event1Btn.Text += '\n' + $"{ev1_rok.ToString()}" + " př. n. l.";
                    Event2Btn.Text += '\n' + ev2_rok.ToString();
                }

            }
            else if (ev2_rok < 0)  // pokud se událost stala před Kristem (parametr objektu je záporné číslo), tak je číslo převedeno na kladné a přidáno "př. n. l."
            {
                if (ev1_rok < 0 && ev2_rok < 0)
                {
                    ev1_rok = -ev1_rok;
                    Event1Btn.Text += '\n' + $"{ev1_rok.ToString()}" + " př. n. l.";
                    ev2_rok = -ev2_rok;
                    Event2Btn.Text += '\n' + $"{ev2_rok.ToString()}" + " př. n. l.";
                }
                else
                {
                    ev2_rok = -ev2_rok;
                    Event2Btn.Text += '\n' + $"{ev2_rok.ToString()}" + " př. n. l.";
                    Event1Btn.Text += '\n' + ev1_rok.ToString();
                }
            }
            else
            {
                Event1Btn.Text += '\n' + ev1_rok.ToString();
                Event2Btn.Text += '\n' + ev2_rok.ToString();
            }
        }
        
              
        private void Event1Btn_Clicked(object sender, EventArgs e)
        {
            // porovná roky eventů na jednotlivých buttonech a podle toho zbarví button
            if (ev1_rok < ev2_rok)
            {
                Event1Btn.BackgroundColor = Color.Green;
                Event2Btn.BackgroundColor = Color.Red;
                vypiš_datum(); // zkontroluje, jestli je datum před nebo po našem letopočtu a zobrazí do Buttonu správný text
                wiki1(); // spustí metodu, která vytvoří label s odkazem na wiki
                wiki2(); // spustí metodu, která vytvoří label s odkazem na wiki
                label_wiki1.IsVisible = true; // zobrazí label s odkazem na wiki
                label_wiki2.IsVisible = true; // zobrazí label s odkazem na wiki

            }
            else if (ev2_rok < ev1_rok)
            {

                Event1Btn.BackgroundColor = Color.Red;
                Event2Btn.BackgroundColor = Color.Green;
                vypiš_datum(); // zkontroluje, jestli je datum před nebo po našem letopočtu a zobrazí do Buttonu správný text
                wiki1(); // spustí metodu, která vytvoří label s odkazem na wiki
                wiki2(); // spustí metodu, která vytvoří label s odkazem na wiki
                label_wiki1.IsVisible = true; // zobrazí label s odkazem na wiki
                label_wiki2.IsVisible = true; // zobrazí label s odkazem na wiki
            }

        }
        private void Event2Btn_Clicked(object sender, EventArgs e)
        {
            // porovná roky eventů na jednotlivých buttonech a podle toho zbarví button
            if (ev1_rok > ev2_rok)
            {
                Event2Btn.BackgroundColor = Color.Green;
                Event1Btn.BackgroundColor = Color.Red;
                vypiš_datum(); // zkontroluje, jestli je datum před nebo po našem letopočtu a zobrazí do Buttonu správný text
                wiki1();
                wiki2();
                label_wiki2.IsVisible = true;
                label_wiki1.IsVisible = true;
            }
            else if (ev2_rok > ev1_rok)
            {
                Event2Btn.BackgroundColor = Color.Red;
                Event1Btn.BackgroundColor = Color.Green;
                vypiš_datum(); // zkontroluje, jestli je datum před nebo po našem letopočtu a zobrazí do Buttonu správný text
                wiki1();
                wiki2();
                label_wiki2.IsVisible = true;
                label_wiki1.IsVisible = true;
            }

        }
        private void Next_button_Clicked(object sender, EventArgs e)
        {
            // po kliknutí na next button resetuje barvu eventových buttonů a vloží nové eventy do buttonů
            reset_btn();
            game();
            label_wiki1.IsVisible = false;
            label_wiki2.IsVisible = false;

        }
        

    }
}