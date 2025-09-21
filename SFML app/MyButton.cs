//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/22
//This class implements a custom button with hover and click effects using SFML.

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SFML_app
{
    internal class MyButton
    {
        private RectangleShape shape;
        private Text label;
        private Color normalColor;
        private Color hoverColor;
        private Color pressedColor;
        private bool isPressed;

        /// <summary>
        /// constructor to initialize the button
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        public MyButton(Vector2f position, Vector2f size, string text, Font font)
        {
            isPressed = false;
            normalColor = new Color(0, 120, 215);       // blue
            hoverColor = new Color(70, 130, 180);        // SteelBlue
            pressedColor = new Color(30, 144, 255);      // DodgerBlue

            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = normalColor
            };

            label = new SFML.Graphics.Text(text, font, 28)
            {
                FillColor = Color.White,
            };

            // set the origin to the center of the text
            SFML.Graphics.FloatRect textBounds = label.GetLocalBounds();
            label.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2f, textBounds.Top + textBounds.Height / 2f);

            // set position to the center of the button
            Vector2f buttonCenter = new Vector2f(
                shape.Position.X + shape.Size.X / 2f,
                shape.Position.Y + shape.Size.Y / 2f);

            label.Position = buttonCenter;
        }

        /// <summary>
        /// update button state based on mouse position and click status
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="mouseHeld"></param>
        public void Update(Vector2i mousePos, bool mouseHeld)
        {
            FloatRect bounds = shape.GetGlobalBounds();
            if (bounds.Contains(mousePos.X, mousePos.Y))
            {
                shape.FillColor = mouseHeld ? pressedColor : hoverColor;
                isPressed = mouseHeld;
            }
            else
            {
                shape.FillColor = normalColor;
                isPressed = false;
            }
        }

        /// <summary>
        /// detect if the button is clicked
        /// </summary>
        /// <param name="mousePos"></param>
        /// <param name="mouseReleased"></param>
        /// <returns></returns>
        public bool IsClicked(Vector2i mousePos, bool mouseReleased)
        {
            return shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && mouseReleased;
        }

        /// <summary>
        /// draw the button with rounded corners
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            float radius = 10f;

            // the main rectangle parameters
            Vector2f size = shape.Size;
            Vector2f position = shape.Position;
            Color fill = shape.FillColor;

            // the center rectangle
            RectangleShape center = new RectangleShape(new Vector2f(size.X - 2 * radius, size.Y - 2 * radius));
            center.Position = new Vector2f(position.X + radius, position.Y + radius);
            center.FillColor = fill;

            // four edge rectangles
            RectangleShape top = new RectangleShape(new Vector2f(size.X - 2 * radius, radius));
            top.Position = new Vector2f(position.X + radius, position.Y);
            top.FillColor = fill;

            RectangleShape bottom = new RectangleShape(new Vector2f(size.X - 2 * radius, radius));
            bottom.Position = new Vector2f(position.X + radius, position.Y + size.Y - radius);
            bottom.FillColor = fill;

            RectangleShape left = new RectangleShape(new Vector2f(radius, size.Y - 2 * radius));
            left.Position = new Vector2f(position.X, position.Y + radius);
            left.FillColor = fill;

            RectangleShape right = new RectangleShape(new Vector2f(radius, size.Y - 2 * radius));
            right.Position = new Vector2f(position.X + size.X - radius, position.Y + radius);
            right.FillColor = fill;

            // four corner circles
            CircleShape corner = new CircleShape(radius, 30);
            corner.Origin = new Vector2f(radius, radius);
            corner.FillColor = fill;

            CircleShape topLeft = new CircleShape(corner);
            topLeft.Position = new Vector2f(position.X + radius, position.Y + radius);
            topLeft.Rotation = 180;

            CircleShape topRight = new CircleShape(corner);
            topRight.Position = new Vector2f(position.X + size.X - radius, position.Y + radius);
            topRight.Rotation = 270;

            CircleShape bottomLeft = new CircleShape(corner);
            bottomLeft.Position = new Vector2f(position.X + radius, position.Y + size.Y - radius);
            bottomLeft.Rotation = 90;

            CircleShape bottomRight = new CircleShape(corner);
            bottomRight.Position = new Vector2f(position.X + size.X - radius, position.Y + size.Y - radius);
            bottomRight.Rotation = 0;

            // draw all parts
            window.Draw(center);
            window.Draw(top);
            window.Draw(bottom);
            window.Draw(left);
            window.Draw(right);
            window.Draw(topLeft);
            window.Draw(topRight);
            window.Draw(bottomLeft);
            window.Draw(bottomRight);
            window.Draw(label);
        }
    }
}
