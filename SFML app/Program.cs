using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_app;
using System;
using System.Text.RegularExpressions;
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
            VideoMode mode = new VideoMode(1980, 900);
            RenderWindow window = new RenderWindow(mode, "SFML.NET");
            Font font = new Font("C:/Windows/Fonts/arial.ttf");

            MyTextBox txtInput = new MyTextBox(new Vector2f(50, 100), new Vector2f(200, 50), font);
            MyLabel errInput = new MyLabel(new Vector2f(300, 100), new Vector2f(400, 50), font);

            MyButton btnEnqueue = new MyButton(new Vector2f(50, 700), new Vector2f(200, 100), "Enquene", font);
            MyButton btnDequeue = new MyButton(new Vector2f(300, 700), new Vector2f(200, 100), "Dequene", font);
            MyButton btnFetch = new MyButton(new Vector2f(550, 700), new Vector2f(200, 100), "Fetch", font);
            MyButton btnClear = new MyButton(new Vector2f(800, 700), new Vector2f(200, 100), "Clear", font);

            QueueVisualizer queue = new QueueVisualizer(font, new Vector2f(50, 300));

            bool mouseHeld = false;
            bool mouseReleased = false;

            window.Closed += (obj, e) => { window.Close(); };
            window.MouseButtonPressed += (_, e) => {
                if (e.Button == Mouse.Button.Left) mouseHeld = true;
                Vector2i mousePos = Mouse.GetPosition(window);
                txtInput.CheckFocus(mousePos);
            };

            window.MouseButtonReleased += (_, e) => {
                if (e.Button == Mouse.Button.Left) { mouseHeld = false; mouseReleased = true; } 
            };
            
            window.TextEntered += (sender, e) =>
            {
                txtInput.HandleEvent(e);
            };

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
                    queue.Clear();
                    errInput.SetText("Queue has been cleared.");
                }
                else if (btnFetch.IsClicked(mousePos, mouseReleased))
                {
                    int? value = queue.Fetch();
                    if (value.HasValue)
                    {
                        errInput.SetText("The top node is " + value.Value.ToString());
                    }
                    else
                    {
                        errInput.SetText("Queue is empty.");
                    }
                }
                else if (btnDequeue.IsClicked(mousePos, mouseReleased))
                {
                    if (queue.Empty())
                    {
                        errInput.SetText("Queue is empty.");
                    }
                    else
                    {
                        int? value = queue.Fetch();
                        if (value.HasValue)
                        {
                            errInput.SetText("Node(" + value.Value.ToString() + ") has been popped.");
                        }
                        queue.Dequeue();
                    }
                }
                else if (btnEnqueue.IsClicked(mousePos, mouseReleased))
                {
                    if (int.TryParse(txtInput.GetText(), out int value))
                    {
                        queue.Enqueue(value);
                        txtInput.SetText("");
                        errInput.SetText("");
                    }
                    else
                    {
                        errInput.SetText("Invalid input.");
                    }
                }
                mouseReleased = false;

                window.Clear(new Color(0, 50, 50));
                btnClear.Draw(window);
                btnEnqueue.Draw(window);
                btnDequeue.Draw(window);
                btnFetch.Draw(window);
                txtInput.Draw(window);
                errInput.Draw(window);
                queue.Draw(window);
                window.Display();
            }

        }
    }
}