using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceManager.Helpers
{
    public class ViewPager<T> where T : class
    {
        private int _totalRecords;
        private int _totalPages;
        private int _pageSize;
        public ViewPager(IEnumerable<T> contents, int selectedPageItem = 1, string selectedRowsPerPage = "10")
        {
            Contents=contents;
            ViewContent = Goto(selectedPageItem, selectedRowsPerPage);
        }
        public IEnumerable<T> Goto(int selectedPageItem = 1, string selectedRowsPerPage = "10")
        {
            _totalRecords = Contents.Count();
            SelectedPageItem = selectedPageItem;
            _pageSize = selectedRowsPerPage.Equals("all") ? (_totalRecords < 1 ? 1 : _totalRecords) : Convert.ToInt32(selectedRowsPerPage);
            _totalPages = _totalRecords < 1 ? 1 : _totalRecords / _pageSize;
            if (_totalRecords % _pageSize > 0) _totalPages++;
            IList<int> pageItems = new List<int>();
            for (int i = 1; i <= _totalPages; i++)pageItems.Add(i);
            PageItems = pageItems;
            if (SelectedPageItem > _totalPages)
            {
                SelectedPageItem = _totalPages;
            }
            return Contents.Skip((SelectedPageItem - 1) * _pageSize).Take(_pageSize);
        }
        public int SelectedPageItem { get; private set; }
        public IEnumerable<int> PageItems { get; set; } = new List<int>();
        public IEnumerable<T> ViewContent { get; set; }
        public IEnumerable<T> Contents { get; }
    }
}
