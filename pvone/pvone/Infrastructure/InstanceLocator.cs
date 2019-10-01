using pvone.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace pvone.Infrastructure
{
   public class InstanceLocator
    {

        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
