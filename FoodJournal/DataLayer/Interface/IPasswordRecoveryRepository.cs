using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Class;

namespace DataLayer.Interface
{
    interface IPasswordRecoveryRepository
    {
        PasswordRecovery Get(int id);
        int Insert(PasswordRecovery passRecover);
        int Update(PasswordRecovery passRecover);
        int Delete(int id);
    }
}
