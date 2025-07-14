using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace MixItUp
{
    public static class ExtensionMethods
    {
        public static List<T> FindChildren<T>(this VisualElement parentElement, VisualElement whereSearch = null, string containsStringName = null, List<T> result = null)
        {
            result = result ?? new List<T>();

            try
            {
                var props = whereSearch == null ? parentElement.GetType().GetRuntimeProperties() : whereSearch.GetType().GetRuntimeProperties();
                var contProp = props.FirstOrDefault(w => w.Name == "Content");
                var childProp = props.FirstOrDefault(w => w.Name == "Children");

                if (childProp == null)
                {
                    if (contProp != null)
                    {
                        var cv = contProp.GetValue(whereSearch) as VisualElement;
                        if (cv != null) FindChildren<T>(parentElement, cv, containsStringName, result);
                    }
                    return result;
                }

                IEnumerable v = childProp.GetValue(whereSearch) as IEnumerable;
                foreach (var i in v)
                {
                    if (i is VisualElement) FindChildren<T>(parentElement, i as VisualElement, containsStringName, result);

                    if (i is T)
                    {
                        if (!string.IsNullOrEmpty(containsStringName))
                        {
                            bool fCheck = false;
                            var fields = parentElement.GetType().GetRuntimeFields().Where(w => w.Name.ToLower().Contains(containsStringName.ToLower())).ToList();
                            foreach (var f in fields)
                            {
                                var fv = f.GetValue(parentElement);
                                if (fv is T && fv == i) { fCheck = true; break; }
                            }
                            if (!fCheck) continue;
                        }

                        var ii = (T)i;
                        result.Add(ii);
                    }
                }
                return result;
            }
            catch { return result; }
        }        
        
        public static List<dynamic> FindChildren(this VisualElement parentElement, VisualElement whereSearch = null, string containsStringName = null, List<dynamic> result = null)
        {
            result = result ?? new List<dynamic>();

            try
            {
                var props = whereSearch == null ? parentElement.GetType().GetRuntimeProperties() : whereSearch.GetType().GetRuntimeProperties();
                var contProp = props.FirstOrDefault(w => w.Name == "Content");
                var childProp = props.FirstOrDefault(w => w.Name == "Children");

                if (childProp == null)
                {
                    if (contProp != null)
                    {
                        var cv = contProp.GetValue(whereSearch) as VisualElement;
                        if (cv != null) FindChildren<dynamic>(parentElement, cv, containsStringName, result);
                    }
                    return result;
                }

                IEnumerable v = childProp.GetValue(whereSearch) as IEnumerable;
                foreach (var i in v)
                {
                    if (i is VisualElement) FindChildren<dynamic>(parentElement, i as VisualElement, containsStringName, result);

                        if (!string.IsNullOrEmpty(containsStringName))
                        {
                            bool fCheck = false;
                            var fields = parentElement.GetType().GetRuntimeFields().Where(w => w.Name.ToLower().Contains(containsStringName.ToLower())).ToList();
                            foreach (var f in fields)
                            {
                                var fv = f.GetValue(parentElement);
                                if (fv == i) { fCheck = true; break; }
                            }
                            if (!fCheck) continue;
                        }

                        var ii = i;
                        result.Add(ii);
                }
                return result;
            }
            catch { return result; }
        }
    }
}
