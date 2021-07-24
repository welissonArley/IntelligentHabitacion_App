using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Homuai.Domain.ValueObjects
{
    public class Color
    {
        public (string colorLightMode, string colorDarkMode) RandomColor()
        {
            var colorsLightMode = ColorListLightMode();
            var colorsDarkMode = ColorListDarkMode();

            var index = RandomNumberGenerator.GetInt32(0, colorsLightMode.Count);

            return (colorsLightMode.ElementAt(index), colorsDarkMode.ElementAt(index));
        }

        private IList<string> ColorListLightMode()
        {
            return new List<string>
            {
                "#40806A", "#008040", "#007A7C", "#006060", "#345A5E", "#005555", "#003636", "#114C2A", "#005500",
                "#007FAA", "#002517", "#2A7AB0", "#2574A9", "#406098", "#205D86", "#1F3A93", "#005051", "#34415E",
                "#0A3055", "#002A2A", "#002A2A", "#7462E0", "#8859B6", "#000060", "#5E50B5", "#A74165", "#765AB0",
                "#663399", "#5D445D", "#7D314C", "#3D2F5B", "#600060", "#522032", "#322A60", "#332533", "#332533",
                "#332533", "#4D6066", "#4F5A65", "#34515E", "#8D6708", "#1C2833", "#555344", "#382903", "#2A2A22",
                "#AA5535", "#9F6B3F", "#80503D", "#804028", "#802200", "#553529", "#DC143C", "#BC3E31", "#C0392B",
                "#AA422F", "#923026", "#871A1A", "#870C25", "#5C0819", "#360000"
            };
        }
        private IList<string> ColorListDarkMode()
        {
            return new List<string>
            {
                "#C8F7C5", "#00FF7F", "#00FA9A", "#A2DED0", "#ABE338", "#86E2D5", "#4ADD8C", "#36D7B7", "#4ECDC4",
                "#66CC99", "#2ECCB0", "#2ECC91", "#8BB82D", "#1BBC9B", "#03C9A9", "#ADD8E6", "#89C4F4", "#81CFE0",
                "#00D4D4", "#34B9DB", "#59ABE3", "#00AAAA", "#00A4A6", "#3498DB", "#3498DB", "#FFECDB", "#F1A9A0",
                "#E08283", "#E26A6A", "#BF6EE0", "#ECF0F1", "#E8E8E8", "#B2CCE5", "#D2D7D3", "#D2D7D3", "#ABB7B7",
                "#ABB7B7", "#C9F227", "#C9F227", "#7BACDD", "#F5D76E", "#F2A127", "#C7A720", "#F27927", "#FDE3A7",
                "#F9BF3B", "#F9BF3B", "#FFA07A", "#F5AB35", "#F4A460", "#E6A522", "#F89406", "#F2784B", "#D48566",
                "#D48566", "#D48566", "#D48566", "#E7903C", "#FF6347", "#FF6347"
            };
        }
    }
}
