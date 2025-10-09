//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/22
//This program creates a window using SFML.NET and visualizes a queue data structure with interactive buttons and text input.

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_app;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using static SFML.Window.Mouse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SFML_app
{
    /// <summary>
    /// this is the main program class
    /// </summary>
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

    /// <summary>
    /// Represents a graphical window for visualizing and interacting with a queue data structure.
    /// </summary>
    /// <remarks>This class provides a graphical interface for managing a queue, including operations such as
    /// enqueue, dequeue, fetch, and clear. It uses the SFML.NET library for rendering and handling user input. The
    /// window includes buttons, text input, and visual feedback for queue operations. The queue visualization is
    /// updated in real-time based on user interactions.</remarks>
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
        private SFML.Graphics.Color defaultColor;
        private ConvexShape arrow;
        private Text headLabel;
        private bool roate;
        private Clock clock;
        private float delta, angle, angleSpeed;
        private Rectangle rectQueue;
        private bool mouseClickQueueArea;

        /// <summary>
        /// constructor
        /// </summary>  
        public MyWindow()
        {
            // Load a font
            font = new Font("C:/Windows/Fonts/arial.ttf");

            //set the position of TextBox and Label
            txtInput = new MyTextBox(new Vector2f(50, 100), new Vector2f(200, 50), font);
            errInput = new MyLabel(new Vector2f(300, 100), new Vector2f(700, 50), font);
            
            // set the position of Buttons
            btnEnqueue = new MyButton(new Vector2f(50, 700), new Vector2f(200, 100), "Enquene", font);
            btnDequeue = new MyButton(new Vector2f(300, 700), new Vector2f(200, 100), "Dequene", font);
            btnFetch = new MyButton(new Vector2f(550, 700), new Vector2f(200, 100), "Fetch", font);
            btnClear = new MyButton(new Vector2f(800, 700), new Vector2f(200, 100), "Clear", font);

            // Initialize the queue visualizer
            mouseClickQueueArea = false; 
            rectQueue = new Rectangle(0, 260, 1900, 440);
            queue = new QueueVisualizer(font, new Vector2f(50, 260));
            defaultColor = new SFML.Graphics.Color(200, 230, 255);
            queue.SetColor(defaultColor);

            // Initialize head arrow
            arrow = new ConvexShape(3);
            arrow.FillColor = SFML.Graphics.Color.Red;
            roate = false;
            headLabel = new Text("Head", font, 28)
            {
                FillColor = SFML.Graphics.Color.Red,
                Position = new Vector2f(-1, -1)
            };

            // Initialize clock for rotation
            clock = new Clock();
            delta = 0;
            angle = 0;
            angleSpeed = 90f; // degrees per second

            mouseHeld = false;
            mouseReleased = false;

            // Create the main window
            VideoMode mode = new VideoMode(1900, 900);
            renderWindow = new RenderWindow(mode, "My Queue");
            renderWindow.Position = new Vector2i(10, 54);

            // Attach event handlers
            renderWindow.Closed += Window_Closed;
            renderWindow.KeyPressed += Window_KeyPressed;
            renderWindow.KeyReleased += Window_KeyReleased;

            renderWindow.Closed += (obj, e) => { renderWindow.Close(); };
            renderWindow.MouseButtonPressed += (_, e) => {
                if (e.Button == Mouse.Button.Left) mouseHeld = true;
                Vector2i mousePos = Mouse.GetPosition(renderWindow);
                txtInput.CheckFocus(mousePos);

                // Check if the mouse is in the queue area
                if (rectQueue.Contains(mousePos.X, mousePos.Y))
                {
                    mouseClickQueueArea = true;
                }
                else
                {
                    mouseClickQueueArea = false;
                }
            };

            renderWindow.MouseButtonReleased += (_, e) => {
                if (e.Button == Mouse.Button.Left) { 
                    mouseHeld = false; 
                    mouseReleased = true; 
                }

                // Handle clicks in the queue area
                if (mouseClickQueueArea)
                {
                    if (e.Button == Mouse.Button.Left)
                    {
                        EnqueueData();
                    }
                    else if (e.Button == Mouse.Button.Right)
                    {
                        DequeueData();
                    }
                }
            };

            renderWindow.TextEntered += (sender, e) =>
            {
                txtInput.HandleEvent(e);
            };
        }

        /// <summary>
        /// Starts the main application loop, which continues running while the render window remains open.
        /// </summary>
        /// <remarks>This method repeatedly calls the <c>Update</c> and <c>Draw</c> methods in a loop to
        /// handle application logic and rendering. The loop will terminate when the render window is closed.</remarks>
        public void Run()
        {
            while (this.renderWindow.IsOpen)
            {
                Upate();
                Draw();
            }
        }

        /// <summary>
        /// updates the state of the clear button and handles its click event.
        /// </summary>
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

        /// <summary>
        /// updates the state of the fetch button and handles its click event.
        /// </summary>
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

        /// <summary>
        /// add a new value to the queue from the text input or a random value if input is empty.
        /// </summary>
        private void EnqueueData()
        {
            int value = 0;
            if (txtInput.GetText() == "")
            {
                Random rnd = new Random();
                value = rnd.Next(0, 100);
            }
            else
            {
                if (!int.TryParse(txtInput.GetText(), out int valueTemp))
                {
                    errInput.SetText("Invalide input!");
                    txtInput.SetText("");
                    return;
                }

                value = valueTemp;
                if (value > 99 || value < 0)
                {
                    errInput.SetText("Please input a nunber between 0 and 99.");
                    txtInput.SetText("");
                    return;
                }
            }
           
            if (queue.Size() > 7)
            {
                errInput.SetText("The queue has reach to the maximum to show!");
                txtInput.SetText("");
            }
            else
            {
                queue.Enqueue(value);
                txtInput.SetText("");
                errInput.SetText("");
            }
        }

        /// <summary>
        /// removes the front value from the queue and displays it in the error label.
        /// </summary>
        private void DequeueData()
        {
            if (queue.Empty())
            {
                errInput.SetText("Queue is empty.");
            }
            else
            {
                int value;
                if (queue.Dequeue(out value))
                {
                    errInput.SetText("Node(" + value.ToString() + ") has been popped.");
                }
            }
        }

        /// <summary>
        /// updates the state of the enqueue button and handles its click event.
        /// </summary>
        private void UpdateEnqueueButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnEnqueue.Update(mousePos, mouseHeld);
            if (btnEnqueue.IsClicked(mousePos, mouseReleased))
            {
                EnqueueData();
            }
        }

        /// <summary>
        /// updates the state of the dequeue button and handles its click event.
        /// </summary>
        private void UpdateDequeueButton()
        {
            Vector2i mousePos = Mouse.GetPosition(renderWindow);
            btnDequeue.Update(mousePos, mouseHeld);
            if (btnDequeue.IsClicked(mousePos, mouseReleased))
            {
                DequeueData();
            }
        }

        /// <summary>
        /// updates the state of the window, including handling events and updating button states.
        /// </summary>
        public void Upate()
        {
            renderWindow.DispatchEvents();

            UpdateClearButton();
            UpdateFetchButton();
            UpdateEnqueueButton();
            UpdateDequeueButton();

            mouseReleased = false;

            renderWindow.Clear(new SFML.Graphics.Color(0, 50, 50));
        }

        private void DrawTip()
        {
            string tip = "Please run the game on (1920*1080)";
            Text tipText = new Text(tip, font, 20)
            {
                FillColor = SFML.Graphics.Color.White,
                Position = new Vector2f(1550, 860)
            };
            renderWindow.Draw(tipText);
        }

        /// <summary>
        /// updates and draws the arrow pointing to the head of the queue, including its rotation if enabled.
        /// </summary>
        private void DrawArrow()
        {
            if (queue.Empty()) return;

            Vector2f headPos = queue.GetHeadPos();
            //Console.WriteLine(headPos);

            arrow.SetPoint(0, new Vector2f(headPos.X - 20, headPos.Y + 170));
            arrow.SetPoint(1, new Vector2f(headPos.X + 20, headPos.Y + 170));
            arrow.SetPoint(2, new Vector2f(headPos.X, headPos.Y + 140));
            renderWindow.Draw(arrow);

            headLabel.Position = new Vector2f(headPos.X - 30, headPos.Y + 200);

            // handle rotation
            if (roate)
            {
                // set origin to center for proper rotation
                var bounds = headLabel.GetLocalBounds();
                headLabel.Origin = new SFML.System.Vector2f(bounds.Left + bounds.Width / 2f, bounds.Top + bounds.Height / 2f);

                // set position to be above the head
                delta = clock.Restart().AsSeconds();
                angle += angleSpeed * delta;
                headLabel.Rotation = angle;
            }

            renderWindow.Draw(headLabel);
        }

        /// <summary>
        /// draws the current state of the window, including buttons, text input, error messages, and the queue visualization.
        /// </summary>
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
            DrawTip();

            renderWindow.Display();
        }

        /// <summary>
        /// handles key press events for the window, including closing the window on ESC key and toggling rotation on SPACE key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    queue.SetColor(SFML.Graphics.Color.Yellow);
                }
            }
        }

        /// <summary>
        /// handles key release events for the window, specifically re-enabling rotation and resetting the queue color when the SPACE key is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyReleased(object? sender, KeyEventArgs e)
        {
            Window? window = (Window?)sender;
            if (e.Code == Keyboard.Key.Space)
            {
                //roate = true;
                queue.SetColor(defaultColor);
            }
        }

        /// <summary>
        /// handles the window closed event by closing the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object? sender, EventArgs e)
        {
            Window? window = (Window?)sender;
            if(window != null)window.Close();
        }
    }
}