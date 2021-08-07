using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using System.IO;

namespace JPWP_Projekt {

    class MenuGlowne {
        ///Klasa obslugujaca Menu gry
        
        Font czcionka;
        Text textNowagra;
        Text textWyniki;
        Text textWyjscie;
        ///Zmienna określająca czy ma nastąpić przełączenie na grę
        public bool stan; 
        RenderWindow Kwindow;
        ///Deklaruje chęć zamknięcia gry
        public bool wylacz; 
        int pozycjamenu;
        public bool pokazwyniki; 
        public bool pokaztryby;
        ///Tabela wyników
        Text[]wyniki; 
        Text wynikitekst;
        ///Zmienna wykorzystywana do zmiany treści pozycji w menu
        string nazwy;
        /// Wybrany tryb gry (mnożenie/dodawanie/odejmowanie)
        public int tryb; 
        

        public MenuGlowne() {
            wylacz = false;
            stan = true;
            czcionka = new Font("youmurdererbb_reg.otf");

            textNowagra = new Text("Nowa gra", czcionka, 90);
            textNowagra.Position = new Vector2f(370, 210);
            textNowagra.Color = new Color(255, 100, 100);

            textWyniki = new Text("Wyniki", czcionka, 90);
            textWyniki.Position = new Vector2f(410, 290);
            textWyniki.Color = new Color(255, 40, 40);

            textWyjscie = new Text("Wyjscie", czcionka, 90);
            textWyjscie.Position = new Vector2f(390, 360);
            textWyjscie.Color = new Color(255, 40, 40);

            wyniki = new Text[10];
            wyniki[0] = new Text("", czcionka, 90);
            wyniki[1] = new Text("", czcionka, 90);
            wyniki[2] = new Text("", czcionka, 90);
            wyniki[3] = new Text("", czcionka, 90);
            wyniki[4] = new Text("", czcionka, 90);
            wyniki[5] = new Text("", czcionka, 90);
            wyniki[6] = new Text("", czcionka, 90);
            wyniki[7] = new Text("", czcionka, 90);
            wyniki[8] = new Text("", czcionka, 90);
            wyniki[9] = new Text("", czcionka, 90);

            wyniki[0].Position = new Vector2f(450, 100+10);
            wyniki[1].Position = new Vector2f(450, 150+10);
            wyniki[2].Position = new Vector2f(450, 200+10);
            wyniki[3].Position = new Vector2f(450, 250+10);
            wyniki[4].Position = new Vector2f(450, 300+10);
            wyniki[5].Position = new Vector2f(450, 350+10);
            wyniki[6].Position = new Vector2f(450, 400+10);
            wyniki[7].Position = new Vector2f(450, 450+10);
            wyniki[8].Position = new Vector2f(450, 500+10);
            wyniki[9].Position = new Vector2f(450, 550+10);

            wyniki[0].Color = new Color(255, 40, 40);
            wyniki[1].Color = new Color(255, 40, 40);
            wyniki[2].Color = new Color(255, 40, 40);
            wyniki[3].Color = new Color(255, 40, 40);
            wyniki[4].Color = new Color(255, 40, 40);
            wyniki[5].Color = new Color(255, 40, 40);
            wyniki[6].Color = new Color(255, 40, 40);
            wyniki[7].Color = new Color(255, 40, 40);
            wyniki[8].Color = new Color(255, 40, 40);
            wyniki[9].Color = new Color(255, 40, 40);
            wynikitekst= new Text("Wyniki", czcionka, 120);
            wynikitekst.Position = new Vector2f(380, 10);
            wynikitekst.Color = new Color(255, 40, 40);
            pozycjamenu = 1;

            pokazwyniki = false;
            pokaztryby = false;
            
        }

        public void Dzialaj() {
            ///Podstawowa funkcja podtrzymujaca dzialanie menu
            var window = new RenderWindow(new VideoMode(1024, 768), "Gra", Styles.Fullscreen);
            window.SetMouseCursorVisible(false);
            Kwindow = null;
            Kwindow = window;
            Kwindow.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            stan = true;
            byte i = 1;
            ///Obsługa tętniących pozycji w menu
            bool zwrot = true; 

            while (Kwindow.IsOpen)
            {
                if (pokazwyniki == true) { 
                    using (StreamReader readtext = new StreamReader("wyniki.txt"))
                    {
                        string readMeText = readtext.ReadLine();
                        wyniki[0].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[1].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[2].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[3].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[4].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[5].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[6].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[7].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[8].DisplayedString = readMeText;
                        readMeText = readtext.ReadLine();
                        wyniki[9].DisplayedString = readMeText;
                    }
                    while (pokazwyniki == true)
                    {//Wyświetlanie wyników
                        Kwindow.Clear();
                        Kwindow.Draw(wynikitekst);
                    Kwindow.Draw(wyniki[0]);
                        Kwindow.Draw(wyniki[1]);
                        Kwindow.Draw(wyniki[2]);
                        Kwindow.Draw(wyniki[3]);
                        Kwindow.Draw(wyniki[4]);
                        Kwindow.Draw(wyniki[5]);
                        Kwindow.Draw(wyniki[6]);
                        Kwindow.Draw(wyniki[7]);
                        Kwindow.Draw(wyniki[8]);
                        Kwindow.Draw(wyniki[9]);
                        Kwindow.DispatchEvents();
                    Kwindow.Display();

                }
            }
                System.Threading.Thread.Sleep(5);
                if (stan)
                {
                    Kwindow.Clear();

                    
                    Kwindow.Draw(textNowagra);
                    Kwindow.Draw(textWyniki);
                    Kwindow.Draw(textWyjscie);

                    

                    Kwindow.DispatchEvents();
                    Kwindow.Display();
                    if (pozycjamenu == 1) textNowagra.Color = new Color(255, i, i);
                    if (pozycjamenu == 2) textWyniki.Color = new Color(255, i, i);
                    if (pozycjamenu == 3) textWyjscie.Color = new Color(255, i, i);
                    
                    if (i == 210) zwrot=false;
                    if (i == 10) zwrot = true;
                    if (zwrot == true) i++;
                    else i--;
                }

                //Math.Abs(i);
                else break;
            }
            
        }

        void ResetujNazwy()
        {
            ///Przywrócenie podstawowych nazw
            pokaztryby = false;
            nazwy = "Nowa gra";
            textNowagra.DisplayedString = nazwy;
            textNowagra.Position = new Vector2f(370, 215);
            nazwy = "Wyniki";
            textWyniki.DisplayedString = nazwy;
            textWyniki.Position = new Vector2f(410, 290);
            nazwy = "Wyjscie";
            textWyjscie.DisplayedString = nazwy;
            textWyjscie.Position = new Vector2f(390, 360);
        }
        void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow Gwindow = (RenderWindow)sender;

            // Escape key : exit
            if (e.Code == Keyboard.Key.Escape)
            {
                pokazwyniki = false;
                ResetujNazwy();

            }

            if (e.Code == Keyboard.Key.Down)
            {

                if (pozycjamenu + 1 < 4) pozycjamenu++;
                else pozycjamenu = 1;
                if (pozycjamenu == 1) { textNowagra.Color = new Color(255, 100, 100); textWyjscie.Color = new Color(255, 40, 40); }
                if (pozycjamenu == 2) { textWyniki.Color = new Color(255, 100, 100); textNowagra.Color = new Color(255, 40, 40); }
                if (pozycjamenu == 3) { textWyjscie.Color = new Color(255, 100, 100); textWyniki.Color = new Color(255, 40, 40); }

            }

            if (e.Code == Keyboard.Key.Up)
            {

                if (pozycjamenu - 1 > 0) pozycjamenu--;
                else pozycjamenu = 3;
                if (pozycjamenu == 1) { textNowagra.Color = new Color(255, 100, 100); textWyniki.Color = new Color(255, 40, 40); }
                if (pozycjamenu == 2) { textWyniki.Color = new Color(255, 100, 100); textWyjscie.Color = new Color(255, 40, 40); }
                if (pozycjamenu == 3) { textWyjscie.Color = new Color(255, 100, 100); textNowagra.Color = new Color(255, 40, 40); }

            }

            if (e.Code == Keyboard.Key.Return)
            {

                if (pozycjamenu == 1)
                {
                    if (pokaztryby == false) {
                        pokaztryby = true;
                        nazwy = "Mnozenie";
                        textNowagra.DisplayedString = nazwy;
                        textNowagra.Position = new Vector2f(370, 225);
                        nazwy = "Dodawanie";
                        textWyniki.DisplayedString = nazwy;
                        textWyniki.Position = new Vector2f(355, 290);
                        nazwy = "Odejmowanie";
                        textWyjscie.DisplayedString = nazwy;
                        textWyjscie.Position = new Vector2f(325, 360);
                    }
                    else { tryb = 1; stan = false; ResetujNazwy(); Gwindow.Close();
                    }
                    
                }
                if (pozycjamenu == 2)
                {
                    if (pokaztryby == true) { tryb = 2; stan = false; ResetujNazwy(); Gwindow.Close(); }
                    else
                    pokazwyniki = true;
                }
                if (pozycjamenu == 3)
                {
                    if (pokaztryby == true) { tryb = 3; stan = false; ResetujNazwy(); Gwindow.Close(); }
                    else
                    {
                        stan = false;
                        Gwindow.Close();
                        wylacz = true;
                    }
                }
            }


            }
        ~MenuGlowne() { Console.WriteLine("Usunieto Menuglowne"); }
    }
}
