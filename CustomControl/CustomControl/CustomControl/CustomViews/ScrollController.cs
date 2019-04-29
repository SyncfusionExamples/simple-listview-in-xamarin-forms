using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CustomControl
{
    public class ScrollController : Layout<View>
    {
        #region Fields

        private bool isFirstTime = true;
        private double scrollY;
        private List<ItemView> viewItems = new List<ItemView>();

        #endregion

        #region Property

        internal SimpleListView ListView { get; set; }

        internal double ScrollY
        {
            get { return this.scrollY; }
            set
            {
                if (this.scrollY != value)
                {
                    this.scrollY = value;
                    this.OnSizeAllocated(this.Width, this.Height);
                }
            }
        }

        #endregion

        #region Override Methods

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (isFirstTime)
                CreateView((int)Math.Ceiling(ListView.Height / this.ListView.ItemHeight));

            var totalheight = this.ListView.DataSource.Count * this.ListView.ItemHeight;
            return (new SizeRequest(new Size(widthConstraint, totalheight)));
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            var start = (int)Math.Floor(ScrollY / this.ListView.ItemHeight);
            int noOfItems = (int)Math.Ceiling(ListView.Height / this.ListView.ItemHeight);

            foreach (var item in viewItems)
                item.IsEnsured = false;

            for (int i = start; i < start + noOfItems; i++)
            {
                var row = this.viewItems.FirstOrDefault(x => x.ItemIndex == i);
                if (row == null)
                {
                    row = this.viewItems.FirstOrDefault(x => ((x.ItemIndex < start || x.ItemIndex > (start + noOfItems - 1)) && !x.IsEnsured));
                    if (row == null)
                    {
                        CreateView(1);
                        row = this.viewItems.FirstOrDefault(x => ((x.ItemIndex < start || x.ItemIndex > (start + noOfItems - 1)) && !x.IsEnsured));
                    }
                    if (row != null)
                    {
                        row.ItemIndex = i;
                        try
                        {
                            row.BindingContext = this.ListView.DataSource[i];
                            row.IsEnsured = true;
                        }
                        catch { }
                    }
                }
                else
                    row.IsEnsured = true;
            }
            base.OnSizeAllocated(width, height);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double yPosition;
            foreach (var item in viewItems)
            {
                yPosition = item.ItemIndex * this.ListView.ItemHeight;
                item.Layout(new Rectangle(x, yPosition, width, this.ListView.ItemHeight));
            }
        }

        protected override bool ShouldInvalidateOnChildAdded(View child)
        {
            return false;
        }

        protected override bool ShouldInvalidateOnChildRemoved(View child)
        {
            return false;
        }

        #endregion

        #region Private and Internal Methods

        public void CreateView(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ItemView template = new ItemView
                {
                    Content = (View)this.ListView.ItemTemplate.CreateContent(),
                    BindingContext = this.ListView.DataSource[i],
                    IsEnsured = false,
                    ItemIndex = i,
                };
                viewItems.Add(template);
                this.Children.Add(template);
            }
            isFirstTime = false;
        }

        internal void ListView_Scrolled(object sender, ScrolledEventArgs e)
        {
            this.ScrollY = e.ScrollY;
        }

        #endregion
    }
}
