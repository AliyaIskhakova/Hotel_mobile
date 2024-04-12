using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    public partial class Shell1 : Shell
    {
        public Shell1()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RoomsSearch), typeof(RoomsSearch));
        }
    }
}