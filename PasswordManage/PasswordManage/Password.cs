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
        public string Site { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public DateTime lastChanges { get; set; }

        public Password()
        {

        }

        public Password(string site, string login, string pasword)
        {
            this.Site = site;
            this.login = login;
            this.password = pasword;
            lastChanges = DateTime.Now;
        }



    }
}
