using System;
using voltaire.Controls.Items;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace voltaire.TemplateSelectors
{
	public class ViewPagerTemplateSelector : DataTemplateSelector
	{

        public List<DataTemplate> PageTemplates = new List<DataTemplate>();


		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
            var tab = (TTab)item;

            var matchedTemplates = PageTemplates.Where((arg) => arg.GetType() == tab.View);

            if(matchedTemplates.Count() != 0)
            {
                return matchedTemplates.First();
            }
            else
            {
                var template = new DataTemplate(tab.View);
                PageTemplates.Add(template);
                return template;
            }

		}

	}
}
