using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chemistry_Studio
{
    class Position_Confidence
    {
        public List<double> confidences;
        public List<int> positions;

        public Position_Confidence(double confidence, int position)
        {
            confidences = new List<double>();
            positions = new List<int>();
            confidences.Add(confidence);
            positions.Add(position);
        }

        public void add(double confidence, int position)
        {
            positions.Add(position);
            confidences.Add(confidence);
        }

        public void remove(int position)
        {
            for (int i = 0; i < this.positions.Count; i++)
            {
                if (positions[i] == position)
                {
                    positions.RemoveAt(i);
                    confidences.RemoveAt(i);
                    break;
                }
            }
        }



    }
}
