using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace JPWP_Projekt {
    
    class Program {
        ///Podstawowa klasa pelniaca role przelacznika pomiedzy Menu gry, a sama gra
        static void Main(string[] args) {

            MenuGlowne a = new MenuGlowne();
            Gra b = new Gra();
            a.Dzialaj();
            while (true)
            {
                if (a.stan == false)
                {
                    if (a.wylacz) break; ///Wylaczenie aplikacji (wybranie 'Wyjście' w menu)
                    b.Dzialaj(a.tryb);
                }
                if (b.stan == false)
                {
                    a.Dzialaj();
                }
            }
            
        }

    }
}