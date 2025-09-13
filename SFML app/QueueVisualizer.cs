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
        private Queue<int> queue = new Queue<int>();
        private Font font;
        private Vector2f startPosition;
        private float radius = 100f;
        private float spacing = 250f;

        public QueueVisualizer(Font font, Vector2f startPosition)
        {
            this.font = font;
            this.startPosition = startPosition;
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

        public void Draw(RenderWindow window)
        {
            float x = startPosition.X;
            float y = startPosition.Y;
            int index = 0;

            foreach (int value in queue)
            {
                CircleShape circle = new CircleShape(radius)
                {
                    FillColor = new Color(200, 230, 255),
                    //OutlineColor = Color.Black,
                    //OutlineThickness = 2,
                    Position = new Vector2f(x, y)
                };

                Text label = new Text(value.ToString(), font, 50)
                {
                    FillColor = Color.Black,
                };

                // 获取文字边界
                FloatRect textBounds = label.GetLocalBounds();

                // 设置原点为文字中心
                label.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2, textBounds.Top + textBounds.Height / 2);

                // 设置位置为圆形中心
                label.Position = new Vector2f(x + radius, y + radius);

                window.Draw(circle);
                window.Draw(label);

                x += spacing;
                index++;
            }
        }
    }

}
