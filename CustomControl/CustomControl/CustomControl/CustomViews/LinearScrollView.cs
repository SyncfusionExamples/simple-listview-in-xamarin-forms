using Xamarin.Forms;

namespace CustomControl
{
    public class LinearScrollView : ScrollView
    {
        public LinearScrollView()
        {
            this.Orientation = ScrollOrientation.Vertical;
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            var scroll = (child as Container);
            if (scroll != null)
                this.Scrolled += scroll.Scroller_Scrolled;
        }

        protected override void OnChildRemoved(Element child)
        {
            base.OnChildRemoved(child);
            var scroll = (child as Container);
            if (scroll != null)
                this.Scrolled -= scroll.Scroller_Scrolled;
        }
    }
}
