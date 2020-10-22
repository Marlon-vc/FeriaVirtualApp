using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FeriaVirtualApp.Models
{
    public class HomeModel : BaseModel
    {
        public ObservableRangeCollection<Models.MenuItem> MenuItems { get; }

        public HomeModel()
        {
            MenuItems = new ObservableRangeCollection<MenuItem>();
        }

        public void AddItems(ICollection<MenuItem> items)
        {
            MenuItems.ReplaceRange(items);
        } 
    }
}
