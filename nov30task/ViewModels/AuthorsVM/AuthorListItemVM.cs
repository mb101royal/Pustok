using nov30task.Models;
using System.ComponentModel.DataAnnotations;

namespace nov30task.ViewModels.AuthorsVM
{
    public class AuthorListItemVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
