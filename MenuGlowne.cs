using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.IO;

class MenuGlowne {
    bool dzialaj; //Czy klasa ma dalej działać czy zakończyć działanie
    public static Font czcionka = new Font("youmurdererbb_reg.otf");
    static Text[] pozycje_menu_text= new Text[10]; //Napisy pozycji w menu
    static int gdzie_jestem = 0; // 0 - menu głowne  1 - tryby gry  2 - High Scores 
    static int pozycjamenu = 1; //Aktualnie najechana pozycja w menu
    static byte odcien = 100; //Aktualny odcien tętniącej pozycji w menu
    static bool zwrot; //Rozjasniac czy sciemniac tętniącą pozycję w menu
    Gra gra;
    public MenuGlowne()
    {
        for (int i = 0; i < 10; i++) pozycje_menu_text[i] = new Text("", czcionka, 90);
        dzialaj = true;
        Program.okienko.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

        Wyswietl_Menu_Glowne();
        while (dzialaj){
      
            Tetniaca_pozycja();
            Program.okienko.DispatchEvents();
            Program.okienko.Display();
            
            System.Threading.Thread.Sleep(50); //Nie zużywaj 100% procka :c + Reguluje szybkosc tętnienia pozycji w menu
            Program.okienko.Clear();
        }
    }
    void Wyswietl_Menu_Glowne()
    {
        pozycje_menu_text[0].DisplayedString = "Nowa Gra";
        pozycje_menu_text[0].Color = new Color(255, 40, 40);
        pozycje_menu_text[0].Position = new Vector2f(370, 215);
        

        pozycje_menu_text[1].DisplayedString = "Wyniki";
        pozycje_menu_text[1].Color = new Color(255, 40, 40);
        pozycje_menu_text[1].Position = new Vector2f(410, 290);
       

        pozycje_menu_text[2].DisplayedString = "Wyjscie";
        pozycje_menu_text[2].Color = new Color(255, 40, 40);
        pozycje_menu_text[2].Position = new Vector2f(390, 360);
        
    }
    void Wyswietl_Tryby_Gry() {
        pozycje_menu_text[0].DisplayedString = "Mnozenie";
        pozycje_menu_text[0].Color = new Color(255, 40, 40);
        pozycje_menu_text[0].Position = new Vector2f(370, 225);
        

        pozycje_menu_text[1].DisplayedString = "Dodawanie";
        pozycje_menu_text[1].Color = new Color(255, 40, 40);
        pozycje_menu_text[1].Position = new Vector2f(355, 290);
        

        pozycje_menu_text[2].DisplayedString = "Odejmowanie";
        pozycje_menu_text[2].Color = new Color(255, 40, 40);
        pozycje_menu_text[2].Position = new Vector2f(325, 360);
        
    }
    void Wyswietl_High_Scores() {

        pozycje_menu_text[0].DisplayedString = "Wyniki";
        pozycje_menu_text[0].Color = new Color(255, 40, 40);
        pozycje_menu_text[0].Position = new Vector2f(380, 10);
        

        StreamReader readtext = new StreamReader("wyniki.txt");
        string bufor_wynikow;
        for (int i=1; i < 10; i++) {
            bufor_wynikow = readtext.ReadLine();
            pozycje_menu_text[i].DisplayedString = bufor_wynikow;
            pozycje_menu_text[i].Color = new Color(255, 40, 40);
            pozycje_menu_text[i].Position = new Vector2f(380, 50+i*50);
        }
        readtext.Close();
    }

    void OnKeyPressed(object sender, KeyEventArgs e){
        switch (e.Code)
        {
            case Keyboard.Key.Up:
                if (pozycjamenu - 1 > 0) pozycjamenu--;
                else pozycjamenu = 3;
                break;
            case Keyboard.Key.Down:
                if (pozycjamenu + 1 < 4) pozycjamenu++;
                else pozycjamenu = 1;
                break;
            case Keyboard.Key.Return:
                switch (gdzie_jestem) // 0 - menu głowne  1 - tryby gry  2 - High Scores
                {
                    case 0:
                        switch (pozycjamenu)
                        {
                            case 1:
                                Wyswietl_Tryby_Gry();
                                gdzie_jestem = 1;
                            break;
                            case 2:
                                Wyswietl_High_Scores();
                                gdzie_jestem = 2;
                            break;
                            case 3:
                                dzialaj = false;
                            break;
                        }     
                   break;
                    case 1:
                        switch (pozycjamenu)
                        {
                            case 1:
                                gra = new Gra("*");
                                break;
                            case 2:
                                gra = new Gra("+");
                                break;
                            case 3:
                                gra = new Gra("-");
                                break;
                        }
                        gra = null; //Przynęta dla GC
                        GC.Collect(); //Garbage Collector, sprzątnij mi niegraną grę
                        break;
                }
           break;
            case Keyboard.Key.Escape:
                switch (gdzie_jestem)// 0 - menu głowne  1 - tryby gry  2 - High Scores
                {
                    case 0:
                        dzialaj = false;
                        break;
                    case 1:
                        gdzie_jestem = 0;
                        Wyswietl_Menu_Glowne();
                        break;
                    case 2:
                        gdzie_jestem = 0;
                        for (int i = 3; i < 10; i++) pozycje_menu_text[i].DisplayedString = "";
                        Wyswietl_Menu_Glowne();
                        break;
                }
                break;
        }
    }

    void Tetniaca_pozycja() {

        switch (pozycjamenu){
            case 1:
                pozycje_menu_text[0].Color = new Color(255, odcien, odcien);
                pozycje_menu_text[1].Color = new Color(255, 20, 20);
                pozycje_menu_text[2].Color = new Color(255, 20, 20);
                break;
            case 2:
                pozycje_menu_text[0].Color = new Color(255, 20, 20);
                pozycje_menu_text[1].Color = new Color(255, odcien, odcien);
                pozycje_menu_text[2].Color = new Color(255, 20, 20);
                break;
            case 3:
                pozycje_menu_text[0].Color = new Color(255, 20, 20);
                pozycje_menu_text[1].Color = new Color(255, 20, 20);
                pozycje_menu_text[2].Color = new Color(255, odcien, odcien);
                break;
        }

        if (odcien == 210) zwrot = false;
        if (odcien == 10) zwrot = true;
        if (zwrot == true) odcien= (byte)(odcien+10);
            else odcien= (byte)(odcien-10);
        for (int i = 0; i < 10; i++) Program.okienko.Draw(pozycje_menu_text[i]);
    }
}
