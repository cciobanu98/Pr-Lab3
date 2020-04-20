using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.Core
{
    public class JsonResponse
    {
        public string Data { get; set; }

        public string Filter { get; set; }

        public int Sort { get; set; }

        public IEnumerable<Stub> Stubs { get; set; }

        public string Limited { get; set; }

        public int Result { get; set; }

        public string Message { get; set; }

        public string Redirect { get; set; }

        public bool Success { get; set; }

        public bool VastImpact { get; set; }
    }
}
