using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Data;
using System.IO;

class Gra {
  string tryb_gry;

  int licznik = 150; //DeFacto położenie platformy; więcej=niżej
  int wynik_liczba = 0;
  int liczba1, liczba2; //skladniki rownania 
  int[] odpowiedzi = new int[3];
  int czas_szczesliwy = 0; //ile czasu jeszcze ma być szczęśliwy
  int blabalblablaba;

  bool dzialaj = true;

  Texture tlo = new Texture("canva.png"); //Tło "normalne"
  Texture tlo2 = new Texture("canvaS2.png"); //Tło z aktywną strefą podwójnych punktów
  Texture pacmanNormal = new Texture("pacman.png");
  Texture pacmanHappy = new Texture("pacmanHappy.png");
  Texture pacmanDead = new Texture("pacmanDead.png");
  Texture pacmanWorried = new Texture("pacmanWorried.png");
  Texture platforma = new Texture("platforma2.png");
  Texture krewtlo = new Texture("krewtlo.png");
  Texture czaszka = new Texture("skulltabliczka.png");
  Texture[] krew_animacja = new Texture[16];

  Sprite tlo_sprite,
  pacman_sprite,
  platforma_sprite,
  krew_lewy_sprite,
  krew_prawy_sprite,
  krew_tlo_sprite,
  krew_tlo_prawy_sprite,
  czaszka1_sprite,
  czaszka2_sprite,
  czaszka3_sprite;

  Music
  dobrze,
  zle,
  koniec,
  podklad_muzyczny;

  Random rnd = new Random(); //generator liczb losowych

  Text[] odpowiedzi_text = new Text[3];
  Text rownanie_text = new Text("", MenuGlowne.czcionka, 200);
  Text wynik_text = new Text("0", czcionka2, 80);

  static Font czcionka2 = new Font("arial.ttf");

  DataTable dt = new DataTable();

  public Gra(string tryb_gry) {
    this.tryb_gry = tryb_gry;
    tlo_sprite = new Sprite(tlo);
    pacman_sprite = new Sprite(pacmanNormal);
    pacman_sprite.Position = new Vector2f(0, 650);
    platforma_sprite = new Sprite(platforma);
    for (int i = 0; i < 16; i++)
      krew_animacja[i] = new Texture("krewprawy3/" + i.ToString() + ".gif");
    krew_lewy_sprite = new Sprite();
    krew_prawy_sprite = new Sprite();
    krew_tlo_sprite = new Sprite(krewtlo);
    krew_tlo_prawy_sprite = new Sprite(krewtlo);
    czaszka1_sprite = new Sprite(czaszka);
    czaszka2_sprite = new Sprite(czaszka);
    czaszka3_sprite = new Sprite(czaszka);
    odpowiedzi_text[0] = new Text("", MenuGlowne.czcionka, 80);
    odpowiedzi_text[1] = new Text("", MenuGlowne.czcionka, 80);
    odpowiedzi_text[2] = new Text("", MenuGlowne.czcionka, 80);
    rownanie_text.Position = new Vector2f(370, 220);
    rownanie_text.Color = new Color(255, 20, 20);
    wynik_text.Position = new Vector2f(5, 0);

    Dzialaj();
  }
  void Dzialaj() {
    var uchwyt_klawiatura = new EventHandler < KeyEventArgs > (OnKeyPressed);
    Program.okienko.KeyPressed += uchwyt_klawiatura;

    dobrze = new Music("dobrze.wav");
    zle = new Music("zle.wav");
    koniec = new Music("koniec.wav");
    podklad_muzyczny = new Music("screamhorrormusic.flac");
    podklad_muzyczny.Play();

    while (dzialaj) {
      if (czas_szczesliwy != 0) czas_szczesliwy--;
      else pacman_sprite.Texture = pacmanNormal;

      if (licznik > 550) {
        pacman_sprite.Texture = (pacmanWorried);
        if (licznik > 768) Koniec();
      }
      platforma_sprite.Position = new Vector2f(118, licznik);

      krew_lewy_sprite.Texture = krew_prawy_sprite.Texture = krew_animacja[Math.Abs(licznik % 16)];
      krew_lewy_sprite.Position = new Vector2f(0, licznik);
      krew_prawy_sprite.Position = new Vector2f(908, licznik);
      krew_tlo_sprite.Position = new Vector2f(0, -768 + licznik);
      krew_tlo_prawy_sprite.Position = new Vector2f(908, -768 + licznik);
      odpowiedzi_text[0].Position = new Vector2f(268, licznik - 60);
      odpowiedzi_text[1].Position = new Vector2f(478, licznik - 60);
      odpowiedzi_text[2].Position = new Vector2f(690, licznik - 60);
      czaszka1_sprite.Position = new Vector2f(280, licznik + 6);
      czaszka2_sprite.Position = new Vector2f(490, licznik + 6);
      czaszka3_sprite.Position = new Vector2f(700, licznik + 6);

      Program.okienko.Draw(tlo_sprite);
      Program.okienko.Draw(pacman_sprite);
      Program.okienko.Draw(platforma_sprite);
      Program.okienko.Draw(krew_lewy_sprite);
      Program.okienko.Draw(krew_prawy_sprite);
      Program.okienko.Draw(krew_tlo_sprite);
      Program.okienko.Draw(krew_tlo_prawy_sprite);
      Program.okienko.Draw(odpowiedzi_text[0]);
      Program.okienko.Draw(odpowiedzi_text[1]);
      Program.okienko.Draw(odpowiedzi_text[2]);
      Program.okienko.Draw(rownanie_text);
      Program.okienko.Draw(wynik_text);
      Program.okienko.Draw(czaszka1_sprite);
      Program.okienko.Draw(czaszka2_sprite);
      Program.okienko.Draw(czaszka3_sprite);
      Program.okienko.DispatchEvents();
      Program.okienko.Display();
      System.Threading.Thread.Sleep(35);
      Program.okienko.Clear();
      licznik++;
    }
    Program.okienko.KeyPressed -= uchwyt_klawiatura;
  }

  void Sprawdz_Rownanie(int klawisz) {
    if ((int) dt.Compute(liczba1 + tryb_gry + liczba2, "") == odpowiedzi[klawisz]) Dobra_odpowiedz();
    else Zla_odpowiedz();

  }

  void Dobra_odpowiedz() {
    if (licznik > 140) {
      tlo_sprite.Texture = tlo;
      wynik_liczba++;
    } else {
      tlo_sprite.Texture = tlo2;
      wynik_liczba += 2;
    }
    wynik_text.DisplayedString = "" + wynik_liczba;
    licznik -= 55;
    dobrze.Play();
    czas_szczesliwy = 30;
    pacman_sprite.Texture = pacmanHappy;

    if (tryb_gry == "*") liczba1 = rnd.Next(1, 10);
    else liczba1 = rnd.Next(1, 100);

    liczba2 = rnd.Next(1, 10);

    switch (rnd.Next(3)) {
    case 0:
      odpowiedzi[0] = (int) dt.Compute(liczba1 + tryb_gry + liczba2, "");
      odpowiedzi[1] = rnd.Next(1, 100);
      odpowiedzi[2] = rnd.Next(1, 100);
      break;
    case 1:
      odpowiedzi[0] = rnd.Next(1, 100);
      odpowiedzi[1] = (int) dt.Compute(liczba1 + tryb_gry + liczba2, "");
      odpowiedzi[2] = rnd.Next(1, 100);
      break;
    case 2:
      odpowiedzi[0] = rnd.Next(1, 100);
      odpowiedzi[1] = rnd.Next(1, 100);
      odpowiedzi[2] = (int) dt.Compute(liczba1 + tryb_gry + liczba2, "");
      break;
    }
    for (int i = 0; i < 3; i++) odpowiedzi_text[i].DisplayedString = "" + odpowiedzi[i];
    rownanie_text.DisplayedString = liczba1 + tryb_gry + liczba2;

  }

  void Zla_odpowiedz() {
    licznik += 35;
    zle.Play();
  }

  void OnKeyPressed(object sender, KeyEventArgs e) {
    switch (e.Code) {
    case Keyboard.Key.Escape:
      dzialaj = false;
      break;
    case Keyboard.Key.Left:
      Sprawdz_Rownanie(0);
      break;
    case Keyboard.Key.Up:
      Sprawdz_Rownanie(1);
      break;
    case Keyboard.Key.Right:
      Sprawdz_Rownanie(2);
      break;
    }
  }

  void Koniec() {
    podklad_muzyczny.Stop();
    koniec.Play();
    Program.okienko.Clear();
    wynik_text = new Text("" + wynik_liczba, czcionka2, 300);
    wynik_text.Position = new Vector2f(450, 130);
    pacman_sprite.Texture = pacmanDead;
    Program.okienko.Draw(wynik_text);
    Program.okienko.Draw(pacman_sprite);
    Program.okienko.DispatchEvents();
    Program.okienko.Display();

    StreamWriter writetext = new StreamWriter("wyniki.txt", true);
    writetext.WriteLine(wynik_liczba);
    writetext.Close();

    System.Threading.Thread.Sleep(4100);
    dzialaj = false;
  }~Gra() {
    Console.WriteLine("Usunieto Gra");
  }
}
