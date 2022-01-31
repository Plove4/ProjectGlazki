using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGlazki.Entities;

namespace ProjectGlazki.Utilities
{
    internal class DBcontext
    {
        private static GlazkiEntities _context { get; set; } 

        public static GlazkiEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new GlazkiEntities();
                return _context;
            }
        }
    }
}
