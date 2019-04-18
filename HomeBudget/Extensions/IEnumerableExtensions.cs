using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeBudget.Extensions
{
    public static class IEnumerableExtensions
    {
        public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, string firstOptionText, string firstOptionValue = null)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            selectListItems.Add(new SelectListItem() { Text = firstOptionText, Value = firstOptionValue });

            foreach (var item in items)
            {
                Type itemType = item.GetType();
                PropertyInfo itemValuePi = itemType.GetProperty(dataValueField);
                PropertyInfo itemTextPi = itemType.GetProperty(dataTextField);

                selectListItems.Add(new SelectListItem()
                {
                    Text = itemTextPi.GetValue(item).ToString(),
                    Value = itemValuePi.GetValue(item).ToString()
                });
            }

            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}
