using System.Windows.Controls;
using System.Windows.Interactivity;

namespace RxExamples
{
    public class FocusOnLoadBehavior : Behavior<Control>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += (_, __) => AssociatedObject.Focus();
        }
    }
}
