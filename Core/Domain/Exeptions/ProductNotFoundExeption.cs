using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ProductNotFoundException(int id) : NotFoundException($"Prdouct With ID {id} Not Found")
    {

    }
}
