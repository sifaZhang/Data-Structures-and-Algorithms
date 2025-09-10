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
        private Color normalColor = new Color(0, 120, 215);       // 蓝色
        private Color hoverColor = new Color(70, 130, 180);        // SteelBlue
        private Color pressedColor = new Color(30, 144, 255);      // DodgerBlue
        private bool isPressed = false;

        public MyButton(Vector2f position, Vector2f size, string text, Font font)
        {
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = normalColor
            };

            label = new SFML.Graphics.Text(text, font, 28)
            {
                FillColor = Color.White,
            };

            // 设置原点为文字的中心
            SFML.Graphics.FloatRect textBounds = label.GetLocalBounds();
            label.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2f, textBounds.Top + textBounds.Height / 2f);

            // 设置位置为按钮中心
            Vector2f buttonCenter = new Vector2f(
            shape.Position.X + shape.Size.X / 2f,
            shape.Position.Y + shape.Size.Y / 2f);

            label.Position = buttonCenter;
        }

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

        public bool IsClicked(Vector2i mousePos, bool mouseReleased)
        {
            return shape.GetGlobalBounds().Contains(mousePos.X, mousePos.Y) && mouseReleased;
        }

        public void Draw(RenderWindow window)
        {
            float radius = 10f;

            // 主矩形尺寸
            Vector2f size = shape.Size;
            Vector2f position = shape.Position;
            Color fill = shape.FillColor;

            // 中间矩形（去掉圆角区域）
            RectangleShape center = new RectangleShape(new Vector2f(size.X - 2 * radius, size.Y - 2 * radius));
            center.Position = new Vector2f(position.X + radius, position.Y + radius);
            center.FillColor = fill;

            // 四条边
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

            // 四个圆角（四分之一圆）
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

            // 绘制所有部分
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
