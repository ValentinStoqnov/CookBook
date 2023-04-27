using Rosi_s_Cook_Book.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rosi_s_Cook_Book
{
    public static class NavigationHelper
    {
        public static void OpenRecipiesPage()
        {
            RecipiesPage RecPage = new RecipiesPage();
            MainWindow mw = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow;
            mw.ContentFrame.Content = RecPage;
        }

        public static void OpenRecipiesCreatePage() 
        {
            RecipiesCreateAndEditPage RecCreateEdit = new RecipiesCreateAndEditPage("Create");
            MainWindow mw = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow;
            mw.ContentFrame.Content = RecCreateEdit;
        }

        public static void OpenRecipiesEditPage(string FilePath) 
        {
            RecipiesCreateAndEditPage RecCreateEdit = new RecipiesCreateAndEditPage("Edit",FilePath);
            MainWindow mw = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow;
            mw.ContentFrame.Content = RecCreateEdit;
        }

        public static void OpenRecipesViewPage(string path) 
        {
            RecipiesViewPage RecViewPage = new RecipiesViewPage(path);
            MainWindow mw = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow;
            mw.ContentFrame.Content = RecViewPage;
        }
    }
}
