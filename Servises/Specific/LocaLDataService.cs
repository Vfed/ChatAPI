using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Servises.Specific
{
    public class LocaLDataService : ILocaLDataService
    {
        private readonly object _locker = new object();
        private int _usersOnline { get; set; }
        public LocaLDataService()
        {
            _usersOnline = 0;
        }
        public int GetUsersOnline()
        {
            lock (_locker)
            {
                return _usersOnline;
            }
        }
        public void SetUsersOnline(int val)
        {
            lock (_locker)
            {
                _usersOnline = val;
            }
        }
    }

}
