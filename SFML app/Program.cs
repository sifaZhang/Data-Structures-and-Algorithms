using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_app;
using System;
using static SFML.Window.Mouse;

namespace RotatingHelloWorldSfmlDotNetCoreCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ESC key to close window");
            MyWindow window = new MyWindow();
            window.Show();
            Console.WriteLine("All done");
        }
    }

    class MyWindow
    {
        public void Show()
        {
            VideoMode mode = new VideoMode(1980, 1024);
            RenderWindow window = new RenderWindow(mode, "SFML.NET");
            Font font = new Font("C:/Windows/Fonts/arial.ttf");
            MyButton btnEnqueue = new MyButton(new Vector2f(50, 900), new Vector2f(200, 100), "Enquene", font);
            MyButton btnDequeue = new MyButton(new Vector2f(270, 900), new Vector2f(200, 100), "Dequene", font);
            MyButton btnFetch = new MyButton(new Vector2f(490, 900), new Vector2f(200, 100), "Fetch", font);
            MyButton btnClear = new MyButton(new Vector2f(710, 900), new Vector2f(200, 100), "Clear", font);

            bool mouseHeld = false;
            bool mouseReleased = false;

            window.Closed += (obj, e) => { window.Close(); };
            window.MouseButtonPressed += (_, e) => { if (e.Button == Mouse.Button.Left) mouseHeld = true; };
            window.MouseButtonReleased += (_, e) => { if (e.Button == Mouse.Button.Left) { mouseHeld = false; mouseReleased = true; } };

            while (window.IsOpen)
            {
                window.DispatchEvents();

                Vector2i mousePos = Mouse.GetPosition(window);
                btnClear.Update(mousePos, mouseHeld);
                btnFetch.Update(mousePos, mouseHeld);
                btnDequeue.Update(mousePos, mouseHeld);
                btnEnqueue.Update(mousePos, mouseHeld);

                if (btnClear.IsClicked(mousePos, mouseReleased))
                {
                    Console.WriteLine("按钮 btnClear 被点击");
                }
                else if (btnFetch.IsClicked(mousePos, mouseReleased))
                {
                    Console.WriteLine("按钮 btnFetch 被点击");
                }
                else if (btnDequeue.IsClicked(mousePos, mouseReleased))
                {
                    Console.WriteLine("按钮 btnDequeue 被点击");
                }
                else if (btnEnqueue.IsClicked(mousePos, mouseReleased))
                {
                    Console.WriteLine("按钮 btnEnqueue 被点击");
                }
                mouseReleased = false;

                //window.Clear(new Color(70, 80, 100));
                window.Clear(new Color(0, 50, 50));
                btnClear.Draw(window);
                btnEnqueue.Draw(window);
                btnDequeue.Draw(window);
                btnFetch.Draw(window);
                window.Display();
            }

        }
    }
}