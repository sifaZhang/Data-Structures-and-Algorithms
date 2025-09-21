using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_app;
using System;
using System.Text.RegularExpressions;
using static SFML.Window.Mouse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RotatingHelloWorldSfmlDotNetCoreCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ESC key to close window");
            MyWindow window = new MyWindow();
            window.Run();
            Console.WriteLine("All done");
        }
    }

    class MyWindow
    {
        private RenderWindow renderWindow;
        private Font font;
        private MyTextBox txtInput;
        private MyLabel errInput;
        private MyButton btnEnqueue;
        private MyButton btnDequeue;
        private MyButton btnFetch;
        private MyButton btnClear;
        private QueueVisualizer queue;
        private bool mouseHeld;
        private bool mouseReleased;
        private Color defaultColor = new Color(200, 230, 255);
        private ConvexShape arrow;
        private Text headLabel;
        private bool roate = true;
        private Clock clock;
        private float delta, angle, angleSpeed;

        public MyWindow()
        {
            VideoMode mode = new VideoMode(1980, 900);
            renderWindow = new RenderWindow(mode, "SFML.NET");

            renderWindow.Closed += Window_Closed;
            renderWindow.KeyPressed += Window_KeyPressed;
            renderWindow.KeyReleased += Window_KeyReleased;

            font = new Font("C:/Windows/Fonts/arial.ttf");

            txtInput = new MyTextBox(new Vector2f(50, 100), new Vector2f(200, 50), font);
            errInput = new MyLabel(new Vector2f(300, 100), new Vector2f(400, 50), font);

            btnEnqueue = new MyButton(new Vector2f(50, 700), new Vector2f(200, 100), "Enquene", font);
            btnDequeue = new MyButton(new Vector2f(300, 700), new Vector2f(200, 100), "Dequene", font);
            btnFetch = new MyButton(new Vector2f(550, 700), new Vector2f(200, 100), "Fetch", font);
            btnClear = new MyButton(new Vector2f(800, 700), new Vector2f(200, 100), "Clear", font);

            queue = new QueueVisualizer(font, new Vector2f(50, 260));
            queue.SetColor(defaultColor);

            arrow = new ConvexShape(3);
            headLabel = new Text("Head", font, 28)
            {
                FillColor = Color.Red,
                Position = new Vector2f(-1, -1)
            };

            clock = new Clock();
            delta = 0;
            angle = 0;
            angleSpeed = 90f; // degrees per second

            mouseHeld = false;
            mouseReleased = false;

            renderWindow.Closed += (obj, e) => { renderWindow.Close(); };
            renderWindow.MouseButtonPressed += (_, e) => {
                if (e.Button == Mouse.Button.Left) mouseHeld = true;
                Vector2i mousePos = Mouse.GetPosition(renderWindow);
                txtInput.CheckFocus(mousePos);
            };

            renderWindow.MouseButtonReleased += (_, e) => {
                if (e.Button == Mouse.Button.Left) { mouseHeld = false; mouseReleased = true; }
            };

            renderWindow.TextEntered += (sender, e) =>
            {
                txtInput.HandleEvent(e);
            };
        }

        public void Run()
        {
            while (this.renderWindow.IsOpen)
            {
                Upate();
                Draw();
            }
        }

        private void UpdateClearButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnClear.Update(mousePos, mouseHeld);

            if (btnClear.IsClicked(mousePos, mouseReleased))
            {
                queue.Clear();
                errInput.SetText("Queue has been cleared.");
            }
        }

        private void UpdateFetchButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnFetch.Update(mousePos, mouseHeld);
            if (btnFetch.IsClicked(mousePos, mouseReleased))
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
        }

        private void UpdateEnqueueButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnEnqueue.Update(mousePos, mouseHeld);
            if (btnEnqueue.IsClicked(mousePos, mouseReleased))
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
        }

        private void UpdateDequeueButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnDequeue.Update(mousePos, mouseHeld);
            if (btnDequeue.IsClicked(mousePos, mouseReleased))
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
        }

        public void Upate()
        {
            renderWindow.DispatchEvents();

            UpdateClearButton();
            UpdateFetchButton();
            UpdateEnqueueButton();
            UpdateDequeueButton();

            mouseReleased = false;

            renderWindow.Clear(new Color(0, 50, 50));
        }

        private void DrawArrow()
        {
            if (queue.Empty()) return;

            Vector2f headPos = queue.GetHeadPos();
            //Console.WriteLine(headPos);

            ConvexShape arrow = new ConvexShape(3);
            arrow.SetPoint(0, new Vector2f(headPos.X - 20, headPos.Y + 170));
            arrow.SetPoint(1, new Vector2f(headPos.X + 20, headPos.Y + 170));
            arrow.SetPoint(2, new Vector2f(headPos.X, headPos.Y + 140));
            arrow.FillColor = Color.Red;
            renderWindow.Draw(arrow);

            headLabel.Position = new Vector2f(headPos.X, headPos.Y + 230);

            if(roate)
            {
                // 设置旋转中心为文本中心
                var bounds = headLabel.GetLocalBounds();
                headLabel.Origin = new SFML.System.Vector2f(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f);

                // 设置位置和旋转
                float delta = clock.Restart().AsSeconds();
                angle += angleSpeed * delta;
                headLabel.Rotation = angle;
            }

            renderWindow.Draw(headLabel);
        }
        public void Draw()
        {
            btnClear.Draw(renderWindow);
            btnEnqueue.Draw(renderWindow);
            btnDequeue.Draw(renderWindow);
            btnFetch.Draw(renderWindow);

            txtInput.Draw(renderWindow);
            errInput.Draw(renderWindow);

            queue.Draw(renderWindow);

            DrawArrow();

            renderWindow.Display();
        }

        private void Window_KeyPressed(object? sender, KeyEventArgs e)
        {
            Window? window = (Window?)sender;
            if (window != null)
            {
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }

                if (e.Code == Keyboard.Key.Space)
                {
                    roate = false;
                    queue.SetColor(Color.Yellow);
                }
            }
        }

        private void Window_KeyReleased(object? sender, KeyEventArgs e)
        {
            Window? window = (Window?)sender;
            if (e.Code == Keyboard.Key.Space)
            {
                roate = true;
                queue.SetColor(defaultColor);
            }
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            Window? window = (Window?)sender;
            if(window != null)window.Close();
        }
    }
}