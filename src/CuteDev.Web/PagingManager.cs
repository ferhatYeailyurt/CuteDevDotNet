using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuteDev.Web
{

    public class PagingManager
    {
        #region Properties

        public int MaximumCount { get; set; }

        public int CurrentPageNumber { get; set; }

        public int DisplayItem { get; set; }

        public int PageCount { get; set; }

        public int StartPage { get; set; }

        public enum PageTypes
        {
            Test = 0,
            deneme = 1
        }

        public class PageProp
        {
            public int PageNo { get; set; }
            public string BaseUrl { get; set; }
            public PageTypes PageType { get; set; }
        }

        public List<PageProp> UrlList { get; set; }

        #endregion

        #region Constructor

        public PagingManager()
        {
            Initialize();

            SetUrlList();
        }

        public PagingManager(int maxCount, int currentPageNo)
        {
            Initialize();

            this.MaximumCount = maxCount;

            this.CurrentPageNumber = currentPageNo;

            SetUrlList();
        }

        public PagingManager(int maxCount, int currentPageNo, int displayItem)
        {
            Initialize();

            this.MaximumCount = maxCount;

            this.CurrentPageNumber = currentPageNo;

            this.DisplayItem = displayItem;

            SetUrlList();
        }

        #endregion

        #region Functions

        private void Initialize()
        {
            this.MaximumCount = 20;
            this.CurrentPageNumber = 1;
            this.DisplayItem = 20;
            this.StartPage = 1;
        }

        private void SetUrlList()
        {
            this.PageCount = (int)Math.Ceiling((double)this.MaximumCount / this.DisplayItem);

            List<PageProp> _urlList = new List<PageProp>();
            for (int i = this.StartPage; i <= this.PageCount; i++)
            {

                if (i == this.CurrentPageNumber)
                {
                    _urlList.Add(new PageProp()
                    {
                        BaseUrl = String.Format("<span class='page-numbers current'>{0}</span>", i),
                        PageNo = i,
                        PageType = PageTypes.deneme
                    });
                }
                else
                {
                    _urlList.Add(new PageProp()
                    {
                        BaseUrl = String.Format("<a class='page-numbers' href='/Haberler/{0}'>{0}</a>", i),
                        PageNo = i,
                        PageType = PageTypes.deneme
                    });

                }
            }

            if (this.CurrentPageNumber < this.PageCount)
            {
                _urlList.Add(new PageProp()
                {
                    BaseUrl = String.Format("<a class='next page-numbers' href='/Haberler/{0}'>&gt;</a>", this.CurrentPageNumber + 1),
                    PageNo = this.CurrentPageNumber + 1,
                    PageType = PageTypes.deneme
                });
            }

            this.UrlList = _urlList;
        }

        #endregion
    }
}
