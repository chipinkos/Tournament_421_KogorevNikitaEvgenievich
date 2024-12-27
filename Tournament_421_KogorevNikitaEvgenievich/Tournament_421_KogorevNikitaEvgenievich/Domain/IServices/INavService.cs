using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament_421_KogorevNikitaEvgenievich.Domain.IServices
{
    public interface INavService
    {
        void Navigate();
        void GoBack();
        void NavigateAndDispose();
    }
}
