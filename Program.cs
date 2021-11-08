using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

class Program {
    public static RenderWindow okienko;
    

    public static void Main()
    {
        okienko = new RenderWindow(new VideoMode(1024, 768), "Gra", Styles.Fullscreen);
        okienko.SetMouseCursorVisible(false);
        MenuGlowne menuglowne = new MenuGlowne();
    }
}
