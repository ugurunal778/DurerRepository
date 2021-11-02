using Facade;

namespace WcfServiceApp
{
    public class BaseWcfService
    {
        private ContentFacade _contentFacade;
        private MailFacade _mailFacade;
        private NewsFacade _newsFacade;
        private PageFacade _pageFacade;
        private UserFacade _userFacade;

        public ContentFacade ContentFacade
        {
            get
            {
                if (_contentFacade is null)
                {
                    _contentFacade = new ContentFacade();
                }
                return _contentFacade;
            }
        }

        public MailFacade MailFacade
        {
            get
            {
                if (_mailFacade is null)
                {
                    _mailFacade = new MailFacade();
                }
                return _mailFacade;
            }
        }
        public NewsFacade NewsFacade
        {
            get
            {
                if (_newsFacade is null)
                {
                    _newsFacade = new NewsFacade();
                }
                return _newsFacade;
            }
        }
        public PageFacade pageFacade
        {
            get
            {
                if (_pageFacade is null)
                {
                    _pageFacade = new PageFacade();
                }
                return _pageFacade;
            }
        }
        public UserFacade UserFacade
        {
            get
            {
                if (_userFacade is null)
                {
                    _userFacade = new UserFacade();
                }
                return _userFacade;
            }
        }
    }
}