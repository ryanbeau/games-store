using System.Collections.Generic;

namespace Sprint.ViewModels
{
    public class FilterGroupViewModel
    {
        public string Name { get; set; }
        public string FormName { get; set; }
        public List<FilterItemViewModel> Filters { get; set; }
    }
}
