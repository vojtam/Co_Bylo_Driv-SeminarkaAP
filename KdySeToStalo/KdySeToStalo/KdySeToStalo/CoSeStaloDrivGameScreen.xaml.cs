using System;
using System.Collections.Generic;
using System.Linq;
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
        public void pridej_udalosti()
        {
            udalosti_list.Add(new Události("Bitva u Hastings", 1066, 1, "https://cs.wikipedia.org/wiki/Bitva_u_Hastingsu"));
            udalosti_list.Add(new Události("Upalení Jana Husa", 1415, 2, "https://cs.wikipedia.org/wiki/Jan_Hus"));
            udalosti_list.Add(new Události("Začátek WWII", 1939, 3, "https://cs.wikipedia.org/wiki/Druh%C3%A1_sv%C4%9Btov%C3%A1_v%C3%A1lka"));
            udalosti_list.Add(new Události("Vznik Francké říše", 486, 4, "https://cs.wikipedia.org/wiki/Fransk%C3%A1_%C5%99%C3%AD%C5%A1e"));
            udalosti_list.Add(new Události("Rozdělení Římské říše", 330, 5, "https://cs.wikipedia.org/wiki/%C5%98%C3%ADmsk%C3%A1_%C5%99%C3%AD%C5%A1e#Rozd%C4%9Blen%C3%AD_%C5%99%C3%AD%C5%A1e_a_nastolen%C3%AD_k%C5%99es%C5%A5anstv%C3%AD"));
            udalosti_list.Add(new Události("Vyplenění Říma Vandaly", 455, 6, "https://cs.wikipedia.org/wiki/Vyplen%C4%9Bn%C3%AD_%C5%98%C3%ADma_(455)"));
            udalosti_list.Add(new Události("Zánik Západořímské říše", 476, 7, "https://cs.wikipedia.org/wiki/P%C3%A1d_Z%C3%A1pado%C5%99%C3%ADmsk%C3%A9_%C5%99%C3%AD%C5%A1e"));
            udalosti_list.Add(new Události("Milánský edikt", 313, 8, "https://cs.wikipedia.org/wiki/Edikt_mil%C3%A1nsk%C3%BD"));
            udalosti_list.Add(new Události("Velké schizma", 1054, 9, "https://cs.wikipedia.org/wiki/Velk%C3%A9_schizma"));
            udalosti_list.Add(new Události("První křížová výprava", 1095, 10, "https://cs.wikipedia.org/wiki/Prvn%C3%AD_k%C5%99%C3%AD%C5%BEov%C3%A1_v%C3%BDprava"));
            udalosti_list.Add(new Události("Zánik Kartága", -146, 11, "https://cs.wikipedia.org/wiki/Kart%C3%A1go"));

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
            pridej_udalosti(); // spustí metodu, která vytvoří příslušné objekty

            random_index1 = randy.Next(udalosti_list.ToArray().Length); // vybere první náhodnou událost 
            Event1Btn.Text = udalosti_list[random_index1].Název; // do buttonu vloží název první události
            ev1_rok = udalosti_list[random_index1].Datum; // vezme si datum první události a uloží do proměnné
            random_index2 = randy.Next(udalosti_list.ToArray().Length); // vybere druhou první náhodnou událost 
            Event2Btn.Text = udalosti_list[random_index2].Název; // do buttonu vloží název druhé události
            ev2_rok = udalosti_list[random_index2].Datum; // vezme si datum druhé události a uloží do proměnné
            if (Event1Btn.Text == Event2Btn.Text)
            {
                game(); // pokud se data obou událostí rovnají, tak spustí metodu znova
            }

        }
        public void reset_btn()
        {
            // vrátí barvy buttonů zpátky do normálu
            Event1Btn.BackgroundColor = Color.FromHex("#499f68");
            Event2Btn.BackgroundColor = Color.FromHex("#499f68");

        }
        public void vypiš_datum()
        {
            if (ev1_rok < 0) // pokud se událost stala před Kristem (parametr objektu je záporné číslo), tak je číslo převedeno na kladné a přidáno "př. n. l."
            {
                ev1_rok = -ev1_rok;
                Event1Btn.Text = $"{ev1_rok.ToString()}" + " př. n. l.";
                Event2Btn.Text = ev2_rok.ToString();
            }
            else if (ev2_rok < 0)  // pokud se událost stala před Kristem (parametr objektu je záporné číslo), tak je číslo převedeno na kladné a přidáno "př. n. l."
            {
                ev2_rok = -ev2_rok;
                Event2Btn.Text = $"{ev2_rok.ToString()}" + " př. n. l.";
                Event1Btn.Text = ev1_rok.ToString();
            }
            else if (ev1_rok < 0 && ev2_rok < 0)
            {
                ev1_rok = -ev1_rok;
                Event1Btn.Text = $"{ev1_rok.ToString()}" + " př. n. l.";
                ev2_rok = -ev2_rok;
                Event2Btn.Text = $"{ev2_rok.ToString()}" + " př. n. l.";
            }
            else
            {
                Event1Btn.Text = ev1_rok.ToString();
                Event2Btn.Text = ev2_rok.ToString();
            }
        }
        private void Event1Btn_Clicked(object sender, EventArgs e)
        {
            // porovná roky eventů na jednotlivých buttonech a podle toho zbarví button a inkrementuje příslušný label
            if (ev1_rok < ev2_rok)
            {
                Event1Btn.BackgroundColor = Color.Green;
                Event2Btn.BackgroundColor = Color.Red;
                pocet_dobre++;
                vypiš_datum();
                wiki1(); // spustí metodu, která vytvoří label s odkazem na wiki
                wiki2(); // spustí metodu, která vytvoří label s odkazem na wiki
                label_wiki1.IsVisible = true; // zobrazí label s odkazem na wiki
                label_wiki2.IsVisible = true; // zobrazí label s odkazem na wiki

            }
            else if (ev2_rok < ev1_rok)
            {

                Event1Btn.BackgroundColor = Color.Red;
                Event2Btn.BackgroundColor = Color.Green;
                pocet_spatne++;
                vypiš_datum();
                wiki1(); // spustí metodu, která vytvoří label s odkazem na wiki
                wiki2(); // spustí metodu, která vytvoří label s odkazem na wiki
                label_wiki1.IsVisible = true; // zobrazí label s odkazem na wiki
                label_wiki2.IsVisible = true; // zobrazí label s odkazem na wiki
            }

        }
        private void Event2Btn_Clicked(object sender, EventArgs e)
        {
            // porovná roky eventů na jednotlivých buttonech a podle toho zbarví button a inkrementuje příslušný label
            if (ev1_rok > ev2_rok)
            {
                pocet_dobre++;
                Event2Btn.BackgroundColor = Color.Green;
                Event1Btn.BackgroundColor = Color.Red;
                vypiš_datum();
                wiki1();
                wiki2();
                label_wiki2.IsVisible = true;
                label_wiki1.IsVisible = true;
            }
            else if (ev2_rok > ev1_rok)
            {
                pocet_spatne++;
                Event2Btn.BackgroundColor = Color.Red;
                Event1Btn.BackgroundColor = Color.Green;
                vypiš_datum();

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