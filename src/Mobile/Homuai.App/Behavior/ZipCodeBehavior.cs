using Xamarin.Forms;

namespace Homuai.App.Behavior
{
    public class ZipCodeBehavior : Behavior<Entry>
    {
        public readonly MaskEntryBehavior _maskEntryBehavior;

        public ZipCodeBehavior()
        {
            _maskEntryBehavior = new MaskEntryBehavior("XX.XXX-XXX");
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += _maskEntryBehavior.OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= _maskEntryBehavior.OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
