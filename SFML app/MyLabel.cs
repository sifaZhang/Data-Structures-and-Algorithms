//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/22
//This class implements a custom label for displaying text using SFML.

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_app
{
    internal class MyLabel
    {
        private RectangleShape box;
        private Text text;
        private StringBuilder input;
        private Color normalColor;

        /// <summary>
        /// constructor to initialize the label
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="font"></param>
        public MyLabel(Vector2f position, Vector2f size, Font font)
        {
            input = new StringBuilder();
            normalColor = new Color(0, 50, 50);

            box = new RectangleShape(size)
            {
                Position = position,
                FillColor = normalColor,
                OutlineColor = new Color(100, 100, 100),
                OutlineThickness = 2
            };

            text = new Text("", font, 28)
            {
                FillColor = Color.White,
                Position = new Vector2f(position.X + 5, position.Y + 5)
            };
        }

        /// <summary>
        /// draw the label
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            window.Draw(box);
            window.Draw(text);
        }

        /// <summary>
        /// get the text in the label
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return input.ToString();
        }

        /// <summary>
        /// set the text in the label
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
