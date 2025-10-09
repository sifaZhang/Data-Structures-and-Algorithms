//Author: Sifa Zhang
//Studeng ID: 1606796
//Date: 2025/09/22
//This class visualizes a queue data structure using SFML.

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
        private LinkedList myList;
        private Font font;
        private Vector2f startPosition;
        private float radius;
        private float spacing;
        private SFML.Graphics.Color fillColor;
        private Vector2f headPos;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="font"></param>
        /// <param name="startPosition"></param>
        public QueueVisualizer(Font font, Vector2f startPosition)
        {
            myList = new LinkedList();
            this.font = font;
            this.startPosition = startPosition;
            fillColor = new SFML.Graphics.Color(200, 230, 255);
            radius = 100f;
            spacing = 230f;
            headPos = new Vector2f();
        }

        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            myList.Clear();
        }

        /// <summary>
        /// fetches the front element of the queue without removing it.
        /// </summary>
        /// <returns></returns>
        public int? Fetch()
        {
            int value;
            return myList.GetHeadNode(out value) ? value : (int?)null;
        }

        /// <summary>
        /// enqueues a new value to the back of the queue.
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(int value)
        {
            myList.AddNode(value);
        }

        /// <summary>
        /// Determines whether the queue is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the queue contains no elements; otherwise, <see langword="false"/>.</returns>
        public bool Empty()
        {
            return myList.Size() == 0;
        }

        /// <summary>
        /// get the size of the queue
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return myList.Size();
        }

        /// <summary>
        /// dequeues the front element of the queue.
        /// </summary>
        public bool Dequeue(out int vaule)
        {
            return myList.RemoveFirstNode(out vaule);
        }

        /// <summary>
        /// Sets the fill color of the object.
        /// </summary>
        /// <param name="color">The new fill color to apply.</param>
        public void SetColor(SFML.Graphics.Color color)
        {
            fillColor = color;
        }

        /// <summary>
        /// Gets the current position of the head in 2D space.
        /// </summary>
        /// <returns>A <see cref="Vector2f"/> representing the coordinates of the head position.</returns>
        public Vector2f GetHeadPos()
        {
            return headPos;
        }

        /// <summary>
        /// draws the queue onto the provided render window.
        /// </summary>
        /// <param name="window"></param>
        public void Draw(RenderWindow window)
        {
            // Reset head position
            headPos.X = -1; 

            // Starting position for drawing
            float x = startPosition.X;
            float y = startPosition.Y;

            // Convert queue to list for indexed access
            List<int> tempList = myList.ToList();

            // Draw from back to front to maintain queue order
            for (int i = tempList.Count - 1; i >= 0; i--)
            {
                int value = tempList[i];
                
                CircleShape circle = new CircleShape(radius)
                {
                    FillColor = fillColor,
                    Position = new Vector2f(x, y)
                };

                Text label = new Text(value.ToString(), font, 50)
                {
                    FillColor = SFML.Graphics.Color.Black,
                };

                // get text bounds for centering
                FloatRect textBounds = label.GetLocalBounds();

                // set origin to center of text
                label.Origin = new Vector2f(textBounds.Left + textBounds.Width / 2, textBounds.Top + textBounds.Height / 2);

                // set position to center of circle
                label.Position = new Vector2f(x + radius, y + radius);

                window.Draw(circle);
                window.Draw(label);

                // Store head position
                if (i == 0) // Head
                {
                    headPos.X = circle.Position.X + radius;
                    headPos.Y = circle.Position.Y + radius;
                }

                // Move x for next circle
                x += spacing;
            }
        }
    }

}
