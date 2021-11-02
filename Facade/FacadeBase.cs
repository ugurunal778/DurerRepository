using System.Globalization;
using System.Threading;
using System.Web;

namespace Facade
{
    public class FacadeBase
    {
        private readonly HazrefSampleEntities _entityModel;

        public HazrefSampleEntities EntityModel
        {
            get
            {
                return _entityModel ?? new HazrefSampleEntities();
            }
        }

        public FacadeBase()
        {
            _entityModel = new HazrefSampleEntities();
        }


        public string Culture
        {
            get
            {
                return CultureInfo.CurrentUICulture.ToString();
            }
        }
    }
}
