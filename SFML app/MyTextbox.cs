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

            text = new Text("", font, 28)
            {
                FillColor = Color.Black,
                Position = new Vector2f(position.X + 5, position.Y + 5)
            };
        }

        public void HandleEvent(TextEventArgs e)
        {
            if (!isFocused) return;

            string inputChar = e.Unicode;

            if (inputChar == "\b" && input.Length > 0) // Backspace
            {
                input.Remove(input.Length - 1, 1);
            }
            else if (inputChar.Length == 1 && inputChar[0] >= 32 && inputChar[0] < 128) // Printable ASCII
            {
                input.Append(inputChar);
            }

            text.DisplayedString = input.ToString();
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

        public void SetText(string newText)
        {
            input.Clear();
            input.Append(newText);
            text.DisplayedString = input.ToString();
        }
    }

}
