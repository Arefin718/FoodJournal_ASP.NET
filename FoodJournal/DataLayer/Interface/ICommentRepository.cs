using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    interface ICommentRepository
    {
        List<Comment> GetAll();
        int Insert(Comment comment);
        int Update(Comment comment);
        int Delete(int id);
    }
}
