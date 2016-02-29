using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor
{
    public class CompressOptions
    {
        /// <summary>
        /// From 0 to 100
        /// </summary>
        public int Quality { get; set; }
        public float Resize { get; set; }
        private Dictionary<string ,Strategy> strategies;

        public CompressOptions() {
            strategies = new Dictionary<string,Strategy>();
        }

        public void Add(Strategy strategy) {
            strategies[strategy.ID] = strategy;
        }
        public Strategy GetStrategy(string id) {
            if (strategies.ContainsKey(id)) {
                return strategies[id];
            }
            return null;
        }



    }
    /// <summary>
    /// 每个策略针对一个图像，每个策略有一个唯一的ID，和图像相对应
    /// </summary>
    public class Strategy
    {
        public String ID { get; private set; }

        public bool EnableResize { get; set; }
        public float Resize { get; set; }
        public Size Size { get; set; }


        public bool EnableQuality { get; set; }
        public int Quality { get; set; }
        public bool Exclude { get; set; }
        
        

        public Strategy(string id) {
            EnableResize = false;
            ID = id;
        }
    }
    public class Size {
        public int Width { get; set; }
        public int Height { get; set; }
        public override string ToString()
        {
            return Width + "x" + Height;
        }
    }
}
