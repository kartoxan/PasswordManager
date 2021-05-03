using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    [Serializable]
    public class PasswordContainer
    {

        public List<Password> passwords;


        public PasswordContainer() 
        {
            passwords = new List<Password>();
        }

    }
}
