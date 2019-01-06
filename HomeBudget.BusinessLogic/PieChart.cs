using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class PieChart
    {
        public PieChart(CategoryTreeNode root, List<Expenses> allExpenses)
        {
            Title = root.Value.Name;

            decimal catCostSum = allExpenses.Where(x => x.CategoryId == root.Value.Id).Sum(x => x.TotalPrice);
            TryAddItemToDataPoints(catCostSum, root.Value.Name);

            if (root.Childs != null)
            {
                foreach (var child in root.Childs)
                {
                    List<int> childAllCatIds = child.ToList().Select(x => x.Id).ToList();
                    catCostSum = allExpenses.Where(x => childAllCatIds.Contains(x.CategoryId)).Sum(x => x.TotalPrice);

                    TryAddItemToDataPoints(catCostSum, child.Value.Name);
                }
            }

            if (DataPoints.Count() > 0)
            {
                DataPoints = DataPoints.OrderByDescending(x => x.Value).ToList();
                CalculatePercentages();
                SetBgColors();
            }
        }
        

        public string Title { get; set; }
        public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();


        public void CalculatePercentages()
        {
            decimal costSum = DataPoints.Sum(x => x.Value);

            foreach (var item in DataPoints)
            {
                item.Percentage = (int)Math.Floor((item.Value * 100) / costSum);
            }

            int missingPercentsNumber = 100 - DataPoints.Sum(x => x.Percentage);

            for (int i = 0; i < missingPercentsNumber; i++)
            {
                DataPoints.OrderByDescending(x => x.Value - Math.Truncate(x.Value)).ToList()[i].Percentage += 1;
            }
        }
        
        public void SetBgColors()
        {
            List<string> colors = new List<string>()
            {
                "#008000", "#FFA500", "#FFD700", "#FFFF00", "#8B0000", "#006400", "#FF0000", "#00FF00",
                "#90EE90", "#0000FF", "#191970", "#87CEEB", "#A52A2A", "#D2691E", "#FFFFFF", "#D3D3D3",
                "#A9A9A9", "#808080", "#000000"
            };

            int colorIndex = 0;

            foreach (var item in DataPoints)
            {
                if (colorIndex < colors.Count())
                {
                    item.HexBackgroundColor = colors[colorIndex];
                    colorIndex++;
                }
                else
                {
                    var random = new Random();
                    var randomColor = String.Format("#{0:X6}", random.Next(0x1000000));
                    item.HexBackgroundColor = randomColor;
                }                
            }
        }

        public void TryAddItemToDataPoints(decimal dataPointValue, string dataPointName)
        {
            if (dataPointValue <= 0)
            {
                return;
            }

            DataPoints.Add(new DataPoint(dataPointValue, dataPointName));
        }


        public class DataPoint
        {
            public DataPoint(decimal value, string name)
            {
                Value = value;
                Name = name;
            }

            public int Percentage { get; set; }
            public decimal Value { get; set; }
            public string HexBackgroundColor { get; set; }
            public string Name { get; set; }
        }
    }
}
