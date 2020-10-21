using System.ComponentModel.DataAnnotations;

namespace Sprint.Enums
{
    public enum WishlistVisibility
    {
        [Display(Name = "Friends Only")]
        FriendsOnly,

        [Display(Name = "Only Me")]
        OnlyMe,

        [Display(Name = "Everyone")]
        Everyone,
    }
}
