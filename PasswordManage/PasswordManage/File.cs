using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManage
{
    [Serializable]
    class File
    {


        PasswordData passwordData;
        Config config;
        
        public File() 
        {
            passwordData = new PasswordData();
            config = new Config();
        }
    }
}
