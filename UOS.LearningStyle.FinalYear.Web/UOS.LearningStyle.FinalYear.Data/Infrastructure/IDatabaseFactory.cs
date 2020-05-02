using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOS.LearningStyle.FinalYear.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {//Data Context is the network of connections among data points, making it possible to recieve information from data
        DataContext Get();
    }
}
