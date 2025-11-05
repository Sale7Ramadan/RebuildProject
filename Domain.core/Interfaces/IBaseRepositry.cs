using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IBaseRepositry<T> where T : class
    {

        T GetById(int id);


    }
}
