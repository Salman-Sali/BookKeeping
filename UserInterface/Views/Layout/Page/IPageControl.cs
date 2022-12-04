using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bk.UserInterface.Views.Layout
{
    public interface IPageControl
    {
        public void First();
        public void Last();
        public void Previous();
        public void Next();

    }
}
