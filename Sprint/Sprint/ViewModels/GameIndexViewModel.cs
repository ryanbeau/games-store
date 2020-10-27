using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sprint.ViewModels
{
    public class GameIndexViewModel
    {
        [Required]
        public string FilterSearch { get; set; }
        public string FilterPrice { get; set; }
        public string FilterCategory { get; set; }

        public List<FilterGroupViewModel> FilterGroups { get; set; }
        public List<GameItemViewModel> Games { get; set; }
    }
}
