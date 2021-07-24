using Xamarin.Forms;

namespace Homuai.App.Behavior
{
    public class DecimalBehavior : Behavior<Entry>
    {
        private bool _firstTime { get; set; }
        private bool _secondTime { get; set; }

        public DecimalBehavior()
        {
            _firstTime = true;
            _secondTime = false;
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        public void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (_secondTime)
            {
                /*
                 * When the text value is changed by the decimal format, this function will be called a second time.
                 * Useless.
                 */
                _secondTime = false;
                return;
            }

            var entry = sender as Entry;

            var strValue = entry.Text;
            decimal decimalValue = 0;

            if (!string.IsNullOrEmpty(strValue))
                decimal.TryParse(strValue, out decimalValue);

            if (!_firstTime)
            {
                /*
                 * The first time this function is called, it is by the Model's get value
                 */
                entry.Text = args.OldTextValue.Length > args.NewTextValue.Length ? string.Format("{0:n}", decimalValue / 10) : string.Format("{0:n}", decimalValue * 10);
                entry.CursorPosition = 0;
            }
            else
                entry.Text = string.Format("{0:n}", decimalValue * 10);

            _firstTime = false;
            _secondTime = true;
        }
    }
}
