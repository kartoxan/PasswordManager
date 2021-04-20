using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    [Serializable]
    public class FilePassword
    {
        
        public PasswordData PasswordData = new PasswordData();
        public Config config = new Config();
        
        public FilePassword() 
        {

        }

    }
}
