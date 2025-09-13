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
        private StringBuilder input = new StringBuilder();
        private Color normalColor = new Color(0, 50, 50);

        public MyLabel(Vector2f position, Vector2f size, Font font)
        {
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
