using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    [Serializable]
    public class PasswordData
    {
        public List<Password> passwords;

        public PasswordData()
        {
            passwords = new List<Password>();
        }
    }
}
