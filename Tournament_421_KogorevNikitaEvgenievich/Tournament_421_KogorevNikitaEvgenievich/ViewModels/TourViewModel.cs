using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament_421_KogorevNikitaEvgenievich.Data;
using Tournament_421_KogorevNikitaEvgenievich.Domain.Utilities;

namespace Tournament_421_KogorevNikitaEvgenievich.ViewModels
{
    public class TourViewModel : ViewModel
    {
        public Tournament Tournament { get; set; } = new Tournament();

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
