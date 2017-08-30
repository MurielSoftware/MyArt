using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Client.Core.Providers
{
    public class TempDataProvider
    {
        private TempDataDictionary _tempData;
        private ViewDataDictionary _viewData;

        public TempDataProvider(TempDataDictionary tempData, ViewDataDictionary viewData)
        {
            _tempData = tempData;
            _viewData = viewData;
        }

        public void SetData(string name, object value)
        {
            SetTempData(name, value);
            SetViewData(name, value);
        }

        public void SetTempData(string name, object value)
        {
            _tempData[name] = value;
        }

        public void SetViewData(string name, object value)
        {
            _viewData[name] = value;
        }

        public T GetTempData<T>(string name) where T : class
        {
            return (T)GetTempData(name);
        }

        public object GetTempData(string name)
        {
            return _tempData[name];
        }

        public T GetViewData<T>(string name) where T : class
        {
            return (T)GetViewData(name);
        }

        public object GetViewData(string name)
        {
            return _viewData[name];
        }

        public T GetTempDataWithoutRemove<T>(string name) where T : class
        {
            return (T)GetTempDataWithoutRemove(name);
        }

        public object GetTempDataWithoutRemove(string name)
        {
            _viewData[name] = _tempData[name];
            _tempData[name] = _viewData[name];
            return _viewData[name];
        }
    }
}
