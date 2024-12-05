using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Posts = new HashSet<Post>();    
    }
    public ICollection<Post> Posts { get; set; }
}