using System;
using System.Collections.Generic;
using System.Text;

namespace AcceleroRecorder.Models
{
    public enum MenuItemType
    {
        Record,
        History,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
