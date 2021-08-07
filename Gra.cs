using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.IO;

namespace JPWP_Projekt {
    class Gra {
        ///Klasa obsługująca pole gry

        ///Tekstura tła
        Texture tlo;
        Texture tlo2;
        Texture pacman;
        Texture pacmanHappy;
        Texture pacmanDead;
        Texture pacmanWorried;
        ///Animacja spływającego krwi
        Texture[] krewprawy;
        /// oraz jej wypełnienie
        Texture krewtlotexture;
        Sprite krewtlosprite;
        Sprite pacmanS;
        Sprite sprite;
        Sprite krew;
        Music music; 
        Music dobrze;
        Music zle;
        Music koniec;
        RenderWindow lwindow;
        ///Pozycja platformy
        int licznik; 
        Texture platforma;
        Sprite platformaS;
        ///Obsługa wyłączenia gry
        public bool stan; 
        Random rnd;
        Font czcionka;
        Font czcionka2;
        Text textNowagra;
        Sprite skulltabliczkaS1;
        Sprite skulltabliczkaS2;
        Sprite skulltabliczkaS3;
        Texture skulltabliczka;
        Text odp1;
        Text odp2;
        Text odp3;
        int odp1L;
        int odp2L;
        int odp3L;
        ///Decyzja, która z odpowiedzi ma być poprawną
        int odpP; 
        ///Elementy równania matematycznego
        int[] dane = { 0, 0 }; 
        string danetext;
        Text wynik;
        ///Aktualny wynik w grze
        int wynikL; 
        int happy_licznik;
        ///Tempo opadania platformy
        int tempo; 
        ///Obsługa animacji krwi
        int i =0 ;
        int w = 0;
        ///Wybrany tryb gry
        public int publictryb; 
        public Gra()
        {
            
            stan = true;
            tlo = new Texture("canva.png");
            tlo2 = new Texture("canvaS2.png");
            sprite = new Sprite(tlo);
            platforma = new Texture("platforma2.png");
            platformaS = new Sprite(platforma);
            platformaS.Position = new Vector2f(118, 0);
            licznik = 170;
            music = new Music("screamhorrormusic.flac");
            dobrze = new Music("dobrze.wav");
            zle = new Music("zle.wav");
            koniec = new Music("koniec.wav");
            rnd = new Random();

            dane[0] = rnd.Next(1, 10);
            dane[1] = rnd.Next(1, 10);
            czcionka = new Font("youmurdererbb_reg.otf");
            czcionka2 = new Font("arial.ttf");
            danetext = "" + dane[0] + " x " + dane[1];
            textNowagra = new Text(danetext, czcionka, 200);
            textNowagra.Position = new Vector2f(350, 220);
            textNowagra.Color = new Color(255, 20, 20);
            skulltabliczka = new Texture("skulltabliczka.png");
            skulltabliczkaS1 = new Sprite(skulltabliczka);
            skulltabliczkaS2 = new Sprite(skulltabliczka);
            skulltabliczkaS3 = new Sprite(skulltabliczka);
            odp1L = 1;
            odp2L = 2;
            odp3L = 3;


            odp1 = new Text("" + odp1L, czcionka, 12);
            odp2 = new Text("" + odp2L, czcionka, 12);
            odp3 = new Text("" + odp3L, czcionka, 12);

            wynikL = -1;
            wynik = new Text("0", czcionka2, 80);
            wynik.Position = new Vector2f(50, 400);

            pacman = new Texture("pacman.png");
            pacman.Smooth = true;
            pacmanHappy = new Texture("pacmanHappy.png");
            pacmanDead = new Texture("pacmanDead.png");
            pacmanWorried = new Texture("pacmanWorried.png");
            pacmanS = new Sprite(pacman);
            pacmanS.Position=new Vector2f(4, 657);

            tempo = 35;

            krewprawy = new Texture[16];
            krewprawy[0] = new Texture("krewprawy3/0.gif");
            krewprawy[1] = new Texture("krewprawy3/1.gif");
            krewprawy[2] = new Texture("krewprawy3/2.gif");
            krewprawy[3] = new Texture("krewprawy3/3.gif");
            krewprawy[4] = new Texture("krewprawy3/4.gif");
            krewprawy[5] = new Texture("krewprawy3/5.gif");
            krewprawy[6] = new Texture("krewprawy3/6.gif");
            krewprawy[7] = new Texture("krewprawy3/7.gif");
            krewprawy[8] = new Texture("krewprawy3/8.gif");
            krewprawy[9] = new Texture("krewprawy3/9.gif");
            krewprawy[10] = new Texture("krewprawy3/10.gif");
            krewprawy[11] = new Texture("krewprawy3/11.gif");
            krewprawy[12] = new Texture("krewprawy3/12.gif");
            krewprawy[13] = new Texture("krewprawy3/13.gif");
            krewprawy[14] = new Texture("krewprawy3/14.gif");
            krewprawy[15] = new Texture("krewprawy3/15.gif");
            krew = new Sprite();
            krew.Position = new Vector2f(0, 0);
            krewtlotexture = new Texture("krewtlo.png");
            krewtlosprite= new Sprite(krewtlotexture);
            krewtlosprite.Position = new Vector2f(0, -768);

        }

        public void Dzialaj(int tryb)
        {
            ///Podstawowa funkcja obsługująca grę
            publictryb = tryb;
            var window = new RenderWindow(new VideoMode(1024, 768), "Gra", Styles.Fullscreen);
            window.SetMouseCursorVisible(false);
            lwindow = null;
            lwindow = window;
            lwindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            stan = true;
            music.Play();
            Generuj();
            while (lwindow.IsOpen)
            {
                if (stan == true)
                {
                    System.Threading.Thread.Sleep(tempo);
                    //System.Threading.Thread.Sleep(16);
                    platformaS.Position = new Vector2f(118, licznik);
                    skulltabliczkaS1.Position = new Vector2f(118 + 196, licznik + 4);
                    skulltabliczkaS2.Position = new Vector2f(118 + 2 * 196, licznik + 4);
                    skulltabliczkaS3.Position = new Vector2f(118 + 3 * 196, licznik + 4);
                    odp1.Position = new Vector2f(108 + 196 + 8, licznik -60);
                    odp2.Position = new Vector2f(108 + 2 * 196 + 8, licznik - 60);
                    odp3.Position = new Vector2f(108 + 3 * 196 + 8, licznik - 60);
                    if (licznik > 500) pacmanS.Texture = (pacmanWorried);
                    else if (happy_licznik > 35) pacmanS.Texture = (pacman);
                    krew.Position = new Vector2f(0, licznik);
                    krewtlosprite.Position = new Vector2f(0, licznik - 768);
                    lwindow.Clear();

                    if (licznik > 730) {
                        lwindow.Clear();
                        //Dojście platformy na dno
                        using (StreamWriter writetext = new StreamWriter("wyniki.txt", true))
                        {
                            writetext.WriteLine(wynikL);
                        }
                        pacmanS.Texture = (pacmanDead);
                        wynik.Position = new Vector2f(430, 220);
                        wynik.CharacterSize = 240;
                        music.Stop();
                        koniec.Play();
                        for (int o = 0; o < 3600; o++)
                        {
                            lwindow.Clear();
                            lwindow.Draw(pacmanS);
                            lwindow.Draw(wynik);
                            lwindow.DispatchEvents();
                            window.Display();


                            System.Threading.Thread.Sleep(1);
                        }
                        stan = false;
                       // lwindow.Close();                       
                        licznik = 0;
                        wynikL = -1;
                        tempo = 35;
                        
                    }
                    
                    lwindow.Draw(sprite);
                    
                    lwindow.Draw(textNowagra);
                    lwindow.Draw(platformaS);
                    lwindow.Draw(skulltabliczkaS1);
                    lwindow.Draw(skulltabliczkaS2);
                    lwindow.Draw(skulltabliczkaS3);
                    lwindow.Draw(odp1);
                    lwindow.Draw(odp2);
                    lwindow.Draw(odp3);
                    
                    lwindow.Draw(pacmanS);
                    
                    krew.Texture = krewprawy[i];
                    i++;
                    if (i == 16) i = 0;
                    lwindow.Draw(krewtlosprite);
                    lwindow.Draw(krew);
                    krew.Position = new Vector2f(908, licznik);
                    krewtlosprite.Position = new Vector2f(908, licznik - 768);
                    lwindow.Draw(krewtlosprite);
                    lwindow.Draw(krew);
                    lwindow.Draw(wynik);
                    lwindow.DispatchEvents();
                    lwindow.Display();
                    licznik++;
                    happy_licznik++;
                
                }
                else break;

            }


        }

        void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;

            // Escape key : exit
            if (e.Code == Keyboard.Key.Escape)
            {

                stan = false;
                lwindow.Close();
                music.Stop();
                licznik = 0;
                wynikL = -1;
                tempo = 35;

            }

            // T key : Menu
            if (e.Code == Keyboard.Key.Left)
            {
                if (publictryb == 1) { if (dane[0] * dane[1] == odp1L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 2) { if (dane[0] + dane[1] == odp1L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 3) { if (dane[0] - dane[1] == odp1L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }


            }
            if (e.Code == Keyboard.Key.Up)
            {
                if (publictryb == 1) { if (dane[0] * dane[1] == odp2L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 2) { if (dane[0] + dane[1] == odp2L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 3) { if (dane[0] - dane[1] == odp2L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }

            }
            if (e.Code == Keyboard.Key.Right)
            {
                if (publictryb == 1) { if (dane[0] * dane[1] == odp3L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 2) { if (dane[0] + dane[1] == odp3L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }
                if (publictryb == 3) { if (dane[0] - dane[1] == odp3L) { dobrze.Play(); Generuj(); } else { licznik = licznik + 35; zle.Play(); } }

            }


        }

        void Generuj()
        {
            ///Generowanie nowego równania
            licznik = licznik - 55;
            if (licznik < 100) { sprite.Texture = tlo2;wynikL++; } else sprite.Texture = tlo;
            odpP = rnd.Next(1, 4);
            if (publictryb == 1)
            {
                dane[0] = rnd.Next(1, 10);
                dane[1] = rnd.Next(1, 10);
                if (odpP == 1) { odp1L = dane[0] * dane[1]; odp2L = rnd.Next(1, 100); odp3L = rnd.Next(1, 100); }
                if (odpP == 2) { odp2L = dane[0] * dane[1]; odp1L = rnd.Next(1, 100); odp3L = rnd.Next(1, 100); }
                if (odpP == 3) { odp3L = dane[0] * dane[1]; odp2L = rnd.Next(1, 100); odp1L = rnd.Next(1, 100); }
                danetext = "" + dane[0] + " x " + dane[1];
            }
            if (publictryb == 2)
            {
                dane[0] = rnd.Next(1, 90);
                dane[1] = rnd.Next(1, 10);
                if (odpP == 1) { odp1L = dane[0] + dane[1]; odp2L = rnd.Next(1, 21); odp3L = rnd.Next(1, 100); }
                if (odpP == 2) { odp2L = dane[0] + dane[1]; odp1L = rnd.Next(1, 21); odp3L = rnd.Next(1, 100); }
                if (odpP == 3) { odp3L = dane[0] + dane[1]; odp2L = rnd.Next(1, 21); odp1L = rnd.Next(1, 100); }
                danetext = "" + dane[0] + " + " + dane[1];
            }
            if (publictryb == 3)
            {
                dane[0] = rnd.Next(10, 90);
                dane[1] = rnd.Next(1, 10);
                if (odpP == 1) { odp1L = dane[0] - dane[1]; odp2L = rnd.Next(1, 100); odp3L = rnd.Next(1, 100); }
                if (odpP == 2) { odp2L = dane[0] - dane[1]; odp1L = rnd.Next(1, 100); odp3L = rnd.Next(1, 100); }
                if (odpP == 3) { odp3L = dane[0] - dane[1]; odp2L = rnd.Next(1, 100); odp1L = rnd.Next(1, 100); }
                danetext = "" + dane[0] + " - " + dane[1];
            }
            odp1 = new Text("" + odp1L, czcionka, 80);
            odp2 = new Text("" + odp2L, czcionka, 80);
            odp3 = new Text("" + odp3L, czcionka, 80);
            textNowagra.DisplayedString = danetext;
            wynikL++;
            wynik = new Text("" + wynikL, czcionka2, 80);
            pacmanS.Texture = pacmanHappy;
            happy_licznik = 0;
            if (wynikL%20==0 & wynikL!=600) tempo--;
        }
    

    
        ~Gra() { Console.WriteLine("Usunieto Gra"); }
    }
}
