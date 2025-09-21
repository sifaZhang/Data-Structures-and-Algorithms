using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_app
{
    internal class QueueVisualizer
    {
        private Queue<int> queue;
        private Font font;
        private Vector2f startPosition;
        private float radius;
        private float spacing;
        private SFML.Graphics.Color fillColor;
        private Vector2f headPos;

        public QueueVisualizer(Font font, Vector2f startPosition)
        {
            queue = new Queue<int>();
            this.font = font;
            this.startPosition = startPosition;
            fillColor = new SFML.Graphics.Color(200, 230, 255);
            radius = 100f;
            spacing = 250f;
            headPos = new Vector2f();
        }

        public void Clear()
        {
            queue.Clear();
        }

        public int? Fetch()
        {
            return queue.Count > 0 ? queue.Peek() : (int?)null;
        }

        public void Enqueue(int value)
        {
            queue.Enqueue(value);
        }

        public bool Empty()
        {
            return queue.Count == 0;
        }
        
        public void Dequeue()
        {
            if (queue.Count > 0)
                queue.Dequeue();
        }

        public void SetColor(SFML.Graphics.Color color)
        {
            fillColor = color;
        }

        public Vector2f GetHeadPos()
        {
            return headPos;
        }

        public void Draw(RenderWindow window)
        {
            headPos.X = -1; // Reset head position
            
            float x = startPosition.X;
            float y = startPosition.Y;

            List<int> tempList = queue.ToList();
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                int value = tempList[i];
                
                CircleShape circle = new CircleShape(radius)
                {
                    FillColor = fillColor,
                    //OutlineColor = Color.Black,
                    //OutlineThickness = 2,
                    Position = new Vector2f(x, y)
                };

                Text label = new Text(value.ToString(), font, 50)
                {
                    FillColor = SFML.Graphics.Color.Black,
                };

                // 获取文字边界
                FloatRect textBounds = label.GetLocalBounds();

                // 设置原点为文字中心
                label.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2, textBounds.Top + textBounds.Height / 2);

                // 设置位置为圆形中心
                label.Position = new Vector2f(x + radius, y + radius);

                window.Draw(circle);
                window.Draw(label);

                if (i == 0) // Head
                {
                    headPos.X = circle.Position.X + radius;
                    headPos.Y = circle.Position.Y + radius;
                }

                x += spacing;
            }
        }
    }

}
