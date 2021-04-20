using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    [Serializable]
    public class Password
    {
        public string site;
        public string login;
        public string pasword;
        public DateTime lastChanges;

        public Password()
        {

        }

        public Password(string site, string login, string pasword)
        {
            this.site = site;
            this.login = login;
            this.pasword = pasword;
            lastChanges = DateTime.Now;
        }



    }
}
