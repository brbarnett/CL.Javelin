using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CL.Javelin.Clients.Shared.Views
{
    public class BoardListViewItemStyleSelector : StyleSelector
    {
        public Style StyleEven { get; set; }
        public Style StyleOdd { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            ListView listView = ItemsControl.ItemsControlFromItemContainer(container) as ListView;

            if (!ReferenceEquals(listView, null))
            {
                int index = listView.IndexFromContainer(container);
                if (index%2 == 0)
                {
                    return this.StyleEven;
                }
                else
                {
                    return this.StyleOdd;
                }
            }

            return base.SelectStyleCore(item, container);
        }
    }
}
