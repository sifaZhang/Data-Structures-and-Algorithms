using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_app
{
    internal class MyTextBox
    {
        private RectangleShape box;
        private Text text;
        private StringBuilder input = new StringBuilder();
        private bool isFocused = false;

        private Color normalColor = Color.White;
        private Color focusedColor = new Color(230, 230, 255); // 淡蓝色

        public MyTextBox(Vector2f position, Vector2f size, Font font)
        {
            box = new RectangleShape(size)
            {
                Position = position,
                FillColor = normalColor,
                OutlineColor = Color.Black,
                OutlineThickness = 2
            };

            text = new Text("", font, 18)
            {
                FillColor = Color.Black,
                Position = new Vector2f(position.X + 5, position.Y + 5)
            };
        }

        public void HandleEvent(Event e)
        {
            if (!isFocused) return;

            if (e.Type == EventType.TextEntered)
            {
                uint unicode = e.Text.Unicode;
                if (unicode == 8 && input.Length > 0) // Backspace
                {
                    input.Remove(input.Length - 1, 1);
                }
                else if (unicode >= 32 && unicode < 128) // Printable ASCII
                {
                    input.Append((char)unicode);
                }
                text.DisplayedString = input.ToString();
            }
        }

        public void CheckFocus(Vector2i mousePos)
        {
            isFocused = box.GetGlobalBounds().Contains(mousePos.X, mousePos.Y);
            box.FillColor = isFocused ? focusedColor : normalColor;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(box);
            window.Draw(text);
        }

        public string GetText()
        {
            return input.ToString();
        }
    }

}
