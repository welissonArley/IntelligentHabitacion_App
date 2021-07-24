using System.Collections.Generic;
using Xamarin.Forms;

namespace Homuai.App.Behavior
{
    public class MaskEntryBehavior
    {
        private readonly string Mask;
        private readonly IDictionary<int, char> Positions;

        public MaskEntryBehavior(string mask)
        {
            Mask = mask;
            Positions = new Dictionary<int, char>();
            SetPositions();
        }

        private void SetPositions()
        {
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] != 'X')
                    Positions.Add(i, Mask[i]);
            }
        }

        public void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text))
                return;

            if (text.Length > Mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in Positions)
            {
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }
            }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}
