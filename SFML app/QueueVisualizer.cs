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
        private float radius = 30f;
        private float spacing = 80f;

        public QueueVisualizer(Font font, Vector2f startPosition)
        {
            this.font = font;
            this.startPosition = startPosition;
        }

        public void Enqueue(int value)
        {
            queue.Enqueue(value);
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
                    OutlineColor = Color.Black,
                    OutlineThickness = 2,
                    Position = new Vector2f(x, y)
                };

                Text label = new Text(value.ToString(), font, 20)
                {
                    FillColor = Color.Black,
                    Position = new Vector2f(x + radius - 10, y + radius - 15)
                };

                window.Draw(circle);
                window.Draw(label);

                x += spacing;
                index++;
            }
        }
    }

}
