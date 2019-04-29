using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CustomControl
{
    public class Container : Layout<View>
    {
        #region Fields

        private bool isFirstTime = true;
        private bool isInTextChange = false;
        private double scrollY;
        private int totalHeight;
        private int itemHeight = 60;
        private int noOfItems;
        private int totalItems = 30;
        private List<ItemView> rowItems = new List<ItemView>();

        #endregion

        #region Property

        public DataTemplate ItemTemplate { get; set; }

        public ScrollView Scroller
        {
            get
            {
                if (this.Parent != null)
                    return this.Parent as ScrollView;
                return null;
            }
        }

        public double Scrolly
        {
            get { return scrollY; }
            set
            {
                if (this.scrollY != value)
                {
                    scrollY = value;
                    this.OnSizeAllocated(this.Width, this.Height);
                }
            }
        }

        #endregion

        #region Override Methods

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            totalItems = (this.BindingContext as ContactsViewModel).Contacts.Count;
            var totalheight = totalItems * itemHeight;
            noOfItems = (int)Math.Ceiling(Scroller.Height / itemHeight);

            if (isFirstTime)
                CreateView(noOfItems);

            var sizerequest = new SizeRequest(new Size(widthConstraint, totalheight));
            return sizerequest;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            var start = (int)Math.Floor(Scrolly / itemHeight);
            totalHeight = totalItems * itemHeight;
            noOfItems = (int)Math.Ceiling(Scroller.Height / itemHeight);

            foreach (var item in rowItems)
                item.IsEnsured = false;

            for (int i = start; i < start + noOfItems; i++)
            {
                isInTextChange = true;
                var row = this.rowItems.FirstOrDefault(x => x.ItemIndex == i);
                if (row == null)
                {
                    row = this.rowItems.FirstOrDefault(x => ((x.ItemIndex < start || x.ItemIndex > (start + noOfItems - 1)) && !x.IsEnsured));
                    if (row == null)
                    {
                        CreateView(1);
                        row = this.rowItems.FirstOrDefault(x => ((x.ItemIndex < start || x.ItemIndex > (start + noOfItems - 1)) && !x.IsEnsured));
                    }
                    if (row != null)
                    {
                        row.ItemIndex = i;
                        try
                        {
                            ObservableCollection<ContactInfo> Contacts = (this.BindingContext as ContactsViewModel).Contacts;
                            row.BindingContext = Contacts[i];
                            row.IsEnsured = true;
                        }
                        catch { }
                    }
                }
                else
                    row.IsEnsured = true;
                isInTextChange = false;
            }
            base.OnSizeAllocated(width, height);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double yPosition;
            foreach (var item in rowItems)
            {
                yPosition = item.ItemIndex * itemHeight;
                item.Layout(new Rectangle(x, yPosition, width, itemHeight));
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

        protected override void OnChildMeasureInvalidated()
        {
            //if (!isInTextChange)
                base.OnChildMeasureInvalidated();
        }

        #endregion

        #region Private and Internal Methods

        public void CreateView(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ObservableCollection<ContactInfo> Contacts = (this.BindingContext as ContactsViewModel).Contacts;
                ItemView template = new ItemView
                {
                    Content = (View)this.ItemTemplate.CreateContent(),
                    BindingContext = Contacts[i],
                    IsEnsured = false,
                    ItemIndex = -1,
                };
                rowItems.Add(template);
                this.Children.Add(template);
            }
            isFirstTime = false;
        }

        internal void Scroller_Scrolled(object sender, ScrolledEventArgs e)
        {
            Scrolly = e.ScrollY;
        }

        #endregion
    }
}
