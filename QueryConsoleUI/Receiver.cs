using Rest_with_json_linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueryConsoleUI
{
    public class Receiver
    {
        public IRequestFactory MethodStore { get; private set; }
        public Receiver()
        {
            MethodStore = new RequestFactory();
        }
    }
}
