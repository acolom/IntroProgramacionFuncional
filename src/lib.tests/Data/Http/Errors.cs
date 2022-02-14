using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.tests.Data.Http
{
    public abstract class BaseError
    {

    }

    public class NotFoundError : BaseError
    {

    }

    public class BadRequestError : BaseError
    {

    }
    public class UnknownResponseError : BaseError
    {

    }

}
