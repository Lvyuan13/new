using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SHHS.UILabs
{
    public class AlternateListView : ListView
    {
        protected override void
            PrepareContainerForItemOverride(DependencyObject element,
            object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (View is GridView)
            {
                int index = ItemContainerGenerator.IndexFromContainer(element);
                ListViewItem lvi = element as ListViewItem;
                if (index % 2 == 0)
                {
                    lvi.Background = Brushes.PowderBlue;  //Brushes.Gainsboro;
                }
                else
                {
                    lvi.Background = Brushes.Beige;
                }
            }
        }
    }
}
