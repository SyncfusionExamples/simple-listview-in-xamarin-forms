using System;
using System.Collections;
using Xamarin.Forms;

namespace CustomControl
{
    public class SimpleListView : ScrollView, IDisposable 
    {
        private ScrollController controller;
        public int ItemHeight { get; set; }
        public DataTemplate ItemTemplate { get; set; }

        public static readonly BindableProperty DataSourceProperty =
            BindableProperty.Create("DataSource", typeof(IList), typeof(SimpleListView), null, BindingMode.TwoWay);
        public IList DataSource
        {
            get { return (IList)GetValue(DataSourceProperty); }
            set { this.SetValue(DataSourceProperty, value); }
        }

        public SimpleListView()
        {
            this.Orientation = ScrollOrientation.Vertical;
            this.ItemHeight = 60;
            this.controller = new ScrollController() { ListView = this };
            this.Scrolled += this.controller.ListView_Scrolled;
            this.Content = this.controller;
        }

        public void Dispose()
        {
            this.Scrolled -= this.controller.ListView_Scrolled;
        }
    }
}
