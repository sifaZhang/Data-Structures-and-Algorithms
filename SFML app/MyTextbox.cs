//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/22
//This class implements a custom text box for user input using SFML.

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
        private StringBuilder input;
        private bool isFocused;
        private Color normalColor;
        private Color focusedColor;

        /// <summary>
        /// constructor to initialize the text box
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="font"></param>
        public MyTextBox(Vector2f position, Vector2f size, Font font)
        {
            input = new StringBuilder();
            isFocused = false;

            normalColor = Color.White;
            focusedColor = new Color(230, 230, 255); // 淡蓝色

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

        /// <summary>
        /// handle text input events
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// focus check based on mouse position
        /// </summary>
        /// <param name="mousePos"></param>
        public void CheckFocus(Vector2i mousePos)
        {
            isFocused = box.GetGlobalBounds().Contains(mousePos.X, mousePos.Y);
            box.FillColor = isFocused ? focusedColor : normalColor;
        }

        /// <summary>
        /// draw the text box onto the provided render window.
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            window.Draw(box);
            window.Draw(text);
        }

        /// <summary>
        /// gets the current text in the text box.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return input.ToString();
        }

        /// <summary>
        /// sets the text in the text box to a new value.
        /// </summary>
        /// <param name="newText"></param>
        public void SetText(string newText)
        {
            input.Clear();
            input.Append(newText);
            text.DisplayedString = input.ToString();
        }
    }

}
